using System;
using R3;
using UnityEngine;

namespace Samples.R3Sample
{
    public class TimeProviderSample : MonoBehaviour
    {
        private void Start()
        {
            // Observable.EveryUpdateは一定のフレーム間隔でメッセージを発行する
            // どのフレームタイミングでメッセージ発行するかを指定できる
            Observable
                .EveryUpdate(UnityFrameProvider.FixedUpdate, destroyCancellationToken)
                .Subscribe(_ =>
                {
                    // FixedUpdateと同じタイミングで実行される
                });

Observable
    .Timer(TimeSpan.FromSeconds(1),
        // Time.scaleの影響を受ける
        // Unityが動作を停止していた場合は時間が進まない
        UnityTimeProvider.Update, 
        destroyCancellationToken)
    .Subscribe(_ =>
    {
        // 1秒後に実行される
    });

Observable
    .Timer(TimeSpan.FromSeconds(1),
        // Time.scaleの影響を受けないが、
        // Unityが動作を停止していた場合は時間が進まない
        UnityTimeProvider.UpdateIgnoreTimeScale, 
        destroyCancellationToken)
    .Subscribe(_ =>
    {
        // 1秒後に実行される
    });

Observable
    .Timer(TimeSpan.FromSeconds(1),
        // Unityの挙動とは独立した時間計測
        // Unityが動作を停止していた場合でも時間が進む
        UnityTimeProvider.UpdateRealtime, 
        destroyCancellationToken)
    .Subscribe(_ =>
    {
        // 1秒後に実行される
    });
        }
    }
}