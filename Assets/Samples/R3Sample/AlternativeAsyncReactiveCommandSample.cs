using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Samples.R3Sample
{
    public class AlternativeAsyncReactiveCommandSample : MonoBehaviour
    {
        // 各種ボタン
        [SerializeField] private Button _buttonA;
        [SerializeField] private Button _buttonB;
        [SerializeField] private Button _buttonC;

        // 状態表示用のテキスト
        [SerializeField] private Text _text;

        // ボタン制御用のReactiveProperty（ゲート）
        private readonly ReactiveProperty<bool> _gate = new(true);

        private void Start()
        {
            _gate.AddTo(this);

            // ゲートがfalseのときはボタンを押せない状態にする
            _gate.SubscribeToInteractable(_buttonA).RegisterTo(destroyCancellationToken);
            _gate.SubscribeToInteractable(_buttonB).RegisterTo(destroyCancellationToken);
            _gate.SubscribeToInteractable(_buttonC).RegisterTo(destroyCancellationToken);
            // 状態を可視化する
            _gate.Select(x => $"Gate={x}").SubscribeToText(_text);


            // ボタンAが押されたときの処理
            _buttonA.OnClickAsObservable()
                .Where(_ => _gate.Value)
                .SubscribeAwait(async (_, ct) =>
                {
                    // 非同期処理にゲートを連動させる
                    await GateControlAsync(MethodAAsync(ct));

                }, AwaitOperation.Drop)
                .RegisterTo(destroyCancellationToken);

            // ボタンBが押されたときの処理
            _buttonB.OnClickAsObservable()
                .Where(_ => _gate.Value)
                .SubscribeAwait(async (_, ct) =>
                {
                    // 非同期処理にゲートを連動させる
                    await GateControlAsync(MethodBAsync(ct));

                }, AwaitOperation.Drop)
                .RegisterTo(destroyCancellationToken);

            // ボタンCが押されたときの処理
            _buttonC.OnClickAsObservable()
                .Where(_ => _gate.Value)
                .SubscribeAwait(async (_, ct) =>
                {
                    // 非同期処理にゲートを連動させる
                   await GateControlAsync(MethodCAsync (ct));
                   
                }, AwaitOperation.Drop)
                .RegisterTo(destroyCancellationToken);
        }

        // 非同期処理実行中はゲートを閉める
        private async UniTask GateControlAsync(UniTask task)
        {
            _gate.Value = false;
            try
            {
                await task;
            }
            finally
            {
                _gate.Value = true;
            }
        }


        // なにか各種非同期な処理があったとする
        private async UniTask MethodAAsync(CancellationToken ct)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(0.5f, 2f)), cancellationToken: ct);
        }

        private async UniTask MethodBAsync(CancellationToken ct)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(0.5f, 2f)), cancellationToken: ct);
        }

        private async UniTask MethodCAsync(CancellationToken ct)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(0.5f, 2f)), cancellationToken: ct);
        }
    }
}