using UnityEngine;

namespace HealthSystem.MathOperations
{
    public static class Math
    {
        public static float GetNormalizedValueMinMax(float current, float min, float max)
        {
            float normalized = (current - min) / (max - min);
            return normalized;
        }
        
        /// <summary>
        /// Min value is always 0.
        /// </summary>
        public static float GetNormalizedValueMax(float current, float max)
        {
            float normalized = current / max;
            return normalized;
        }
    }
}
