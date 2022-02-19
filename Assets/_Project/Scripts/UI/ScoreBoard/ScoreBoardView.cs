using TMPro;
using UnityEngine;

namespace SirGames.Showcase.UI
{
    public class ScoreBoardView : ViewBase<ScoreBoardViewModel>
    {
        [SerializeField]
        private TMP_Text _txtScore;

        protected override void OnBind(ScoreBoardViewModel model)
        {
            _txtScore.text = model.Score.ToString();
            model.PropertyChanged += (sender,property)=>
            {
                if(property.PropertyName.Equals(nameof(model.Score)))
                {
                    _txtScore.text = "Puan " + model.Score.ToString();
                }
            };
        }
    }
}