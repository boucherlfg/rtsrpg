using Data;
using UnityEngine;

namespace Core
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private PoolingConfig pooling;
        [SerializeField] private GeneratorConfig generator;
        protected void Awake()
        {
            ServiceManager.Instance.Get<WorldState>().Initialize(pooling);
            ServiceManager.Instance.Get<Pooling>().Initialize(pooling);
            ServiceManager.Instance.Get<Generator>().Initialize(generator);
        }
    }
}