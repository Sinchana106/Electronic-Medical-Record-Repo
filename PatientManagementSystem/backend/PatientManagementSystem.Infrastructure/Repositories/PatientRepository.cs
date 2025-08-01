
using MongoDB.Bson;
using MongoDB.Driver;
using PatientManagementSystem.ApplicationCore.Interfaces;

namespace PatientManagementSystem.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IMongoCollection<Patient> _patient;

        public PatientRepository(IMongoClient client)
        {
            var database = client.GetDatabase("PatientDB");
            var collections = database.GetCollection<Patient>(nameof(Patient));
            _patient = collections;
        }

        async Task<Patient> IPatientRepository.Create(Patient patient)
        {
            await _patient.InsertOneAsync(patient);
            return patient;
            
        }

        async Task<IEnumerable<Patient>> IPatientRepository.GetAll()
        {
            var patients = await _patient.FindAsync(_ => true);
            return await patients.ToListAsync();
        }

        Task<Patient> IPatientRepository.GetById(String id)
        {
            var filter = Builders<Patient>.Filter.Eq(p => p.Id, id);
            var patient = _patient.Find(filter).FirstOrDefaultAsync();
            return patient;
        }

        async Task<bool> IPatientRepository.Update(String id, Patient patient)
        {
            var filter = Builders<Patient>.Filter.Eq(p=>p.Id, id);
            var update = Builders<Patient>.Update
                .Set(p => p.Name, patient.Name)
                .Set(p => p.ContactNo, patient.ContactNo)
                .Set(p => p.Age, patient.Age)
                .Set(p => p.Type, patient.Type)
                .Set(p => p.Age, patient.Age)
                .Set(p => p.Age, patient.Age)
                .Set(p => p.InsuredBy, patient.InsuredBy)
                .Set(p => p.VisitType, patient.VisitType)
                .Set(p => p.Description, patient.Description);

            var result = await _patient.UpdateOneAsync(filter, update);
            return result.ModifiedCount == 1; // Ensure a boolean value is returned

        }

        Task<bool> IPatientRepository.Delete(String id)
        {
            var filter = Builders<Patient>.Filter.Eq(p => p.Id, id);
            var result = _patient.DeleteOneAsync(filter);
            return result.ContinueWith(task => task.Result.DeletedCount == 1); //
        }
    }
}
