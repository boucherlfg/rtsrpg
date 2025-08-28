using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using States;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [CreateAssetMenu(menuName = "Felix/AgentConfig")]
    public class AgentConfig : ScriptableObject
    {
        [FormerlySerializedAs("agentStates")]
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
        
    }
}