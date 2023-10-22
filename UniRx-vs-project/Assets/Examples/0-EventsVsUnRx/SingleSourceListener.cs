using UniRx;
using UnityEngine;

namespace Examples.EventsVsUnRx
{
    public class SingleSourceListener : MonoBehaviour
    {
        [SerializeField] private EventSendHandler eventSendHandler;
        [SerializeField] private SubjectSendHandler subjectHandler;

        private void Awake()
        {
            eventSendHandler.OnSomeAction += EventSendHandlerOnOnSomeAction;
            subjectHandler.OnChange.Subscribe(OnSomeSubject).AddTo(this);
        }
        
        private void OnDestroy()
        {
            eventSendHandler.OnSomeAction -= EventSendHandlerOnOnSomeAction;
        }
        
        
        private void EventSendHandlerOnOnSomeAction(string identifier)
        {
            Debug.Log($"Triggered system event #{identifier}");
        }

        private void OnSomeSubject(string identifier)
        {
            Debug.Log($"Triggered UniRx subject #{identifier}");
        }
    }
}