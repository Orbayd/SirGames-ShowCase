using SirGames.Showcase.Events;
using SirGames.Showcase.GamePlay;
using SirGames.Showcase.Helpers;
using SirGames.Showcase.Services;
using UnityEngine;

namespace SirGames.Showcase.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Configs")]

        [SerializeField]
        private ResourceConfig _resourceConfig;

        [SerializeField]
        private GameConfig _gameConfig;

        [Header("Managers")]

        [SerializeField]
        private TimerManager _timerManager;

        [SerializeField]
        private UIManager _uiManager;

        private ResourceService _resourceService;

        private int _score;
        private int Score { get { return _score; } set { _score = value; OnScoreValueChanged(); } }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            InitServices();
            InitManagers();
            InitCamera();
            AddEvents();
        }

        private void InitServices()
        {
            _resourceService = new ResourceService(_resourceConfig);
        }

        private void InitManagers()
        {
            _resourceService.Init();
            _uiManager.Init();
            _uiManager.Navigate(ViewName.Start);
        }

        private void InitCamera()
        {
            var follow = Camera.main.GetComponent<FollowTarget>();
            follow.SetTarget(_resourceService.GetPlayer().transform);
            follow.SetOffset(_gameConfig.CameraOffset);
        }

        private void GiveReward(GameObject gameObject)
        {
            _resourceService.Release(gameObject);
            _timerManager.Register(Random.Range(1, 5), () => _resourceService.CreatePrize());

            Score += _gameConfig.ScoreGain;


            if (Score >= _gameConfig.MaxScore)
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
            Score = 0;
            _resourceService.ResetPlayer();
            _resourceService.CreatePrizes();
        }

        private void OnScoreValueChanged()
        {
            MessageBus.Publish<GiveRewardEvent>(new GiveRewardEvent(_score));
        }

        private void AddEvents()
        {
            MessageBus.Subscribe<GameStartEvent>((x) => GameStart());
            MessageBus.Subscribe<PrepareRewardEvent>((x) => GiveReward(x.Prize));
        }

        private void RemoveEvents()
        {
            MessageBus.UnSubscribe<GameStartEvent>();
            MessageBus.UnSubscribe<PrepareRewardEvent>();
        }
    }
}

