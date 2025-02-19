using HospitalInformationSystem.Application.Commands;
using HospitalInformationSystem.Application.Commands.Patient;
using HospitalInformationSystem.Application.Common.Models;
using HospitalInformationSystem.Application.DTOs;
using HospitalInformationSystem.Application.Queries.Patient;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreatePatientDto patient)
    {
        var command = new CreatePatientCommand { Patient = patient };
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeletePatientCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<PatientDto>>> Get(
        [FromQuery] GetPatientsQuery query)
    {

        var result = await _mediator.Send(query);
        return Ok(result.Items);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdatePatientDto patient)
    {
        var command = new UpdatePatientCommand { Id = id, Patient = patient };
        await _mediator.Send(command);
        return NoContent();
    }
}