using EasyButtons;

namespace Examples.Shared.ReactiveCollections
{
    public class IntReactiveCollectionProvider : BaseReactiveCollectionProvider<int>
    {
        /* Not safe. For editor only  */
        [Button]
        private void Add(int value)
        {
            _reactiveCollection.Add(value);
        }

        [Button]
        private void Remove(int value)
        {
            _reactiveCollection.Remove(value);
        }

        [Button]
        private void Replace(int index, int value)
        {
            _reactiveCollection[index] = value;
        }
    }
}