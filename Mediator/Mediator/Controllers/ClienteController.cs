using Mediator.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mediator.Controllers
{
    public class ClienteController : ApiController
    {
        private IMediator Mediator { get; set; }

        public ClienteController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        public IHttpActionResult Post([FromBody]Cliente model)
        {
            var response = this.Mediator.Send(model);

            return Ok(response);

        }

    }
}