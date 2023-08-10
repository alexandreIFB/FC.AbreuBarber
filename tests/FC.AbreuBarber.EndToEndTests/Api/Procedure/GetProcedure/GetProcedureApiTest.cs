﻿using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Xunit;

namespace FC.AbreuBarber.EndToEndTests.Api.Procedure.GetProcedure
{

    [Collection(nameof(GetProcedureApiTestFixture))]
    public class GetProcedureApiTest
    {

        private readonly GetProcedureApiTestFixture _fixture;

        public GetProcedureApiTest(GetProcedureApiTestFixture fixture)
            => _fixture = fixture;


        [Fact(DisplayName = nameof(GetById))]
        [Trait("End2End/API", "Procedure/Get - Endpoints")]
        public async Task GetById()
        {

            var exampleProceduresList = _fixture.GetExampleProceduresList(15);

            await _fixture.Persistence.InsertList(exampleProceduresList);

            var expectedGetProcedure = exampleProceduresList[10];

            var (response, output) = await _fixture.
                ApiClient.Get<ProcedureModelOutput>(
                $"/procedures/{expectedGetProcedure.Id}"
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.OK);
            output.Should().NotBeNull();
            output!.Id.Should().NotBeEmpty();
            output.Name.Should().Be(expectedGetProcedure.Name);
            output.Description.Should().Be(expectedGetProcedure.Description);
            output.IsActive.Should().Be(expectedGetProcedure.IsActive);
            output.Value.Should().Be(expectedGetProcedure.Value);
            output.CreatedAt.Should().BeSameDateAs(expectedGetProcedure.CreatedAt);
        }

        [Fact(DisplayName = nameof(ThrowWhenNotFound))]
        [Trait("End2End/API", "Procedure/Get - Endpoints")]
        public async Task ThrowWhenNotFound()
        {

            var exampleProceduresList = _fixture.GetExampleProceduresList(15);

            await _fixture.Persistence.InsertList(exampleProceduresList);

            var randomGuid = Guid.NewGuid();

            var (response, output) = await _fixture.
                ApiClient.Get<ProblemDetails>(
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
