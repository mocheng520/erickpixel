using UnityEngine;

namespace HealthSystem
{
    public class HealthBarDestroyer : MonoBehaviour
    {
        private GameObject _ownerGameObject;

        public GameObject setGameObjectReference
        {
            set
            {
                _ownerGameObject = value;
            }
        }

        public void DestroyHPBar()
        {
            Destroy(gameObject);
        }
    }
}

