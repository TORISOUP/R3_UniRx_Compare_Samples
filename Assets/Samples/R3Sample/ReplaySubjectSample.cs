using System;
using R3;
using R3.Triggers;
using UnityEngine;

namespace Samples.R3Sample
{
    public class ReplaySubjectSample : MonoBehaviour
    {
        private void Start()
        {
            var replayFrameSubject = new ReplayFrameSubject<Unit>(window: 10, UnityFrameProvider.FixedUpdate);

            // Update()のタイミングで押されたボタン入力をReplaySubjectに記録する
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.Space))
                .Subscribe(_ =>
                {
                    replayFrameSubject.OnNext(Unit.Default);
                });
        }
    }
}