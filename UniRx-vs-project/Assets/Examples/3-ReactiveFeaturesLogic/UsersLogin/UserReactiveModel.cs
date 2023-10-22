using System;
using Examples.Shared;
using UniRx;

namespace Examples.ReactiveFeaturesLogic.UsersLogin
{
    [Serializable]
    public class UserReactiveModel
    {
        public int ID { get; }
        public DataObject Data { get; }
        public ReactiveProperty<float> Coins { get; } = new();

        public UserReactiveModel(int id, DataObject data)
        {
            ID = id;
            Data = data;
            Coins.Value = 5;
        }
    }
}