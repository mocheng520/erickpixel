using ResourcesSystem.Data;
using UnityEngine;

namespace ResourcesSystem
{
    public class ResourceNode : MonoBehaviour
    {
        [SerializeField]
        private int _resourceAmount = 0;

        [SerializeField]
        private GameResource _resourceType;

        public int resourceAmount
        {
            get
            {
                return _resourceAmount;
            }
        }
        public GameResource ResourceType
        {
            get
            {
                return _resourceType;
            }
        }

        private void Start()
        {
            if(_resourceAmount < 1)
                _resourceAmount = Random.Range(0, 5001);
        }

        public int ExtractResource(int amount)
        {
            _resourceAmount = Mathf.Max(0, _resourceAmount - amount);

            if (_resourceAmount <= 0)
                gameObject.SetActive(false);

            return _resourceAmount;
        }
    }
}