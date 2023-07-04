﻿

using FC.AbreuBarber.Application.UseCases.Procedure.UpdateProcedure;

namespace FC.AbreuBarber.UnitTests.Application.Procedure.UpdateProcedure
{
    public class UpdateProcedureTestDataGenerator
    {
        public static IEnumerable<object[]> GetProcedureToUpdate(int times = 10)
        {
            var fixture = new UpdateProcedureTestFixture();

            for(int i = 0; i < times; i++)
            {
                var exampleCategory = fixture.GetValidProcedure();

                var input = new UpdateProcedureInput(
                    exampleCategory.Id,
                    fixture.GetValidProcedureName(),
                    fixture.GetValidProcedureValue(),
                    fixture.GetValidProcedureDescription(),
                    fixture.GetRandomBoolean()
                   );

                yield return new object[] { exampleCategory, input };
            }
        }
    }
}
