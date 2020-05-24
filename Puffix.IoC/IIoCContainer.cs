using System;

namespace Puffix.IoC
{
    public interface IIoCContainer
    {
        ObjectT Resolve<ObjectT>(params IoCNamedParameter[] parameters);

        object Resolve(Type objectType, params IoCNamedParameter[] parameters);
    }
}
