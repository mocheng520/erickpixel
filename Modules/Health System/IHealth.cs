using System;

namespace HealthSystem
{
    public interface IHealth : IDamageable, IHealable
    {
        event Action OnDeath;

        void SetMaxHeath(int amount, bool updateCurrentHealth = false);
    }
}