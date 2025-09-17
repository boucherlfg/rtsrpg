using States;

namespace Events
{
    public class OnInteracted : GenericEvent<(AgentState source, AgentState target)>
    {
        
    }
}