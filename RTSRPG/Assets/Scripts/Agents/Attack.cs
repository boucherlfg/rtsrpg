using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Agents
{
    public class Attack : MonoBehaviour
    {
        public float damage;
        public float range;
        public Agent target;
    }
}