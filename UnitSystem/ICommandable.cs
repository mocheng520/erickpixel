using UnityEngine;

namespace PixelRTS.UnitSystem
{
    public interface ICommandable
    {
        void Move(Vector3 point);
        void Patrol(Vector3 point);
        void Stop();
        void HoldPosition();
    }
}
