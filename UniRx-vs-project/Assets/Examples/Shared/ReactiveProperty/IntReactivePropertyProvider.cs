using EasyButtons;

namespace Examples.Shared
{
    public class IntReactivePropertyProvider : BaseReactiveProperty<int>
    {
        /* Not safe. For editor only  */
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
