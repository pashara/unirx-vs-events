using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Examples.ReactiveFeaturesLogic.UsersLogin
{
    public class UserViewModel : MonoBehaviour
    {
        [SerializeField] private Text idLabel;
        [SerializeField] private Text coinsLabel;
        [SerializeField] private Text descriptionLabel;

        public ReactiveProperty<UserReactiveModel> Model { get; } = new();
        
        private readonly CompositeDisposable _commonDisposable = new();
        
        private void Awake()
        {
            Model.Subscribe(x =>
            {
                if (x == null)
                    Unlink();
                else
                    Link(x);
            }).AddTo(this);
        }

        private void Link(UserReactiveModel userData)
        {
            _commonDisposable.Clear();

            userData.Coins.Subscribe(x =>
            {
                coinsLabel.text = $"coins: {Mathf.Round(x)}";
            }).AddTo(_commonDisposable);
            idLabel.text = $"Id: {userData.ID}";
            descriptionLabel.text = userData.Data.ToString();
        }

        private void Unlink()
        {
            _commonDisposable.Clear();
        }
    }
}