using Autofac;
using Autofac.Core;
using System.Linq;

namespace CompanyEquip.Autofac
{
    public class ApplicationNameModule : Module
    {
        private string applicationName;
        public ApplicationNameModule(string applicationName)
        {
            this.applicationName = applicationName;
        }

        protected override void AttachToComponentRegistration(
          IComponentRegistry componentRegistry,
          IComponentRegistration registration)
        {
            // Any time a component is resolved, it goes through Preparing
            registration.Preparing += InjectLangParameter;
        }

        protected void InjectLangParameter(object sender, PreparingEventArgs e)
        {
            // Add your named parameter to the list of available parameters.
            e.Parameters = e.Parameters.Union(
              new[] { new NamedParameter("applicationName", this.applicationName) });
        }
    }
}