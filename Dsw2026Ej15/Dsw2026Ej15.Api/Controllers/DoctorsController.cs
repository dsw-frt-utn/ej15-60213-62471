using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistencia;

    //Para llamar a la persistencia en memoria, necesitamos una instancia de esa clase.
    //Pero ya aprendimos la INYECCION DE DEPENDENCIAS:

    public DoctorsController(IPersistence persistencia)
    {
        _persistencia = persistencia;
        //Estamos generando una dependencia sobre sobre una implementacion en particular de una persistencia
        /*¿Que nos convendria (Por el ppio de inversion de dependencia)?: usar la interfaz. Entonces se crea una 
        interfaz en el domain llamada IPersistence, Se crea una variable _persistencia
        
        Para que la inyeccion funcione tengo que registrarla entonces me voy al program.cs y escribo la linea:
        builder.Services.AddSingleton<IPersistence, PersistenceInMemory>();
        Diciendo que registro la interfaz y remplazo a PersistenceInMemory*/
    }


    //Primer EndPoint
    


    //Segundo Endpoint
    [HttpGet("api/doctors")]
    public IActionResult GetDoctors()
    {
        var doctors = _persistencia.GetAllDoctors(); //Llamo al metodo de la persistencia.
        return Ok(doctors); //Se pone el argumento doctors para que me devuelva los doctores
    }

    //Tercer EndPoint
    [HttpGet("api/doctors/{id}")]
    public IActionResult GetDoctors(Guid id)
    {
        var doctor = _persistencia.GetDoctorById(id);
        if (doctor == null) return NotFound();
        return Ok(doctor);
    }

    //CuartoEndPoint
    [HttpDelete("api/doctors/{id}")]
    public IActionResult DeleteDoctor(Guid id)
    {
        var doctor = _persistencia.GetDoctorById(id);
        if (doctor == null) return NotFound();

        _persistencia.DeleteDoctor(id);
        return NoContent(); // 204
    }
}
