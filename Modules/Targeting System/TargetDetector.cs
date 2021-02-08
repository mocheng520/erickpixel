using System;
using UnityEngine;

namespace TargetingSystem
{
    [DisallowMultipleComponent]
    public sealed class TargetDetector : MonoBehaviour, ITargetDetector, ITargeter
    {
        private const int MAX_BUFFER_SIZE = 11;
        private const float REFRESH_RATE = 0.1f;
        private const string FOV_TOOLTIP = "Used to define a custom origin point for the Field of View detection. \n If it's not initialized then the object's position is used instead as a reference.";

        enum DetectionMode
        {
            Soft,
            Dynamic
        }

        [SerializeField]
        private DetectionMode _detectionMode = DetectionMode.Soft;

        [Space]
        [SerializeField]
        [Tooltip("Current target transform.")]
        private Transform _target;

        [Space]
        [Tooltip(FOV_TOOLTIP)]
        [SerializeField] 
        private Transform _fovTransform = null;

        [Space]
        [SerializeField] 
        private float _fovRadius = 0f;

        [Space]
        [SerializeField] 
        private LayerMask _targetLayers;

        [NonSerialized]
        private Vector3 _fovPoint = Vector3.zero;
        [NonSerialized]
        private float timer = 0f;
        [HideInInspector]
        private SphereCollider _sphereCollider;
        [HideInInspector]
        private Collider[] _buffer = new Collider[MAX_BUFFER_SIZE];
        
        private ITargetable _currentTarget;

        private Vector3 fovPoint
        {
            get
            {
                if (_fovTransform)
                    return _fovTransform.position;

                if (!gameObject.isStatic)
                    _fovPoint = transform.position + Vector3.up * 0.5f;

                return _fovPoint;
            }
        }

        public ITargetable[] targetsInRange 
        { 
            get; 
        } = new ITargetable[MAX_BUFFER_SIZE];
        public float fieldOfViewRadius
        {
            get => _fovRadius;
        }
        public bool searchingTargets
        {
            get => enabled;
        }
        public LayerMask targetsLayerMask
        {
            get
            {
                return _targetLayers;
            }
        }

        private void Start()
        {
             _fovPoint = transform.position + transform.up * 0.5f;

            if(TryGetComponent(out SphereCollider scollider))
            {
                scollider.isTrigger = true;
                scollider.radius = _fovRadius;
                _sphereCollider = scollider;
            }
            else
            {
                _sphereCollider = gameObject.AddComponent<SphereCollider>();
                _sphereCollider.isTrigger = true;
                _sphereCollider.radius = _fovRadius;
            }
        }

        private void Update()
        {
            switch (_detectionMode)
            {
                case DetectionMode.Soft:
                    _sphereCollider.enabled = true;

                    return;

                case DetectionMode.Dynamic:
                    _sphereCollider.enabled = false;

                    timer += Time.deltaTime;

                    if (timer < REFRESH_RATE)
                        return;

                    GetNearbyTargets();

                    timer = 0f;

                    break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((1 << other.gameObject.layer & targetsLayerMask) != 0)
            {
                if (!other.TryGetComponent(out ITargetable target)) return;

                for (int i = 0; i < targetsInRange.Length; i++)
                {
                    if (targetsInRange[i] == null)
                        targetsInRange[i] = target;
                }

                if (_currentTarget != null) return;

                for (int i = 0; i < targetsInRange.Length; i++)
                {
                    if (SetTarget(targetsInRange[i]))
                        return;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out ITargetable target)) return;

            int targetIndex = Array.IndexOf(targetsInRange, target);

            if (targetIndex > -1)
            {
                targetsInRange[targetIndex] = null;
                SetTarget(null);
            }
        }

        #region PUBLIC
        public TargetInfo GetTargetInfo()
        {
            return new TargetInfo(_currentTarget);
        }

        public void GetNearbyTargets()
        {
            ClearTargets();

            int bufferCount = Physics.OverlapSphereNonAlloc(fovPoint, fieldOfViewRadius, _buffer, _targetLayers);

            for (int i = 0; i < bufferCount; i++)
            {
                if (_buffer[i].transform == transform)
                    continue;

                Transform currentTarget = _buffer[i].transform;

                if (!currentTarget.TryGetComponent(out ITargetable target)) continue;

                targetsInRange[i] = target;
            }

            if (_currentTarget != null) return;

            for (int i = 0; i < targetsInRange.Length; i++)
            {
                if (SetTarget(targetsInRange[i]))
                    return;
            }
        }

        public bool SetTarget(ITargetable target)
        {
            try
            {
                _currentTarget = target;
                _target = target.aimPoint;

                return true;
            }
            catch
            {
                _currentTarget = null;
                _target = null;

                return false;
            }
        }

        public void ClearTargets()
        {
            _currentTarget = null;
            _target = null;

            Array.Clear(_buffer, 0, _buffer.Length);
            Array.Clear(targetsInRange, 0, targetsInRange.Length);
        }

        public void PauseTargetDetection()
        {
            enabled = false;
        }

        public void ResumeTargetDetection()
        {
            enabled = true;
        }
        #endregion

#if UNITY_EDITOR
        #region UNITY EDITOR

        [Space]
        [Header("Editor Settings")]
        [Space]
        [SerializeField] bool drawFOV = true;

        private void OnDrawGizmos()
        {
            if (!drawFOV) return;

            Gizmos.color = Color.white;

            if (_fovTransform == null)
            {
                if (gameObject.isStatic)
                    Gizmos.DrawWireSphere(_fovPoint, _fovRadius);
                else
                {
                    _fovPoint = transform.position + transform.up * 0.5f;
                    Gizmos.DrawWireSphere(_fovPoint, _fovRadius);
                }
            }
            else
                Gizmos.DrawWireSphere(_fovTransform.position, _fovRadius);

            if (_currentTarget == null) return;
            if (_currentTarget.aimPoint == null) return;

            UnityEditor.Handles.color = Color.red;

            if (_fovTransform == null)
            {
                if (gameObject.isStatic)
                    UnityEditor.Handles.DrawLine(_fovPoint, _currentTarget.aimPoint.position);
                else
                {
                    _fovPoint = transform.position + transform.up * 0.5f;
                    UnityEditor.Handles.DrawLine(_fovPoint, _currentTarget.aimPoint.position);
                }
            }
            else
                UnityEditor.Handles.DrawLine(_fovTransform.position, _currentTarget.aimPoint.position);
        }

        #endregion
#endif
    }
}