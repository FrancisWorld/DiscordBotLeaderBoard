using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IApiConsumer
    {
        public bool CheckAccountExists(string nickName);
        public double GetKillStats(string nickName);
        public double GetWinRate(string nickName);
    }
}
