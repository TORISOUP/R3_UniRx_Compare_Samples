using Cysharp.Threading.Tasks;
using R3;

namespace Samples.R3Sample
{
    public static class UniTaskR3Extensions
    {
        public static Observable<T> ToR3Observable<T>(this UniTask<T> task)
        {
            return task.AsValueTask().ToObservable();
        }

        public static Observable<Unit> ToR3Observable(this UniTask task)
        {
            return task.AsValueTask().ToObservable();
        }

        public static Observable<T> ToR3Observable<T>(this UniTaskCompletionSource<T> tcs)
        {
            return tcs.Task.ToR3Observable();
        }

        public static Observable<Unit> ToR3Observable(this UniTaskCompletionSource tcs)
        {
            return tcs.Task.ToR3Observable();
        }

        public static Observable<T> ToR3Observable<T>(this AutoResetUniTaskCompletionSource<T> tcs)
        {
            return tcs.Task.ToR3Observable();
        }

        public static Observable<Unit> ToR3Observable(this AutoResetUniTaskCompletionSource tcs)
        {
            return tcs.Task.ToR3Observable();
        }
    }
}