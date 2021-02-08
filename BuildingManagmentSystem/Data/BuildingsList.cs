using System;
using System.Collections;
using System.Collections.Generic;
using BuildingManagmentSystem.Data;
using UnityEngine;

namespace BuildingManagmentSystem
{
    [CreateAssetMenu(fileName = nameof(BuildingsList), menuName = "Building Management System/Allowed Buildings List")]
    public class BuildingsList : ScriptableObject, ICollection<BuildingData>
    {
        private readonly string errorMessage = "Operation is not allowed.";

        [SerializeField]
        private BuildingData[] buildings = Array.Empty<BuildingData>();

        public BuildingData this[int index]
        {
            get
            {
                return buildings[index];
            }
        }

        public int Count => ((ICollection<BuildingData>)buildings).Count;

        public bool IsReadOnly => ((ICollection<BuildingData>)buildings).IsReadOnly;

        public void Add(BuildingData item)
        {
            Debug.LogError(errorMessage);
        }

        public bool Remove(BuildingData item)
        {
            Debug.LogError(errorMessage);
            return false;
        }

        public void Clear()
        {
            Debug.LogError(errorMessage);
        }

        public bool Contains(BuildingData item)
        {
            return ((ICollection<BuildingData>)buildings).Contains(item);
        }

        public BuildingData Find(Predicate<BuildingData> match)
        {
            return Array.Find(buildings, match);
        }

        public void CopyTo(BuildingData[] array, int arrayIndex)
        {
            Debug.LogError(errorMessage);
        }

        public IEnumerator<BuildingData> GetEnumerator()
        {
            return ((IEnumerable<BuildingData>)buildings).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return buildings.GetEnumerator();
        }
    }
}