using System.Collections.Generic;
using Examples.Shared;
using Examples.Shared.ReactiveCollections;
using UniRx;
using UnityEngine;

namespace Examples.ReactiveFeaturesLogic.UsersLogin
{
    public class UserViewLayer : MonoBehaviour
    {
        [SerializeField] private Transform root;
        [SerializeField] private GameObject userViewModel;
        [SerializeField] private UserLoginLayer userLoginLayer;
        
        private readonly ReactiveDictionary<UserReactiveModel, UserViewModel> _spawnedControllers = new();


        private void Awake()
        {
            //You can use Cancellation token, CancellationDisposable on CompositeDisposable here. I'll use OnDestoryAsObservable)))
            HandleVisualize();
        }

        
        private void HandleVisualize()
        {
            foreach (var spawnedPlayer in userLoginLayer.LoginUsers)
            {
                OnSpawn(spawnedPlayer.Value);
            }

            userLoginLayer.LoginUsers.ObserveAdd().Subscribe(x => OnSpawn(x.Value)).AddTo(this);
            userLoginLayer.LoginUsers.ObserveRemove().Subscribe(x => OnDespawn(x.Value)).AddTo(this);
        }

        private void OnSpawn(UserReactiveModel reactiveModel)
        {
            var instance = Instantiate(userViewModel, root);
            var handler = instance.GetComponent<UserViewModel>();
            handler.Model.Value = reactiveModel;
            _spawnedControllers.Add(reactiveModel, handler);
        }

        private void OnDespawn(UserReactiveModel userReactiveModel)
        {
            _spawnedControllers[userReactiveModel].Model.Value = null;
            _spawnedControllers.Remove(userReactiveModel);
        }
    }
}