using Module = Autofac.Module;

namespace Application
{
    public class DefaultApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
        }
    }
}
