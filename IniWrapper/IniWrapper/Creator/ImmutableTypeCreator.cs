using IniWrapper.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IniWrapper.Creator
{
    internal class ImmutableTypeCreator : IImmutableTypeCreator
    {
        private readonly Dictionary<string, object> _constructorParameters = new Dictionary<string, object>();
        public object Instantiate(Type configurationType)
        {
            var constructor = configurationType.GetConstructors().First(x => x.GetCustomAttribute<IniConstructor>() != null);

            var parameters = constructor.GetParameters();

            var constructorParameters = new object[parameters.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                var mostFitValueForParameter = GetParameter(parameters[i]);
                constructorParameters[i] = mostFitValueForParameter;
            }
            return constructor.Invoke(constructorParameters);
        }

        public void AddConstructorParameter(string parameterName, object value)
        {
            _constructorParameters.Add(parameterName, value);
        }

        private object GetParameter(ParameterInfo parameter)
        {
            return _constructorParameters.TryGetValue(parameter.Name, out var result) ? result :
                _constructorParameters.FirstOrDefault(x => string.Compare(parameter.Name, x.Key, StringComparison.InvariantCultureIgnoreCase) == 0).Value;
        }
    }
}