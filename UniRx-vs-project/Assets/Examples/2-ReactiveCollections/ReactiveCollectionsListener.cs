using Examples.Shared;
using Examples.Shared.ReactiveCollections;
using UniRx;
using UnityEngine;

namespace Examples._2_ReactiveCollections
{
    public class ReactiveCollectionsListener : MonoBehaviour
    {
        [SerializeField] private BaseReactiveCollectionProvider<int> collectionProvider1;
        [SerializeField] private BaseReactiveCollectionProvider<string> collectionProvider2;
        [SerializeField] private BaseReactiveCollectionProvider<DataObject> collectionProvider3;
        
        private void Awake()
        {
            collectionProvider1.ReadOnlyCollection.ObserveAdd().Subscribe(OnAddValue).AddTo(this);
            collectionProvider2.ReadOnlyCollection.ObserveAdd().Subscribe(OnAddValue).AddTo(this);
            collectionProvider3.ReadOnlyCollection.ObserveAdd().Subscribe(OnAddValue).AddTo(this);


            collectionProvider1.ReadOnlyCollection
                .ObserveRemove()
                .Subscribe(x => Debug.Log($"On removed on index {x.Index} value {x.Value}"))
                .AddTo(this);

            collectionProvider1.ReadOnlyCollection
                .ObserveCountChanged()
                .Subscribe(x => Debug.Log($"On every collection size change: size is {x}"))
                .AddTo(this);

        }


        private void OnAddValue<T>(T value)
        {
            Debug.Log($"On add value {value}");
        }
    }
}