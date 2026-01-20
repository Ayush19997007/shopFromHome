using eCommerce.BusinessLogicLayer.Mappers;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using eCommerce.BusinessLogicLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.ProductsService.BusinessLogicLayer
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayerServices(this IServiceCollection services)
        {
            // Register your data access layer services here
            // services.AddScoped<IYourRepository, YourRepositoryImplementation>();
            // Add more services as needed
            services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);//we are refferring entire assemble not a single class
            services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>(); // Fix: use correct method name
            
            services.AddScoped<IProductsService, eCommerce.BusinessLogicLayer.Services.ProductsService>();
            return services;
        }
    }
}
