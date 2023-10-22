using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Examples.EventsVsUnRx;
using Examples.Shared;
using Examples.Shared.ReactiveCollections;
using UniRx;
using UnityEngine;

namespace Examples.ReactiveFeaturesLogic
{
    public class UniRxStartGameExample : MonoBehaviour
    {
        [SerializeField] private IntReactivePropertyProvider loadingHandler;
        [SerializeField] private SubjectSendHandler stateHandler;
        [SerializeField] private IntReactiveCollectionProvider joinedPlayers;
        
        [SerializeField] private string stateToTrigger;

        private readonly CancellationTokenSource _cts = new();
        

        private void Awake()
        {
            HandleBusinessLogic(_cts.Token);
        }

        private void OnDestroy()
        {
            _cts.Cancel();
        }

        private bool IsAllowedToStartGame((int userLoadingPercents, string state, int playersCount) d)
        {
            var result = d.userLoadingPercents == 100 &&
                         d.state.Equals(stateToTrigger) &&
                         d.playersCount > 0;
            
            Debug.Log($"==Check game state:==\n " +
                      $"userLoadingPercents: {d.userLoadingPercents}\n" +
                      $"state: {d.state}\n" +
                      $"playersCount: {d.playersCount}");
            return result;
        }

        private async void HandleBusinessLogic(CancellationToken ct)
        {
            CompositeDisposable allInsideDisposables = new();

            try
            {
                var isAllowed = false;
                var disposable = loadingHandler.Property.CombineLatest(
                        stateHandler.OnChange,
                        joinedPlayers.ReadOnlyCollection.ObserveCountChanged(),
                        (propertyValue, subjectValue, collectionSize) =>
                            IsAllowedToStartGame((propertyValue, subjectValue, collectionSize)))
                    .Where(x => x)
                    .Take(1)
                    .Subscribe(x => { isAllowed = x; }).AddTo(allInsideDisposables);

                await UniTask.WaitWhile(() => !isAllowed, cancellationToken: ct);

                Debug.Log("Load game scene!");
                disposable.Dispose();
            }
            catch (OperationCanceledException canceledException)
            {
                // Handle cancel logic
                Debug.LogError($"Stop waiting game start");
            }
            catch (Exception e)
            {
                Debug.LogError($"Some exception: {e.Message}");
            }
            finally
            {
                allInsideDisposables.Clear();
            }
        }
        
        
    }
}