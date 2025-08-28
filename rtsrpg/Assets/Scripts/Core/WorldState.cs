using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Generic;
using States;

namespace Core
{
    public class WorldState
    {
        private readonly Dictionary<Type, StaticDict<AgentState>> _agentStatesByType = new();
        private Dictionary<Type, int> _maxCounts = new();
    
        public void Initialize(PoolingConfig config)
        {
            foreach (var count in config.spawnCounts)
            {
                _maxCounts[count.prefab.agentState.GetType()] = count.maxSpawnCount;
            }
        }
    
        public void Add(int id, AgentState agentState)
        {
            var type = agentState.GetType();
            _agentStatesByType.TryAdd(type, new StaticDict<AgentState>(_maxCounts[type]));
            _agentStatesByType[type][id] = agentState;
        }

        public void Remove(int id)
        {
            foreach (var item in _agentStatesByType)
            {
                item.Value[id] = null;
            }
        }
    
        public IEnumerable<(int index, T value)> GetAll<T>() where T : AgentState
        {
            return _agentStatesByType[typeof(T)].Select(x => (x.index, x.value as T));
        }

        public Dictionary<Type, AgentState> GetStatesFor(int id)
        {
            return _agentStatesByType.Select(kvp => kvp.Value[id]).ToDictionary(agent => agent.GetType(), agent => agent);
        }
    }
}