using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsInfoApp
{
    internal static class GlobalSettings
    {
        internal static readonly string SupermarketURLsFile = @"Pages.dat";
        internal static readonly string LocalDatabaseFile = "Food_db.db";
        internal static string FullPathDb = LocalDatabaseFile;
    }
}
