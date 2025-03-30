using Dynamic.Application.Ports.In.CreateEntityType;
using Dynamic.Application.Ports.In.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Dynamic.Adapters.In.EntityType
{
    [Route("api/entity-type")]
    [ApiController]
    public class EntityTypeController : ControllerBase
    {
        private readonly ICreateEntityTypeUseCase _createEntityTypeUseCase;

        public EntityTypeController(ICreateEntityTypeUseCase createEntityTypeUseCase)
        {
            _createEntityTypeUseCase = createEntityTypeUseCase;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateEntityTypeRequest request)
        {
            var entityTypeInput = new CreateEntityTypeInput
            {
                // Populate the input object using the request
                Name = request.Name
            };

            Output<CreateEntityTypeOutput> output = await _createEntityTypeUseCase.CreateAsync(entityTypeInput);
            if (!output.IsValid)
            {
                return BadRequest(output.Errors);
            }

            CreateEntityTypeOutput entityTypeOutput = output.Data!;
            var response = new CreateEntityTypeResponse
            {
                // Populate the response object using the output
                Id = entityTypeOutput.Id,
                Name = entityTypeOutput.Name
            };
            return Ok(response);
        }
    }
}
