using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Rhino.Mocks;
using Smi.Caching.Implementation;
using Smi.Caching.Interfaces;
using Smi.Caching.Providers;
using Assert = NUnit.Framework.Assert;

namespace Caching.Tests
{
	[TestClass]
	public class CacheTest
	{
		private TimeSpan _expiration;
		private ExpirationPolicy _expirationPolicy;
		private int _threshold;
		private TimeSpan _pollingInterval;
		private FakeTimerProvider _timerProvider;
		private IDateTimeProvider _dateTimeProvider;
		private ConcurrentDictionary<string, CacheItem<string, object>> _concurrentDictionary;
		private ConcurrentQueue<Hook<string, object>> _usageQueue;
		private UtcDateTime _now;

		[TestInitialize]
		public void Initialize()
		{
			_expiration = TimeSpan.FromSeconds(10);
			_expirationPolicy = ExpirationPolicy.Sliding;
			_threshold = int.MaxValue;
			_pollingInterval = TimeSpan.MaxValue;
			_timerProvider = FakeTimerProvider.CreateMock();
			_dateTimeProvider = MockRepository.GenerateStub<IDateTimeProvider>();
			_now = UtcDateTime.Now;
			_dateTimeProvider.Stub(x => x.UtcDateTimeNow).Return(_now);
			_concurrentDictionary = new ConcurrentDictionary<string, CacheItem<string, object>>();
			_usageQueue = new ConcurrentQueue<Hook<string, object>>();
		}

		private Cache<string, object> CreateSut()
		{
			return new Cache<string, object>(_expiration, _expirationPolicy, _threshold, _pollingInterval, _timerProvider,
											 _dateTimeProvider, _concurrentDictionary, _usageQueue);
		}

		[TestMethod, TestCategory("Unit")]
		public void Constructor_WhenExpirationIsNegative_ShouldThrowException()
		{
			_expiration = TimeSpan.FromSeconds(-1);
			Assert.That(() => CreateSut(), Throws.Exception.TypeOf<ArgumentException>());
		}

		[TestMethod, TestCategory("Unit")]
		public void Constructor_WhenThresholdIsZeroOrNegative_ExceptionIsThrown()
		{
			_threshold = 0;
			Assert.That(() => CreateSut(), Throws.Exception.TypeOf<ArgumentException>());
			_threshold = -1;
			Assert.That(() => CreateSut(), Throws.Exception.TypeOf<ArgumentException>());
		}

		[TestMethod, TestCategory("Unit")]
		public void Constructor_WhenPollingIntervalIsZeroOrNegative_ExceptionIsThrown()
		{
			_pollingInterval = TimeSpan.Zero;
			Assert.That(() => CreateSut(), Throws.Exception.TypeOf<ArgumentException>());
			_pollingInterval = TimeSpan.FromMilliseconds(-1);
			Assert.That(() => CreateSut(), Throws.Exception.TypeOf<ArgumentException>());
		}

		[TestMethod, TestCategory("Unit")]
		public void Constructor_WhenExpirationPolicyIsNoneAndExpirationIsNotZero_ExceptionIsThrown()
		{
			_expirationPolicy = ExpirationPolicy.None;
			_expiration = TimeSpan.FromSeconds(10);

			Assert.That(() => CreateSut(), Throws.Exception.TypeOf<ArgumentException>());
		}

		[TestMethod, TestCategory("Unit")]
		public void Constructor_WhenTimerProviderIsNull_ShouldThrowException()
		{
			_timerProvider = null;
			Assert.That(() => CreateSut(), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[TestMethod, TestCategory("Unit")]
		public void Constructor_WhenDateTimeProviderIsNull_ShouldThrowException()
		{
			_dateTimeProvider = null;
			Assert.That(() => CreateSut(), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[TestMethod, TestCategory("Unit")]
		public void Constructor_WhenConcurrentDictionaryIsNull_ExceptionIsThrown()
		{
			_concurrentDictionary = null;
			Assert.That(() => CreateSut(), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[TestMethod, TestCategory("Unit")]
		public void Constructor_WhenUsageQueueIsNull_ExceptionIsThrown()
		{
			_usageQueue = null;
			Assert.That(() => CreateSut(), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[TestMethod, TestCategory("Unit")]
		public void Constructor_WhenCalled_ShouldSetupTimer()
		{
			var actual = CreateSut();

			Assert.That(actual, Is.Not.Null);
			_timerProvider.TimerOptions.AssertWasCalled(x => x.SetInterval(_pollingInterval));
			_timerProvider.TimerOptions.AssertWasCalled(x => x.AutoReset());
			_timerProvider.TimerOptions.AssertWasCalled(x => x.WhenElapsed(Arg<Action>.Is.NotNull));
			_timerProvider.FakeTimer.AssertWasCalled(x => x.Start());
		}

		[TestMethod, TestCategory("Unit")]
		public void GetOrAdd_WhenValueFactoryIsNull_ExceptionIsThrown()
		{
			var sut = CreateSut();

			Assert.That(() => sut.GetOrAdd("xyz", null), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[TestMethod, TestCategory("Unit")]
		public void GetOrAdd_WhenCalled_ShouldAddItemToDictionary()
		{
			var sut = CreateSut();
			const string key = "xyz";
			var factoryValue = new object();

			sut.GetOrAdd(key, x => factoryValue);

			var item = _concurrentDictionary[key];
			Assert.That(item.Value, Is.SameAs(factoryValue));
		}

		[TestMethod, TestCategory("Unit")]
		public void GetOrAdd_WhenCalled_ShouldReturnExpectedValue()
		{
			var sut = CreateSut();
			const string key = "xyz";
			var factoryValue = new object();

			var actual = sut.GetOrAdd(key, x => factoryValue);

			Assert.That(actual, Is.SameAs(factoryValue));
		}

		[TestMethod, TestCategory("Unit")]
		public void GetOrAdd_WhenItemExists_ShouldReturnExistingInstance()
		{
			var sut = CreateSut();
			const string key = "xyz";
			var factoryValue = new object();
			var existingValue = new object();
			_concurrentDictionary[key] = new CacheItem<string, object>(key, existingValue);

			sut.GetOrAdd(key, x => factoryValue);

			var item = _concurrentDictionary[key];
			Assert.That(item.Value, Is.SameAs(existingValue));
		}

		[TestMethod, TestCategory("Unit")]
		public void GetOrAdd_WhenCalled_ShouldRecordUsage()
		{
			var sut = CreateSut();
			const string key = "xyz";
			var factoryValue = new object();
			_concurrentDictionary[key] = new CacheItem<string, object>(key, new object());

			sut.GetOrAdd(key, x => factoryValue);

			var item = _concurrentDictionary[key];
			AssertUsageRecorded(item);
		}

		[TestMethod, TestCategory("Unit")]
		public void GetOrAdd_WhenExpirationPolicyIsNotSlidingAndItemExists_UsageIsNotRecorded()
		{
			SetExpirationPolicyNone();
			var sut = CreateSut();
			const string key = "xyz";
			_concurrentDictionary[key] = new CacheItem<string, object>(key, new object());

			sut.GetOrAdd(key, x => new object());

			Assert.That(_usageQueue.Count, Is.EqualTo(0));
		}

		[TestMethod, TestCategory("Unit")]
		public void GetOrAdd_WhenExpirationPolicyIsAbsoluteAndItemDoesNotExist_UsageIsRecorded()
		{
			_expirationPolicy = ExpirationPolicy.Absolute;
			var sut = CreateSut();
			const string key = "xyz";

			sut.GetOrAdd(key, x => new object());

			var item = _concurrentDictionary[key];
			AssertUsageRecorded(item);
		}

		[TestMethod, TestCategory("Unit")]
		public void AddOrUpdate_WhenAddValueFactoryIsNull_ExceptionIsThrown()
		{
			var sut = CreateSut();

			Assert.That(() => sut.AddOrUpdate("xyz", null, (key, value) => new object()),
						Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[TestMethod, TestCategory("Unit")]
		public void AddOrUpdate_WhenUpdateValueFactory_ExceptionIsThrown()
		{
			var sut = CreateSut();

			Assert.That(() => sut.AddOrUpdate("xyz", key => new object(), null), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[TestMethod, TestCategory("Unit")]
		public void AddOrUpdate_WhenKeyIsNotPresentInDictionary_ItemIsAdded()
		{
			var sut = CreateSut();
			var addValue = new object();
			var updateValue = new object();
			const string key = "xyz";

			var actual = sut.AddOrUpdate(key, k => addValue, (k, value) => updateValue);

			var item = _concurrentDictionary[key];
			Assert.That(actual, Is.EqualTo(addValue));
			Assert.That(item.Value, Is.EqualTo(addValue));
		}

		[TestMethod, TestCategory("Unit")]
		public void AddOrUpdate_WhenKeyIsPresentInDictionary_ItemIsUpdated()
		{
			var sut = CreateSut();
			var addValue = new object();
			var updateValue = new object();
			const string key = "xyz";
			_concurrentDictionary[key] = new CacheItem<string, object>(key, new object());

			var actual = sut.AddOrUpdate(key, k => addValue, (k, value) => updateValue);

			var item = _concurrentDictionary[key];
			Assert.That(actual, Is.EqualTo(updateValue));
			Assert.That(item.Value, Is.EqualTo(updateValue));
		}

		[TestMethod, TestCategory("Unit")]
		public void AddOrUpdate_WhenCalled_ShouldRecordUsage()
		{
			var sut = CreateSut();
			const string key = "xyz";
			_concurrentDictionary[key] = new CacheItem<string, object>(key, new object());

			sut.AddOrUpdate(key, k => new object(), (k, value) => new object());

			var item = _concurrentDictionary[key];
			AssertUsageRecorded(item);
		}

		[TestMethod, TestCategory("Unit")]
		public void TryUpdate_WhenKeyIsNotPresent_FalseIsReturned()
		{
			var sut = CreateSut();
			var updateValue = new object();
			const string key = "xyz";

			var actual = sut.TryUpdate(key, updateValue, new object());

			Assert.That(actual, Is.False);
		}

		[TestMethod, TestCategory("Unit")]
		public void TryUpdate_WhenKeyIsNotPresent_ItemIsNotAdded()
		{
			var sut = CreateSut();
			var updateValue = new object();
			const string key = "xyz";

			sut.TryUpdate(key, updateValue, new object());

			Assert.That(_concurrentDictionary.Count, Is.EqualTo(0));
			Assert.That(_usageQueue.Count, Is.EqualTo(0));
		}

		[TestMethod, TestCategory("Unit")]
		public void TryUpdate_WhenValuesDiffer_FalseIsReturned()
		{
			var sut = CreateSut();
			var updateValue = new object();
			const string key = "xyz";
			var existingValue = new object();
			_concurrentDictionary[key] = new CacheItem<string, object>(key, existingValue);

			var actual = sut.TryUpdate(key, updateValue, new object());

			Assert.That(actual, Is.False);
		}

		[TestMethod, TestCategory("Unit")]
		public void TryUpdate_WhenValuesDiffer_ItemIsNotUpdated()
		{
			var sut = CreateSut();
			var updateValue = new object();
			const string key = "xyz";
			var existingValue = new object();
			_concurrentDictionary[key] = new CacheItem<string, object>(key, existingValue);

			sut.TryUpdate(key, updateValue, new object());

			Assert.That(_concurrentDictionary[key].Value, Is.EqualTo(existingValue));
		}

		[TestMethod, TestCategory("Unit")]
		public void TryUpdate_WhenValuesAreEqual_TrueIsReturned()
		{
			var sut = CreateSut();
			var updateValue = new object();
			const string key = "xyz";
			var existingValue = new object();
			_concurrentDictionary[key] = new CacheItem<string, object>(key, existingValue);

			var actual = sut.TryUpdate(key, updateValue, existingValue);

			Assert.That(actual, Is.True);
		}

		[TestMethod, TestCategory("Unit")]
		public void TryUpdate_WhenValuesAreEqual_ItemIsUpdated()
		{
			var sut = CreateSut();
			var updateValue = new object();
			const string key = "xyz";
			var existingValue = new object();
			_concurrentDictionary[key] = new CacheItem<string, object>(key, existingValue);

			sut.TryUpdate(key, updateValue, existingValue);

			Assert.That(_concurrentDictionary[key].Value, Is.SameAs(updateValue));
		}

		[TestMethod, TestCategory("Unit")]
		public void TryUpdate_WhenSuccessful_UsageIsRecorded()
		{
			var sut = CreateSut();
			var updateValue = new object();
			const string key = "xyz";
			var existingValue = new object();
			_concurrentDictionary[key] = new CacheItem<string, object>(key, existingValue);

			sut.TryUpdate(key, updateValue, existingValue);

			var item = _concurrentDictionary[key];
			AssertUsageRecorded(item);
		}

		[TestMethod, TestCategory("Unit")]
		public void TryGet_WhenItemDoesNotExist_FalseIsReturned()
		{
			var sut = CreateSut();
			object value;

			var actual = sut.TryGet("xyz", out value);

			Assert.That(actual, Is.False);
		}

		[TestMethod, TestCategory("Unit")]
		public void TryGet_WhenItemDoesNotExist_NullIsReturned()
		{
			var sut = CreateSut();
			object value;

			sut.TryGet("xyz", out value);

			Assert.That(value, Is.Null);
		}

		[TestMethod, TestCategory("Unit")]
		public void TryGet_WhenItemExists_TrueIsReturned()
		{
			var sut = CreateSut();
			const string key = "xyz";
			_concurrentDictionary[key] = new CacheItem<string, object>(key, new object());
			object value;

			var actual = sut.TryGet(key, out value);

			Assert.That(actual, Is.True);
		}

		[TestMethod, TestCategory("Unit")]
		public void TryGet_WhenItemExists_TheExpectedValueIsReturned()
		{
			var sut = CreateSut();
			const string key = "xyz";
			var existingValue = new object();
			_concurrentDictionary[key] = new CacheItem<string, object>(key, existingValue);
			object value;

			sut.TryGet(key, out value);

			Assert.That(value, Is.EqualTo(existingValue));
		}

		[TestMethod, TestCategory("Unit")]
		public void TryGet_WhenExpirationPolicyIsSliding_UsageIsRecorded()
		{
			_expirationPolicy = ExpirationPolicy.Sliding;
			var sut = CreateSut();
			const string key = "xyz";
			var cacheItem = new CacheItem<string, object>(key, new object());
			_concurrentDictionary[key] = cacheItem;
			object value;

			sut.TryGet(key, out value);

			AssertUsageRecorded(cacheItem);
		}

		[TestMethod, TestCategory("Unit")]
		public void TryGet_WhenExpirationPolicyIsNotSliding_UsageIsNotRecorded()
		{
			_expirationPolicy = ExpirationPolicy.Absolute;
			var sut = CreateSut();
			const string key = "xyz";
			_concurrentDictionary[key] = new CacheItem<string, object>(key, new object());
			object value;

			sut.TryGet(key, out value);

			Assert.That(_usageQueue.Count, Is.EqualTo(0));
		}


		[TestMethod, TestCategory("Unit")]
		public void OnExpiry_WhenActionIsNull_ExceptionIsThrown()
		{
			var sut = CreateSut();

			Assert.That(() => sut.OnExpiry(null), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[TestMethod, TestCategory("Unit")]
		public void OnExpiry_WhenActionIsValid_ItIsCalled()
		{
			var sut = CreateSut();
			var called = false;

			sut.OnExpiry(x =>
			{
				Assert.That(x, Is.Not.Null);
				called = true;
			});

			Assert.That(called, Is.True);
		}

		[TestMethod, TestCategory("Unit")]
		public void TimerElapses_WhenExpirationIsZero_ExpiredItemsAreNotRemoved()
		{
			_expiration = TimeSpan.Zero;
			var dictionary = new Dictionary<string, object>
				{
					{"a", new object()},
					{"b", new object()},
					{"c", new object()},
					{"d", new object()},
					{"e", new object()}
				};
			PopulateDictionaryAndUsageQueue(dictionary, _now.Subtract(TimeSpan.FromMinutes(10)));
			var sut = CreateSut();
			Assert.That(sut, Is.Not.Null);

			_timerProvider.FakeTimer.TriggerElapse();

			Assert.That(_concurrentDictionary.Count, Is.EqualTo(dictionary.Count));
			Assert.That(dictionary.All(x =>
			{
				CacheItem<string, object> cacheItem;
				if (!_concurrentDictionary.TryGetValue(x.Key, out cacheItem))
					return false;
				return cacheItem.Value == x.Value;
			}));
		}

		[TestMethod, TestCategory("Unit")]
		public void TimerElapses_WhenFired_ShouldCallReplaceCallback()
		{
			var value = new object();
			var dictionary = new Dictionary<string, object>
				{
					{"a", value}
				};
			PopulateDictionaryAndUsageQueue(dictionary, _now.Subtract(TimeSpan.FromMinutes(10)));
			var sut = CreateSut();
			var called = false;
			sut.OnExpiry(x => x.Replace(y =>
			{
				Assert.That(y, Is.EqualTo(value));
				called = true;
				return y;
			}));

			_timerProvider.FakeTimer.TriggerElapse();

			Assert.That(called, Is.True);
		}

		[TestMethod, TestCategory("Unit")]
		public void TimerElapses_WhenNoExpiration_ReplaceIsNotCalled()
		{
			_expiration = TimeSpan.Zero;
			_threshold = 2;
			var value = new object();
			var dictionary = new Dictionary<string, object>
				{
					{"a", new object()},
					{"b", new object()},
					{"c", new object()},
					{"d", new object()},
					{"e", new object()}
				};
			PopulateDictionaryAndUsageQueue(dictionary, _now.Subtract(TimeSpan.FromMinutes(10)));
			var sut = CreateSut();
			var called = false;
			sut.OnExpiry(x => x.Replace(y =>
			{
				Assert.That(y, Is.EqualTo(value));
				called = true;
				return y;
			}));

			_timerProvider.FakeTimer.TriggerElapse();

			Assert.That(called, Is.False);
		}

		[TestMethod, TestCategory("Unit")]
		public void TimerElapses_WhenFired_ShouldRemoveExpiredItems()
		{
			_expiration = TimeSpan.FromSeconds(10);

			var expired = new Dictionary<string, object>
				{
					{"a", new object()},
					{"b", new object()},
					{"c", new object()},
					{"d", new object()},
					{"e", new object()}
				};
			PopulateDictionaryAndUsageQueue(expired, _now.Subtract(TimeSpan.FromSeconds(11)));


			var notExpired = new Dictionary<string, object>
				{
					{"f", new object()},
					{"g", new object()},
					{"h", new object()},
					{"i", new object()},
					{"j", new object()}
				};
			PopulateDictionaryAndUsageQueue(notExpired, _now.Subtract(TimeSpan.FromSeconds(9)));

			var sut = CreateSut();
			Assert.That(sut, Is.Not.Null);

			_timerProvider.FakeTimer.TriggerElapse();

			Assert.That(_concurrentDictionary.Count, Is.EqualTo(expired.Count));
			Assert.That(expired.All(x => !_concurrentDictionary.ContainsKey(x.Key)));
			Assert.That(notExpired.All(x =>
			{
				CacheItem<string, object> cacheItem;
				if (!_concurrentDictionary.TryGetValue(x.Key, out cacheItem))
					return false;
				return cacheItem.Value == x.Value;
			}));
		}

		[TestMethod, TestCategory("Unit")]
		public void TimerElapses_WhenFired_ShouldRemoveTrailingOrphanHooks()
		{
			_expiration = TimeSpan.FromSeconds(10);

			for (var i = 0; i < 10; i++)
			{
				_usageQueue.Enqueue(new Hook<string, object>(new CacheItem<string, object>("x", new object())));
			}

			var expired = new Dictionary<string, object>
				{
					{"a", new object()},
					{"b", new object()},
					{"c", new object()},
					{"d", new object()},
					{"e", new object()}
				};
			PopulateDictionaryAndUsageQueue(expired, _now.Subtract(TimeSpan.FromSeconds(11)));

			for (var i = 0; i < 10; i++)
			{
				_usageQueue.Enqueue(new Hook<string, object>(new CacheItem<string, object>("x", new object())));
			}

			var notExpired = new Dictionary<string, object>
				{
					{"f", new object()},
					{"g", new object()},
					{"h", new object()},
					{"i", new object()},
					{"j", new object()}
				};
			PopulateDictionaryAndUsageQueue(notExpired, _now.Subtract(TimeSpan.FromSeconds(9)));

			var sut = CreateSut();
			Assert.That(sut, Is.Not.Null);

			_timerProvider.FakeTimer.TriggerElapse();

			Assert.That(_usageQueue.Count, Is.EqualTo(notExpired.Count));
		}

		[TestMethod, TestCategory("Unit")]
		public void TimerElapses_WhenFired_ShouldRemoveExcessItems()
		{
			_expiration = TimeSpan.Zero;
			var notExpired = new Dictionary<string, object>
				{
					{"f", new object()},
					{"g", new object()},
					{"h", new object()},
					{"i", new object()},
					{"j", new object()}
				};
			PopulateDictionaryAndUsageQueue(notExpired, _now.Subtract(TimeSpan.FromSeconds(9)));
			_threshold = 2;

			var sut = CreateSut();
			Assert.That(sut, Is.Not.Null);

			_timerProvider.FakeTimer.TriggerElapse();

			Assert.That(_concurrentDictionary.Count, Is.EqualTo(_threshold));
		}

		[TestMethod, TestCategory("Unit")]
		public void TimerElapses_WhenExpirationPolicyIsNone_ExpiredItemsAreNotRemoved()
		{
			SetExpirationPolicyNone();
			var expired = new Dictionary<string, object>
				{
					{"a", new object()},
					{"b", new object()},
					{"c", new object()},
					{"d", new object()},
					{"e", new object()}
				};
			PopulateDictionaryAndUsageQueue(expired, _now.Subtract(TimeSpan.FromSeconds(11)));
			var sut = CreateSut();
			Assert.That(sut, Is.Not.Null);

			_timerProvider.FakeTimer.TriggerElapse();

			Assert.That(expired.All(x => _concurrentDictionary.Any(y => y.Key == x.Key && y.Value.Value == x.Value)));
		}

		[TestMethod, TestCategory("Unit")]
		public void Clear_WhenCalled_ShouldClearConcurrentDictionary()
		{
			var expired = new Dictionary<string, object>
				{
					{"a", new object()},
					{"b", new object()},
					{"c", new object()},
					{"d", new object()},
					{"e", new object()}
				};
			PopulateDictionaryAndUsageQueue(expired, _now.Subtract(TimeSpan.FromSeconds(11)));
			var sut = CreateSut();

			sut.Clear();

			Assert.That(_concurrentDictionary.Count, Is.EqualTo(0));
		}

		private void AssertUsageRecorded(CacheItem<string, object> cacheItem)
		{
			Assert.That(cacheItem.LastUsage, Is.EqualTo(_now));
			var hook = _usageQueue.ToArray().Single();
			Assert.That(hook.CacheItem, Is.SameAs(cacheItem));
			Assert.That(cacheItem.Hook, Is.SameAs(hook));
		}

		private void PopulateDictionaryAndUsageQueue(IEnumerable<KeyValuePair<string, object>> items, UtcDateTime lastUsage)
		{
			foreach (var item in items)
			{
				PopulateCacheItemAndUsage(lastUsage, item.Key, item.Value);
			}
		}

		private void PopulateCacheItemAndUsage(UtcDateTime lastUsage, string key, object value)
		{
			var cacheItem = new CacheItem<string, object>(key, value);
			var hook = new Hook<string, object>(cacheItem);
			cacheItem.Hook = hook;
			cacheItem.LastUsage = lastUsage;
			_concurrentDictionary.TryAdd(key, cacheItem);
			_usageQueue.Enqueue(hook);
		}

		private void SetExpirationPolicyNone()
		{
			_expirationPolicy = ExpirationPolicy.None;
			_expiration = TimeSpan.Zero;
		}
	}
}
