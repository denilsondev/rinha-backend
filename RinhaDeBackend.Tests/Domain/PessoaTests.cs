using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RinhaDeBackend.Domain.ValueObjects;
using RinhaDeBackend.Entities;
using Xunit;

namespace RinhaDeBackend.Tests.Domain
{
    public class PessoaTests
    {
        [Fact]
        public void DeveAdicionarNovaStack()
        {
            //Arrange 
            var pessoa = new Pessoa("DevMaster", "João da Silva", DateTime.UtcNow.AddYears(-20), new List<Stack>());
            var novaStack = new Stack("C#");

            //Act
            pessoa.AddStack(novaStack);

            //Assert
            Assert.Single(pessoa.Stack);
            Assert.Equal("C#", pessoa.Stack.First().Nome);

        }


        [Fact]
        public void NaoDeveAdicionarStackDuplicada()
        {
            // Arrange
            var pessoa = new Pessoa("DevMaster", "Carlos Silva", DateTime.UtcNow.AddYears(-25), new List<Stack>());
            var stack = new Stack("C#");

            pessoa.AddStack(stack);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => pessoa.AddStack(stack));
        }

        [Fact]
        public void DeveRemoverStackExistente()
        {
            // Arrange
            var stack = new Stack("C#");
            var pessoa = new Pessoa("DevMaster", "Carlos Silva", DateTime.UtcNow.AddYears(-25), new List<Stack> { stack });

            // Act
            pessoa.RemoveStack(stack);

            // Assert
            Assert.Empty(pessoa.Stack);
        }

        [Fact]
        public void NaoDeveRemoverStackInexistente()
        {
            // Arrange
            var pessoa = new Pessoa("DevMaster", "Carlos Silva", DateTime.UtcNow.AddYears(-25), new List<Stack>());
            var stackInexistente = new Stack("Java");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => pessoa.RemoveStack(stackInexistente));
        }
    }
}
}
