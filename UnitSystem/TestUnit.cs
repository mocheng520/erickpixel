using UnityEngine;
using PixelRTS.UnitSystem;
using UnityEngine.AI;

namespace PixelRTS
{
    public class TestUnit : BaseUnit
    {
        
        public override void Move(Vector3 point)
        {
            if (NavMesh.SamplePosition(point, out var hit, 1f, agent.areaMask))
                agent.SetDestination(hit.position);
        }
    }
}