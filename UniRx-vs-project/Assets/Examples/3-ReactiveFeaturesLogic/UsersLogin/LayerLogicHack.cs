using EasyButtons;
using Examples.Shared;
using Examples.Shared.ReactiveCollections;
using UnityEngine;

namespace Examples.ReactiveFeaturesLogic.UsersLogin
{
    public class LayerLogicHack : MonoBehaviour
    {
        [SerializeField] private DataReactiveCollectionProvider adminUsersProvider;
        [SerializeField] private DataReactiveCollectionProvider moderatorUsersProvider;
        [SerializeField] private DataReactiveCollectionProvider playerUsersProvider;
        [SerializeField] private UserLoginLayer loggedUsersProvider;
        
        
        [Button]
        private void AddAdmin(DataObject source)
        {
            adminUsersProvider.Collection.Add(new DataObject());
        }
        
        [Button]
        private void AddModerator(DataObject source)
        {
            moderatorUsersProvider.Collection.Add(new DataObject());
        }
        
        [Button]
        private void AddUser(DataObject source)
        {
            playerUsersProvider.Collection.Add(new DataObject());
        }

        [Button]
        private void GiveCoins(string key, float amount)
        {
            var model = loggedUsersProvider.GetReactiveModel(key);
            if (model != null)
            {
                model.Coins.Value += amount;
            }
        }
    }
}