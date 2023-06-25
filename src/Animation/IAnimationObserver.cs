using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animation
{
    public interface IAnimationObserver
    {
        void AnimationUpdated(IAnimation animation);
    }
}
