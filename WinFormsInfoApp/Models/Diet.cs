using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsInfoApp.Models
{
    public class Diet
    {
        public int DietId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
    }
}
