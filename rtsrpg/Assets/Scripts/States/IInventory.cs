using System.Collections.Generic;
using Data;

namespace States
{
    public interface IInventory
    {
        public List<Item> Inventory { get; }
        public List<Item> StartInventory { get; }
    }
}