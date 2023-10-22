using UniRx;
using UnityEngine;

namespace Examples.Shared
{
    public class BaseReactiveProperty<T> : MonoBehaviour
    {
        [SerializeField] private T defaultValue;

        protected readonly ReactiveProperty<T> _property = new();

        public IReactiveProperty<T> Property => _property;
        public IReadOnlyReactiveProperty<T> PropertyRead => _property;

        private void Awake()
        {
            _property.Value = defaultValue;
        }

        private void OnDestroy()
        {
            _property.Dispose();
        }
        
    }
}