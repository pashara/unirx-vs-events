using EasyButtons;

namespace Examples.Shared
{
    public class BoolReactivePropertyProvider : BaseReactiveProperty<bool>
    {
        /* Not safe. For editor only  */
        [Button]
        private void SetValue(bool value)
        {
            _property.Value = value;
        }

        [Button]
        private void SetValueForce(bool value)
        {
            _property.SetValueAndForceNotify(value);
        }
    }
}
