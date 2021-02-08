using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Units.State
{
    public class IdleState : MonoBehaviour, IUnitState
    {
        public StateTypes stateType = StateTypes.Idle;
        void Update()
        {
            HandleState();
        }
        public void HandleState()
        {
            //check if any stuff for state changes

            //play idle anim?
            throw new NotImplementedException();
        }
    }
}
