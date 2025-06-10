using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using MongoDB.Bson;

namespace Auditoria.TestInfra;

public static class AutoFixtureHelper
{
    public static Fixture MontarFixture()
    {
        var fixture = new Fixture();
        
        fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        fixture.Customize(new AutoFakeItEasyCustomization());
        fixture.Register(ObjectId.GenerateNewId);


        return fixture;
    }
}