using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProcGen
{
    public class LifeAndDeath : MonoBehaviour
    {
        [SerializeField] private float health;
        [Tooltip("<= 0 means infinite")][SerializeField] private float reviveTime;
        private float reviveTimer;
        [SerializeField] private GameObject onDeath;
        [SerializeField] private GameObject onRevive;
        
        public float Health
        {
            get => health;
            set
            {
                health = value;
                if (health <= 0)
                {
                    Die();
                }
            }
        }

        private void Update()
        {
            if(reviveTime <= 0) return;
            
            reviveTimer += Time.deltaTime;
            if (reviveTimer >= reviveTime)
            {
                Revive();
            }
        }

        private void Die()
        {
            if (onDeath)
            {
                Instantiate(onDeath, transform.position, Quaternion.identity, transform.parent);
            }
            Destroy(gameObject);
        }

        private void Revive()
        {
            if (!onRevive) return;
            Instantiate(onRevive, transform.position, Quaternion.identity, transform.parent);
            Destroy(gameObject);
        }
       
    }
}