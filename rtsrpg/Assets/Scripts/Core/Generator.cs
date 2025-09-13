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
            pool.Spawn(_config.player, Vector2.zero);
            pool.Spawn(_config.test, new Vector2(2.5f, 2.5f));
        }
    }
}
