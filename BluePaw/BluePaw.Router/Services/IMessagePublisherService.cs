using BPShared = BluePaw.Shared;

namespace BluePaw.Router.Services
{
    public interface IMessagePublisherService
    {
        void PublishPatientRequest(BPShared.Envelope envelope);
    }
}