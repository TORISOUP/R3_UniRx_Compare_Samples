using R3;
using UnityEngine;

namespace Samples.R3Sample
{
    public class EveryValueChangedSample : MonoBehaviour
    {
        private void Start()
        {
            // UniRxではこうだったが
            // transform.ObserveEveryValueChanged(t => t.position).Subscribe(p => Debug.Log(p));

            // R3での記法はこうなった
            Observable
                .EveryValueChanged(transform, t => t.position)
                .Subscribe(p => Debug.Log(p));

            // CancellationTokenを渡すこともできる
            Observable
                .EveryValueChanged(transform, t => t.rotation, destroyCancellationToken)
                .Subscribe(r => Debug.Log(r));
        }
    }
}