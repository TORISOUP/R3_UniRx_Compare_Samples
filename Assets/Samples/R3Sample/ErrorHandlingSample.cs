using System;
using R3;
using UnityEngine;

namespace Samples.R3Sample
{
    public class ErrorHandlingSample : MonoBehaviour
    {
        private void Start()
        {
            using var subject = new Subject<string>();

            // 文字列をintに変換する
            subject
                .Select(int.Parse) // パース失敗時に例外が発生する
                // OnErrorResume を OnCompleted(Exception) に変換する
                .OnErrorResumeAsFailure()
                // CatchはOnCompleted(Exception）に反応する
                .Catch<int, FormatException>(ex =>
                {
                    Debug.LogError(ex);
                    return Observable.Empty<int>();
                })
                .Subscribe(
                    x => Debug.Log(x),
                    ex => Debug.LogError($"OnErrorResume: {ex}"),
                    result => Debug.Log($"OnCompleted: {result}"));

            subject.OnNext("123"); // 出力は OnNext(123)
            subject.OnNext("xyz"); // 出力は OnCompleted: Success
            subject.OnCompleted(); // 到達しない
        }
    }
}