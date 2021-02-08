using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using CameraMovementSystem;
using SelectionSystem;
using SelectionSystem.Components;
using BuildingManagementSystem;
using BuildingManagmentSystem.Data;
using PixelRTS.UnitSystem;
using PixelRTS.EventSystem;
using PixelRTS.Factory;
using PixelRTS.BuildingSystem;
using System;

namespace PixelRTS
{
    [RequireComponent(typeof(SelectionHandler), typeof(BuildingPlacementManager), typeof(CameraRigController))]
    public class Controller : MonoBehaviour
    {
        [SerializeField]
        private Camera _playerCamera;

        [SerializeField]
        [Tooltip("The visual representation of this Controller over it's owned game elements.")]
        [Space]
        private Color _representativeColor;

        // CONTROLLER RESOURCES resources;
        private SelectionHandler _selectionHandler;
        private BuildingPlacementManager _buildingPlacementManager;
        private CameraRigController _cameraRig;

        private Vector3 _mousePositionScreen;
        private Vector3 _mousePositionWorld;

        private List<BaseUnit> _controledUnits = new List<BaseUnit>();
        private List<BaseBuilding> _controledBuildings = new List<BaseBuilding>();

        public Color colorRepresentation
        {
            get
            {
                return _representativeColor;
            }
        }

        private void Awake()
        {
            if (!_playerCamera)
                _playerCamera = Camera.main;

            if (!_playerCamera)
                _playerCamera = FindObjectOfType<Camera>();

            _selectionHandler = GetComponent<SelectionHandler>();
            _buildingPlacementManager = GetComponent<BuildingPlacementManager>();
            _cameraRig = GetComponent<CameraRigController>();
            _selectionHandler.camera = _playerCamera;

            EventManager.OnEntityCreated += HandleElementCreated;
            EventManager.OnEntityDestroyed += HandleElementDestroyed;
        }

        private void HandleElementDestroyed(GameElement element)
        {
            try
            {
                if (element is BaseUnit unit)
                {
                    if (unit.controller.Equals(this))
                        _controledUnits.Remove(unit);
                }

                if (element is BaseBuilding building)
                {
                    if (building.controller.Equals(this))
                        _controledBuildings.Remove(building);
                }
            }
            catch(Exception e) 
            {
                Debug.Log($"Operation error caught. {e.TargetSite}");
            }
        }
        private void HandleElementCreated(GameElement element)
        {
            try
            {
                if (element is BaseUnit unit)
                {
                    if (unit.controller.Equals(this))
                        _controledUnits.Add(unit);
                }

                if (element is BaseBuilding building)
                {
                    if (building.controller.Equals(this))
                        _controledBuildings.Add(building);
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Operation error caught. {e.TargetSite}");
            }
        }

        public BaseUnit[] GetUnits()
        {
            return _controledUnits.ToArray();
        }
        public BaseBuilding[] GetBuildings()
        {
            return _controledBuildings.ToArray();
        }
        public ISelectable[] GetCurrentSelection()
        {
            var selection = _selectionHandler.currentSelection.ToArray();

            return selection;
        }
        private bool GetRayCallback(out RaycastHit result)
        {
            var ray = _playerCamera.ScreenPointToRay(_mousePositionScreen);
            return Physics.Raycast(ray, out result);
        }
        
        public void GetMousePositionOnScreen(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                _mousePositionScreen = ctx.ReadValue<Vector2>();
                _mousePositionWorld = Utilities.Utility.GetMousePositionIn3DWorld(_mousePositionScreen, _playerCamera);

                _cameraRig.mousePositionScreen = _mousePositionScreen;
                _buildingPlacementManager.mousePositionWorld = _mousePositionWorld;
            }
        }
        public void OrderUnitsToBuild(BuildingData building)
        {
            // IF RESOURCES ARE NOT ENOUGH TO BUILD <building> SHOW ERROR MESSAGE.
            // OTHERWISE

            //_buildingPlacementManager.SetActiveBuilding(building);

            // Order uppon the building is settled down.


        }
        public void CommandOnRightClick(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                var CurrentSelection = GetCurrentSelection();

                foreach (var item in CurrentSelection)
                {
                    if(item is ICommandable commandable)
                    {
                        Debug.Log(commandable.GetType());
                        continue;
                    }
                }
            }
        }
        public void BeginBuildingProduction(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                var SelectedElements = GetCurrentSelection();

                foreach (var selected in SelectedElements.Cast<MonoBehaviour>())
                {
                    if (selected.TryGetComponent<BaseBuilding>(out var building))
                    {
                        if (!building.controller.Equals(this))
                            continue;

                        if(building is ISpawner spawner)
                            spawner.Spawn();
                    }
                }   
            }
        }

#if UNITY_EDITOR
        public void DebugTest()
        {
            Debug.Log("Test");
        }
#endif
    }
}