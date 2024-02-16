using System;
using NUnit.Framework;
using R3;

namespace Samples.R3Tests
{
    public class LiveListSample
    {
        [Test]
        public void LiveListが便利()
        {
            using var subject = new Subject<int>();

            // Observable -> LiveList
            using var liveList = subject.ToLiveList();

            // 現時点で出力はゼロ
            CollectionAssert.AreEqual(Array.Empty<int>(), liveList);

            // 「1」を発行
            subject.OnNext(1);

            // 発行された「１」が反映されている
            CollectionAssert.AreEqual(new[] { 1 }, liveList);

            subject.OnNext(2);
            subject.OnNext(3);

            // 3つの値が反映されている
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, liveList);
        }
    }
}