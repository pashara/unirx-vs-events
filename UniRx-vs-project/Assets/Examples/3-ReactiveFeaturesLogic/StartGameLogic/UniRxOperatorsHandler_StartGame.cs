using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Examples.EventsVsUnRx;
using Examples.ReactivePropertyAndSubject;
using Examples.Shared.ReactiveCollections;
using UniRx;
using UnityEngine;

namespace Examples.ReactiveFeaturesLogic
{
    /// Business logic: Game starts when:
    /// 1) players count > 1
    /// 2) game loader progress == 100
    /// 3) player inside state `stateToTrigger`
    
    public class UniRxOperatorsHandler_StartGame : MonoBehaviour
    {
        [SerializeField] private ReactivePropertyProvider loadingHandler;
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
            return d.userLoadingPercents == 100 &&
                   d.state.Equals(stateToTrigger) &&
                   d.playersCount > 0;
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

                await UniTask.WaitUntil(() => !isAllowed, cancellationToken: ct);

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