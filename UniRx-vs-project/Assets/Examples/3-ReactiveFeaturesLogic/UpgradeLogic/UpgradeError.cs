namespace Examples.ReactiveFeaturesLogic.UpgradeLogic
{
    public abstract class BaseError
    {
        public int order;
    }
    
    public class UpgradeError : BaseError
    {
        public int order;
        public string descritption;
    }
}