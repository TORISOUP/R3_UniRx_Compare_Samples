using R3;
using UnityEngine;

namespace Samples.R3Sample
{
    public static class ObservableDebugExt
    {
        public static Observable<T> Debug<T>(this Observable<T> source, string label = null)
        {
#if DEBUG
            var l = (label == null) ? "" : $"[{label}] ";
            return source.Materialize()
                .Do(
                    onNext: x => UnityEngine.Debug.Log(l + x),
                    onDispose: () => UnityEngine.Debug.Log($"{l}OnDispose"),
                    onSubscribe: () => UnityEngine.Debug.Log($"{l}OnSubscribe")
                )
                .Dematerialize();

#else
            return source;
#endif
        }

        public static Observable<T> Debug<T>(this Observable<T> source, ILogger logger)
        {
#if DEBUG
            return source.Materialize()
                .Do(
                    onNext: x => logger.Log(x.ToString()),
                    onDispose: () => logger.Log("OnDispose"),
                    onSubscribe: () => logger.Log("OnSubscribe")
                )
                .Dematerialize();

#else
            return source;
#endif
        }
    }
}