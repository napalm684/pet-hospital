using System.Threading.Tasks;
using BluePaw.Shared;
using Refit;

namespace BluePaw.Ui.Services
{
    public interface IAdministrationService
    {
        [Post("/patient")]
        Task<int> CreatePatient([Body] CreatePatientRequest request);
    }
}