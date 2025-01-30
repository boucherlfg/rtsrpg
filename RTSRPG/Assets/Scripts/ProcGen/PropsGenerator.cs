using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Random = UnityEngine.Random;

namespace ProcGen
{
    public class PropsGenerator : MonoBehaviour
    {
        public Props[] props;
        private Agent[] _propsAgents;
        private Dictionary<int, float> _propsTimer = new();
        public List<Agent> Generate(Dictionary<TerrainType, List<TerrainScript>> map, GameObject parent)
        {
            List<Agent> agents = new();
            // var allProps = new List<GameObject>();
            foreach (var prop in props)
            {
                var thoseProps = new List<GameObject>();
                var whereTo = new List<TerrainScript>();
            
                // find all positions where this prop can spawn
                foreach (var type in map.Keys.Where(type => prop.whereItGrows.HasFlag(type)))
                {
                    whereTo.AddRange(map[type]);
                }
            
                // create a couple of first positions
                for (int i = 0; i < prop.epicenters; i++)
                {
                    var terrain = whereTo.GetRandom();
                    whereTo.Remove(terrain);
                
                    var instance = Instantiate(prop.prefab, terrain.transform.position, Quaternion.identity, parent.transform);
                    thoseProps.Add(instance);
                }

                // randomized breadth first search
                for (var i = 0; i < 10000; i++)
                {
                    if (thoseProps.Count >= prop.quantity) break;
                
                    var center = thoseProps.GetRandom();
                    var direction = (Vector3)Random.insideUnitCircle.normalized;
                    var distance = Random.Range(prop.minDistance, prop.maxDistance);
                    var potentialPosition = center.transform.position + direction * distance;
                
                    if (!whereTo.Any(x => Vector2.Distance(potentialPosition, x.transform.position) < 0.5f)) continue;
                    if (thoseProps.Any(x => Vector2.Distance(potentialPosition, x.transform.position) < prop.minDistance)) continue;

                    var instance = Instantiate(prop.prefab, potentialPosition, Quaternion.identity, parent.transform);
                    agents.Add(instance.GetComponent<Agent>());                    
                    // allProps.Add(instance);
                    thoseProps.Add(instance);
                    i = 0; // reset panic iterator
                }
            }
            return agents;
        }

        private void Start()
        {
            _propsAgents = new Agent[props.Length];
            for (var i = 0; i < _propsAgents.Length; i++)
            {
                _propsAgents[i] = props[i].prefab.GetComponent<Agent>();
            }
        }

        public void Regenerate(float deltaTime, List<Agent> existingAgents)
        {
            var agents = MapGenerator.Instance.agents;
            var terrain = MapGenerator.Instance.terrains;
           
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                var agent = _propsAgents[i];
                var possibleSpawns = terrain.FindAll(x => (prop.whereItGrows).HasFlag(x.category));
                _propsTimer.TryAdd(i, 0);
                var count = agents.Count(x => agent.GetType().Name == x.GetType().Name);
                
                if (count >= prop.quantity) continue;
                
                _propsTimer[i] += deltaTime;
                if (_propsTimer[i] < prop.respawnRate) continue;
                
                var position = possibleSpawns.GetRandom().transform.position 
                               + (Vector3)(Random.insideUnitCircle - Vector2.one/2);
                var instance = Instantiate(prop.prefab, position, Quaternion.identity, transform);
                existingAgents.Add(instance.GetComponent<Agent>());
                _propsTimer[i] = 0;
            }
        }
    }
}