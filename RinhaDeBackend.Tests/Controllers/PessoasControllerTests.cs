using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RinhaDeBackend.API.Model;
using RinhaDeBackend.Application.Services;
using RinhaDeBackend.Controllers;
using RinhaDeBackend.Domain.Entities;
using Xunit;

namespace RinhaDeBackend.Tests.Controllers
{

    public class PessoasControllerTests
    {
        private readonly Mock<IPessoaService> _serviceMock;
        private readonly PessoasController _controller;
        public PessoasControllerTests()
        {
            _serviceMock = new Mock<IPessoaService>();
            _controller = new PessoasController(_serviceMock.Object);
            
        }


        [Fact]
        public async Task AddPessoa_ShouldReturn201_WhenRequestIsValid()
        {
            var IdPessoa = Guid.NewGuid();
            var novaPessoa = new PessoaInputModel
            { 
                Apelido = "jose",
                Nome = "José da Silva",
                Nascimento = DateTime.Parse("2000-10-01"),
                Stack = new List<string> { "C#", "Node" }
            };

            _serviceMock.Setup(s => s.CriarPessoaAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<List<string>>()))
                .ReturnsAsync(IdPessoa);

            var result = await _controller.AddPessoa(novaPessoa) as CreatedAtActionResult;

            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task AddPessoa_ShouldReturn422_WhenApelidoAlreadyExists()
        {
            var novaPessoa = new PessoaInputModel
            {
                Apelido = "jose",
                Nome = "José da Silva",
                Nascimento = DateTime.Parse("2000-10-01"),
                Stack = new List<string> { "C#", "Node" }
            };

            _serviceMock.Setup(s => s.CriarPessoaAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<List<string>>())).ThrowsAsync(new Exception("Apelido já está em uso."));

            var result = await _controller.AddPessoa(novaPessoa) as ObjectResult;


            result.StatusCode.Should().Be(422);
            result.Value.Should().BeEquivalentTo(new { error = "Apelido já está em uso." });
        }

        [Fact]
        public async Task AddPessoa_ShouldReturn400_WhenRequestIsMalformed()
        {
            var pessoaInvalida = new PessoaInputModel
            {
                Apelido = "jose",
                Nome = "José da Silva",
                Nascimento = DateTime.Parse("2000-10-01"),
                Stack = null
            };

            var result = await _controller.AddPessoa(pessoaInvalida);


            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task AddPessoa_ShouldReturn400_WhenStackContainsInvalidValues()
        {
            var pessoaComStackInvalido = new PessoaInputModel
            {
                Apelido = "josé",
                Nome = "José Roberto",
                Nascimento = DateTime.Parse("2000-10-01"),
                Stack = new List<string> { "C#", "Node", "FrameworkComUmNomeExtremamenteLongo" }
            };

            var result = await _controller.AddPessoa(pessoaComStackInvalido) as ObjectResult;

            result.StatusCode.Should().Be(400);
            result.Value.Should().BeEquivalentTo(new { error = "Cada item do Stack deve ter no máximo 32 caracteres." });
        }
    }
}
