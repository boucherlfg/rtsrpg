using Data;
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
            pool.Spawn(_config.toSpawn, Vector2.zero);
        }
    }

}