namespace Examples.ReactiveFeaturesLogic.UpgradeLogic.Validate
{
    public abstract class BaseValidator<T> where T : BaseError
    {
        public abstract bool Validate(out T error);
    }
}