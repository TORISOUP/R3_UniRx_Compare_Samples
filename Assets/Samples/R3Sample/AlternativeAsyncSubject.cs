using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Samples.R3Sample
{
    public class AlternativeAsyncSubject : MonoBehaviour
    {
        private void Start()
        {
            // UniTaskCompletionSourceをAsyncSubjectの代わりに使ってみる
            var utcs = new UniTaskCompletionSource<int>();

            // OnNext(100) + OnCompleted()と同じ
            utcs.TrySetResult(100);

            // OnError(new Exception())と同じ
            utcs.TrySetException(new Exception());

            // OnError(OperationCanceledException)とだいたい同じ
            utcs.TrySetCanceled();
            
            // -----------------------------------------------
            
            // ラムダ式で待ち受け。ただこれだとキャンセルができない
            utcs.Task.ContinueWith(result => Debug.Log(result));

            
            // async/awaitで待ち受け
            // AttachExternalCancellationでキャンセルを外付け
            UniTask.Void(async () =>
            {
                var result = await utcs.Task.AttachExternalCancellation(destroyCancellationToken);
                Debug.Log(result);
            });

            // UniTask -> ValueTask -> R3.Observable
            utcs.Task
                .AsValueTask()
                .ToObservable()
                .Subscribe(x => Debug.Log(x))
                .AddTo(this);
        }
    }
}