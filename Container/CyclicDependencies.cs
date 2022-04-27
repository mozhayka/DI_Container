using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    public class CyclicDependencies
    {
        private Type type;
        private Dictionary<Type, HashSet<Type>> dependence;

        public CyclicDependencies(Type type)
        {
            this.type = type;
            dependence = new();
        }

        HashSet<Type> nonRecursiveTypes, used;        
        
        public bool Check()
        {
            try
            {
                nonRecursiveTypes = new HashSet<Type> { typeof(int), typeof(string) }; // add other simple types here
                used = new();
                RecursiveCheckFields(type);
            }
            catch(CyclicDependenceException)
            {
                return false;
            }
            return true;
        }

        private void RecursiveCheckFields(Type type)
        {
            if (used.Contains(type))
                throw new CyclicDependenceException($"type {type.Name} has cyclic dependencies");
            
            used.Add(type);
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields)
            {
                var innerType = field.FieldType;
                if (!nonRecursiveTypes.Contains(innerType))
                {
                    if (!dependence.ContainsKey(type))
                        dependence[type] = new();

                    dependence[type].Add(innerType);
                    RecursiveCheckFields(innerType);
                }
            }
        }
    }
}
