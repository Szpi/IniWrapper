using IniWrapper.Attribute;
using IniWrapper.Creator.MemberInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IniWrapper.Creator
{
    public class ImmutableTypeCreator : IImmutableTypeCreator
    {
        private readonly IConstructorParametersProvider _constructorParametersProvider;

        public ImmutableTypeCreator(IConstructorParametersProvider constructorParametersProvider)
        {
            _constructorParametersProvider = constructorParametersProvider;
        }

        public bool HasConstructorWithAttribute(Type configurationType)
        {
            return configurationType.GetConstructors().Any(x => x.GetCustomAttribute<IniConstructor>() != null);
        }

        public object Instantiate(Type configurationType)
        {
            var constructor = configurationType.GetConstructors().First(x => x.GetCustomAttribute<IniConstructor>() != null);

            var parameters = constructor.GetParameters();

            var setParameters = _constructorParametersProvider.GetConstructorParameters();

            var constructorParameters = new object[parameters.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                var mostFitValueForParameter = GetParameter(setParameters, parameters[i]);
                constructorParameters[i] = mostFitValueForParameter;
            }
            return constructor.Invoke(constructorParameters);
        }

        private object GetParameter(IReadOnlyDictionary<string, object> setParameters, ParameterInfo parameter)
        {
            return setParameters.TryGetValue(parameter.Name, out var result) ? result : 
                setParameters.FirstOrDefault(x => string.Compare(parameter.Name, x.Key, StringComparison.InvariantCultureIgnoreCase) == 0).Value;
        }
    }
}