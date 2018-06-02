using Autofac;
using Autofac.Integration.WebApi;
using WebAPIMongoDBExample.Negocio;
using WebAPIMongoDBExample.Repository;
using System.Reflection;
using System.Web.Http;

namespace WebAPIMongoDBExample.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UtilizacaoRepository>().As<IUtilizacaoRepository>().InstancePerRequest();
            builder.RegisterType<ImobilizadoRepository>().As<IImobilizadoRepository>().InstancePerRequest();
            builder.RegisterType<TipoImobilizadoRepository>().As<ITipoImobilizadoRepository>().InstancePerRequest();
            
            builder.RegisterType<DisponibilidadeNegocio>().As<IDisponibilidadeNegocio>().InstancePerRequest();
            builder.RegisterType<ImobilizadoNegocio>().As<IImobilizadoNegocio>().InstancePerRequest();
            builder.RegisterType<TipoImobilizadoNegocio>().As<ITipoImobilizadoNegocio>().InstancePerRequest();
            builder.RegisterType<UtilizacaoNegocio>().As<IUtilizacaoNegocio>().InstancePerRequest();

            Container = builder.Build();
            return Container;
        }
    }
}