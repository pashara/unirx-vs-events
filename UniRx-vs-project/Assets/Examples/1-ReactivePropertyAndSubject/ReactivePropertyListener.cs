using Examples.Shared;
using UniRx;
using UnityEngine;

namespace Examples.ReactivePropertyAndSubject
{
    public class ReactivePropertyListener : MonoBehaviour
    {
        [SerializeField] private IntReactivePropertyProvider propertyProvider;

        private readonly CompositeDisposable _disposable = new();


        private void OnEnable()
        {
            _disposable.Clear();
            
            // Listen property on subscribe and every change: initialize and change actions
            propertyProvider.PropertyRead
                .Subscribe(x =>
                {
                    Debug.Log($"[UniRx] Property on subscribe and change: {x}");
                }).AddTo(_disposable);

            // Listen property on every change action
            propertyProvider.PropertyRead
                .SkipLatestValueOnSubscribe() // Shugar for .Skip(1) + hasValue check
                .Subscribe(x =>
                {
                    Debug.Log($"[UniRx] Property value changed to {x}");
                })
                .AddTo(_disposable);

            // Listen property on every change action
            propertyProvider.PropertyRead
                .SkipLatestValueOnSubscribe()
                .Pairwise()
                .Subscribe(x =>
                {
                    Debug.Log($"[UniRx] Property value changed from {x.Previous} to {x.Current}. Diff: {x.Current - x.Previous}");
                })
                .AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Clear();
        }
    }
}