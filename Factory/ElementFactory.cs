using System.Collections;
using UnityEngine;
using PixelRTS;
using PixelRTS.Factory.Base;
using PixelRTS.EventSystem;

namespace PixelRTS.Factory
{
    public class ElementFactory : Factory<GameElement>
    {
        public override GameElement Create(GameElement type)
        {
            var newElement = Instantiate(type);

            EventManager.RaiseElementCreatedEvent(newElement);

            return newElement;
        }

        public static GameElement Create(GameElement type, Controller owner)
        {
            var newElement = Instantiate(type);
            newElement.controller = owner;

            EventManager.RaiseElementCreatedEvent(newElement);

            return newElement;
        }
    }
}