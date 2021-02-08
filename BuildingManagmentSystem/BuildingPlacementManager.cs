using System;
using UnityEngine;
using UnityEngine.EventSystems;
using BuildingManagmentSystem.Events;
using BuildingManagmentSystem.Data;
using BuildingManagmentSystem;

namespace BuildingManagementSystem
{
    public class BuildingPlacementManager : MonoBehaviour
    {
        #region STATIC
        
        public static bool CanSpawnBuilding(BuildingData buildingData, Vector3 position)
        {
            var buildingCollider = buildingData.prefab.GetComponent<Collider>();

            Vector3 spawnSpot = Vector3.zero;

            Collider[] collidersInTheArea = null;

            switch (buildingCollider)
            {
                case BoxCollider boxCollider when buildingCollider is BoxCollider:
                    boxCollider = buildingCollider as BoxCollider;
                    spawnSpot = position + boxCollider.center;
                    collidersInTheArea = Physics.OverlapBox(spawnSpot, boxCollider.size, Quaternion.identity);
                    break;

                case SphereCollider sphereCollider when buildingCollider is SphereCollider:
                    sphereCollider = buildingCollider as SphereCollider;
                    spawnSpot = position + sphereCollider.center;
                    collidersInTheArea = Physics.OverlapSphere(spawnSpot, sphereCollider.radius);
                    break;

                default: throw new Exception($"Recomended use {nameof(BoxCollider)} or {nameof(SphereCollider)} for buildings.");
            }

            bool areaIsClear = collidersInTheArea.Length == 0;

            if (!areaIsClear) return false;

            collidersInTheArea = Physics.OverlapSphere(position, buildingData.minAcceptablePlacementDistance);

            foreach (var collider in collidersInTheArea)
            {
                var building = collider.GetComponent<Building>();

                if (building != null && building.type == buildingData)
                {
                    return false;
                }
            }

            return true;
        }
        public static bool CanSpawnBuilding(BuildingData buildingData, Vector3 position, int ignoreLayer)
        {
            var buildingCollider = buildingData.prefab.GetComponent<Collider>();

            Vector3 spawnSpot;

            Collider[] collidersInTheArea;

            switch (buildingCollider)
            {
                case BoxCollider boxCollider when buildingCollider is BoxCollider:
                    boxCollider = buildingCollider as BoxCollider;
                    spawnSpot = position + boxCollider.center;
                    collidersInTheArea = Physics.OverlapBox(spawnSpot, boxCollider.size, Quaternion.identity, ~ignoreLayer);
                    break;

                case SphereCollider sphereCollider when buildingCollider is SphereCollider:
                    sphereCollider = buildingCollider as SphereCollider;
                    spawnSpot = position + sphereCollider.center;
                    collidersInTheArea = Physics.OverlapSphere(spawnSpot, sphereCollider.radius, ~ignoreLayer);
                    break;

                default:
                    Debug.Log($"Collider must be: {nameof(BoxCollider)} or {nameof(SphereCollider)}");
                    return false;
            }

            if (collidersInTheArea.Length > 0) 
                return false;

            collidersInTheArea = Physics.OverlapSphere(position, buildingData.minAcceptablePlacementDistance);

            foreach (var collider in collidersInTheArea)
            {
                if (collider.TryGetComponent<Building>(out var building) && 
                    building.type == buildingData)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        [SerializeField]
        private LayerMask _environmentLayer;

        private BuildingsList _buildingList;
        private BuildingData _activeBuilding;

        public BuildingData activeBuilding 
        {
            get => _activeBuilding;
        }
        /// <summary>
        /// Access or define mouse position in world (3D).
        /// </summary>
        public Vector3 mousePositionWorld
        {
            get;
            set;
        }

        private void Awake()
        {
            _buildingList = Resources.Load<BuildingsList>(nameof(BuildingsList));
        }

        public LayerMask GetEnvironmentDefinedLMask()
        {
            return _environmentLayer;
        }

        public void TryToPlaceBuilding()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (!_activeBuilding) return;

            Vector3 placementSpot = mousePositionWorld;

            if (placementSpot == Vector3.zero) return;

            if(CanSpawnBuilding(_activeBuilding, placementSpot, _environmentLayer))
            {
                Instantiate(activeBuilding.prefab, placementSpot, Quaternion.identity);
                CancelBuildingPlacement();
            }
        }

        public void TryToPlaceBuilding(bool performMultiPlacement)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (!_activeBuilding) return;

            Vector3 placementSpot = mousePositionWorld;

            if (placementSpot == Vector3.zero) return;

            if (CanSpawnBuilding(_activeBuilding, placementSpot, _environmentLayer))
            {
                Instantiate(activeBuilding.prefab, placementSpot, Quaternion.identity);

                if (!performMultiPlacement)
                    CancelBuildingPlacement();
            }
        }

        public void SetActiveBuilding(BuildingData building)
        {
            _activeBuilding = _buildingList.Find(b => b.Equals(building)); 

            EventManager.RaiseBuildingSelectionEvent(_activeBuilding);
        }

        public void CancelBuildingPlacement()
        {
            _activeBuilding = null;
            EventManager.RaiseBuildingSelectionEvent(_activeBuilding);
        }
    }
}