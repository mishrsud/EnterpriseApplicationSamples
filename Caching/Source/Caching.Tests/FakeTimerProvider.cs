using System;
using System.Collections.Generic;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;
using Smi.Caching.Interfaces;

namespace Caching.Tests
{
	public class FakeTimerProvider : ITimerProvider, IDisposable
	{
		private readonly ITimerOptions _timerOptions;
		private readonly FakeTimer _fakeTimer;
		private readonly List<Action> _elapseActions;

		/// <summary>
		/// Creates a new <see cref="FakeTimerProvider"/> instance.
		/// </summary>
		public FakeTimerProvider()
		{
			_elapseActions = new List<Action>();
			_timerOptions = CreateTimerOptions();
			_fakeTimer = CreateFakeTimer();
		}

		private ITimerOptions CreateTimerOptions()
		{
			var timerOptions = MockRepository.GenerateStub<ITimerOptions>();
			timerOptions.Stub(x => x.AutoReset()).Return(timerOptions);
			timerOptions.Stub(x => x.SetInterval(Arg<TimeSpan>.Is.Anything)).Return(timerOptions);
			timerOptions.Stub(x => x.WhenElapsed(Arg<Action>.Is.Anything)).Do(new Func<Action, ITimerOptions>(action =>
			{
				_elapseActions.Add(action);
				return timerOptions;
			}));
			return timerOptions;
		}

		private FakeTimer CreateFakeTimer()
		{
			Func<Action> elapseActionProvider = () => (() => _elapseActions.ForEach(action => action()));
			var fakeTimer = MockRepository.GeneratePartialMock<FakeTimer>(elapseActionProvider);
			fakeTimer.Stub(x => x.Start()).CallOriginalMethod(OriginalCallOptions.CreateExpectation);
			fakeTimer.Stub(x => x.Stop()).CallOriginalMethod(OriginalCallOptions.CreateExpectation);
			fakeTimer.Stub(x => x.Reset()).CallOriginalMethod(OriginalCallOptions.CreateExpectation);
			fakeTimer.Stub(x => x.TriggerElapse()).CallOriginalMethod(OriginalCallOptions.CreateExpectation);
			return fakeTimer;
		}

		/// <summary>
		/// Creates a new <see cref="ITimer"/> instance using the specfied options.
		/// </summary>
		/// <param name="optionsAction">The action to configure the timer.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="optionsAction"/> is null.</exception>
		public virtual ITimer Create(Action<ITimerOptions> optionsAction)
		{
			if (optionsAction == null) throw new ArgumentNullException("optionsAction");
			optionsAction(_timerOptions);
			return _fakeTimer;
		}

		/// <summary>
		/// Creates a new <see cref="ITimer"/> instance using the specfied updateInterval and callback.
		/// Optionally start the timer immediately.
		/// </summary>
		/// <param name="updateInterval">The update interval.</param>
		/// <param name="callback">The callback.</param>
		/// <param name="autoStart">if set to <c>true</c> [start timer immediately].</param>
		public virtual ITimer Create(TimeSpan updateInterval, Action callback, bool autoStart)
		{
			if (callback == null) throw new ArgumentNullException("callback");

			_timerOptions.SetInterval(updateInterval);
			_timerOptions.WhenElapsed(callback);
			_timerOptions.AutoReset();

			if (autoStart) _fakeTimer.Start();

			return _fakeTimer;
		}

		/// <summary>
		/// Reference to the static timer options instance. Can be used for making assertions.
		/// </summary>
		public virtual ITimerOptions TimerOptions
		{
			get { return _timerOptions; }
		}

		/// <summary>
		/// Reference to the static fake timer returned by Create. Can be used for assertions and explicitly triggering elapse.
		/// </summary>
		public FakeTimer FakeTimer
		{
			get { return _fakeTimer; }
		}

		/// <summary>
		/// Creates a mock instance of <see cref="FakeTimerProvider"/> allowing assertions and stubbing.
		/// </summary>
		public static FakeTimerProvider CreateMock()
		{
			var result = MockRepository.GeneratePartialMock<FakeTimerProvider>();
			result
				.Stub(x => x.Create(Arg<Action<ITimerOptions>>.Is.Anything))
				.CallOriginalMethod(OriginalCallOptions.CreateExpectation);
			result
				.Stub(x => x.TimerOptions)
				.CallOriginalMethod(OriginalCallOptions.CreateExpectation);
			return result;
		}

		/// <summary>
		/// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_fakeTimer.Dispose();
				_timerOptions.Dispose();
			}
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
