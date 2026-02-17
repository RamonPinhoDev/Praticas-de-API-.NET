using APICatologo.Controllers;
using APICatologo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalocoxUnitTests.UnitTests
{
    public class DeleteProdutoUnitTests : IClassFixture<ProdutosUnitTestsController>
    {
        private readonly ProdutosController _controller;

        public DeleteProdutoUnitTests(ProdutosUnitTestsController controller)
        {
            _controller = new ProdutosController(controller.repository, controller.mapper);
        }


            [Fact]
            public async Task DeleteProduto_ReturnsOkResult()
            {
                // Arrange
                var produtoId = 16; // ID do produto existente
             // Act
            var data = await _controller.Delete(produtoId);//as ActionResult<ProdutosDTO>;
             // Assert
             data.Should().NotBeNull();
            data.Result.Should().BeOfType<OkObjectResult>();
                   
          
        }

        [Fact]
        public async Task DeleteProduto_ReturnsNotFoundResult()
        {
            // Arrange
            var produtoId = 14; // ID do produto existente
             // Act
            var data = await _controller.Delete(produtoId);
              // Assert
            data.Should().NotBeNull(); // verifica se a resposta não é nula
            data.Result.Should().BeOfType<NotFoundResult>(); // verifica se o resultado é do tipo NotFoundResult


        }




        //[Fact]
        //public async Task DeleteProduto_ReturnsNoContentResut()
        //{
        //    // Arrange
        //    var produtoId = 11; // ID do produto existente
        //                        // Act
        //    var data = await _controller.Delete(produtoId) as ActionResult<ProdutosDTO>;
        //    // Assert
        //    data.Should().BeOfType<NoContentResult>()
        //        .Which.StatusCode.Should().Be(200);
        //}
    }
}
