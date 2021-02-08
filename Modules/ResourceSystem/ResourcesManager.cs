using System;
using System.Collections.Generic;
using UnityEngine;
using ResourcesSystem.Data;
using ResourceSystem.Event;

namespace ResourcesSystem
{
    public class ResourcesManager : MonoBehaviour
    {
        [SerializeField]
        private List<ResourceCost> startingResources = new List<ResourceCost>();

        private Dictionary<GameResource, int> resourceAmount = new Dictionary<GameResource, int>();

        private void Awake()
        {
            var resourcesList = Resources.Load<ResourceList>(nameof(ResourceList));

            foreach (GameResource resource in resourcesList)
            {
                resourceAmount[resource] = 0;
            }
        }
        private void Start()
        {
            foreach (var resource in startingResources)
            {
                AddResource(resource.type, resource.amount);
            }
        }

        public int GetResourceAmount(GameResource resource)
        {
            return resourceAmount[resource];
        }
        public void AddResource(GameResource resource, int amount)
        {
            resourceAmount[resource] += amount;

            EventManager.NotifyResourceAmountChanges(this);
        }
        public void SpendResource(ResourceCost[] resourceCosts)
        {
            foreach (var resourceCost in resourceCosts)
            {
                resourceAmount[resourceCost.type] = Mathf.Max(0, resourceAmount[resourceCost.type] - resourceCost.amount);
            }

            EventManager.NotifyResourceAmountChanges(this);
        }
        public bool HasEnoughResourceToBuild(params ResourceCost[] resourceCosts)
        {
            foreach (var cost in resourceCosts)
            {
                if (GetResourceAmount(cost.type) >= cost.amount)
                    continue;

                return false;
            }

            return true;
        }
    }
}