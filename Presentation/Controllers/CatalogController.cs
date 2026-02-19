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

		public CatalogController(
			ILogger<CatalogController> logger,
			IQueryHandler<GetPublishedCoursesQuery, IEnumerable<CourseDto>> handler)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetCatalog")]
		public async Task<IEnumerable<CourseDto>> Get([FromQuery] bool published = false)
		{

			if (published)
			{
				var handler = HttpContext.RequestServices.GetService<IQueryHandler<GetPublishedCoursesQuery, IEnumerable<CourseDto>>>();
				var query = new GetPublishedCoursesQuery();
				Result<IEnumerable<CourseDto>> result = await handler.Handle(query);
				return result.Value;
			}
			else
			{
				var handler = HttpContext.RequestServices.GetService<IQueryHandler<GetNotPublishedCoursesQuery, IEnumerable<CourseDto>>>();
				var query = new GetNotPublishedCoursesQuery();
				Result<IEnumerable<CourseDto>> result = await handler.Handle(query);
				return result.Value;
			}
		}

		[HttpPost(Name = "CreateCourse")]
		public async Task<ActionResult<Guid>> Create([FromBody] CreateCourseCommand command)
		{
			var handler = HttpContext.RequestServices.GetService<ICommandHandler<CreateCourseCommand, Guid>>();
			if (handler == null)
				return StatusCode(500, "Handler not found");

			var result = await handler.Handle(command);
			return Ok(result);
		}
	}
}