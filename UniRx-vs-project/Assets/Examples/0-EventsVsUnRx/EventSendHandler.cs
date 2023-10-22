using System;
using EasyButtons;
using UnityEngine;

namespace Examples.EventsVsUnRx
{
    public class EventSendHandler : MonoBehaviour
    {
        [SerializeField] private string dataIdentifier;

        public event Action<string> OnSomeAction;


        public void TriggerAction()
        {
            OnSomeAction?.Invoke(dataIdentifier);
        }
        
        [Button]
        private void Trigger()
        {
            TriggerAction();
        }
    }
}
