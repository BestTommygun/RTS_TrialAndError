using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Units.Grouping
{
    public struct GroupFormation
    {
        public string name;
        public List<(GameObject, SingleUnit)> idealformation;
    }
}
