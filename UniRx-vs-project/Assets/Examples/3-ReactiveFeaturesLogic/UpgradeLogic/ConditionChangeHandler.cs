using System;
using System.Collections.Generic;
using Examples.ReactiveFeaturesLogic.UpgradeLogic.Validate;
using UniRx;
using UnityEngine;

namespace Examples.ReactiveFeaturesLogic.UpgradeLogic
{
    public class ConditionChangeHandler : MonoBehaviour
    {
        public void SubscribeOnChange<T>(
            IEnumerable<IObservable<object>> sources, 
            IDictionary<IReactiveProperty<bool>, BaseValidator<T>> validators,
            IReactiveCollection<T> errorsCollection,
            CompositeDisposable disposable
        ) where T : BaseError
        {
            
            var errors = new List<T>();
            /* Моментальное реагирование на изменение параметров */

            Observable.Merge(sources)
                .ThrottleFrame(1)
                .Subscribe(_ =>
                {
                    errors.Clear();
                    foreach (var validator in validators)
                    {
                        validator.Key.Value = validator.Value.Validate(out var error);
                        if (!validator.Key.Value)
                        {
                            errors.Add(error);
                        }
                    }
                }).AddTo(disposable);
            
            
            /* Моментальное реагирование на изменение параметров */

            Observable.CombineLatest(validators.Keys)
                .ThrottleFrame(1)
                .Subscribe(x =>
                {
                    errors.Sort((a, b) => a.order.CompareTo(b.order));
                    errorsCollection.Clear();
                    foreach (var error in errors)
                    {
                        errorsCollection.Add(error);
                    }
                }).AddTo(disposable);
        }
    }
}