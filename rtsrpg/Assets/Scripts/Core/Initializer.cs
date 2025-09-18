using Data;
using Generic;
using UnityEngine;

namespace Core
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private PoolingConfig pooling;
        [SerializeField] private GeneratorConfig generator;
        [SerializeField] private VisualConfig visual;
        protected void Awake()
        {
            ServiceManager.Instance.Get<WorldState>().Initialize(pooling);
            ServiceManager.Instance.Get<Pooling>().Initialize(pooling);
            ServiceManager.Instance.Get<Generator>().Initialize(generator);
            ServiceManager.Instance.Get<Visuals>().Initialize(visual);
        }

        protected void OnDestroy()
        {
            ServiceManager.Instance.Get<Visuals>().Uninitialize();
        }
    }
}