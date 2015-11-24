using System;
using System.Runtime.Caching;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smi.Caching.Implementation;
using Smi.Caching.Providers;
using Assert = NUnit.Framework.Assert;

namespace Caching.Tests
{
	[TestClass]
	public class MemoryCacheProviderTest
	{
		private MemoryCacheProvider _cache;
		private IDateTimeProvider _dateTimeProvider;
		private IGuidProvider _guidProvider;
		private const int StandardTolerance = 100;

		[TestInitialize]
		public void TestInitialize()
		{
			_guidProvider = new GuidProvider();
			_dateTimeProvider = new DateTimeProvider();
			_cache = new MemoryCacheProvider(_guidProvider);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			if (_cache != null)
			{
				_cache.Dispose();
				_cache = null;
			}
		}

		[TestMethod, TestCategory("Unit")]
		public void Constructor_GuidProviderIsNull_ThrowsArgumentException()
		{
			Assert.Throws<ArgumentNullException>(() => new MemoryCacheProvider(null));
		}

		[TestMethod, TestCategory("Unit")]
		public void Cache_WhenItemIsAdded_ItemShouldBeRetrievable()
		{
			var cacheItem = new object();
			const string key = "key";
			_cache.Add(key, cacheItem);

			Assert.AreEqual(cacheItem, _cache.Get<object>(key));
		}

		[TestMethod, TestCategory("Unit")]
		public void Cache_WhenItemIsNotAdded_ItemShouldNotBeRetrievable()
		{
			const string key = "key";

			Assert.IsNull(_cache.Get<object>(key));
		}

		[TestMethod, TestCategory("Unit")]
		public void Cache_WhenItemIsAddedAndRemoved_ItemShouldNotBeRetrievable()
		{
			var cacheItem = new object();
			const string key = "key";
			_cache.Add(key, cacheItem);
			Assert.AreEqual(cacheItem, _cache.Get<object>(key));
			_cache.Remove<object>(key);

			Assert.IsNull(_cache.Get<object>(key));
		}

		[TestMethod, TestCategory("Unit")]
		public void Cache_WhenItemAddedWithAbsoluteExpiration_ItemShouldBeRetrievableBeforeExpiration()
		{
			var expiration = _dateTimeProvider.UtcDateTimeNow.AddSeconds(1);
			var cacheItem = new object();
			const string key = "key";
			_cache.Add(key, cacheItem, expiration);
			Thread.Sleep((int)(expiration - _dateTimeProvider.UtcDateTimeNow).TotalMilliseconds - StandardTolerance);
			Assert.AreEqual(cacheItem, _cache.Get<object>(key));
		}

		[TestMethod, TestCategory("Unit"), TestCategory("SlowRunning")]
		public void Cache_WhenItemAddedWithAbsoluteExpiration_ItemShouldNotBeRetrievableAfterExpiration()
		{
			var expiration = _dateTimeProvider.UtcDateTimeNow.AddSeconds(5);
			var cacheItem = new object();
			const string key = "key";
			_cache.Add(key, cacheItem, expiration);
			Thread.Sleep((int)(expiration - _dateTimeProvider.UtcDateTimeNow).TotalMilliseconds + StandardTolerance);
			var actual = _cache.Get<object>(key);
			Assert.IsNull(actual);
		}

		[TestMethod, TestCategory("Unit")]
		public void Cache_WhenItemAddedWithSlidingExpiration_ItemShouldBeRetrievableBeforeExpiration()
		{
			const int expiration = 1000;
			var cacheItem = new object();
			const string key = "key";
			_cache.Add(key, cacheItem, TimeSpan.FromMilliseconds(expiration));
			Assert.AreEqual(cacheItem, _cache.Get<object>(key));
			Thread.Sleep(expiration - StandardTolerance);
			Assert.AreEqual(cacheItem, _cache.Get<object>(key));
		}

		[TestMethod, TestCategory("Unit"), TestCategory("SlowRunning")]
		public void Cache_WhenItemAddedWithSlidingExpiration_ItemShouldNotBeRetrievableAfterExpiration()
		{
			const int expiration = 1000;
			var cacheItem = new object();
			const string key = "key";
			_cache.Add(key, cacheItem, TimeSpan.FromMilliseconds(expiration));
			Assert.AreEqual(cacheItem, _cache.Get<object>(key));
			Thread.Sleep(expiration + StandardTolerance);
			Assert.IsNull(_cache.Get<object>(key));
		}

		[TestMethod, TestCategory("Unit")]
		public void Dispose_WithCachedItems_ItemsAreNotDisposed()
		{
			using (var cacheItem = new DisposableObject())
			{
				const string key = "key";
				_cache.Add(key, cacheItem, TimeSpan.FromSeconds(5));

				Assert.AreEqual(cacheItem, _cache.Get<DisposableObject>(key), "Item not found in cache");
				_cache.Dispose();
				_cache = null;

				Assert.IsFalse(cacheItem.IsDisposed, "Cache item is unexpectedly disposed");
			}
		}

		[TestMethod, TestCategory("Unit")]
		public void Dispose_WithCachedItemsAndDisposingCallback_ItemsAreDisposed()
		{
			var callbackEvent = new AutoResetEvent(false);

			using (var cacheItem = new DisposableObject())
			{
				const string key = "key";
				_cache.Add(key, cacheItem, TimeSpan.FromSeconds(5), CacheItemPriority.Default, args =>
				{
					var item = args.CacheItem.Value as IDisposable;
					if (item != null)
					{
						item.Dispose();
					}
					callbackEvent.Set();
				});

				Assert.AreEqual(cacheItem, _cache.Get<DisposableObject>(key), "Item not found in cache");
				_cache.Dispose();
				_cache = null;

				var success = callbackEvent.WaitOne(TimeSpan.FromMilliseconds(100));
				Assert.IsTrue(success, "Callback never fired");
				Assert.IsTrue(cacheItem.IsDisposed, "Cache item is not disposed");
			}
		}

		[TestMethod, TestCategory("Unit"), TestCategory("SlowRunning")]
		public void Dispose_WithCachedItemsAndDisposingCallback_ItemsAreDisposedOnExpiration()
		{
			var callbackEvent = new AutoResetEvent(false);

			var lifetime = TimeSpan.FromSeconds(1);
			using (var cacheItem = new DisposableObject())
			{
				const string key = "key";
				_cache.Add(key, cacheItem, lifetime, CacheItemPriority.Default, args =>
				{
					var item = args.CacheItem.Value as IDisposable;
					if (item != null)
					{
						item.Dispose();
					}
					callbackEvent.Set();

					Assert.AreEqual(CacheEntryRemovedReason.Expired, args.RemovedReason, "Cache entry removed by unexpected reason: {0}", args.RemovedReason);
				});

				Assert.AreEqual(cacheItem, _cache.Get<DisposableObject>(key), "Item not found in cache");

				// Note: It appears that callbacks are not always invoked just after expiration - and this behaves differently on different machines
				var success = callbackEvent.WaitOne(TimeSpan.FromSeconds(30));
				Assert.IsNull(_cache.Get<DisposableObject>(key), "Object still in cache");
				Assert.IsTrue(success, "Callback never fired");
				Assert.IsTrue(cacheItem.IsDisposed, "Cache item is not disposed");
			}
		}
	}

	internal class DisposableObject : IDisposable
	{
		public bool IsDisposed { get; private set; }

		public void Dispose()
		{
			IsDisposed = true;
		}
	}
}
