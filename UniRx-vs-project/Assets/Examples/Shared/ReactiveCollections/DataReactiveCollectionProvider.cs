using EasyButtons;

namespace Examples.Shared.ReactiveCollections
{
    public class DataReactiveCollectionProvider : BaseReactiveCollectionProvider<DataObject>
    {
        /* Not safe. For editor only  */
        [Button]
        private void Add(DataObject value)
        {
            _reactiveCollection.Add(value);
        }

        [Button]
        private void Remove(DataObject value)
        {
            _reactiveCollection.Remove(value);
        }

        [Button]
        private void Replace(int index, DataObject value)
        {
            _reactiveCollection[index] = value;
        }
    }
}