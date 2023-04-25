using LocadoraVeiculos.Repository.EFCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LocadoraVeiculos.IntegrationTests.Setup
{
    public class IntegrationTestBase : IClassFixture<IntegrationTestFactory<Program, LocadoraVeiculosContext>>
    {
        public readonly IntegrationTestFactory<Program, LocadoraVeiculosContext> _factory;
        public readonly LocadoraVeiculosContext _dbContext;

        public IntegrationTestBase(IntegrationTestFactory<Program, LocadoraVeiculosContext> factory)
        {
            _factory = factory;
            var scope = factory.Services.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<LocadoraVeiculosContext>();
        }
    }
}
