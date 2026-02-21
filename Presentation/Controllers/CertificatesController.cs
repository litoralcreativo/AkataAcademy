using AkataAcademy.Application.Certification.Commands;
using AkataAcademy.Application.Certification.DTOs;
using AkataAcademy.Application.Certification.Queries;
using AkataAcademy.Application.Common;
using AkataAcademy.Application.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace AkataAcademy.Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CertificatesController : ControllerBase
	{

		private readonly ILogger<CertificatesController> _logger;
		private readonly IQueryDispatcher _queryDispatcher;
		private readonly ICommandDispatcher _commandDispatcher;

		public CertificatesController(ILogger<CertificatesController> logger, IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
		{
			_logger = logger;
			_queryDispatcher = queryDispatcher;
			_commandDispatcher = commandDispatcher;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<CertificateDto>> GetById(Guid id)
		{
			GetCertificateByIdQuery query = new GetCertificateByIdQuery(id);

			var result = await _queryDispatcher.Dispatch(query);
			if (!result.IsSuccess)
				return NotFound(result.Error);
			return Ok(result.Value);
		}

		[HttpGet("valid")]
		public async Task<ActionResult<IEnumerable<CertificateDto>>> GetValidCertificates()
		{
			var result = await _queryDispatcher.Dispatch(new GetValidCertificatesQuery());
			if (!result.IsSuccess)
				return NotFound(result.Error);
			return Ok(result.Value);
		}

		[HttpPost]
		public async Task<ActionResult<Guid>> IssueCertificate([FromBody] IssueCertificateCommand command)
		{
			var result = await _commandDispatcher.Dispatch(command);
			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
		}
	}
}
