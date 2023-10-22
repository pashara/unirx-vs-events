namespace Examples.ReactiveFeaturesLogic.UpgradeLogic.Validate
{
    public class TutorialValidator : BaseValidator<UpgradeError>
    {
        private readonly TutorialProcessProvider _tutorialProcessProvider;

        public TutorialValidator(TutorialProcessProvider tutorialProcessProvider)
        {
            _tutorialProcessProvider = tutorialProcessProvider;
        }

        public override bool Validate(out UpgradeError error)
        {
            error = null;
            if (_tutorialProcessProvider.IsTutorialPassed)
            {
                return true;
            }

            error = new UpgradeError()
            {
                descritption = "Tutorial in process",
                order = -1
            };
            return false;
        }
    }
}