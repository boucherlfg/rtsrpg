using States;
using UnityEngine;

namespace Events
{
    public class OnPositionChanged : GenericEvent<(AgentState agent, Vector2 newPosition)>
    {
        
    }
}