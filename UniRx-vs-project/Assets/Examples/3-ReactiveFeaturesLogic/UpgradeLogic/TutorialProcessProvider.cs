using UnityEngine;

namespace Examples.ReactiveFeaturesLogic.UpgradeLogic
{
    public class TutorialProcessProvider : MonoBehaviour
    {
        [SerializeField] private bool isPassed;
        public bool IsTutorialPassed => isPassed;
    }
}