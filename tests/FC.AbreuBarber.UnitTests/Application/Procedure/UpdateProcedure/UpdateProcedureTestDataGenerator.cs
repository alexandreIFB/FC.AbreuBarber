

using FC.AbreuBarber.Application.UseCases.Procedure.UpdateProcedure;
using FC.AbreuBarber.UnitTests.Application.Procedure.CreateProcedure;

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

                var input = fixture.GetValidProcedureInput(exampleCategory.Id);

                yield return new object[] { exampleCategory, input };
            }
        }

        public static IEnumerable<object[]> GetInvalidInputs(int times = 15)
        {
            var fixture = new UpdateProcedureTestFixture();
            var invalidInputsList = new List<object[]>();
            var totalInvalidCases = 4;

            for (int index = 0; index < times; index++)
            {
                switch (index % totalInvalidCases)
                {
                    case 0:
                        invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputShortName(),
                        "Name should be at leats 3 characters long"
                    });
                        break;
                    case 1:
                        invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputTooLongName(),
                        "Name should be less or equal 255 characters long"
                    });
                        break;
                    case 2:
                        invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputTooLongDescription(),
                        "Description should be less or equal 10_000 characters long"
                    });
                        break;
                    case 3:
                        invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputValueIsMinor(),
                        "Value should not be less than 30"
                    });
                        break;
                    case 4:
                        invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputValueIsBigger(),
                        "Value should not be bigger than 1000"
                    });
                        break;
                    default:
                        break;
                }
            }

            return invalidInputsList;
        }
    }
}
