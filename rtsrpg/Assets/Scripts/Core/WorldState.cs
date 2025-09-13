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
        private readonly List<AgentState> _agentStates = new();
        private readonly Dictionary<Type, int> _maxCounts = new();
    
        public void Initialize(PoolingConfig config)
        {
            foreach (var count in config.spawnCounts)
            {
                _maxCounts[count.prefab.agentState.GetType()] = count.maxSpawnCount;
            }
        }
    
        public void Add<T>(T agentState) where T : AgentState
        {
            if (Count(agentState.GetType()) >= _maxCounts[agentState.GetType()]) return;
            _agentStates.Add(agentState);
        }

        public void Remove<T>(T agent) where T : AgentState
        {
            _agentStates.Remove(agent);
        }

        public void Remove(int id)
        {
            _agentStates.RemoveAll(x => x.id == id);
        }
    
        public IEnumerable<T> GetAll<T>(Func<T, bool> predicate = null) 
        {
            return predicate == null ? _agentStates.OfType<T>() : _agentStates.OfType<T>().Where(predicate);
        }

        public IEnumerable<AgentState> GetStatesFor(int id)
        {
            return _agentStates.Where(x => x.id == id);
        }

        public int Count(Type type, Func<AgentState, bool> predicate = null) 
        {
            return predicate == null ? _agentStates.Count(x => x.GetType() == type) 
                : _agentStates.Count(x => x.GetType() == type && predicate(x));
        }
    }
}