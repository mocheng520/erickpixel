using UnityEngine;

namespace HealthSystem
{
    public class HealthBarDestroyer : MonoBehaviour
    {
        private GameObject _ownerGameObject;

        public void SetGameObjectReference(GameObject value)
        {
            _ownerGameObject = value;
        }

        public void DestroyHPBar()
        {
            if(!_ownerGameObject)
                Destroy(gameObject);
        }
    }
}

