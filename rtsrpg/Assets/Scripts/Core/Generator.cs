using Data;
using Generic;
using UnityEngine;

namespace Core
{
    public class Generator
    {
        private GeneratorConfig _config;
        public void Initialize(GeneratorConfig config)
        {
            _config = config;
            var pool = ServiceManager.Instance.Get<Pooling>();
            pool.Spawn(_config.player, Vector2.zero);
            for (int i = 0; i < 100; i++)
            {
                Vector2 pos = Random.insideUnitCircle * 10;
                while(pos.magnitude < 1f) pos = Random.insideUnitCircle * 50;
                pool.Spawn(_config.test, pos);
            }
            
        }
    }
}
