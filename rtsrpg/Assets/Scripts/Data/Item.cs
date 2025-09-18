using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Item", menuName = "Felix/Item")]
    public class Item : ScriptableObject
    {
        public string displayName;
        public Sprite image;
    }
}