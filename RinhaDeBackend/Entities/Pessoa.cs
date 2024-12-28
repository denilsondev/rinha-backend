using System;
using System.ComponentModel.DataAnnotations;

namespace RinhaDeBackend.Entities
{
    public class Pessoa
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(32)]
        public string Apelido { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        private DateTime _nascimento;

        [Required]
        public DateTime Nascimento
        {
            get => _nascimento;
            set => _nascimento = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        [Required]
        public List<string> Stack { get; set; }
    }
}
