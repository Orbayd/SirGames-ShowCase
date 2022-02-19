using System;
using UnityEngine;
using UnityEngine.UI;

namespace SirGames.Showcase.UI
{

    public class GameStartView : ViewBase<GameStartViewModel>
    {
        [SerializeField]
        private Button _btnPlay;

        protected override void OnBind(GameStartViewModel model)
        {         
            _btnPlay.onClick.AddListener(() => model.ButtonPlayAgainClicked());
        }

    }

    public class GameStartViewModel : ViewModelBase
    {
        public event Action OnButtonStartClicked;

        public void ButtonPlayAgainClicked()
        {
            OnButtonStartClicked?.Invoke();
        }
    }
}
