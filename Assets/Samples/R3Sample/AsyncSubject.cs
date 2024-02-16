using System;
using R3;

namespace Samples.R3Sample
{
    public sealed class AsyncSubject<T> : Observable<T>, ISubject<T>, IDisposable
    {
        private readonly Subject<T> _subject = new();
        private bool _isDisposed;
        private readonly object _gate = new();

        T _lastValue;
        bool _hasValue;
        bool _isStopped;
        Exception _lastError;

        protected override IDisposable SubscribeCore(Observer<T> observer)
        {
            var ex = default(Exception);
            var v = default(T);
            var hv = false;

            lock (_gate)
            {
                ThrowIfDisposed();
                if (!_isStopped)
                {
                    return _subject.Subscribe(observer);
                }

                ex = _lastError;
                v = _lastValue;
                hv = _hasValue;
            }

            if (ex != null)
            {
                observer.OnCompleted(ex);
            }
            else if (hv)
            {
                observer.OnNext(v);
                observer.OnCompleted();
            }
            else
            {
                observer.OnCompleted();
            }

            return Disposable.Empty;
        }

        public void OnNext(T value)
        {
            lock (_gate)
            {
                ThrowIfDisposed();
                if (_isStopped) return;

                _hasValue = true;
                _lastValue = value;
            }
        }

        public void OnErrorResume(Exception error)
        {
            // do nothing...
        }

        public void OnCompleted(Result complete)
        {
            T v;
            bool hv;
            lock (_gate)
            {
                ThrowIfDisposed();
                if (_isStopped) return;
                _isStopped = true;
                v = _lastValue;
                hv = _hasValue;
            }

            if (hv)
            {
                _subject.OnNext(v);
                _subject.OnCompleted();
            }
            else
            {
                _subject.OnCompleted();
            }
        }

        void ThrowIfDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(nameof(AsyncSubject<T>));
        }

        public void Dispose()
        {
            lock (_gate)
            {
                _isDisposed = true;
                _subject.Dispose();
            }
        }
    }
}