using System;
using Cysharp.Threading.Tasks;
using R3;
using R3.Triggers;
using UnityEngine;

namespace Samples.R3Sample
{
    public class ThrottleFirstAsyncSample : MonoBehaviour
    {
        private void Start()
        {
            // スペースキーが押されている間、一定間隔で弾を打つ
            this.UpdateAsObservable()
                .Where(_ => Input.GetKey(KeyCode.Space))
                .ThrottleFirst(async (_, ct) =>
                {
                    // 左Shiftキーが押されている場合は連射間隔を短くする
                    var waitTime = Input.GetKey(KeyCode.LeftShift) ? 0.1f : 0.5f;
                    await UniTask.Delay(TimeSpan.FromSeconds(waitTime), cancellationToken: ct);
                })
                .Subscribe(_ => ShotBullet());
        }

        private void ShotBullet()
        {
            Debug.Log("Shot Bullet!");
        }
    }
}