using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Assert = NUnit.Framework.Assert;
using CollectionAssert = NUnit.Framework.CollectionAssert;

namespace ApplicationLayer.Test
{
	[TestClass]
	public class ProducerConsumerQueueTest
	{
		private readonly IFixture _fixture = new Fixture();

		#region Argument Validation
		[TestMethod, TestCategory("Unit")]
		public void Start_MultipleTimes_ShouldThrow()
		{
			using (var sut = new ProducerConsumerQueue<int>())
			{
				sut.Start();
				Assert.Throws<InvalidOperationException>(sut.Start);
			}
		}

		[TestMethod, TestCategory("Unit")]
		public void Start_ZeroDegreeOfParallelism_ShouldNotThrow()
		{
			using (var sut = new ProducerConsumerQueue<int>())
			{

				// Act and Test
				Assert.DoesNotThrow(() => sut.Start(0));
			}
		}

		[TestMethod, TestCategory("Unit")]
		public void Start_DegreeOfParallelismOutOfRange_Throws()
		{
			using (var sut = new ProducerConsumerQueue<int>())
			{

				// Act and Test
				Assert.Throws<ArgumentOutOfRangeException>(() => sut.Start(-1));
				Assert.Throws<ArgumentOutOfRangeException>(() => sut.Start(-2));

				Assert.Throws<ArgumentOutOfRangeException>(() => sut.Start(1000));
			}
		}
		#endregion

		[TestMethod, TestCategory("Unit")]
		public void Add_AfterDispose_Throws()
		{
			var sut = new ProducerConsumerQueue<int>();
			sut.Dispose();

			// Act and Test
			Assert.Throws<ObjectDisposedException>(() => sut.Add(42));
			Assert.Throws<ObjectDisposedException>(() => sut.Add(42, new CancellationToken()));
		}

		[TestMethod, TestCategory("Unit")]
		public void Start_AfterDispose_Throws()
		{
			var sut = new ProducerConsumerQueue<int>();
			sut.Dispose();

			// Act and Test
			Assert.Throws<ObjectDisposedException>(sut.Start);
			Assert.Throws<ObjectDisposedException>(() => sut.Start(5));
		}

		[TestMethod, TestCategory("Unit")]
		public void CompleteAdding_AfterDispose_Throws()
		{
			var sut = new ProducerConsumerQueue<int>();
			sut.Dispose();

			// Act and Test
			Assert.Throws<ObjectDisposedException>(sut.CompleteAdding);
		}

		[TestMethod, TestCategory("Unit")]
		public void Properties_AfterDispose_Throws()
		{
			var sut = new ProducerConsumerQueue<int>();
			sut.Dispose();

			bool tempBool = false;
			int tempInt = -1;

			// Act and Test
			Assert.Throws<ObjectDisposedException>(() => tempBool = sut.IsAddingCompleted);
			Assert.Throws<ObjectDisposedException>(() => tempBool = sut.IsCompleted);
			Assert.Throws<ObjectDisposedException>(() => tempInt = sut.Count);

			// This is just to remove the warning/error about unused local variables
			Assert.IsFalse(tempBool);
			Assert.AreEqual(-1, tempInt);
		}

		[TestMethod, TestCategory("Unit")]
		public void Dispose_AfterDispose_DoesntThrow()
		{
			var sut = new ProducerConsumerQueue<int>();

			// Act
			sut.Dispose();

			// Test
			Assert.DoesNotThrow(sut.Dispose);
		}

		[TestMethod, TestCategory("Unit")]
		public void Start_WithDataBeforeStart_DataIsConsumed()
		{
			using (var sut = new ProducerConsumerQueue<int>())
			{
				var testData = _fixture.CreateMany<int>(25).ToList();

				var dataConsumed = new ConcurrentBag<int>();
				sut.OnConsumeData += (sender, i) => dataConsumed.Add(i);

				// Act
				testData.ForEach(sut.Add);

				sut.Start(5);

				// Test
				Thread.Sleep(1500);

				CollectionAssert.AreEquivalent(testData, dataConsumed);
			}
		}

		[TestMethod, TestCategory("Unit")]
		public void Start_WithDataAfterStart_DataIsConsumed()
		{
			using (var sut = new ProducerConsumerQueue<int>())
			{
				var testData = _fixture.CreateMany<int>(25).ToList();

				var dataConsumed = new ConcurrentBag<int>();
				sut.OnConsumeData += (sender, i) => dataConsumed.Add(i);

				// Act
				sut.Start(5);
				testData.ForEach(sut.Add);

				// Test
				Thread.Sleep(1500);

				CollectionAssert.AreEquivalent(testData, dataConsumed);
			}
		}

		[TestMethod, TestCategory("Unit")]
		public void Start_WithDataBeforeAndAfterStart_DataIsConsumed()
		{
			using (var sut = new ProducerConsumerQueue<int>())
			{
				var testBeforeData = _fixture.CreateMany<int>(12).ToList();
				var testAfterData = _fixture.CreateMany<int>(17).ToList();
				var totalTestData = testBeforeData.Union(testAfterData);

				var dataConsumed = new ConcurrentBag<int>();
				sut.OnConsumeData += (sender, i) => dataConsumed.Add(i);

				// Act
				testBeforeData.ForEach(sut.Add);
				sut.Start(5);
				testAfterData.ForEach(sut.Add);

				// Test
				Thread.Sleep(1500);

				CollectionAssert.AreEquivalent(totalTestData, dataConsumed);
			}
		}

		[TestMethod, TestCategory("Unit"), Ignore]
		public void Start_WithDataAfterStartFromThreads_DataIsConsumed()
		{
			const int threads = 4;
			const int sizePerThread = 25;

			using (var sut = new ProducerConsumerQueue<int>())
			{
				var dataConsumed = new ConcurrentBag<int>();
				sut.OnConsumeData += (sender, i) => dataConsumed.Add(i);

				sut.Start(5);

				var allTestData = new List<int>();
				for (int i = 0; i < threads; i++)
				{
					var testData = _fixture.CreateMany<int>(sizePerThread).ToList();
					allTestData.AddRange(testData);

					var localSut = sut;
					Task.Run(
						() =>
						{
							Thread.Sleep(200);
							testData.ForEach(localSut.Add);
						});
				}

				// Test
				Thread.Sleep(1500);

				CollectionAssert.AreEquivalent(allTestData, dataConsumed);
			}
		}

		[TestMethod, TestCategory("Unit")]
		public void Add_WithNullData_Works()
		{
			using (var sut = new ProducerConsumerQueue<object>())
			{
				bool eventCalled = false;
				sut.OnConsumeData += (sender, o) => eventCalled = true;

				sut.Start(5);


				// Act
				Assert.DoesNotThrow(() => sut.Add(null));

				// Test
				Thread.Sleep(500);
				Assert.IsFalse(eventCalled);
			}
		}

		[TestMethod, TestCategory("Unit")]
		public void Add_UserHandlerThrows_Works()
		{
			using (var sut = new ProducerConsumerQueue<object>())
			{
				sut.OnConsumeData += (sender, o) =>
				{
					throw new InvalidOperationException("User exception");
				};

				sut.Start(5);


				// Act
				Assert.DoesNotThrow(() => sut.Add(new object()));

				sut.CompleteAdding();

				// Test
				Thread.Sleep(500);

				Assert.IsTrue((sut.IsAddingCompleted));
			}
		}

		[TestMethod, TestCategory("Unit")]
		public void IsAddingCompleted_BeforeCompleteAdding_ShouldBeFalse()
		{
			using (var sut = new ProducerConsumerQueue<object>())
			{
				Assert.IsFalse(sut.IsAddingCompleted);
			}
		}

		[TestMethod, TestCategory("Unit")]
		public void IsAddingCompleted_AfterCompleteAdding_ShouldBeTrue()
		{
			using (var sut = new ProducerConsumerQueue<object>())
			{
				sut.CompleteAdding();
				Assert.IsTrue(sut.IsAddingCompleted);
			}
		}

		[TestMethod, TestCategory("Unit")]
		public void IsCompleted_BeforeCompleteAdding_ShouldBeFalse()
		{
			using (var sut = new ProducerConsumerQueue<object>())
			{
				Assert.IsFalse(sut.IsCompleted);
			}
		}

		[TestMethod, TestCategory("Unit")]
		public void IsCompleted_WithCompletedAndEmptyQueue_ShouldBeTrue()
		{
			using (var sut = new ProducerConsumerQueue<object>())
			{
				sut.CompleteAdding();
				Assert.IsTrue(sut.IsCompleted);
			}
		}
	}
}
