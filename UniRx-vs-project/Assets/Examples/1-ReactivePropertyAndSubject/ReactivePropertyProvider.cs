using UniRx;
using UnityEngine;

namespace Examples.ReactivePropertyAndSubject
{
    public class ReactivePropertyProvider : MonoBehaviour
    {
        [SerializeField] private int defaultValue;

        private readonly ReactiveProperty<int> _property = new();

        public IReadOnlyReactiveProperty<int> PropertyRead => _property;

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