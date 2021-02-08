using UnityEngine;
using BuildingManagmentSystem.Data;
using UnityEngine.AI;
using TargetingSystem;
using HealthSystem;
using SelectionSystem;

namespace PixelRTS.BuildingSystem
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(NavMeshObstacle))]
    public class BaseBuilding : GameElement, ISelectable, IDamageable, IHealable
    {
        [SerializeField]
        private BuildingData _placementInfo;

        [SerializeField]
        [Space]
        private Health _health;

        public BuildingData buildingData
        {
            get => _placementInfo;
        }
        public int currentHealth
        {
            get
            {
                return ((IDamageable)_health).currentHealth;
            }
        }
        public int maxHealth
        {
            get
            {
                return ((IDamageable)_health).maxHealth;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            _placementInfo.prefab = data.originalPrefab.transform;
            _placementInfo.previewMesh = data.previewMesh;
        }

        public virtual void Heal(int amount)
        {
            
        }

        public virtual void TakeDamage(int amount)
        {
            
        }
    }
}