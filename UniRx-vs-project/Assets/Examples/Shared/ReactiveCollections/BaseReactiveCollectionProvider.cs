using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Examples.Shared.ReactiveCollections
{
    public class BaseReactiveCollectionProvider<T> : MonoBehaviour
    {
        [SerializeField] private List<T> defaultValues;

        protected readonly ReactiveCollection<T> _reactiveCollection = new();

        public ReactiveCollection<T> Collection => _reactiveCollection;
        public IReadOnlyReactiveCollection<T> ReadOnlyCollection => _reactiveCollection;
        
        /* Reactive collection implements interfaces */
        public IReadOnlyCollection<T> SimpleCollection => _reactiveCollection;

        private void Awake()
        {
            defaultValues.ForEach(_reactiveCollection.Add);
        }

        private void OnDestroy()
        {
            _reactiveCollection.Dispose();
        }
    }
}