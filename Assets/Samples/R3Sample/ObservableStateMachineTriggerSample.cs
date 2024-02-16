using R3;
using R3.Triggers;
using UnityEngine;

namespace Samples.R3Sample
{
    public class ObservableStateMachineTriggerSample : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        public Animator Animator { get; private set; }
        public AnimatorStateInfo StateInfo { get; private set; }
        public int LayerIndex { get; private set; }
        void Start()
        {
            
            var t = _animator.GetBehaviour<ObservableStateMachineTrigger>();
            
            t.OnStateEnterAsObservable().Subscribe(x=>Debug.Log($"OnStateEnter {x.StateInfo}"));
            t.OnStateExitAsObservable().Subscribe(x=>Debug.Log($"OnStateExit {x.StateInfo}"));
        }
    }
}