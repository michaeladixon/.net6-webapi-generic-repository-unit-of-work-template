

namespace Logic.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceImplementationAttribute : Attribute
    {

        public Type ServiceInterface { get; }

        public Enums.DependencyInjectionLifetime ServiceLifetime { get; }

        public ServiceImplementationAttribute(Type serviceInterface, Enums.DependencyInjectionLifetime serviceLifetime = Enums.DependencyInjectionLifetime.Transient)
        {
            ServiceInterface = serviceInterface;
            ServiceLifetime = serviceLifetime;
        }

    }
}
