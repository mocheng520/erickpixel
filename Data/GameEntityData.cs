using UnityEngine;

namespace PixelRTS.Data
{
    [CreateAssetMenu(menuName = "Pixel RTS/GameElement Data")]
    public class GameEntityData : ScriptableObject
    {
        [Header("General Settings")]
        public string elementName;

        [TextArea]
        [Space]
        public string description;

        public Texture2D icon;
        public Mesh previewMesh;
        public Material secondaryMaterial;

        [Space]
        public GameObject originalPrefab;

        [Header("HP / Defense Settings")]
        [Space]
        public int maxHealth;
        public int defense;

        [Header("Production Settings")]
        [SerializeField]
        public float productionTime;
    }
}