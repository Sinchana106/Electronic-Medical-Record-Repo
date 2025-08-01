using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.ApplicationCore.Models;
using MongoDB.Bson;

namespace Insurance.ApplicationCore.Interfaces
{
    public interface IInsuranceRepository
    {
        Task<Insurer> Create(Insurer insurer);
        Task<Insurer> GetById(String id);
        Task<IEnumerable<Insurer>> GetAll();
        //Task<IEnumerable<Insurer>> GetByName(string name);
        Task<string?> GetInsuranceNameByInsuranceCode(string insuranceCode);

        Task<string[]> GetListOfInsuranceName();
        Task<bool> Update(String id, Insurer insurer);
        Task<bool> Delete(String id);
    }
}
