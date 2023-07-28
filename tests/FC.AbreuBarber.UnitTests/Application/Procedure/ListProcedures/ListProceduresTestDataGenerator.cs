using FC.AbreuBarber.Application.UseCases.Procedure.ListProcedures;

namespace FC.AbreuBarber.UnitTests.Application.Procedure.ListProcedures
{
    public class ListProceduresTestDataGenerator
    {

        public static IEnumerable<object[]> GetInputsWithoutAllParameter(int times = 15)
        {
            var fixture = new ListProceduresTestFixture();


            for (int index = 0; index < times; index++)
            {
                var inputExample = fixture.GetExampleInput();
                switch (index % 5)
                {
                    case 0:
                        yield return new object[] { new ListProceduresInput() };
                        break;
                    case 1:
                        yield return new object[] { 
                            new ListProceduresInput(inputExample.Page) 
                        };

                        break;
                    case 2:
                        yield return new object[] {
                            new ListProceduresInput(inputExample.Page, inputExample.PerPage) 
                        };

                        break;
                    case 3:
                        yield return new object[] {
                            new ListProceduresInput(inputExample.Page, inputExample.PerPage, inputExample.Search) 
                        };

                        break;
                    case 4:
                        yield return new object[] {
                            new ListProceduresInput(inputExample.Page, inputExample.PerPage, inputExample.Search, inputExample.Sort) 
                        };
                        break;
                    default:
                        yield return new object[] {
                            new ListProceduresInput()
                        };
                        break;
                }
            }
        }
    }
}
