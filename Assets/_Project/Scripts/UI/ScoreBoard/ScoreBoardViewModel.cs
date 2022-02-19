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
    }
}