using UnityEngine;
using UnityEngine.AI;
using HealthSystem;

namespace PixelRTS.UnitSystem
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(NavMeshAgent), typeof(Health))]
    public class BaseUnit : GameElement, IDamageable, IHealable, ICommandable
    {
        [SerializeField]
        private NavMeshAgent _agent;
                
        [SerializeField]
        [Space]
        private Health _health;
        
        protected NavMeshAgent agent
        {
            get => _agent;
        }
        protected IHealth health
        {
            get
            {
                return _health;
            }
        } 

        public int maxHealth
        {
            get
            {
                return ((IDamageable)_health).maxHealth;
            }
        }
        public int currentHealth
        {
            get
            {
                return ((IDamageable)_health).currentHealth;
            }
        }

        protected override void Initialize()
        {
            _health.SetMaxHeath(data.maxHealth, true);
        }

        public void Stop()
        {
            _agent.ResetPath();
        }
        public void HoldPosition()
        {
            _agent.isStopped = true;
        }
        public virtual void Move(Vector3 point)
        {
            
        }
        public virtual void Patrol(Vector3 point)
        {

        }

        public virtual void Heal(int amount)
        {
            
        }
        public virtual void TakeDamage(int amount)
        {
            
        }
    }
}