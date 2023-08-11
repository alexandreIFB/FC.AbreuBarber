using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FC.AbreuBarber.EndToEndTests.Api.Procedure.DeleteProcedure
{

    [Collection(nameof(DeleteProcedureApiTestFixture))]
    public class DeleteProcedureApiTest
    {

        private readonly DeleteProcedureApiTestFixture _fixture;

        public DeleteProcedureApiTest(DeleteProcedureApiTestFixture fixture) 
            => _fixture = fixture;

        [Fact(DisplayName = nameof(DeleteOk))]
        [Trait("End2End/API", "Procedure/Delete - Endpoints")]
        public async Task DeleteOk()
        {

            var exampleProceduresList = _fixture.GetExampleProceduresList(15);

            await _fixture.Persistence.InsertList(exampleProceduresList);

            var procedureForDelete = exampleProceduresList[10];

            var (response,output) = await _fixture.
                ApiClient.Delete<object>(
                $"/procedures/{procedureForDelete.Id}"
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.NoContent);
            output.Should().BeNull();


            var dbProcedure = await _fixture.Persistence.GetById(procedureForDelete.Id);

            dbProcedure.Should().BeNull();

        }

        [Fact(DisplayName = nameof(ThrowWhenNotFound))]
        [Trait("End2End/API", "Procedure/Delete - Endpoints")]
        public async Task ThrowWhenNotFound()
        {

            var exampleProceduresList = _fixture.GetExampleProceduresList(15);

            await _fixture.Persistence.InsertList(exampleProceduresList);

            var randomGuid = Guid.NewGuid();


            var (response, output) = await _fixture.
                ApiClient.Delete<ProblemDetails>(
                $"/procedures/{randomGuid}"
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.NotFound);
            output.Should().NotBeNull();
            output!.Title.Should().Be("Not Found");
            output.Status.Should().Be(StatusCodes.Status404NotFound);
            output.Type.Should().Be("NotFound");
            output.Detail.Should().Be($"Procedure '{randomGuid}' not found");
        }

    }
}
