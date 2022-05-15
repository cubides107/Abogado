﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abogado.Application.UsersServices.GetUserId
{
    public class GetUserIdQuery: IRequest<GetUserIdDTO>
    {
        public string Id { get; set; }
    }
}
