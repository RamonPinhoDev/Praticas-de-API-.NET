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
    public class PutProdutoUnitTests : IClassFixture<ProdutosUnitTestsController>
    {
        private readonly ProdutosController _controller;

        public PutProdutoUnitTests(ProdutosUnitTestsController controller)
        {
            _controller = new ProdutosController(controller.repository, controller.mapper);
        }

        [Fact]
        public async Task PutProduto_ReturnsOkResult()
        {
            // Arrange
            var produtoId = 2; // ID do produto existente
            var updatedProduto = new ProdutosDTO
            {
                ProdutoId = produtoId,
                Nome = "Produto Atualizado",
                Descricao = "Descrição do Produto Atualizado",
                Preco = 149.99m,
                CategoriaId = 1
            };
            // Act
            var data = await _controller.Put(produtoId, updatedProduto) as ActionResult<ProdutosDTO>;
            // Assert
            data.Result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be(200);

            
        }



        [Fact]
        public async Task PutProduto_ReturnsBadRequest()
        {
            // Arrange
            var produtoId = 2; // ID do produto existente
            var updatedProduto = new ProdutosDTO
            {
                ProdutoId = 1,
                Nome = "Produto Atualizado",
                Descricao = "Descrição do Produto Atualizado",
                Preco = 149.99m,
                CategoriaId = 1
            };
            // Act
            var data = await _controller.Put(produtoId, updatedProduto);
            // Assert
            data.Result.Should().BeOfType<BadRequestResult>()
                .Which.StatusCode.Should().Be(400);


        }

    }
}
