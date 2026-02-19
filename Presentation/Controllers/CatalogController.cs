using System.Threading.Tasks;
using AkataAcademy.Application.Catalog.Commands;
using AkataAcademy.Application.Catalog.DTOs;
using AkataAcademy.Application.Catalog.Queries;
using AkataAcademy.Application.Common;
using AkataAcademy.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace AkataAcademy.Presentation.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CatalogController : ControllerBase
	{
		private readonly ILogger<CatalogController> _logger;
		private readonly IQueryDispatcher _queryDispatcher;
		private readonly ICommandDispatcher _commandDispatcher;

		public CatalogController(
			ILogger<CatalogController> logger,
			IQueryDispatcher queryDispatcher,
			ICommandDispatcher commandDispatcher)
		{
			_logger = logger;
			_queryDispatcher = queryDispatcher;
			_commandDispatcher = commandDispatcher;
		}

		[HttpGet(Name = "GetCatalog")]
		public async Task<IEnumerable<CourseDto>> Get([FromQuery] bool published = false)
		{

			if (published)
			{
				var query = new GetPublishedCoursesQuery();
				var result = await _queryDispatcher.Dispatch(query);
				return result.Value;
			}
			else
			{
				var query = new GetNotPublishedCoursesQuery();
				var result = await _queryDispatcher.Dispatch(query);
				return result.Value;
			}
		}

		[HttpPost(Name = "CreateCourse")]
		public async Task<ActionResult<Guid>> Create([FromBody] CreateCourseCommand command)
		{
			var result = await _commandDispatcher.Dispatch(command);
			return Ok(result);
		}
	}
}