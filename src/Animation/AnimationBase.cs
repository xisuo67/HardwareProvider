using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Animation
{
    public class AnimationBase : IAnimation
    {
        private System.Timers.Timer m_timer;
        private bool m_isWorking;
        private bool m_isComplete;
        private int m_frameIndex;
        private bool m_isBack;
        private List<IAnimationObserver> m_observers = new List<IAnimationObserver>();
        private string m_name;
        private IAnimation m_nextAnimation;
        private int m_frames = 30;
        private RepeatBehavior m_repeatBehavior = RepeatBehavior.None;
        private int m_repeatCount = 1;
        private int m_repeatRemainCount;
        private double m_delay;
        private double? m_timelineOffset;

        private event Action AnimationPrepare;
        private event Func<bool> AnimationUpdated;
        public event Action<IAnimation> Started;
        public event Action<IAnimation> Stoped;

        public AnimationBase()
        {
            this.m_timer = new System.Timers.Timer();
            this.AnimationPrepare += AnimationBase_AnimationPrepare;
            this.AnimationUpdated += AnimationBase_AnimationUpdated;
        }

        public bool IsWorking
        {
            get
            {
                return this.m_isWorking;
            }
        }

        public bool IsComplete
        {
            get
            {
                return this.m_isComplete;
            }
        }

        public int FrameIndex
        {
            get
            {
                return this.m_frameIndex;
            }
        }

        public bool IsBack
        {
            get
            {
                return m_isBack;
            }
        }

        public double Interval
        {
            get
            {
                return (double)1000 / this.m_frames;
            }
        }

        public string Name
        {
            get
            {
                return this.m_name;
            }
            set
            {
                this.m_name = value;
            }
        }

        public IAnimation NextAnimation
        {
            get
            {
                return this.m_nextAnimation;
            }
            set
            {
                if (this.m_isWorking || this.m_nextAnimation == value)
                {
                    return;
                }
                this.m_nextAnimation = value;
            }
        }

        public int Frames
        {
            get
            {
                return this.m_frames;
            }
            set
            {
                if (this.m_isWorking || this.m_frames == value)
                {
                    return;
                }
                this.m_frames = value;
            }
        }

        public RepeatBehavior RepeatBehavior
        {
            get
            {
                return this.m_repeatBehavior;
            }
            set
            {
                if (this.m_isWorking || this.m_repeatBehavior == value)
                {
                    return;
                }
                this.m_repeatBehavior = value;
            }
        }

        public int RepeatCount
        {
            get
            {
                return this.m_repeatCount;
            }
            set
            {
                if (this.m_isWorking || this.m_repeatCount == value)
                {
                    return;
                }
                this.m_repeatCount = value;
            }
        }

        public double Delay
        {
            get
            {
                return this.m_delay;
            }
            set
            {
                if (this.m_isWorking || this.m_delay == value)
                {
                    return;
                }
                this.m_delay = value;
            }
        }

        public double? TimelineOffset
        {
            get
            {
                return this.m_timelineOffset;
            }
            set
            {
                if (this.m_isWorking || this.m_timelineOffset == value)
                {
                    return;
                }
                this.m_timelineOffset = value;
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!this.m_isWorking)
            {
                this.m_isWorking = true;
                this.m_timer.Interval = this.Interval;
            }
            if (this.AnimationUpdated != null)
            {
                bool isComplete = this.AnimationUpdated.Invoke();
                if (isComplete)
                {
                    switch (this.m_repeatBehavior)
                    {
                        case RepeatBehavior.None:
                            this.Stop();
                            break;
                        case RepeatBehavior.Repeat:
                            this.m_repeatRemainCount--;
                            if (this.m_repeatRemainCount == 0)
                            {
                                this.Stop();
                            }
                            else
                            {
                                this.m_frameIndex = 0;
                            }
                            break;
                        case RepeatBehavior.Back:
                            this.m_repeatRemainCount--;
                            if (this.m_repeatRemainCount == 0)
                            {
                                this.Stop();
                            }
                            else
                            {
                                this.m_isBack = !this.m_isBack;
                            }
                            break;
                    }
                }
                foreach (IAnimationObserver observer in this.m_observers)
                {
                    observer.AnimationUpdated(this);
                }
            }
            if (this.m_isWorking)
            {
                if (!this.m_isBack)
                {
                    this.m_frameIndex++;
                }
                else
                {
                    this.m_frameIndex--;
                }
            }
        }

        protected virtual void AnimationBase_AnimationPrepare()
        {
            throw new NotImplementedException();
        }

        protected virtual bool AnimationBase_AnimationUpdated()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            if (!this.m_isWorking)
            {
                this.m_isComplete = false;
                this.m_frameIndex = 0;
                this.m_isBack = false;
                this.m_repeatRemainCount =
                    this.m_repeatBehavior == RepeatBehavior.Back ? this.m_repeatCount * 2 : this.m_repeatCount;
                this.m_timer.Interval = (this.m_delay < 1 ? 1 : this.m_delay)
                    + (this.m_timelineOffset != null
                        && this.m_timelineOffset.Value > 0D ? this.m_timelineOffset.Value : 0D);
                this.m_timer.Elapsed += Timer_Elapsed;
                if (this.AnimationPrepare != null)
                {
                    this.AnimationPrepare.Invoke();
                }
                this.m_timer.Start();
                if (this.m_nextAnimation != null)
                {
                    if (this.m_nextAnimation.TimelineOffset != null)
                    {
                        this.m_nextAnimation.Start();
                    }
                }
                if (this.Started != null)
                {
                    this.Started.Invoke(this.m_nextAnimation);
                }
            }
        }

        public void Stop()
        {
            if (this.m_isWorking)
            {
                this.m_timer.Elapsed -= Timer_Elapsed;
                this.m_timer.Stop();
                this.m_isWorking = false;
                this.m_isComplete = true;
                if (this.m_nextAnimation != null)
                {
                    if (this.m_nextAnimation.TimelineOffset == null)
                    {
                        this.m_nextAnimation.Start();
                    }
                }
                if (this.Stoped != null)
                {
                    this.Stoped.Invoke(this.m_nextAnimation);
                }
            }
        }

        public void AddObserver(IAnimationObserver observer)
        {
            if (!this.m_observers.Contains(observer))
            {
                this.m_observers.Add(observer);
            }
        }

        public void RemoveObserver(IAnimationObserver observer)
        {
            if (this.m_observers.Contains(observer))
            {
                this.m_observers.Remove(observer);
            }
        }
    }

    public enum RepeatBehavior
    {
        None = 0,
        Repeat = 1,
        Back = 2
    }
}