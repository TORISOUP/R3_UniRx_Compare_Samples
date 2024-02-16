using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.R3Sample
{
    public class ThrottleLastSample : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _buttonText;
        [SerializeField] private Text _outputText;
        [SerializeField] private Slider _processSlider;

        private readonly ReactiveProperty<int> _currentValue = new();

        private void Start()
        {
            _currentValue.AddTo(this);

            // ボタンが押されたらカウンタを更新
            _button.OnClickAsObservable()
                .Subscribe(_ => _currentValue.Value++)
                .AddTo(this);

            // カウンタの数値をボタンに反映
            _currentValue.SubscribeToText(_buttonText).AddTo(this);

            // カウンタが増加したらオペレータを通してOutputのテキストに出力
            _currentValue
                .Skip(1)
                // ThrottleLastで遮断
                .ThrottleLast((_, ct) => UpdateSliderAsync(1f, ct))
                .SubscribeToText(_outputText)
                .AddTo(this);
        }

        // 一定時間待機する（その状況をスライダーに反映）
        private async UniTask UpdateSliderAsync(float waitSeconds, CancellationToken ct)
        {
            _processSlider.value = 0;

            // 合計で1秒待機する
            var currentTime = 0f;

            while (!ct.IsCancellationRequested && currentTime < waitSeconds)
            {
                await UniTask.Yield();
                currentTime += Time.deltaTime;
                // 経過状況をスライダーに反映
                _processSlider.value = Mathf.Clamp01(currentTime / waitSeconds);
            }
        }
    }
}