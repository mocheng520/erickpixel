using UnityEngine;
using UnityEngine.UI;
using HealthSystem.MathOperations;

namespace HealthSystem
{
    public class HealthBar : MonoBehaviour
    {
        private static Canvas hpBarCanvas = null;
        private static Transform _mainCamera = null;
        public static Transform mainCamera 
        { 
            get => _mainCamera; 
            set => _mainCamera = value; 
        }

        [SerializeField] 
        private Vector2 barScale = Vector2.one;

        [Tooltip("The transform that the bar will be positioned in the gameObject. \n\nRecomendation: Use an empty gameObject as child")]
        [SerializeField] 
        private Transform targetTransform;

        [SerializeField] 
        [Tooltip("Only works for non-static game objects.")]
        private bool updateBarPosition;
        [SerializeField]
        private bool updateBarDirection;

        private Transform barTransform;
        private Image imageSlider;
        private HealthBarDestroyer barDestroyer;

        private void Awake()
        {
            if(hpBarCanvas == null)
            {
                Canvas canvas = Resources.Load<Canvas>("HPBarCanvas(World)");
                hpBarCanvas = Instantiate(canvas);
            }

            if(_mainCamera == null)
                _mainCamera = Camera.main.transform;

            GameObject hPBarPrefab = Resources.Load<GameObject>("HPBar");

            barTransform = Instantiate(hPBarPrefab, hpBarCanvas.transform).transform;
            imageSlider = barTransform.Find("HPBarFill").GetComponent<Image>();
            barDestroyer = barTransform.GetComponent<HealthBarDestroyer>();
            barDestroyer.SetGameObjectReference(this.gameObject);

            if(barTransform.TryGetComponent(out BarScaler scaler))
                scaler.SetNewBarScale(barScale);

            HandleDirection();
            HandlePosition();

            Resources.UnloadUnusedAssets();
        }
        private void Start()
        {
            if (!enabled)
                barTransform.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            barTransform.gameObject.SetActive(true);
        }
        private void OnDisable()
        {
            barTransform.gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            barDestroyer?.DestroyHPBar();
        }

        private void LateUpdate()
        {
            if (updateBarDirection)
                HandleDirection();

            if (gameObject.isStatic) return;

            if (updateBarPosition)
                HandlePosition();
        }

        public void HandleDirection()
        {

            barTransform.forward = -_mainCamera.forward;
        }

        public void HandlePosition()
        {
            barTransform.position = targetTransform.position;
        }

        public void UpdateImageFillAmount(int current, int max)
        {
            imageSlider.fillAmount = Math.GetNormalizedValueMax(current, max);
        }
    }
}

