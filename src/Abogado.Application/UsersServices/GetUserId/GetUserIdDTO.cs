﻿using Abogado.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abogado.Application.UsersServices.GetUserId
{
    public class GetUserIdDTO
    {
        public Role Role { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public List<CaseDTO> Cases { get; set; }

        public List<MeetingDTO> Meetings { get; set; }
        
        public class MeetingDTO
        {
            public DateTime Date { get; set; }
        }

        public class CaseDTO
        {
            public string CaseName { get; set; }

            public string Description { get; set; }

            public Trial Trial { get; set; }

            public DivorceForm DivorceForm { get; set; }

            public DivorceMechanism DivorceMechanism { get; set; }

            public DateTime StartDate { get; set; }
        }
    }

}
