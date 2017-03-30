using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Domain.Entities
{
    public class Product
    {
        public int IdPack { get; set; }

        public string PackName { get; set; }  
           
        public int PackPrice { get; set; }

        public int Quantity{ get; set; }

        //public byte[] PackPic { get; set; }

    }
}
