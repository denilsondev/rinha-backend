
using RinhaDeBackend.Domain.ValueObjects;

namespace RinhaDeBackend.Domain.Entities
{
    public class Pessoa
    {
        public Guid Id { get; private set; } 

        public string Apelido { get; private set; }

        public string Nome { get; private set; }

        public DateTime Nascimento { get; private set; }
        public List<Stack> Stack { get; private set; }


        public Pessoa(string apelido, string nome, DateTime nascimento, List<Stack> stack)
        {
            Id = Guid.NewGuid();
            SetApelido(apelido);
            SetNome(nome);
            SetNascimento(nascimento);
            Stack = stack ?? throw new ArgumentException("Stack é obrigatório.");
        }

        // Construtor sem parâmetros necessário para o EF Core
        // Esse construtor não deve ser acessado diretamente fora do EF Core
        protected Pessoa()
        {
            Stack = new List<Stack>(); // Inicializando a lista para evitar null
        }


        public void AddStack(Stack novaStack)
        {
            if (Stack.Any(s => s.Equals(novaStack)))
                throw new ArgumentException("Essa stack já foi adicionada");

            Stack.Add(novaStack);
        }

        public void RemoveStack(Stack stackToRemove)
        {
            if (Stack.Any(s => s.Equals(stackToRemove)))
                throw new ArgumentException("Essa stack não existe na lista");

            Stack.Remove(stackToRemove);
        }

        private void SetNome(string nome)
        {
            if(string.IsNullOrWhiteSpace(nome) || nome.Length > 100)
                    throw new ArgumentException("Nome deve ter no máximo 100 caracteres");
            Nome = nome;
        }


        private void SetApelido(string apelido)
        {
            if (string.IsNullOrWhiteSpace(apelido) || apelido.Length > 32)
                throw new ArgumentException("Apelido deve ter no máximo 32 caracteres.");
            Apelido = apelido;
        }

        private void SetNascimento(DateTime nascimento)
        {
            if (nascimento > DateTime.UtcNow)
                throw new ArgumentException("Data de nascimento não pode ser no futuro.");
            Nascimento = nascimento;
        }
    }
}
