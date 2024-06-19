using DotnetService.APIs.Common;
using DotnetService.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class TaskFindMany : FindManyInput<Task, TaskWhereInput> { }
