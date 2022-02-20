using SirGames.Showcase.Services;
using UnityEngine;

namespace SirGames.Showcase.Managers
{
    public class ResourceManager : MonoBehaviour
    { 
        [SerializeField]
        private Resources _resourceConfig;

        public PoolingService PoolingService { get; private set; }
        
        public void Init()
        {
            InitServices();
        }

        private void InitServices()
        {
            PoolingService = new PoolingService(_resourceConfig.PrizeCount, _resourceConfig.PrizePrefab);
            PoolingService.Init();
        }

        public void CreatePrize()
        {
            var randomValue = Random.insideUnitCircle;
            var positionBounds = _resourceConfig.SpawnPositionBounds;
            var pooledObject = PoolingService.Spawn(new Vector3(randomValue.x * positionBounds.x, 0.5f, randomValue.y * positionBounds.y), Vector3.zero);
        }

        public void CreatePrizes()
        {
             for (int i = 0; i < _resourceConfig.PrizeCount; i++)
            {
                CreatePrize();
            }
        }

        public void Release(GameObject gameObject)
        {
            PoolingService.Release(gameObject);
        }

    }
}