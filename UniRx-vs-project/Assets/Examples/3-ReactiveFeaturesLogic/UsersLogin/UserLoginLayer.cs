using System.Collections.Generic;
using System.Linq;
using Examples.Shared;
using Examples.Shared.ReactiveCollections;
using UniRx;
using UnityEngine;

namespace Examples.ReactiveFeaturesLogic.UsersLogin
{
    public class UserLoginLayer : MonoBehaviour
    {
        [SerializeField] private DataReactiveCollectionProvider adminUsersProvider;
        [SerializeField] private DataReactiveCollectionProvider moderatorUsersProvider;
        [SerializeField] private DataReactiveCollectionProvider playerUsersProvider;

        private readonly ReactiveDictionary<DataObject, UserReactiveModel> _loginUsers = new();
        public IReadOnlyReactiveDictionary<DataObject, UserReactiveModel> LoginUsers => _loginUsers;

        private void Awake()
        {
            //You can use Cancellation token, CancellationDisposable on CompositeDisposable here. I'll use OnDestoryAsObservable)))
            HandleLogin();
        }
        
        

        public UserReactiveModel GetReactiveModel(string key)
        {
            var handler = _loginUsers.FirstOrDefault(x => x.Key.textString.Equals(key));

            if (handler.Value != null)
                return handler.Value;

            return null;
        }
        

        private void HandleLogin()
        {
            
            // Many sources with same objects
            var collections = new List<IReadOnlyReactiveCollection<DataObject>>()
            {
                adminUsersProvider.ReadOnlyCollection,
                moderatorUsersProvider.ReadOnlyCollection,
                playerUsersProvider.ReadOnlyCollection
            };

            foreach (var collection in collections)
            {
                ProcessLogin(collection);
            }

            var addHandler = Observable.Empty<DataObject>();
            var removeHandler = Observable.Empty<DataObject>();

            foreach (var collection in collections)
            {
                addHandler = addHandler.Merge(collection.ObserveAdd().Select(x => x.Value));
                removeHandler = removeHandler.Merge(collection.ObserveRemove().Select(x => x.Value));
            }

            addHandler.Subscribe(ProcessLogin).AddTo(this);
            removeHandler.Subscribe(ProcessLogout).AddTo(this);
        }
        
        
        private void ProcessLogin(IEnumerable<DataObject> dataObjects)
        {
            foreach (var dataObject in dataObjects)
            {
                ProcessLogin(dataObject);
            }
        }

        private void ProcessLogin(DataObject dataObject)
        {
            Debug.Log($"User {dataObject} successfully login inside game!");
            
            // Load data from DB, for example
            var metaData = new UserReactiveModel(dataObject.numberInt, dataObject)
            {
                Coins = { Value = Random.Range(0, 500) },
            };
            
            _loginUsers.Add(dataObject, metaData);
        }

        private void ProcessLogout(DataObject dataObject)
        {
            Debug.Log($"User {dataObject} successfully login inside game!");
            
            _loginUsers.Remove(dataObject);
        }
    }
}