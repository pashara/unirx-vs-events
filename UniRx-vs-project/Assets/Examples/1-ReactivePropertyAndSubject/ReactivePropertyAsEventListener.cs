using UnityEngine;

namespace Examples.ReactivePropertyAndSubject
{
    public class ReactivePropertyAsEventListener : MonoBehaviour
    {
        [SerializeField] private ReactivePropertyAsEventProvider eventProvider;

        private void OnEnable()
        {
            ProcessSomeLogicWithProperty(eventProvider.Value);
            eventProvider.OnValueInitialized += EventProviderOnOnValueInitialized;
            eventProvider.OnValueChanged += EventProviderOnOnValueChanged;
        }

        private void OnDisable()
        {
            eventProvider.OnValueInitialized -= EventProviderOnOnValueInitialized;
            eventProvider.OnValueChanged -= EventProviderOnOnValueChanged;
        }

        
        private void EventProviderOnOnValueInitialized(int initialValue)
        {
            Debug.Log("[Event] Value maybe initialized. This code may not called anytime");
            ProcessSomeLogicWithProperty(initialValue);
        }

        private void EventProviderOnOnValueChanged(int prevValue, int newValue)
        {
            Debug.Log($"[Event] Property value changed from {prevValue} to {newValue}. Diff: {newValue - prevValue}");
            ProcessSomeLogicWithProperty(newValue);
        }


        private void ProcessSomeLogicWithProperty(int value)
        {
            Debug.Log($"[Event] Some calculations with {value}");
        }
    }
}