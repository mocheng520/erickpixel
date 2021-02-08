using UnityEngine;
using HealthSystem;
using SelectionSystem;
using SelectionSystem.Components;
using SelectionSystem.Base;
using PixelRTS.Data;
using PixelRTS.EventSystem;

namespace PixelRTS
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Selector), typeof(Health))]
    public abstract class GameElement : Selectable
    {
        [SerializeField]
        [Tooltip("Indicates who owns this game element. If none, it's a neutral element or part of the environment.")]
        private Controller _owner;

        [SerializeField]
        private Selector _selector;

        [SerializeField]
        private GameEntityData _info;
        
        public Controller controller
        {
            get
            {
                return _owner;
            }
            internal set
            {
                if (_owner && _owner.Equals(value))
                    return;

                _owner = value;
            }
        }
        public Color representativeColor
        {
            get
            {
                return _owner.colorRepresentation;
            }
        }
        public GameEntityData data
        {
            get
            {
                return _info;
            }
        }

        public override bool isSelected 
        {
            get
            {
                return _selector.isSelected;
            }
        }
        
        private void ValidateColorRepresentation()
        {
            var mr = GetComponentInChildren<MeshRenderer>();

            foreach (var material in mr.materials)
            {
                string materialName = material.name.Remove(material.name.IndexOf('(') - 1);
                string targetMaterialName = data.secondaryMaterial.name;

                if (materialName.Equals(targetMaterialName))
                {
                    material.color = representativeColor;
                }
            }
        }

        protected void Awake()
        {
            Initialize();
        }
        private void Start()
        {
            ValidateColorRepresentation();
        }
        protected void OnEnable()
        {
            if (!gameObject.activeSelf)
                return;

            EventManager.RaiseElementCreatedEvent(this);
        }
        protected void OnDisable()
        {
            if (gameObject.activeSelf)
                return;

            EventManager.RaiseEntityDestroyedEvent(this);
        }

        protected virtual void Initialize() { }

        public override void Select()
        {
            _selector.Select();
        }
        public override void Deselect()
        {
            _selector.Deselect();
        }
    }
}