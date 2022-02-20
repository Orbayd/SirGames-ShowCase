using SirGames.Showcase.Events;
using SirGames.Showcase.Helpers;
using UnityEngine;

namespace SirGames.Showcase.Managers
{
    public class GameManager : MonoBehaviour
    {
       
        [SerializeField]
        private GameObject _player;

        [Header("Managers")]
        [SerializeField]
        private TimerManager _timerManager;

        [SerializeField]
        private UIManager _uiManager;

        [SerializeField]
        private ResourceManager _resourceManager;

        private int _score;

        private void Start()
        {
            Init();
        }

        private void Init()
        {    
            InitManagers();
            InitCamera();
            AddEvents();
        }

        public void InitManagers()
        {
            _resourceManager.Init();
            _uiManager.Init();
            _uiManager.Navigate(ViewName.Start);
        }

        private void InitCamera()
        {
            var follow = Camera.main.GetComponent<FollowTarget>();
            follow.SetTarget(_player.transform);
            follow.SetOffset(new Vector2(10, 5));
        }
 
        private void GiveReward(GameObject gameObject)
        {
            _resourceManager.Release(gameObject);
            _timerManager.Register(Random.Range(1, 5), () => _resourceManager.CreatePrize());
            _score += 10;
            MessageBus.Publish<GiveRewardEvent>(new GiveRewardEvent(_score));
            
            if (_score >= 100)
            {
                GameEnd();
            }
        }

        private void GameEnd()
        {
            _uiManager.Navigate(ViewName.GameOver);
        }

        private void GameStart()
        {
            _uiManager.Navigate(ViewName.ScoreBoard);      
            _score = 0;
            _player.transform.position = new Vector3(0.5f, 0.9f, -7.0f);
            _resourceManager.CreatePrizes();
        }

        private void AddEvents()
        {
            MessageBus.Subscribe<GameStartEvent>((x)=> GameStart());
            MessageBus.Subscribe<PrepareRewardEvent>((x)=> GiveReward(x.Prize));
        }

        private void RemoveEvents()
        {
            MessageBus.UnSubscribe<GameStartEvent>();
            MessageBus.UnSubscribe<PrepareRewardEvent>();
        }
    }
}

