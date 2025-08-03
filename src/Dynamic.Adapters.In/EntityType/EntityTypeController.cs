

using Dynamic.Application.Ports.In.CreateEntityType;
using Dynamic.Application.Ports.In.EntityTypeQuery;
using Dynamic.Application.Ports.In.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Dynamic.Adapters.In.EntityType
{
    [Route("api/entity-type")]
    [ApiController]

    public class EntityTypeController : ControllerBase
    {
        private readonly ICreateEntityTypeUseCase _createEntityTypeUseCase;
        private readonly IEntityTypeQueryUseCase _entityTypeQueryUseCase;

        public EntityTypeController(
            ICreateEntityTypeUseCase createEntityTypeUseCase,
            IEntityTypeQueryUseCase entityTypeQueryUseCase)
        {
            _createEntityTypeUseCase = createEntityTypeUseCase;
            _entityTypeQueryUseCase = entityTypeQueryUseCase;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateEntityTypeRequest request)
        {
            var entityTypeInput = new CreateEntityTypeInput
            {
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
                Id = entityTypeOutput.Id,
                Name = entityTypeOutput.Name
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entityTypes = await _entityTypeQueryUseCase.GetAllAsync();
            return Ok(entityTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var entityType = await _entityTypeQueryUseCase.GetByIdAsync(id);
            if (entityType == null)
            {
                return NotFound();
            }
            return Ok(entityType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateEntityTypeRequest request)
        {
            var updated = await _entityTypeQueryUseCase.UpdateAsync(id, request.Name);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }
    }
}
