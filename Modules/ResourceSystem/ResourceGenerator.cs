using ResourcesSystem.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace ResourcesSystem
{
    public class ResourceGenerator : MonoBehaviour
    {
        public static int CheckNearbyResources(Vector3 position, ResourceGenerationData generationData)
        {
            Collider[] colliders = Physics.OverlapSphere(position, generationData.resourceDetectionRadius);

            int nearbyResources = 0;

            foreach (var col in colliders)
            {
                var resource = col.GetComponent<ResourceNode>();
                if (resource != null)
                {
                    nearbyResources++;
                }
            }

            nearbyResources = Mathf.Clamp(nearbyResources, 0, generationData.maxEfficiencyNodesAmount);
            return nearbyResources;
        }
        public static int CheckNearbyResources(Vector3 position, ResourceGenerationData generationData, out List<ResourceNode> nearbyResourcesFound)
        {
            Collider[] colliders = Physics.OverlapSphere(position, generationData.resourceDetectionRadius);

            int nearbyResources = 0;
            nearbyResourcesFound = new List<ResourceNode>();

            foreach (var col in colliders)
            {
                var resource = col.GetComponent<ResourceNode>();
                if (resource != null)
                {
                    nearbyResourcesFound.Add(resource);
                    nearbyResources++;
                }
            }

            nearbyResourcesFound.TrimExcess();
            nearbyResources = Mathf.Clamp(nearbyResources, 0, generationData.maxEfficiencyNodesAmount);
            return nearbyResources;
        }

#if UNITY_EDITOR
        [SerializeField] 
        private string generatedResourcesAmount;
#endif
        [SerializeField]
        private ResourceGenerationData generationData;
        
        private Dictionary<GameResource, int> generatedResources = new Dictionary<GameResource, int>();
        private List<ResourceNode> nearbyResourceNode;

        private float timer;

        public float StorageCapacity
        {
            get
            {
                return generationData.maxStorageCapacity;
            }
        }

        [Space]
        public UnityEvent OnResourceChange;

        private void Start()
        {
            generatedResources[generationData.resourceToGenerate] = 0;

            int nearbyResources = CheckNearbyResources(transform.position, generationData, out nearbyResourceNode);

            if (nearbyResources <= 0)
                enabled = false;
        }
        private void LateUpdate()
        {
            if (nearbyResourceNode.Count < 1)
                enabled = false;

            timer = Mathf.Min(generationData.generationTime, timer + Time.deltaTime);

            if (timer < generationData.generationTime)
                return;

            GenerateResources();
        }

        private void GenerateResources()
        {
            ResourceNode resourceNode = nearbyResourceNode[0];

            int currentGeneratedResourceAmount = generatedResources[generationData.resourceToGenerate];
            int maxCapacity = generationData.maxStorageCapacity;
            int resourceAmountToExtract = generationData.extractionAmount;
            int extractedResource = resourceNode.ExtractResource(resourceAmountToExtract);

            currentGeneratedResourceAmount = Mathf.Min(maxCapacity, currentGeneratedResourceAmount + extractedResource);
            generatedResources[generationData.resourceToGenerate] = currentGeneratedResourceAmount;

            OnResourceChange?.Invoke();

#if UNITY_EDITOR
            generatedResourcesAmount = generatedResources[generationData.resourceToGenerate].ToString();
#endif
        }
    }
}