using AkataAcademy.Application.Common;
using System;

namespace AkataAcademy.Application.Catalog.Commands
{
	public record AddModuleToCourseCommand(
		Guid CourseId,
		string ModuleTitle,
		int ModuleDuration
	) : ICommand<Guid>;
}
