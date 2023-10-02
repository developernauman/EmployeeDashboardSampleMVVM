using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Reflection;
using TinyIoC;

namespace Sample.Common.Ioc
{
    public abstract class BaseResolver
    {
        private readonly List<Assembly> _assemblies = new List<Assembly>();
        private readonly TinyIoCContainer _container = TinyIoCContainer.Current;

        public bool Initialized { get; set; }


        public void Initialize()
        {
            ConfigureAutoRegister(_assemblies);

            _container.AutoRegister(_assemblies, DuplicateImplementationActions.RegisterSingle);

            Initialized = true;
        }


        public T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public void Register<TInt, TImpl>() where TInt : class where TImpl : class, TInt
        {
            if (!Initialized)
                throw new InvalidOperationException("Resolver must be initialized before calling Register");

            _container.Register<TInt, TImpl>();
        }

        public void Register<TInt>(TInt value) where TInt : class
        {
            if (!Initialized)
                throw new InvalidOperationException("Resolver must be initialized before calling Register");

            _container.Register<IHttpContextAccessor, HttpContextAccessor>().AsSingleton();
            _container.Register(value);
        }


        public abstract void ConfigureAutoRegister(List<Assembly> assemblies);
    }
}
