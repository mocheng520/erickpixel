using UnityEngine;

namespace TargetingSystem.MathOperations
{
    public static class Math
    {
        public static float GetSquared(float value)
        {
            return value * value;
        }

        public static float GetSquaredDistance(Vector3 vector1, Vector3 vector2)
        {
            return (vector1 - vector2).sqrMagnitude;
        }

        /// <summary>
        /// Check both floats, assuming they're squared.
        /// </summary>
        /// <returns> Distance 1 is greater than Distance 2 </returns>
        public static bool SquaredDistanceIsHigher(float sqrdDistance1, float sqrdDistance2)
        {
            return Mathf.Abs(sqrdDistance1) > Mathf.Abs(sqrdDistance2);
        }

        /// <summary>
        /// Check both floats, assuming they're squared.
        /// </summary>
        /// <returns> Distance 1 is less than Distance 2 </returns>
        public static bool SquaredDistanceIsLower(float sqrdDistance1, float sqrdDistance2)
        {
            return Mathf.Abs(sqrdDistance1) < Mathf.Abs(sqrdDistance2);
        }
    }
}
