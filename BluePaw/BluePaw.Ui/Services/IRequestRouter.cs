using System.Threading.Tasks;
using BluePaw.Shared;
using Refit;

namespace BluePaw.Ui.Services
{
    public interface IRequestRouter
    {
        [Post("/patientrequests")]
        Task CreateRequest([Body] Envelope e);
    }
}