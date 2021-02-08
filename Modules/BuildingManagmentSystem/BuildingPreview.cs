using UnityEngine;
using BuildingManagmentSystem.Events;
using BuildingManagmentSystem.Data;

namespace BuildingManagementSystem
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class BuildingPreview : MonoBehaviour
    {
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private BuildingPlacementManager _buildingManagerCache;
        private LayerMask _environmentLayer;

        private readonly Color allowedToBuildColor = new Color(0, 1, 0, 0.5f);
        private readonly Color notAllowedToBuildColor = new Color(1, 0, 0, 0.5f);

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();

            if (_buildingManagerCache == null)
                _buildingManagerCache = GetComponent<BuildingPlacementManager>();

            if (_buildingManagerCache == null)
                _buildingManagerCache = GetComponentInParent<BuildingPlacementManager>();

            if (_buildingManagerCache == null)
                _buildingManagerCache = transform.root.GetComponent<BuildingPlacementManager>();

            _environmentLayer = _buildingManagerCache.GetEnvironmentDefinedLMask();
        }   

        private void Start()
        {
            gameObject.SetActive(false);
            EventManager.OnBuildingSelection += OnBuildingSelectionRaised;
        }

        private void LateUpdate()
        {
            HandleBuildingPreview();
        }

        private void HandleBuildingPreview()
        {
            Vector3 previewSpotPosition = _buildingManagerCache.mousePositionWorld;

            if (previewSpotPosition != Vector3.zero)
            {
                _meshRenderer.enabled = true;

                if (BuildingPlacementManager.CanSpawnBuilding(_buildingManagerCache.activeBuilding, previewSpotPosition, _environmentLayer))
                {
                    _meshRenderer.sharedMaterial.color = allowedToBuildColor;
                }
                else
                {
                    _meshRenderer.sharedMaterial.color = notAllowedToBuildColor;
                }

                transform.position = previewSpotPosition;
                return;
            }
            else
            {
                _meshRenderer.enabled = false;
            }
        }

        private void OnBuildingSelectionRaised(BuildingData building)
        {
            try
            {
                _meshFilter.mesh = building.previewMesh;
                gameObject.SetActive(true);
            }
            catch
            {
                gameObject.SetActive(false);
            }
        }
    }
}