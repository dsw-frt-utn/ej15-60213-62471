using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public class DoctorRequest
    {
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public Guid SpecialityId { get; set; }
    }
}
