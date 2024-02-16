using R3;
using R3.Triggers;
using UnityEngine;

namespace Samples.R3Sample
{
    public class AddToSample : MonoBehaviour
    {
        [SerializeField] private GameObject _childObject;

        private void Start()
        {
            // childObjectに紐づいたOnCollisionEnterをObservableとして取得
            _childObject
                .OnCollisionEnterAsObservable()
                .Subscribe(collision =>
                {
                    Debug.Log("OnCollisionEnter: " + collision.gameObject.name);
                })
                // Observableの寿命をこのMonoBehaviourに紐付ける
                .AddTo(this);
        }
    }
}