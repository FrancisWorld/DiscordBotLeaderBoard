using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class RankedUsersDTO
    {
        public string UserName { get; set; }

        public string ThumbUrl { get; set; }

        public double RankPoints { get; set; }
    }
}
