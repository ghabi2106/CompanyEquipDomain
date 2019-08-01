using Autofac;
using Autofac.Integration.Mvc;
using CompanyEquip.Autofac;
using Domain.Context;
using System.Reflection;
using System.Web.Mvc;
using System.Linq;
using Microsoft.AspNet.Identity;
using Models.Constants;
using Domain.Entities.Identity;
using Services;
using Domain.Repositories;
using CompanyEquip.Controllers;
using Domain.Entities;

namespace CompanyEquip.App_Start
{

    public class AutofacConfig
    {
        /*Quelques soit le container utilisé(container natif ou Autofac), il faut privilégier l’injection par 
         * le constructeur de façon à ce que les dépendances d’une classe soient facilement visibles.*/

        public static void Run()
        {
            var builder = new ContainerBuilder();
            RegisterDependencies(builder);
        }

        public static void RegisterDependencies(ContainerBuilder builder)
        {

            // Register the web controller (defined in Autofac.Integration.Mvc)
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(typeof(EquipmentsController).Assembly);

            builder.RegisterType<DataContext>().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(EquipmentRepository).Assembly)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(EquipmentService).Assembly)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces().InstancePerRequest();

            //builder.RegisterType<ApplicationUserStore<User>>().AsSelf().InstancePerRequest();
            //builder.RegisterType<UserManager<User>>().AsSelf().InstancePerRequest();

            // Register the modules
            builder.RegisterModule(new ApplicationNameModule(ApplicationConstants.CompanyEquip));

            // Resolve the container
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }

}