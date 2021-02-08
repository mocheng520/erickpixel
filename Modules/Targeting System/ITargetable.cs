using UnityEngine;

namespace TargetingSystem
{
    public interface ITargetable
    {
        Transform aimPoint { get; }
    }
}
