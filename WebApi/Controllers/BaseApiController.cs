using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApi.Controllers
{
    public class BaseApiController: ControllerBase
    {
        protected Services.IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider != null)
                {
                    return _serviceProvider;
                }


                Type serviceType = typeof(Services.IServiceProvider);
                _serviceProvider = (Services.IServiceProvider)HttpContext.RequestServices.GetService(serviceType);
                return _serviceProvider;
            }
        }

        private Services.IServiceProvider _serviceProvider;
        
        protected Services.IServiceProviderSingleton ServiceProviderSingleton
        {
            get
            {
                if (_serviceProviderSingleton != null)
                {
                    return _serviceProviderSingleton;
                }


                Type serviceType = typeof(Services.IServiceProviderSingleton);
                _serviceProviderSingleton = (Services.IServiceProviderSingleton)HttpContext.RequestServices.GetService(serviceType);
                return _serviceProviderSingleton;
            }
        }

        private Services.IServiceProviderSingleton _serviceProviderSingleton;
        
        
        



    }
}
