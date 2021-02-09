using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Units.Grouping.Formations
{

    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class FormationAttribute : Attribute
    {
        public string Name { get; }

        // This is a positional argument
        public FormationAttribute(string name)
        {
            this.Name = name;
        }
    }
}
