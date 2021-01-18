using UnityEngine;

namespace HealthSystem
{
    public interface IDamageable
    {
        int currentHealth { get; }
        int maxHealth { get; }

        void TakeDamage(int amount);
    }
}