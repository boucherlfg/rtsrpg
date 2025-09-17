using System;
using System.Collections.Generic;
using Agent;
using Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    public class Pooling
    {
        private readonly List<AgentScript> _agents = new();
        public void Initialize(PoolingConfig config)
        {
            foreach(var count in config.spawnCounts) 
            {
                for (var i = 0; i < count.maxSpawnCount; i++)
                {
                    var agent = Object.Instantiate(count.prefab);
                    agent.gameObject.SetActive(false);
                    _agents.Add(agent);
                }
            }
        }

        public void Spawn(AgentScript script, Vector2 position)
        {
            var agent = _agents.Find(x => 
                x.agentState.GetType() == script.agentState.GetType() 
                && !x.gameObject.activeSelf);
            
            agent.gameObject.SetActive(true);
            agent.transform.position = position;
            agent.OnSpawn();
        }
        public void Despawn(int agentId)
        {
            var agent = _agents.Find(x => x.Id == agentId);
            agent.gameObject.SetActive(false);
            agent.OnDespawn();
        }
        public void Despawn(AgentScript agent)
        {
            agent.gameObject.SetActive(false);
            agent.OnDespawn();
        }
    }
}
