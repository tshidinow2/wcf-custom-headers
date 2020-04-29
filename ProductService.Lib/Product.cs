using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Lib
{
    [DataContract]
    public class Product
    {

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

    }
}
