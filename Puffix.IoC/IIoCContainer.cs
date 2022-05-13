using System;

namespace Puffix.IoC
{
    public interface IIoCContainer
    {
        ObjectT Resolve<ObjectT>(params IoCNamedParameter[] parameters)
            where ObjectT : class;

        object Resolve(Type objectType, params IoCNamedParameter[] parameters);
    }
}
