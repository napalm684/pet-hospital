using AutoFixture;
using AutoFixture.AutoFakeItEasy;

namespace TestUtilities.Customizations
{
    public class DomainCustomization : CompositeCustomization
    {
        public DomainCustomization()
            : base(new AutoFakeItEasyCustomization(),
                new EnvelopeCustomization())
        { }
    }
}