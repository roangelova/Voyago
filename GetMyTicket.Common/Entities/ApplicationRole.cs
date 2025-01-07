using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetMyTicket.Common.Entities
{
    public class ApplicationRole: IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
