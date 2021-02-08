using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Units.State
{
    public enum StateTypes : byte
    {
        Idle = 0,
        Walking = 1,
        Attacking = 2,
        Dead = 4
    }
}
