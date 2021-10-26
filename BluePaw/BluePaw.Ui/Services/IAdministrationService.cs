using System.Collections.Generic;
using System.Threading.Tasks;
using BluePaw.Shared;
using Refit;

namespace BluePaw.Ui.Services
{
    public interface IAdministrationService
    {
        [Get("/patient/all")]
        Task<IEnumerable<PatientData>> RetrievePatients();

        [Get("/patient/{id}")]
        Task<PatientData> RetrievePatient(int id);
        
        [Post("/patient")]
        Task<int> CreatePatient([Body] CreatePatientRequest request);

        [Get("/request/all")]
        Task<IEnumerable<PatientRequest>> RetrieveRequests();
    }
}