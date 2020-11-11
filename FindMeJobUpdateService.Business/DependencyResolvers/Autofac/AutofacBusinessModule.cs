using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using FindMeJobUpdateService.Business.Abstract;
using FindMeJobUpdateService.Business.Concrete;
using FindMeJobUpdateService.Core.Utilities.Interceptors;
using FindMeJobUpdateService.DataAccess.Abstract;
using FindMeJobUpdateService.DataAccess.Concrete;

namespace FindMeJobUpdateService.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JobsManager>().As<IJobsService>();
            builder.RegisterType<EfJobsDal>().As<IJobsDal>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(
                    new ProxyGenerationOptions()
                    {
                        Selector = new AspectInterceptorSelector()
                    }).SingleInstance();
        }
    }
}