using R3;
using UnityEngine;

namespace Samples.R3Sample
{
    public class EveryUpdateSample : MonoBehaviour
    {
        private void Start()
        {
            // このGameObjectに寿命が連動したObservableになる
            // 実質 this.UpdateAsObservable()
            Observable
                .EveryUpdate(destroyCancellationToken)
                .Subscribe(_ => OnUpdate());
            
        }

        private void OnUpdate()
        {
        }
    }
}