using UnityEngine;

namespace BuildingManagmentSystem.Data
{
    [CreateAssetMenu(menuName = "Building Management System/Building Data")]
    public class BuildingData : ScriptableObject
    {
        public static readonly string nameColorHex = "FFDB00";

        public float minAcceptablePlacementDistance;


        public Transform prefab { get; set; }
        public Mesh previewMesh { get; set; }
    }
}