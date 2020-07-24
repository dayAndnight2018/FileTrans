using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrans
{
    public class SocketUser
    {
        public Guid Uid { get; set; }

        public String Ip { get; set; }

        public String Name { get; set; }

        public IWebSocketConnection Connection { get; set; }

    }
}
