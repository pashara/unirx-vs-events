using System;
using UniRx;

namespace Examples.ReactiveFeaturesLogic.UpgradeLogic.Validate
{
    public class CurrencyValidator : BaseValidator<UpgradeError>
    {
        private readonly IReadOnlyReactiveProperty<int> _countProperty;
        private readonly Func<int> _neededValue;
        private readonly string _description;
        private readonly TutorialProcessProvider _tutorialProcessProvider;

        public CurrencyValidator(IReadOnlyReactiveProperty<int> countProperty, Func<int> neededValue, string description)
        {
            _countProperty = countProperty;
            _neededValue = neededValue;
            _description = description;
        }

        public override bool Validate(out UpgradeError error)
        {
            error = null;
            if (_neededValue == null)
            {
                return true;
            }

            if (_neededValue.Invoke() <= _countProperty.Value)
            {
                return true;
            }
            
            error = new UpgradeError()
            {
                descritption = _description,
                order = -1
            };
            return false;
        }
    }
}