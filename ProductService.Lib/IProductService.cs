using ProductService.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ProductService.Lib
{
    [ServiceContract]
    [CustomMessageHeader]
    public interface IProductService
    {
        [OperationContract]
        List<Product> GetProducts();
    }

}
