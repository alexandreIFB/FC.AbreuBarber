using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.AbreuBarber.UnitTests.Application.Procedure.CreateProcedure
{
    public class CreateProcedureTestDataGenerator
    {
        public static IEnumerable<object[]> GetInvalidInputs(int times = 15)
        {
            var fixture = new CreateProcedureTestFixture();
            var invalidInputsList = new List<object[]>();
            var totalInvalidCases = 5;

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
                        fixture.GetInvalidInputDescriptionNull(),
                        "Description should not be null"
                    });
                        break;
                    case 3:
                        invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputTooLongDescription(),
                        "Description should be less or equal 10_000 characters long"
                    });
                        break;
                    case 4:
                        invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputValueIsMinor(),
                        "Value should not be less than 30"
                    });
                        break;
                    case 5:
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
