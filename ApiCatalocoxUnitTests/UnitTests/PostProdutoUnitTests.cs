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
    public class PostProdutoUnitTests : IClassFixture<ProdutosUnitTestsController>
    {
        private readonly ProdutosController _controller;
        public PostProdutoUnitTests(ProdutosUnitTestsController controller)
        {
            _controller = new ProdutosController(controller.repository, controller.mapper);
        }

        [Fact]
        public async Task PutProduto_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var newProduto = new ProdutosDTO
            {
                Nome = "Produto Teste",
                Descricao = "Descrição do Produto Teste",
                Preco = 99.99m,
                CategoriaId = 1
            };
            // Act
            var data = await _controller.Post(newProduto);
            // Assert
            var createdAtActionResult = data.Result.Should().BeOfType<CreatedAtRouteResult>();
            createdAtActionResult.Subject.StatusCode.Should().Be(201);
        }


        [Fact]
        public async Task PostProduto_BadResquest()
        {
            // Arrange
            ProdutosDTO newProduto = null;


            var data = await _controller.Post(newProduto);
            // Assert
            var badRequestResult = data.Result.Should().BeOfType<BadRequestResult>();
            badRequestResult.Subject.StatusCode.Should().Be(400);
        }
    }
}