using System;
using EasyButtons;
using UniRx;
using UnityEngine;

namespace Examples.EventsVsUnRx
{
    public class SubjectSendHandler : MonoBehaviour
    {
        [SerializeField] private string dataIdentifier;

        private readonly Subject<string> _onSomeChange = new();

        //IMHO - not good practice
        public Subject<string> OnSomeAction => _onSomeChange;

        /* Separate change and subscribe logic */

        public IObservable<string> OnChange => _onSomeChange;
        public IObserver<string> ProcessChange => _onSomeChange;

        /* Separate change and subscribe logic */


        [Button]
        private void Trigger()
        {
            ProcessChange.OnNext(dataIdentifier);
        }
    }
}
