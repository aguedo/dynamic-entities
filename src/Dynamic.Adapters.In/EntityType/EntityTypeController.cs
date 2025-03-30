using Dynamic.Application.Ports.In.CreateEntityType;
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

        [HttpGet("create")]
        public async Task<IActionResult> Create(CreateEntityTypeRequest request)
        {
            var entityTypeInput = new CreateEntityTypeInput
            {
                // Populate the input object using the request
            };
            CreateEntityTypeOutput output = await _createEntityTypeUseCase.CreateAsync(entityTypeInput);

            var response = new CreateEntityTypeResponse
            {
                // Populate the response object using the output
            };
            return Ok(response);
        }
    }
}
