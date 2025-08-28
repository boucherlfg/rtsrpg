using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Data;
using Sirenix.OdinInspector;
using States;
using UnityEngine;

namespace Agent
{
    public sealed class AgentScript : MonoBehaviour
    {
        private static int _generator;

        public AgentAnimator agentAnimator;
        private WorldState _worldState;
        [SerializeReference]
        [TypeFilter("GetFilteredTypeList")]
        public AgentState agentState;
        
        public IEnumerable<Type> GetFilteredTypeList()
        {
            return typeof(AgentState).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsGenericTypeDefinition)
                .Where(x => typeof(AgentState).IsAssignableFrom(x));
        }
        
        public int Id { get; private set; }

        private void Awake()
        {
            Id = _generator;
            _generator += 1;
            _worldState = ServiceManager.Instance.Get<WorldState>();
        }

        public void OnSpawn()
        {
            if (agentState is IPosition position)
            {
                position.Position = transform.position;
                position.PositionChanged.Subscribe(HandlePositionChanged);
            }
            
            _worldState.Add(Id, agentState);
            agentAnimator.Initialize(agentState);
        }

        private void HandlePositionChanged()
        {
            if (agentState is IPosition position) transform.position = position.Position;
        }

        public void OnDespawn()
        {
            agentAnimator.Uninitialize(agentState);
            if(agentState is IPosition position) position.PositionChanged.Unsubscribe(HandlePositionChanged);
            _worldState.Remove(Id);
        }
    }
}
