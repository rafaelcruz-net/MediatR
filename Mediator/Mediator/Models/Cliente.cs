using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mediator.Models
{
    public class Cliente  : IRequest<Cliente>
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Email { get; set; }
        public String CPF { get; set; }
    }
}