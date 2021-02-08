using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PixelRTS.Factory;
using PixelRTS.UnitSystem;
using PixelRTS.TimedActions;
using System.Runtime.InteropServices;

namespace PixelRTS.BuildingSystem
{
    public sealed class ProductionBuilding : BaseBuilding, ISpawner
    {
        public const int MAX_QUEUE_COUNT = 5;

        [SerializeField]
        [Space]
        private SpawnablesList _spawnalbes;

        [SerializeField]
        [Space]
        private Transform _exitPosition;

        [SerializeField]
        [Space]
        private UnityEvent _onUnitReady;

        private readonly Queue<GameElement> _productionQueue = new Queue<GameElement>();
        private GameElement _elementInProduction;
        private Vector3 _rallyPoint;
        
        private TimedAction timedAction;

        public float progress
        {
            get
            {
                if(timedAction != null)
                    return timedAction.progressPercentage / 100;

                return 0;
            }
        }
        public int queueCount
        {
            get => _productionQueue.Count;
        }

        protected override void Initialize()
        {            
            base.Initialize();
            
            _onUnitReady.AddListener(HandleUnitReady);
            
            enabled = false;
        }

        private void LateUpdate()
        {
            switch (_productionQueue.Count)
            {
                case 0: enabled = false; return;

                default: timedAction.Process(Time.deltaTime);
                    return;
            }
        }

        private void HandleUnitReady()
        {
            if (_productionQueue.Count < 1)
                return;

            var unitReady = _productionQueue.Dequeue();

            // Unparent for free-use.
            unitReady.transform.parent = null;

            // Reposition the new Unit to the exit point.
            unitReady.transform.position = _exitPosition.position;
            unitReady.transform.rotation = Quaternion.Euler(0, _exitPosition.rotation.eulerAngles.y, 0);

            // Activate back the Unit.
            unitReady.gameObject.SetActive(true);

            // At the end, send it to the rally point (if there's any)
            if (unitReady is ICommandable mobileUnit && !_rallyPoint.Equals(Vector3.zero))
                mobileUnit.Move(_rallyPoint);
            
            // Track the current element in production. This allow to keep track of the correct production time.
            switch (_productionQueue.Count)
            {
                case 0: 
                    _elementInProduction = null;
                    timedAction = null;
                    return;

                default: 
                    _elementInProduction = _productionQueue.Peek();
                    timedAction = new TimedAction(_elementInProduction.data.productionTime, HandleUnitReady);
                    return;
            }
        }
        private void BeginProduction()
        {
            if (_productionQueue.Count >= MAX_QUEUE_COUNT)
                return;

            // Pre-instatiate a copy of the new Unit.
            var clone = ElementFactory.Create(_spawnalbes[0], controller);

            // Enqueue this one, allowing to wait the timer so the copy is going to be Dequeued in order that it was queued.
            _productionQueue.Enqueue(clone);

            // Set the parent to this transform temporarily
            clone.transform.SetParent(transform, true);

            // Set the copy inactive.
            clone.gameObject.SetActive(false);

            // Track the current element in production. This allow to keep track of the correct production time.
            if (!_elementInProduction)
                _elementInProduction = _productionQueue.Peek();

            timedAction = new TimedAction(_elementInProduction.data.productionTime, HandleUnitReady);

            enabled = true;
        }

        public void Spawn()
        {
            BeginProduction();
        }
        public void SetRallyPoint(Vector3 point)
        {
            _rallyPoint = point;
        }
    }
}