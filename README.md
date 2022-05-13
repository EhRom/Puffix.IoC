# Puffix IoC

Minimal library to map your IoC container in your project, and remain independent from IoC Framework in your base code.

[![NuGet version (Puffix.IoC)](https://img.shields.io/nuget/v/Puffix.IoC.svg?style=flat-square)](https://www.nuget.org/packages/Puffix.IoC/)
[![Build status](https://github.com/EhRom/Puffix.IoC/workflows/.NET%20Core/badge.svg)](https://github.com/EhRom/Puffix.IoC/actions?query=workflow%3A%22.NET+Core%22)

In the sample below, [Autofac](https://autofac.org/) is used.

First, a base class is needed to reference the objects to map:
``` csharp
using Autofac;
using Puffix.IoC;

namespace YourAppName;

public class IoCContainer : IIoCContainer
{
    private readonly IContainer? container;

    public IoCContainer(ContainerBuilder containerBuilder)
    { 
        // Self-register the container.
        containerBuilder.Register(_ => this).As<IIoCContainer>().SingleInstance();

        container = containerBuilder.Build();
    }

    public static IIoCContainer BuildContainer()
    {
        ContainerBuilder containerBuilder = new ContainerBuilder();
        containerBuilder.RegisterAssemblyTypes
                        (
                            typeof(IoCContainer).Assembly // Current Assembly.
                        )
                        .AsSelf()
                        .AsImplementedInterfaces();

        containerBuilder.RegisterInstance(configuration).SingleInstance();

        return new IoCContainer(containerBuilder);
    }

    public ObjectT Resolve<ObjectT>(params IoCNamedParameter[] parameters)
        where ObjectT : class
    {
        if (container == null)
            throw new ArgumentNullException($"The class {GetType().Name} is not well initialized.");

        ObjectT resolvedObject;
        if (parameters != null)
            resolvedObject = container.Resolve<ObjectT>(ConvertIoCNamedParametersToAutfac(parameters));
        else
            resolvedObject = container.Resolve<ObjectT>();

        return resolvedObject;
    }

    public object Resolve(Type objectType, params IoCNamedParameter[] parameters)
    {
        if (container == null)
            throw new ArgumentNullException($"The class {GetType().Name} is not well initialized.");

        object resolvedObject;
        if (parameters != null)
            resolvedObject = container.Resolve(objectType, ConvertIoCNamedParametersToAutfac(parameters));
        else
            resolvedObject = container.Resolve(objectType);

        return resolvedObject;
    }

    private IEnumerable<NamedParameter> ConvertIoCNamedParametersToAutfac(IEnumerable<IoCNamedParameter> parameters)
    {
        foreach (var parameter in parameters)
        {
            if (parameter != null)
                yield return new NamedParameter(parameter.Name, parameter.Value);
        }
    }
}
```


The container must be initialized:
``` csharp
IIoCContainer container = IoCContainer.BuildContainer();
```

An the you can resolve your objects in the code:
``` csharp
MyObject myObject = container.Resolve<MyObject>();
```
