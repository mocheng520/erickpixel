using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelRTS.Factory
{
    [CreateAssetMenu(fileName = "Spawnable Entities List", menuName = "Pixel RTS/Factory/Spawnables List")]
    public class SpawnablesList : ScriptableObject, ICollection<GameElement>
    {
        [SerializeField]
        private GameElement[] _prefabs;

        public GameElement this[int index]
        {
            get
            {
                try
                {
                    return _prefabs[index];
                }
                catch
                {
                    return default;
                }
            }
        }

        #region Hiden
        int ICollection<GameElement>.Count
        {
            get
            {
                return _prefabs.Length;
            }
        }
        bool ICollection<GameElement>.IsReadOnly
        {
            get
            {
                return true;
            }
        }
        void ICollection<GameElement>.Add(GameElement item)
        {
            return;
        }
        void ICollection<GameElement>.Clear()
        {
            return;
        }
        bool ICollection<GameElement>.Contains(GameElement item)
        {
            return ((ICollection<GameElement>)_prefabs).Contains(item);
        }
        void ICollection<GameElement>.CopyTo(GameElement[] array, int arrayIndex)
        {
            return;
        }
        public IEnumerator<GameElement> GetEnumerator()
        {
            return ((IEnumerable<GameElement>)_prefabs).GetEnumerator();
        }
        bool ICollection<GameElement>.Remove(GameElement item)
        {
            return false;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _prefabs.GetEnumerator();
        }
        #endregion
    }
}