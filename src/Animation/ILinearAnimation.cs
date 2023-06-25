using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animation
{
    public interface ILinearAnimation : IAnimation
    {
        int Duration { get; set; }
    }
}
