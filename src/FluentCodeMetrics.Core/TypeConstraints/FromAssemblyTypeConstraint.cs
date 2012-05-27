using System;
using System.Reflection;
using ThrowHelper;

namespace FluentCodeMetrics.Core.TypeConstraints
{
    public class FromAssemblyTypeConstraint : TypeConstraint
    {
        private readonly Assembly assemblyField;

        internal FromAssemblyTypeConstraint(Assembly assembly)
        {
            assemblyField = assembly;
        }

        public override bool Check(Type type)
        {
            Throw.IfArgumentNull(type, "type");
            return type.Assembly == assemblyField;
        }
    }
}