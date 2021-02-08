using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ResourcesSystem.Data;
using ResourceSystem.Event;

namespace ResourcesSystem.UI
{
    public class ResourceUI : MonoBehaviour
    {
        [SerializeField] 
        private Transform resourceTemplatePrefab;
        [SerializeField]
        private float offset;

        private ResourceList resourceList;
        private Dictionary<GameResource, Transform> resourceTransformDictionary = new Dictionary<GameResource, Transform>();

        private void Awake()
        {
            resourceList = Resources.Load<ResourceList>(typeof(ResourceList).Name);

            int index = 0;
            foreach (GameResource resource in resourceList)
            {
                Transform resourceTransform = Instantiate(resourceTemplatePrefab, transform);

                resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset * index, 0);

                resourceTransform.Find("resourceIcon").GetComponent<Image>().sprite = resource.icon;
                resourceTransformDictionary[resource] = resourceTransform;

                index++;
            }
        }

        private void Start()
        {
            EventManager.OnResourceAmountChanges += OnResourcesChanges;
        }

        private void OnResourcesChanges(ResourcesManager resourceManager)
        {
            foreach (var resource in resourceList.resources)
            {
                Transform resourceTransform = resourceTransformDictionary[resource];

                int resourceAmount = resourceManager.GetResourceAmount(resource);
                resourceTransform.Find("resourceText").GetComponent<TMP_Text>().SetText(resourceAmount.ToString());
            }
        }
    }
}