using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class RandomTimer
    {
        private float timeMin;
        private float timeMax;
        private float remainingSeconds;

        public RandomTimer(float timeMin, float timeMax)
        {
            this.timeMin = timeMin;
            this.timeMax = timeMax;
        }

        public virtual void Reset()
        {
            remainingSeconds = RandomGenerator.GetRandomFloat() * (timeMax - timeMin) + timeMin;
        }

        public virtual void Cancel()
        {
            remainingSeconds = 0.0f;
        }

        public virtual void Tick()
        {
            remainingSeconds -= Game.DeltaTime;

            if(remainingSeconds <= 0)
            {
                remainingSeconds = 0;
            }
        }

        public virtual bool IsOver()
        {
            return remainingSeconds <= 0;
        }
    }
}
