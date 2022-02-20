using SirGames.Showcase.Events;
using SirGames.Showcase.Helpers;

namespace SirGames.Showcase.UI
{
    public class ScoreBoardViewModel : ViewModelBase
    {
        public int _score;
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                NotifyPropertyChanged();
            }
        }
        private void OnGiveRewards(int value)
        {
            Score = value;
        }

        public override void OnBind()
        {
            AddEvents();
        }

        public override void OnUnBind()
        {
            RemoveEvents();
        }

        public void AddEvents()
        {
            MessageBus.Subscribe<GiveRewardEvent>((x)=> OnGiveRewards(x.Value));
        }


        public void RemoveEvents()
        {
           MessageBus.UnSubscribe<GiveRewardEvent>();
        }
    }
}