using Examples.EventsVsUnRx;
using UniRx;
using UnityEngine;

namespace Examples.ReactivePropertyAndSubject
{
    public class ReactivePropertyAndSubjectSubscribe : MonoBehaviour
    {
        [SerializeField] private ReactivePropertyProvider propertyProvider;
        [SerializeField] private SubjectSendHandler subjectSendHandler;

        private readonly CompositeDisposable _disposable = new();

        private void OnEnable()
        {
            propertyProvider.PropertyRead.Subscribe(x => Debug.Log($"On property action: {x}")).AddTo(_disposable);
            subjectSendHandler.OnChange.Subscribe(x => Debug.Log($"On subject action: {x}")).AddTo(_disposable);
            
            propertyProvider.PropertyRead.Subscribe(GenericProcessor).AddTo(_disposable);
            subjectSendHandler.OnChange.Subscribe(GenericProcessor).AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Clear();   
        }

        
        private void GenericProcessor<T>(T data)
        {
            Debug.Log($"Generic processor: {data}");
        }
    }
}
