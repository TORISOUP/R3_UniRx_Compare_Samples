using R3;
using UnityEngine;

namespace Samples
{
    public class StateSample : MonoBehaviour
    {
        [SerializeField] private float _fallThreshold = -10;

        private void Start()
        {
            Observable.EveryValueChanged(transform, t => t.position, destroyCancellationToken)
                // ラムダ式から外部変数をキャプチャさせない
                .Where(_fallThreshold, (position, threshold) => position.y < threshold)
                .Subscribe(_ => Destroy(gameObject));
        }
    }
}