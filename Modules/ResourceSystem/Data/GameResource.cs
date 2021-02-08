using UnityEngine;

namespace ResourcesSystem.Data
{
    [CreateAssetMenu(fileName = "Game Resource", menuName = "Resource System/Data")]
    public class GameResource : ScriptableObject
    {
        public string resourceName;

        [Space] 
        public Sprite icon;
    }
}