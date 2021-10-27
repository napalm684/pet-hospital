using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;
using BluePaw.Shared;

namespace TestUtilities.SpecimenBuilders
{
    public class EnvelopeBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            var pi = request as ParameterInfo;
            if (pi == null)
            {
                return new NoSpecimen();
            }

            if (pi.ParameterType != typeof(Envelope))
            {
                return new NoSpecimen();
            }

            // Note: Probably overkill here as the default specimen builder handles
            // this without the customization. Just demonstrating what can be done and
            // creating a touch point should customization needs evolve.
            return context.Create<Envelope>();
        }
    }
}