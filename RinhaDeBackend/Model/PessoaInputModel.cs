using System.ComponentModel.DataAnnotations;

namespace RinhaDeBackend.API.Model
{
    public class PessoaInputModel
    {
        [Required(ErrorMessage = "O apelido é obrigatório.")]
        [MaxLength(32, ErrorMessage = "O apelido deve ter no máximo 32 caracteres.")]
        public string Apelido { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "A data de nascimento deve ser uma data válida.")]
        public DateTime Nascimento { get; set; }

        [Required(ErrorMessage = "A stack é obrigatória.")]
        [MinLength(1, ErrorMessage = "A stack deve conter pelo menos um item.")]
        public List<string> Stack { get; set; }
    }
}
