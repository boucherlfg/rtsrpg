using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Generic;
using Sirenix.OdinInspector;
using States;
using UnityEngine;

namespace Agent
{
    public sealed class AgentScript : MonoBehaviour
    {
        private static int _generator;
        private WorldState _worldState;
        [SerializeReference]
        [TypeFilter("GetFilteredTypeList")]
        public AgentState agentState;

        [SerializeField] private List<AbstractUpdater> updaters;
        public IEnumerable<Type> GetFilteredTypeList()
        {
            return typeof(AgentState).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsGenericTypeDefinition)
                .Where(x => typeof(AgentState).IsAssignableFrom(x));
        }

        public int Id { get; private set; }

        private void OnValidate()
        {
            updaters.Clear();
            foreach (var updater in GetComponents<AbstractUpdater>())
            {
                updaters.Add(updater);
            }
        }

        private void Awake()
        {
            Id = _generator;
            _generator += 1;
            _worldState = ServiceManager.Instance.Get<WorldState>();
        }

        public void OnSpawn()
        {
            agentState = agentState.Copy();
            agentState.id = Id;
            
            _worldState.Add(agentState);
            
            foreach (var component in updaters)
            {
                component.Initialize(agentState);
            }
        }

        public void OnDespawn()
        {
            foreach (var component in updaters)
            {
                component.Uninitialize();
            }
            _worldState.Remove(agentState.id);
            agentState.shouldUninitialize = true;
        }
    }
}
