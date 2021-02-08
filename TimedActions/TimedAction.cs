using System;

namespace PixelRTS.TimedActions
{
    public class TimedAction
    {
        public TimedAction(float duration, Action onFinish)
        {
            this.duration = duration;

            OnFinished = onFinish;
        }

        [NonSerialized]
        private float timer;
        private readonly float duration;
        private event Action OnFinished;

        public float progressPercentage
        {
            get
            {
                return (timer / duration) * 100;
            }
        }

        private void FinshedActionCall()
        {
            OnFinished();
        }

        public void Process(float deltaTime)
        {
            timer = Math.Min(duration, timer + deltaTime);
            
            if(timer >= duration)
                FinshedActionCall();
        }
    }
}