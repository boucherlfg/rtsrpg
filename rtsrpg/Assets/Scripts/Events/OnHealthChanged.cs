using States;

namespace Events
{
    public class OnHealthChanged : GenericEvent<(AgentState agent, int oldHealth, int newHealth)>
    {
        
    }
}