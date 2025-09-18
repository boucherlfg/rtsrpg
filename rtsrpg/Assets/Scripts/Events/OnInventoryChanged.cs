using Data;
using States;

namespace Events
{
    public class OnInventoryChanged : GenericEvent<(IInventory sourceInventory, IInventory targetInventory, Item item)>
    {
        
    }
}