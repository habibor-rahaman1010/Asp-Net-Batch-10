using Autofac;
using DevSkill.Inventory.Web.Service;

namespace DevSkill.Inventory.Web.WebModules
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
        }
    }
}
