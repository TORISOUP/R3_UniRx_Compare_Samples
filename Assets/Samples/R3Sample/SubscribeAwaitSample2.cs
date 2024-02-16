using System;
using Cysharp.Threading.Tasks;
using R3;
using R3.Triggers;
using UnityEngine;

namespace Samples.R3Sample
{
    public class SubscribeAwaitSample2 : MonoBehaviour
    {
        // 無敵フラグ
        [SerializeField] SerializableReactiveProperty<bool> _isInvincible = new(false);

        private void Start()
        {
            // 衝突したら３秒間無敵フラグをTrueにする
            // 無敵中に再衝突した場合はそこからまた３秒数える
            this.OnCollisionEnterAsObservable()
                .SubscribeAwait(async (collision, ct) =>
                {
                    _isInvincible.Value = true;
                    await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: ct);
                    _isInvincible.Value = false;
                }, 
                    // AwaitOperation.Switchは非同期処理中に次のメッセージが来たら
                    // 今実行中の非同期処理をキャンセルして新しい非同期処理を開始する
                    AwaitOperation.Switch)
                .AddTo(this);
        }
    }
}