using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.R3Sample
{
    public class GuiEventSample : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private InputField _inputField;
        [SerializeField] private Slider _slider;
        [SerializeField] private Text _text;

        private void Start()
        {
            // ボタンのクリック
            _button
                .OnClickAsObservable()
                .Subscribe(_ => Debug.Log("Button Clicked!"))
                .AddTo(this);

            // InputFieldのテキスト変更
            _inputField.OnValueChangedAsObservable()
                .Subscribe(txt => Debug.Log("InputField Text: " + txt))
                .AddTo(this);

            // Sliderの値変更
            _slider.OnValueChangedAsObservable()
                .Subscribe(v => Debug.Log("Slider Value: " + v))
                .AddTo(this);

            // InputFieldのテキストをTextに反映
            _inputField.OnValueChangedAsObservable()
                .SubscribeToText(_text)
                .AddTo(this);
        }
    }
}