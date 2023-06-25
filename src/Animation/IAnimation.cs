using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animation
{
    public interface IAnimation
    {
        event Action<IAnimation> Started;

        event Action<IAnimation> Stoped;

        bool IsWorking { get; }

        bool IsComplete { get; }

        int FrameIndex { get; }

        bool IsBack { get; }

        double Interval { get; }

        string Name { get; set; }

        IAnimation NextAnimation { get; set; }

        int Frames { get; set; }

        RepeatBehavior RepeatBehavior { get; set; }

        int RepeatCount { get; set; }

        double Delay { get; set; }

        double? TimelineOffset { get; set; }

        void Start();

        void Stop();

        void AddObserver(IAnimationObserver observer);

        void RemoveObserver(IAnimationObserver observer);
    }
}
