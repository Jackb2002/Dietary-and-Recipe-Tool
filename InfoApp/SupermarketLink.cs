using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoApp
{
    internal struct SupermarketLink
    {
        public SupermarketLink(string link, string store)
        {
            Link = link;
            Store = store;
            MaxItems = 0;
            HTML = "";
        }
        internal string Link;
        internal string Store;
        internal int MaxItems;
        internal string HTML;
    }
}
