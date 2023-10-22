using System;
using EasyButtons;
using UnityEngine;

namespace Examples.ReactivePropertyAndSubject
{
    public class ReactivePropertyAsEventProvider : MonoBehaviour
    {
        public event Action<int> OnValueInitialized;
        public event Action<int, int> OnValueChanged;
        
        [SerializeField] private int defaultValue;

        private int? _value;

        public int Value
        {
            get => _value ?? defaultValue;
            set
            {
                if (_value == null)
                {
                    ApplyValue(value);
                    OnValueInitialized?.Invoke(value);   
                    OnValueChanged?.Invoke(defaultValue, value);   
                    return;
                }
                if (_value != value)
                {
                    var prevValue = _value.Value;
                    ApplyValue(value);
                    OnValueChanged?.Invoke(prevValue, value);
                }
            }
        }
        
        private void Awake()
        {
            Value = defaultValue;
        }

        private void OnDestroy()
        {
            OnValueInitialized = null;
            OnValueChanged = null;
        }

        public void ForceApply(int value)
        {
            var prevValue = Value;
            ApplyValue(value);
            OnValueChanged?.Invoke(prevValue, value);
        }
        
        private void ApplyValue(int value) => _value = value;

        [Button]
        private void SetValue(int value) => Value = value;

        [Button("Force Apply")]
        private void SetValueForce(int value) => ForceApply(value);
    }
}