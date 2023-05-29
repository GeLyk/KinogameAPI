namespace API
{
    public class DefaultApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            builder
                .RegisterMediatR(assemblies);
        }
    }
}
