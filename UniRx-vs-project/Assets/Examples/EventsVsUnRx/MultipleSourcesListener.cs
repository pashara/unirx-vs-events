using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Examples.EventsVsUnRx
{
    public class MultipleSourcesListener : MonoBehaviour
    {
        [SerializeField] private List<EventSendHandler> eventsSendHandlers;
        [SerializeField] private EventSendHandler eventSendHandler1;
        [SerializeField] private EventSendHandler eventSendHandler2;
        [SerializeField] private EventSendHandler eventSendHandler3;
        [SerializeField] private List<SubjectSendHandler> subjectHandlers;
        [SerializeField] private SubjectSendHandler subjectHandler1;
        [SerializeField] private SubjectSendHandler subjectHandler2;
        [SerializeField] private SubjectSendHandler subjectHandler3;

        private readonly CompositeDisposable _disposable = new();
        
        private void OnEnable()
        {
            /* System event subscribe logic */
            
            foreach (var eventHandler in eventsSendHandlers)
            {
                eventHandler.OnSomeAction += EventSendHandlerOnOnSomeAction;
            }

            eventSendHandler1.OnSomeAction += EventSendHandlerOnOnSomeAction;
            eventSendHandler2.OnSomeAction += EventSendHandlerOnOnSomeAction;
            eventSendHandler3.OnSomeAction += EventSendHandlerOnOnSomeAction;
            
            /* System event subscribe logic */

            
            /* UniRx subscribe logic */
            foreach (var eventHandler in subjectHandlers)
            {
                eventHandler.OnChange.Subscribe(OnSomeSubject).AddTo(_disposable);
            }
            subjectHandler1.OnChange.Subscribe(OnSomeSubject).AddTo(_disposable);
            subjectHandler2.OnChange.Subscribe(OnSomeSubject).AddTo(_disposable);
            subjectHandler3.OnChange.Subscribe(OnSomeSubject).AddTo(_disposable);
            /* UniRx subscribe logic */
        }

        private void OnDisable()
        {
            /* System event unsubscribe logic */
            
            foreach (var eventsSendHandler in eventsSendHandlers)
            {
                eventsSendHandler.OnSomeAction -= EventSendHandlerOnOnSomeAction;
            }

            eventSendHandler1.OnSomeAction += EventSendHandlerOnOnSomeAction;
            eventSendHandler1.OnSomeAction += EventSendHandlerOnOnSomeAction;
            eventSendHandler3.OnSomeAction += EventSendHandlerOnOnSomeAction;
            
            /* System event unsubscribe logic */
            
            
            
            /* UniRx unsubscribe logic */
            _disposable.Clear();
            /* UniRx unsubscribe logic */
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