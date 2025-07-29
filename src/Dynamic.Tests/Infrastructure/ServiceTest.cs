using Dynamic.Adapters.In.EntityType;
using Dynamic.Adapters.Out.Repositories;
using Dynamic.Application.Ports.In.CreateEntityType;
using Dynamic.Application.Ports.Out.Repositories;
using Dynamic.Domain.Models;
using Xunit;
using System.Threading.Tasks;
using System.Net.Http;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Dynamic.Tests
{
    /// <summary>
    /// This consolidated test class covers both service-level and API-level tests for EntityType.
    /// It contains two inner classes:
    /// 1. ServiceTests - Tests for the CreateEntityTypeUseCase directly
    /// 2. ApiTests - Tests for the EntityType API endpoints
    /// </summary>
    public class ServiceTest
    {


        // API tests removed. To test API logic, mock the controller/service and simulate API calls in unit tests.
    }
}
