using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RinhaDeBackend.Domain.ValueObjects
{
    public class Stack
    {
        public Stack(string nome)
        {
            Nome = nome;
            
        }
        public string Nome { get; private set; }
    }
}
