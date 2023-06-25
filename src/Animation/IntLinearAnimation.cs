using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animation
{
    public class IntLinearAnimation : AnimationBase, ILinearAnimation
    {
        private int m_duration;
        private int m_from;
        private int m_to;
        private int m_step;
        private List<double> m_keyFrames = new List<double>();

        public IntLinearAnimation() { }

        public IntLinearAnimation(string name)
            : this()
        {
            this.Name = name;
        }

        public int Current
        {
            get
            {
                return (int)this.m_keyFrames[this.FrameIndex];
            }
        }

        public int Duration
        {
            get
            {
                return this.m_duration;
            }
            set
            {
                if (this.IsWorking || this.m_duration == value)
                {
                    return;
                }
                this.m_duration = value;
            }
        }

        public int From
        {
            get
            {
                return this.m_from;
            }
            set
            {
                if (this.IsWorking || this.m_from == value)
                {
                    return;
                }
                this.m_from = value;
            }
        }

        public int To
        {
            get
            {
                return this.m_to;
            }
            set
            {
                if (this.IsWorking || this.m_to == value)
                {
                    return;
                }
                this.m_to = value;
            }
        }

        public int Step
        {
            get
            {
                return this.m_step;
            }
            set
            {
                if (this.IsWorking || this.m_step == value)
                {
                    return;
                }
                this.m_step = value;
            }
        }

        private void CalculateKeyFrames()
        {
            this.m_keyFrames.Clear();
            double step =
                this.m_step > 0 ? this.m_step : (this.m_to - this.m_from) / (this.m_duration / this.Interval);
            int i = 0;
            do
            {
                this.m_keyFrames.Add(this.m_from + i * step);
                i++;
            } while (Math.Abs((this.m_to - this.m_keyFrames[this.m_keyFrames.Count - 1])) >= step);
            this.m_keyFrames.Add(this.m_to);
        }

        protected override void AnimationBase_AnimationPrepare()
        {

            this.CalculateKeyFrames();
        }

        protected override bool AnimationBase_AnimationUpdated()
        {
            bool isComplete = false;
            if (!this.IsBack)
            {
                if (this.FrameIndex == this.m_keyFrames.Count - 1)
                {
                    isComplete = true;
                }
            }
            else
            {
                if (this.FrameIndex == 0)
                {
                    isComplete = true;
                }
            }
            return isComplete;
        }
    }
}
