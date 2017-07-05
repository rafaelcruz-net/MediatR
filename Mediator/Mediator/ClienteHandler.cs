using Mediator.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mediator
{
    public class ClienteHandler : IRequestHandler<Cliente, Cliente>
    {
        public Cliente Handle(Cliente message)
        {
            if (String.IsNullOrEmpty(message.Email))
                throw new Exception("Email não pode estar vazio");

            message.Id = 1;

            //Salva no banco de dados;
            // Mais código //

            return message;
        }
    }

}