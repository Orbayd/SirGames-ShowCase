using UnityEngine;

namespace SirGames.Showcase.UI
{
    public abstract class ViewBase<TModel> : MonoBehaviour
    {
        TModel DataContex;

        public void Show(bool value)
        {
            this.gameObject.SetActive(value);
            OnShow(value);
        }

        protected virtual void OnShow(bool value) { }

        public void Bind(TModel model)
        {
            DataContex = model;
            OnBind(model);
        }

        protected virtual void OnBind(TModel model) { }
    }
}