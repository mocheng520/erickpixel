using System;
using UnityEngine;

namespace HealthSystem
{
    public class Health : MonoBehaviour, IDamageable, IHealable
    {
        [SerializeField] 
        private int maxHealthPoints;
        
        private int currentHealthPoints;
        
        private HealthBar healthBar;

        public event Action OnDeath;

        public int maxHealth => maxHealthPoints;
        public int currentHealth => currentHealthPoints;

        private void Start()
        {
            if(TryGetComponent(out HealthBar healthBar))
            {
                this.healthBar = healthBar;
            }                

            currentHealthPoints = maxHealthPoints;
            OnDeath += HPSystem_OnDeath;
        }
        private void OnDestroy()
        {
            OnDeath -= HPSystem_OnDeath;
        }

        private void HPSystem_OnDeath()
        {
            Destroy(gameObject);
        }

        public void TakeDamage(int amount)
        {
            currentHealthPoints = Mathf.Max(0, currentHealthPoints - amount);
            
            healthBar?.UpdateImageFillAmount(currentHealthPoints, maxHealthPoints);

            if (currentHealthPoints <= 0)
            {
                OnDeath.Invoke();
            }
        }

        public void Heal(int amount)
        {
            currentHealthPoints = Mathf.Min(maxHealthPoints, currentHealthPoints + amount);

            healthBar?.UpdateImageFillAmount(currentHealthPoints, maxHealthPoints);
        }

        public void SetMaxHeath(int amount, bool updateCurrentHealth = false)
        {
            if (updateCurrentHealth)
            {
                maxHealthPoints = amount;
                currentHealthPoints = maxHealthPoints;
            }
            else
                maxHealthPoints = amount;
        }
    }
}
