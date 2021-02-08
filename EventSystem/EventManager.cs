using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelRTS.EventSystem
{
    public static class EventManager
    {
        public static event Action<GameElement> OnEntityDestroyed;
        public static event Action<GameElement> OnEntityCreated;

        public static void RaiseEntityDestroyedEvent(GameElement entity)
        {
            OnEntityDestroyed?.Invoke(entity);
        }
        public static void RaiseElementCreatedEvent(GameElement entity)
        {
            OnEntityCreated?.Invoke(entity);
        }
    }
}