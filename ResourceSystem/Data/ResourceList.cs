using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourcesSystem.Data
{
    [CreateAssetMenu(fileName = nameof(ResourceList), menuName = "Resource System/Resources List")]
    public class ResourceList : ScriptableObject, IEnumerable<GameResource>
    {
        public List<GameResource> resources = new List<GameResource>();

        IEnumerator<GameResource> IEnumerable<GameResource>.GetEnumerator()
        {
            return ((IEnumerable<GameResource>)resources).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)resources).GetEnumerator();
        }
    }
}