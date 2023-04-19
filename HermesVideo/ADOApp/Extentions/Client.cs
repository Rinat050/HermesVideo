using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HermesVideo.ADOApp
{
    public partial class Client
    {
        public DateTime LastDate => Visit.OrderBy(x => x.Date).LastOrDefault().Date;
        public int VisitCount => Visit.Count;
        public List<ClientTag> Tags => App.Connection.ClientTag.Where(x => x.ClientId == Id).ToList();
    }
}
