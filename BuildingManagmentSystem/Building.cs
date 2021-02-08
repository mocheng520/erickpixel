using BuildingManagmentSystem.Data;
using UnityEngine;

namespace BuildingManagementSystem
{
    public class Building : MonoBehaviour
    {
        [SerializeField] 
        private BuildingData _buildingData;

        public BuildingData type => _buildingData;
    }
}