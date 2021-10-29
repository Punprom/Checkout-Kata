using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PK.Kata.Shared.Models
{
    public class Promotion
    {
        public string Code { get; set; }

        public int SetAmount { get; set; }

        public int SetPrice { get; set; }

        public int PercentOff { get; set; }

        public bool IsSet { get; set; }
    }
}
