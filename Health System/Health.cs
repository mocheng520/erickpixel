using System;
using UnityEngine;
using UnityEngine.Events;

namespace HealthSystem
{
    public sealed class Health : MonoBehaviour, IHealth
    {
        [SerializeField]
        private int _maxHealthPoints;

        [SerializeField]
        [Space]
        private UnityEvent _onDeath;
        private HealthBar _healthBar;

        public event Action OnDeath;

        public int maxHealth
        {
            get
            {
                return _maxHealthPoints;
            }
        }
        public int currentHealth { get; private set; }

        private void Start()
        {
            if (TryGetComponent(out HealthBar healthBar))
            {
                this._healthBar = healthBar;
            }

            currentHealth = _maxHealthPoints;

            _onDeath.AddListener(() => OnDeath?.Invoke());
        }

        public void SetMaxHeath(int amount, bool updateCurrentHealth = false)
        {
            if (updateCurrentHealth)
            {
                _maxHealthPoints = amount;
                currentHealth = _maxHealthPoints;
                return;
            }
            
            _maxHealthPoints = amount;
        }
        public void TakeDamage(int amount)
        {
            currentHealth = Mathf.Max(0, currentHealth - amount);

            _healthBar?.UpdateImageFillAmount(currentHealth, _maxHealthPoints);

            if (currentHealth <= 0)
                _onDeath.Invoke();
        }
        public void Heal(int amount)
        {
            currentHealth = Mathf.Min(_maxHealthPoints, currentHealth + amount);

            _healthBar?.UpdateImageFillAmount(currentHealth, _maxHealthPoints);
        }
    }
}
