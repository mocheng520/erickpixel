using System.Collections;
using UnityEngine;

namespace PixelRTS.Factory.Base
{
    public abstract class Factory<T> : MonoBehaviour
    {
        public abstract T Create(T type);
    }
}