using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsInfoApp
{
    internal struct SupermarketLink
    {
        public SupermarketLink(string link, string store)
        {
            Link = link;
            Store = store;
        }
        internal string Link;
        internal string Store;
    }
}
