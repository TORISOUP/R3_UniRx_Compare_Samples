using R3;
using R3.Triggers;
using UnityEngine;

namespace Samples.R3Sample
{
    public class RegisterToSample : MonoBehaviour
    {
        private void Start()
        {
            // このObservableをCancellationTokenに連動させる
            this.UpdateAsObservable()
                .Subscribe(_ => Debug.Log("Update!"))
                .RegisterTo(destroyCancellationToken);
        }
    }
}