using System;
using Microsoft.Extensions.Time.Testing;
using NUnit.Framework;
using Samples.R3Sample;
using R3;

namespace Samples.R3Tests
{
    public class TestR3Observable
    {
        private FakeFrameProvider _fakeFrameProvider;
        private FakeTimeProvider _fakeTimeProvider;

        [SetUp]
        public void SetUp()
        {
            // SetUpでデフォルトのFrameProviderを差し替える
            _fakeFrameProvider = new FakeFrameProvider();
            _fakeTimeProvider = new FakeTimeProvider();
            ObservableSystem.DefaultFrameProvider = _fakeFrameProvider;
            ObservableSystem.DefaultTimeProvider = _fakeTimeProvider;
        }

        [Test]
        public void OutputDelayFrameのテスト()
        {
            using var target = new TestTargetObject();

            // LiveListに変換
            using var liveList = target.OutputDelayFrame.ToLiveList();

            // 現時点で出力はゼロ
            CollectionAssert.AreEqual(Array.Empty<int>(), liveList);

            target.Publish(1);
            target.Publish(2);
            target.Publish(3);

            // 30フレーム経過するまで出力はゼロ
            CollectionAssert.AreEqual(Array.Empty<int>(), liveList);

            // 29フレーム経過させる
            _fakeFrameProvider.Advance(29);

            // まだ出力はゼロ
            CollectionAssert.AreEqual(Array.Empty<int>(), liveList);

            // さらに1フレーム経過させる
            _fakeFrameProvider.Advance(1);

            // 30フレーム経過したので出力されているはず
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, liveList);
        }

        [Test]
        public void OutputDelayのテスト()
        {
            using var target = new TestTargetObject();

            // LiveListに変換
            using var liveList = target.OutputDelay.ToLiveList();

            // 現時点で出力はゼロ
            CollectionAssert.AreEqual(Array.Empty<int>(), liveList);

            target.Publish(1);
            target.Publish(2);
            target.Publish(3);

            // 3秒経過するまで出力はゼロ
            CollectionAssert.AreEqual(Array.Empty<int>(), liveList);

            // 2秒進める経過させる
            _fakeTimeProvider.Advance(TimeSpan.FromSeconds(2));

            // まだ出力はゼロ
            CollectionAssert.AreEqual(Array.Empty<int>(), liveList);

            // さらに1秒進める
            _fakeTimeProvider.Advance(TimeSpan.FromSeconds(1));

            // 計3秒経ったので値が発行された
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, liveList);
        }
    }
}