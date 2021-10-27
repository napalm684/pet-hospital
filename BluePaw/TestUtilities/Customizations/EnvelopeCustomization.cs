using AutoFixture;
using TestUtilities.SpecimenBuilders;

namespace TestUtilities.Customizations
{
    public class EnvelopeCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new EnvelopeBuilder());
        }
    }
}