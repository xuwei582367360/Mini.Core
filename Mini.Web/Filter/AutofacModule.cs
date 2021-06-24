using Autofac;
using Microsoft.AspNetCore.Mvc;
using Mini.Common.Cache;
using Mini.Extensions.ServiceExtensions.Jwt;
using System.Linq;

namespace Mini.Web.Filter
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var controllerBaseType = typeof(ControllerBase);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();
            builder.RegisterType<JwtAppService>().AsImplementedInterfaces().PropertiesAutowired();    //注册jwt
            builder.RegisterType(typeof(CacheContext)).As(typeof(ICacheContext));   //注册cache缓存
        }
    }
}


