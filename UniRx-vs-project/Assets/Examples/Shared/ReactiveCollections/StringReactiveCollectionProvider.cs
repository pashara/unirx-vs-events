using EasyButtons;

namespace Examples.Shared.ReactiveCollections
{
    public class StringReactiveCollectionProvider : BaseReactiveCollectionProvider<string>
    {
        /* Not safe. For editor only  */
        [Button]
        private void Add(string value)
        {
            _reactiveCollection.Add(value);
        }

        [Button]
        private void Remove(string value)
        {
            _reactiveCollection.Remove(value);
        }

        [Button]
        private void Replace(int index, string value)
        {
            _reactiveCollection[index] = value;
        }
    }
}