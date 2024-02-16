using System;
using R3;

namespace Samples.R3Sample
{
    // テスト対象
    public class TestTargetObject : IDisposable
    {
        private readonly Subject<int> _subject = new();

        // Publish()した値を一定時間後に出力するだけのObservable
        public Observable<int> OutputDelayFrame => _subject.DelayFrame(30);
        public Observable<int> OutputDelay => _subject.Delay(TimeSpan.FromSeconds(3));
        
        public void Publish(int value)
        {
            _subject.OnNext(value);
        }

        public void Dispose()
        {
            _subject.Dispose();
        }
    }
}