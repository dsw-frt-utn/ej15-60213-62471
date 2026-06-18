using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Doctor> _doctors = new List<Doctor>();

        private List<Speciality>? _specialities = new List<Speciality>();

        public PersistenceInMemory()
        {
            LoadSpecialities(); //Porque tengo que cargarlos y esta persistencia se va a instanciar en algun lado de la libreria
        }

        //Este es para el primer endpoint POST
        public void AddDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        public Speciality? GetSpecialityById(Guid id)
        {
            return _specialities?.FirstOrDefault(s => s.Id == id);
        }
        //Este es para el segundo endpoint GET ALL
        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _doctors;
        }
        /*
        public List<Doctor>? GetDoctors()
        {
            return _doctors?.Where(d => d.IsActive).ToList();
        }
        Este es para cuando tengo que verificar que el doctor este activo
        */ 


        //Este es para el tercer endpoint GET BY ID
        public Doctor? GetDoctorById(Guid id)
        {
            return _doctors.FirstOrDefault(d => d.Id == id);
        }


        //Este es para el cuarto endpoint DELETE
        public void DeleteDoctor(Guid id)
        {
            var doctorExistente = GetDoctorById(id);

            if(doctorExistente != null)
            {
                doctorExistente.IsActive = false;
            }
        }


        private void LoadSpecialities()
        {
            /*var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "specialities.json");
            var json = File.ReadAllText(path);*/
            var json = File.ReadAllText("specialities.json");
            /*si yo no pongo esto:
             Despues de >>(json -- No puede matchear porque en el json los campos estan en minusculas y en la clase
            doctor y speciality los atributos estan con la primera letra en MAYUSCULA. Entonces se agrega ese codigo
            para new JsonSerializerOptions ...*/
            var specialities = JsonSerializer.Deserialize<List<Speciality>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, //Es decir que no tenga en cuenta MAY ni min en las propiedades al momento de deserealizar
            });
        }

    }
}
