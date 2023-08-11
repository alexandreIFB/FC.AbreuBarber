﻿using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FluentAssertions;
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

            var response = await _fixture.
                ApiClient.Delete(
                $"/procedures/{procedureForDelete.Id}"
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.OK);


            var dbProcedure = await _fixture.Persistence.GetById(procedureForDelete.Id);

            dbProcedure.Should().BeNull();

        }

    }
}