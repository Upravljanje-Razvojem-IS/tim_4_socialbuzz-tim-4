using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.MockLogger
{
    public interface IFakeLogger
    {
        public void Log(string message);
    }
}
