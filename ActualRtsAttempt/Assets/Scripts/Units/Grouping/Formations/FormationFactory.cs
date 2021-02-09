using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Units.Grouping.Formations
{
    public class FormationFactory
    {
        private Dictionary<string, Func<IFormation>> _parsers = typeof(FormationFactory).Assembly.GetTypes()       //get all types in the assembly
            .Where(t => t.GetInterfaces().Contains(typeof(IFormation)))                                         //get all classes that implement the IParser interface
            .Where(t => !t.IsAbstract && !t.IsArray && !t.IsGenericType && !t.IsInterface && !t.IsValueType) //get all classes that are not abstract, array, generic, interface or struct
            .Where(t => t.GetConstructors().Any(c => c.GetParameters().Length == 0))                         //get all classes with a default constructor
            .SelectMany(t => t.GetCustomAttributes(typeof(FormationAttribute), false)                                        //get all attributes of type parserAttribute on the classes
                .Select(a => new Tuple<string, Func<IFormation>>(((FormationAttribute)a).Name, () =>                                  //create a new tuple with the name of the parser and its constructor
                { return (IFormation)t.GetConstructors().Single(c => c.GetParameters().Length == 0).Invoke(new object[0]); })))
            .ToDictionary(t => t.Item1, t => t.Item2);                                                       //convert this IEnumerable to Dictionary

        public IFormation returnFormation(string formationName)
        {
            if (!string.IsNullOrEmpty(formationName) && !string.IsNullOrWhiteSpace(formationName))
            {
                
                return _create(formationName);
            }
            else throw new ArgumentNullException();
        }

        private IFormation _create(string formationName)
        {
            return _parsers[formationName]();
        }
    }
}
