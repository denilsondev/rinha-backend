using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RinhaDeBackend.Controllers;
using RinhaDeBackend.Entities;
using RinhaDeBackend.Services;
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
            var novaPessoa = new Pessoa
            {
                Apelido = "jose",
                Nome = "José da Silva",
                Nascimento = DateTime.Parse("2000-10-01"),
                Stack = new List<string> { "C#", "Node" }
            };

            _serviceMock.Setup(s => s.AddAsync(It.IsAny<Pessoa>()))
                .ReturnsAsync(new Pessoa
                {
                    Id = Guid.NewGuid(),
                    Apelido = novaPessoa.Apelido,
                    Nome = novaPessoa.Nome,
                    Nascimento = novaPessoa.Nascimento,
                    Stack = novaPessoa.Stack
                });

            var result = await _controller.AddPessoa(novaPessoa) as CreatedAtActionResult;

            result.StatusCode.Should().Be(201);
            result.Value.Should().BeEquivalentTo(novaPessoa, options => options.Excluding(p => p.Id));
        }

        [Fact]
        public async Task AddPessoa_ShouldReturn422_WhenApelidoAlreadyExists()
        {
            var novaPessoa = new Pessoa
            {
                Apelido = "jose",
                Nome = "José da Silva",
                Nascimento = DateTime.Parse("2000-10-01"),
                Stack = new List<string> { "C#", "Node" }
            };

            _serviceMock.Setup(s => s.AddAsync(It.IsAny<Pessoa>())).ThrowsAsync(new Exception("Apelido já está em uso."));

            var result = await _controller.AddPessoa(novaPessoa) as ObjectResult;


            result.StatusCode.Should().Be(422);
            result.Value.Should().BeEquivalentTo(new { error = "Apelido já está em uso." });
        }

        [Fact]
        public async Task AddPessoa_ShouldReturn400_WhenRequestIsMalformed()
        {
            var pessoaInvalida = new Pessoa
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
            var pessoaComStackInvalido = new Pessoa
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
