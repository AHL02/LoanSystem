using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class AddEndpointsExtension
{
    public static IServiceCollection AddEndpoints(
    this IServiceCollection services,
    Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly
                .DefinedTypes
                .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                               type.IsAssignableTo(typeof(IEndpoint)))
                .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
                .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }
}