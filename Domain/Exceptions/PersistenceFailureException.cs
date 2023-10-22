using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    internal class PersistenceFailureException : Exception
    {
        public PersistenceFailureException() : base("Não foi possível salvar o registro no banco de dados.") { }

        public PersistenceFailureException(string message) :base(message) { }
    }
}
