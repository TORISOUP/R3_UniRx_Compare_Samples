using R3;
using UnityEngine;

namespace Samples.R3Sample
{
    public class TrampolineSample : MonoBehaviour
    {
        // ゲーム中で扱うイベント
        private enum GameEvent
        {
            PlayerJoined,
            PlayerAddedToTeam
        }

        // ゲームイベントを通知するためのSubject
        private readonly Subject<GameEvent> _subject = new();
        
        private void Start()
        {
            _subject.AddTo(this);

            // Trampoline()で再帰したときに末尾再帰になる
            // Share()はTrampoline()を通した同じストリームを全員で共有するために必要
            var observable = _subject.Trampoline().Share();

            // イベント通知をいろんな場所で購読して処理している（というイメージ）
            SubscriberA(observable);
            SubscriberB(observable);
            SubscriberC(observable);
            
            // 新しくプレイヤーが接続してきたことを通知する
            _subject.OnNext(GameEvent.PlayerJoined);
        }

        // 購読者A
        // PlayerJoinedイベントを受け取ったら、ゲームに参加させる処理を行う
        private void SubscriberA(Observable<GameEvent> observable)
        {
            observable.Subscribe(e =>
            {
                // 受け取ったイベントをログに出す
                Debug.Log($"A: {e}");

                // PlayerJoinedイベントを受け取ったら、ゲームに参加させる処理を行う
                // その後、PlayerAddedToTeamイベントを通知する(というイメージ)
                if (e == GameEvent.PlayerJoined)
                {
                    _subject.OnNext(GameEvent.PlayerAddedToTeam);
                }
                
            }).RegisterTo(destroyCancellationToken);
        }
        
        // 購読者B
        // イベント通知をログに出す
        private void SubscriberB(Observable<GameEvent> observable)
        {
            observable.Subscribe(e =>
            {
                // 受け取ったイベントをログに出す
                Debug.Log($"B: {e}");
            }).RegisterTo(destroyCancellationToken);
        }
        
        // 購読者C
        // イベント通知をログに出す
        private void SubscriberC(Observable<GameEvent> observable)
        {
            observable.Subscribe(e =>
            {
                // 受け取ったイベントをログに出す
                Debug.Log($"C: {e}");
            }).RegisterTo(destroyCancellationToken);
        }
    }
}