using R3;
using R3.Triggers;
using UnityEngine;

namespace Samples.R3Sample
{
    public class TriggerSample : MonoBehaviour
    {
        private void Start()
        {
            // このGameObjectに紐づいたOnCollisionEnterをObservableとして取得できる
            this.OnCollisionEnterAsObservable()
                .Subscribe(collision =>
                {
                    Debug.Log("OnCollisionEnter: " + collision.gameObject.name);
                });

            // Update()をObservableとして取得できる
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    Debug.Log("Update!");
                });
            
            // 他にもいろいろある

        }
    }
}