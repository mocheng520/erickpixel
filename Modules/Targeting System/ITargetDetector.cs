using UnityEngine;

namespace TargetingSystem
{
    public interface ITargetDetector
    {
        float fieldOfViewRadius { get; }
        bool searchingTargets { get; }
        LayerMask targetsLayerMask { get; }

        void PauseTargetDetection();
        void ResumeTargetDetection();
    }
}