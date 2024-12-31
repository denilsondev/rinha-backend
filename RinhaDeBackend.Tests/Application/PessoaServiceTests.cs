using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using RinhaDeBackend.Application.Services;
using RinhaDeBackend.Domain.Entities;
using RinhaDeBackend.Domain.Repositories;
using RinhaDeBackend.Domain.ValueObjects;
using Xunit;

namespace RinhaDeBackend.Tests.Application
{
    public class PessoaServiceTests
    {
        [Fact]
        public async Task CriarPessoa_DeveRetornarId()
        {
            //Arrange 
            var mockRepo = new Mock<IPessoaRepository>();
            var service = new PessoaService(mockRepo.Object);

            var stack = new List<string> { "C#", "Angular" };


            //Act
            var id = await service.CriarPessoaAsync("DevMaster", "Joao", DateTime.UtcNow.AddYears(-20), stack);

            //Assert 
            Assert.NotEqual(Guid.Empty, id );
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Pessoa>()), Times.Once);

        }


        [Fact]
        public async Task BuscarPessoasAsync_DeveRetornarLista()
        {
            // Arrange
            var pessoas = new List<Pessoa>
            {
                new Pessoa("DevMaster", "Carlos Silva", DateTime.UtcNow.AddYears(-25), new List<Stack> { new Stack("C#") })
            };
            var mockRepo = new Mock<IPessoaRepository>();
            mockRepo.Setup(r => r.SearchAsync("Dev")).ReturnsAsync(pessoas);

            var service = new PessoaService(mockRepo.Object);

            // Act
            var result = await service.BuscarPessoas("Dev");

            // Assert
            Assert.Single(result);
            Assert.Equal("DevMaster", result.First().Apelido);
        }





    }
}
