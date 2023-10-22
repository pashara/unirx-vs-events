using EasyButtons;
using UniRx;
using UnityEngine;

namespace Examples.ReactivePropertyAndSubject
{
    public class ReactivePropertyProvider : MonoBehaviour
    {
        [SerializeField] private int defaultValue;

        private readonly ReactiveProperty<int> _property = new();

        public IReactiveProperty<int> Property => _property;
        public IReadOnlyReactiveProperty<int> PropertyRead => _property;

        private void Awake()
        {
            _property.Value = defaultValue;
        }

        private void OnDestroy()
        {
            _property.Dispose();
        }
        
        [Button]
        private void SetValue(int value)
        {
            _property.Value = value;
        }

        [Button]
        private void SetValueForce(int value)
        {
            _property.SetValueAndForceNotify(value);
        }
    }
}
