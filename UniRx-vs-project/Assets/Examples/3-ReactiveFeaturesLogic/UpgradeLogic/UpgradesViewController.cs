using System;
using System.Collections.Generic;
using System.Linq;
using Examples.ReactiveFeaturesLogic.UpgradeLogic.Validate;
using Examples.Shared;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Examples.ReactiveFeaturesLogic.UpgradeLogic
{
    public class UpgradesViewController : MonoBehaviour
    {
        [Header("conditions")]
        [SerializeField] private int neededCurrency1Count;
        [SerializeField] private int neededCurrency2Count;

        [Header("injection")]
        [SerializeField] private IntReactivePropertyProvider currency1Provider;
        [SerializeField] private IntReactivePropertyProvider currency2Provider;
        [SerializeField] private TutorialProcessProvider tutorialProcessProvider;

        [Header("UI")] 
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Text errorLabel;
        [SerializeField] private ConditionChangeHandler updateHandler;
        
        private readonly ReactiveProperty<bool> _isAllowedToUpgrade = new();
        private readonly HashSet<IDisposable> _runtimeDisposables = new();
        private readonly CompositeDisposable _disposable = new();

        private void OnEnable()
        {
            _disposable.Clear();
            
            CalculateAvailableToUpgrade(_disposable);
            ApplyVisual(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Clear();
        }

        private void OnDestroy()
        {
            foreach (var runtimeDisposable in _runtimeDisposables)
            {
                runtimeDisposable.Dispose();
            }
        }
        
        // Describe validators
        private IEnumerable<BaseValidator<UpgradeError>> GetAllowUpgradeValidators()
        {
            var validators = new List<BaseValidator<UpgradeError>>();
            validators.Add(new TutorialValidator(tutorialProcessProvider));
            validators.Add(new CurrencyValidator(currency1Provider.PropertyRead, () => neededCurrency1Count, "Not enough currency 1"));
            validators.Add(new CurrencyValidator(currency2Provider.PropertyRead, () => neededCurrency2Count, "Not enough currency 2"));

            return validators;
        }

        //Describe triggers, to start validation
        private IEnumerable<IObservable<object>> GetTriggers()
        {
            var sources = new List<IObservable<object>>();
            sources.Add(Cast(Observable.EveryUpdate()
                .DistinctUntilChanged()
                .Select(x => tutorialProcessProvider.IsTutorialPassed)
                .DistinctUntilChanged().ToReactiveProperty()));
            
            sources.Add(Cast(currency1Provider.PropertyRead));
            sources.Add(Cast(currency2Provider.PropertyRead));

            return sources;
        }

        private void ApplyVisual(CompositeDisposable disposable)
        {
            _isAllowedToUpgrade.Subscribe(x => upgradeButton.interactable = x).AddTo(disposable);

            upgradeButton.OnClickAsObservable().Subscribe(x =>
            {
                ProcessUpgrade();
            }).AddTo(disposable);
        }

        private void ProcessUpgrade()
        {
            Debug.Log("Process upgrade!");
            
            currency1Provider.Property.Value -= neededCurrency1Count;
            currency2Provider.Property.Value -= neededCurrency2Count;
        }

        private void CalculateAvailableToUpgrade(CompositeDisposable disposable)
        {
            /* Prepare data: original reactive property, validator */
           
            var validators = new Dictionary<IReactiveProperty<bool>, BaseValidator<UpgradeError>>();
            foreach (var validator in GetAllowUpgradeValidators())
            {
                PutValidator(validators, validator);
            }

            ReactiveCollection<UpgradeError> errors = new();
            var sources = GetTriggers();
            updateHandler.SubscribeOnChange(sources, validators, errors, disposable);
            
            errors.ObserveCountChanged().ThrottleFrame(1).Subscribe(count =>
            {
                _isAllowedToUpgrade.Value = count == 0;
                errorLabel.text = string.Join("\n", errors.Select(x => x.descritption));
            }).AddTo(disposable);
        }


        private void PutValidator<T>(
            IDictionary<IReactiveProperty<bool>, BaseValidator<T>> validators, 
            BaseValidator<T> validator) 
            where T : BaseError
        {
            var property = new ReactiveProperty<bool>(false);
            _runtimeDisposables.Add(property);
            
            validators.Add(property, validator);
        }
        
        private static IObservable<object> Cast<T>(IReadOnlyReactiveProperty<T> property)
        {
            return property.Select(x => x as object);
        }
    }
}
