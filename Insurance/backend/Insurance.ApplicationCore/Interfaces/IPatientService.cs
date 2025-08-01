using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.ApplicationCore.DTOs;

namespace Insurance.ApplicationCore.Interfaces
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllPatients();

    }
}
