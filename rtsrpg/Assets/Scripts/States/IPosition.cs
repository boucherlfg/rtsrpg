using UnityEngine;

namespace States
{
    public interface IPosition
    {
        public Vector2 Position { get; set; }
        
        public GenericEvent PositionChanged { get; }
    }
}