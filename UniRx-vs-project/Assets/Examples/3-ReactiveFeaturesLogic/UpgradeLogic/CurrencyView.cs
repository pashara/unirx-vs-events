using Examples.Shared;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Examples.ReactiveFeaturesLogic.UpgradeLogic
{
    public class CurrencyView : MonoBehaviour
    {
        [SerializeField] private IntReactivePropertyProvider provider;
        [SerializeField] private Text label;

        private void Awake()
        {
            provider.PropertyRead.Subscribe(x => label.text = $"{Mathf.Round(x)}").AddTo(this);
        }
    }
}