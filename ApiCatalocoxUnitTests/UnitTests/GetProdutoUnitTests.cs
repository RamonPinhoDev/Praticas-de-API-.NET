using APICatologo.Controllers;
using APICatologo.DTOs;
using APICatologo.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalocoxUnitTests.UnitTests
{
    public class GetProdutoUnitTests : IClassFixture<ProdutosUnitTestsController>
    {
        private readonly ProdutosController _controller;

        public GetProdutoUnitTests(ProdutosUnitTestsController controller)
        {
            _controller = new ProdutosController(controller.repository, controller.mapper);
        }


        [Fact]
        public async Task GetProdutosAsync_ReturnsOkResult()
        {
            //Arrange
            var produtoID = 2;

            //Act
            var data = await _controller.GetById(produtoID);


            //Assert(xunit)
            //var okResult = Assert.IsType<OkObjectResult>(data.Result);
            //Assert.Equal(200, okResult.StatusCode);

            //Assert(fluentassertions)
            data.Result.Should().BeOfType<OkObjectResult>() // Verifica se o resultado é do tipo OkObjectResult
                .Which.StatusCode.Should().Be(200); // Verifica se o status do OkObjectResult é 200

        }


        [Fact]
        public async Task GetProdutosById_Return_NotFound()
        {
            //Arrange
            var produtoID = 999;

            //Act
            var data = await _controller.GetById(produtoID);


            //Assert(xunit)
            //var okResult = Assert.IsType<OkObjectResult>(data.Result);
            //Assert.Equal(200, okResult.StatusCode);

            //Assert(fluentassertions)
            data.Result.Should().BeOfType<NotFoundResult>() // Verifica se o resultado é do tipo OkObjectResult
                .Which.StatusCode.Should().Be(404); // Verifica se o status do OkObjectResult é 200

        }
    

        [Fact]
        public async Task GetProdutosById_Return_BadRequest()
        {
            //Arrange
            var produtoID = 0;

            //Act
            var data = await _controller.GetById(produtoID);


            //Assert(xunit)
            //var okResult = Assert.IsType<OkObjectResult>(data.Result);
            //Assert.Equal(200, okResult.StatusCode);

            //Assert(fluentassertions)
            data.Result.Should().BeOfType<BadRequestResult>() // Verifica se o resultado é do tipo OkObjectResult
                .Which.StatusCode.Should().Be(400); // Verifica se o status do OkObjectResult é 200

        }

        [Fact]
        public async Task GetProdutosById_Return_List()
        {
            //Arrange
            //  var produtoID = 0;

            //Act
            var data = await _controller.Get();


            //Assert(xunit)
            //var okResult = Assert.IsType<OkObjectResult>(data.Result);
            //Assert.Equal(200, okResult.StatusCode);

            //Assert(fluentassertions)

            data.Result.Should().BeOfType<OkObjectResult>() // Verifica se o resultado é do tipo OkObjectResult
             .Which.Value.Should().BeAssignableTo<IEnumerable<ProdutosDTO>>(); // Verifica se o status do OkObjectResult é 200
        }


        [Fact]
        public async Task GetProdutosById_Return_List_BadResquest()
        {
           

            //Act
            var data = await _controller.Get();


            //Assert(fluentassertions)

            data.Result.Should().BeOfType<BadRequestResult>(); // Verifica se o resultado é do tipo OkObjectResult
           //.Which.StatusCode.Should().Be(400);
        }
    }
}