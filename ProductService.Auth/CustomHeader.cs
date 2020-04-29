using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProductService.Auth
{
    [DataContract]
    public class CustomHeader
    {
        public const string Name = "customHeader";
        public const string Namespace = "http://mycompany.com";
        public const string OperationContextKey = "CustomSoapHeader";

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }
    }   
}