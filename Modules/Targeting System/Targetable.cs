using UnityEngine;

namespace TargetingSystem
{
    [DisallowMultipleComponent]
    public sealed class Targetable : MonoBehaviour, ITargetable
    {
        [SerializeField]
        private Transform transformAimPoint;

        public Transform aimPoint
        {
            get
            {
                return transformAimPoint;
            }
        }

        private void Start()
        {
            if (!transformAimPoint)
                transformAimPoint = transform;
        }
    }
}