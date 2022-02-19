using System.Collections;
using System.Collections.Generic;
using SirGames.Showcase.Services;
using SirGames.Showcase.UI;
using UnityEngine;

namespace SirGames.Showcase.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private GameObject _player;

        [SerializeField]
        private Vector2 _spawnBounds;

        [SerializeField]
        public TimerManager _timerManager;
        public PoolingService PoolingService { get; private set; }
        public static GameManager Singleton { get; private set; }

        [SerializeField]
        private ScoreBoardView ScoreBoardView;
        private ScoreBoardViewModel ScoreBoardViewModel;

        [SerializeField]
        private GameOverView GameOverView;
        private GameOverViewModel GameOverViewModel;

        [SerializeField]
        private GameStartView GameStartView;
        private GameStartViewModel GameStartViewModel ;

        private void Start()
        {
            if (Singleton is null)
            {
                Singleton = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            
            Init();
        }

        private void Init()
        {    
            InitServices();
            InitUI();
            InitCamera();
            AddEvents();
        }

        private void InitServices()
        {
            PoolingService = new PoolingService(5, _prefab);
            PoolingService.Init();
        }

        private void InitCamera()
        {
            var follow = Camera.main.GetComponent<FollowTarget>();
            follow.SetTarget(_player.transform);
            follow.SetOffset(new Vector2(10, 5));
        }

        private void InitUI()
        {
            ScoreBoardViewModel = new ScoreBoardViewModel();
            ScoreBoardView.Bind(ScoreBoardViewModel);
          
            GameOverViewModel = new GameOverViewModel(ScoreBoardViewModel);
            GameOverView.Bind(GameOverViewModel);

            GameStartViewModel = new GameStartViewModel();
            GameStartView.Bind(GameStartViewModel);
            GameStartView.Show(true);
        }

        private void Create()
        {
            var randomValue = Random.insideUnitCircle;
            var pooledObject = PoolingService.Spawn(new Vector3(randomValue.x * _spawnBounds.x, 0.5f, randomValue.y * _spawnBounds.y), Vector3.zero);
        }

        public void GiveReward(GameObject gameObject)
        {
            PoolingService.Release(gameObject);
            _timerManager.Register(Random.Range(1, 5), () => Create());
            ScoreBoardViewModel.Score += 10;

            if (ScoreBoardViewModel.Score >= 100)
            {
                GameEnd();
            }
        }

        private void GameEnd()
        {
            ScoreBoardView.Show(false);
            GameOverView.Show(true);
            GameStartView.Show(false);
        }

        private void GameStart()
        {
            ScoreBoardView.Show(true);
            GameOverView.Show(false);
            GameStartView.Show(false);

            ScoreBoardViewModel.Score = 0;

            _player.transform.position = new Vector3(0.5f, 0.9f, -7.0f);

            for (int i = 0; i < 5; i++)
            {
                Create();
            }
        }

        private void AddEvents()
        {
            GameOverViewModel.OnButtonPlayAgainClicked += GameStart;
            GameStartViewModel.OnButtonStartClicked += GameStart;
        }

        private void RemoveEvents()
        {
            GameOverViewModel.OnButtonPlayAgainClicked -= GameStart;
            GameStartViewModel.OnButtonStartClicked -= GameStart;
        }
    }
}

