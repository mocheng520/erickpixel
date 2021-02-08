
namespace TargetingSystem.MathOperations
{
    public static class MillisecondsConverter
    {
        private const int milliseconds = 1000;

        public static int ToMilliseconds(this float value)
        {
            float seconds = value * milliseconds;

            return (int)seconds;
        }
    }
}