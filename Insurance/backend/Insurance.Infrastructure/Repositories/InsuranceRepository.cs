using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Insurance.ApplicationCore.Interfaces;
using Insurance.ApplicationCore.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Misc;
using static System.Net.Mime.MediaTypeNames;

namespace Insurance.Infrastructure.Repositories
{
    public class InsuranceRepository: IInsuranceRepository
    {
        private readonly IMongoCollection<Insurer> _insurer;
        public InsuranceRepository(IMongoClient client)
        {
            var database = client.GetDatabase("InsurerDB");
            var collections = database.GetCollection<Insurer>(nameof(Insurer));
            _insurer = collections;
        }

        public async Task<Insurer> Create(Insurer insurer)
        {
            await _insurer.InsertOneAsync(insurer);
            return insurer;
        }

        public Task<bool> Delete(String id)
        {
            var filter = Builders<Insurer>.Filter.Eq(i => i.Id, id);
            var result = _insurer.DeleteOneAsync(filter);
            return result.ContinueWith(task => task.Result.DeletedCount == 1); // Ensure a boolean value is returned
        }

        public async Task<IEnumerable<Insurer>> GetAll()
        {
            var insurers = await _insurer.FindAsync(_ => true);
            return await insurers.ToListAsync();
        }

        public Task<Insurer> GetById(String id)
        {
            var filter = Builders<Insurer>.Filter.Eq(i => i.Id, id);
            var insurer = _insurer.Find(filter).FirstOrDefaultAsync();
            return insurer;
        }

        //public async Task<IEnumerable<Insurer>> GetByName(string name)
        //{
        //    var filter = Builders<Insurer>.Filter.Eq(i => i.Name, name);
        //    var insurers = await _insurer.Find(filter).ToListAsync(); // Corrected to return a list of insurers
        //    return insurers;
        //}
        public async Task<string[]> GetListOfInsuranceName()
        {
            var insurers = await _insurer.FindAsync(_ => true);
            var insurerList = await insurers.ToListAsync();
            return insurerList.Select(i => i.Name).ToArray(); // Return an array of insurer names

        }

        public async Task<bool> Update(String id, Insurer insurer)
        {
            var filter = Builders<Insurer>.Filter.Eq(i => i.Id, id);
            var update = Builders<Insurer>.Update
                .Set(i => i.Name, insurer.Name)
                .Set(i => i.Email, insurer.Email)
                .Set(i => i.Phone, insurer.Phone);

            var result = await _insurer.UpdateOneAsync(filter, update);
            return result.ModifiedCount == 1; // Ensure a boolean value is returned
        }

        async Task<string?> IInsuranceRepository.GetInsuranceNameByInsuranceCode(string insuranceCode)
        {
            var filter = Builders<Insurer>.Filter.Eq(i => i.Id, insuranceCode);
            var insurer = await _insurer.Find(filter).FirstOrDefaultAsync();
            return insurer?.Name;
        }

    }

}
