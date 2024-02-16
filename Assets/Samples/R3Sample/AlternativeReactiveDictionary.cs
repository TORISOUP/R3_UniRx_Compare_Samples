using ObservableCollections;
using UnityEngine;
using R3;

namespace Samples.R3Sample
{
    public class AlternativeReactiveDictionary : MonoBehaviour
    {
        private void Start()
        {
            // ReactiveDictionaryの代わり
            var observableDictionary = new ObservableDictionary<int, string>();

            // 新しい要素が追加されたイベント
            observableDictionary.ObserveAdd(destroyCancellationToken)
                .Subscribe(collectionAddEvent =>
                {
                    var (key, value) = collectionAddEvent.Value;
                    Debug.Log($"Add [{key}]={value}");
                });

            observableDictionary.ObserveReplace(destroyCancellationToken)
                .Subscribe(replaceEvent =>
                {
                    var key = replaceEvent.NewValue.Key;
                    var newValue = replaceEvent.NewValue.Value;
                    var oldValue = replaceEvent.OldValue.Value;
                    Debug.Log($"Replace [{key}]={oldValue} -> {newValue}");
                });
            
            observableDictionary[1] = "hoge";
            observableDictionary[2] = "fuga";
            observableDictionary[1] = "piyo";
        }
    }
}