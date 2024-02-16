using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using R3.Triggers;
using Samples.R3Sample;
using UnityEngine;

public class Sandbox : MonoBehaviour
{
    private void Start()
    {
        Observable.Range(1, 10)
            .Take(1) // Firstとだいたい一緒（ただしEmptyの時にエラーにはならない）
            .Subscribe(x => Debug.Log(x));

        Observable.Range(1, 10)
            .TakeLast(1) // Lastとだいたい一緒（ただしEmptyの時にエラーにはならない）
            .Subscribe(x => Debug.Log(x));

        Observable.Empty<int>()
            .Take(1).DefaultIfEmpty() // FirstOrDefaultとだいたい一緒
            .Subscribe(x => Debug.Log(x));

        Observable.Empty<int>()
            .TakeLast(1).DefaultIfEmpty() // LastOrDefaultとだいたい一緒
            .Subscribe(x => Debug.Log(x))
            .AddTo(this);

    }

    private async UniTaskVoid ExampleAsync(CancellationToken ct)
    {
        // 最後の値を待機する
        var lastValue = await Observable.Range(1, 10).LastAsync(cancellationToken: ct);

        // 最初の値を待機する
        var firstValue = await Observable.Range(1, 10).FirstAsync(cancellationToken: ct);
    }
}