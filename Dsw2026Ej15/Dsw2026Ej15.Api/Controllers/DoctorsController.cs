using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

public class DoctorsController : AppController
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpPost("doctors")]
    public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
    {
        if(string.IsNullOrWhiteSpace(request.Name) ||
            string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            return BadRequest("Nombre y Matricula son requeridos");
        }

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if(speciality is null)
        {
            return BadRequest("Especialidad no existe");
        }

        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        _persistence.SaveDoctor(doctor);

        return Created(); //201
    }

    [HttpGet("doctors")]
    public ActionResult<IEnumerable<Doctor>> obtenerDoctoresActivos()
    {
        List<Doctor> doctores = new List<Doctor>();
        doctores = _persistence.getActiveDoctors();
        return Ok(doctores);
    }

    [HttpGet("{id}")]
    public ActionResult<Doctor> obtenerDoctorPorId(Guid id)
    {
        Doctor? doctor = null;
        try
        {
            doctor = _persistence.getDoctorById(id);
        }
        catch (Exception ex)
        {
            return NotFound();
        }
        return Ok(doctor);
    }

}
