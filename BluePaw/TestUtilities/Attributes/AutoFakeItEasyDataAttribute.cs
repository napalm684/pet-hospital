using AutoFixture;
using AutoFixture.Xunit2;
using TestUtilities.Customizations;

namespace TestUtilities.Attributes
{
    public class AutoFakeItEasyDataAttribute : AutoDataAttribute
    {
        public AutoFakeItEasyDataAttribute()
            : base(() => new Fixture().Customize(new DomainCustomization()))
        { }
    }
}