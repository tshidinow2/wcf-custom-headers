using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ProductService.Auth
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class CustomMessageHeaderAttribute : Attribute, IContractBehavior, IWsdlExportExtension
    {
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            // Attach the message inspector to the endpoint.
            var inspector = new CustomHeaderMessageInspector();
            dispatchRuntime.MessageInspectors.Add(inspector);
        }

        public void ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
        {
            // Build the custom header description
            var headerDescription = new MessageHeaderDescription(CustomHeader.Name, CustomHeader.Namespace);
            headerDescription.Type = typeof(CustomHeader);

            var headers = context.ContractConversionContext.Contract.Operations;

            // Loop through all the operations defined for the contract and add custom SOAP header to the WSDL
            //foreach (OperationDescription op in context.ContractConversionContext.Contract.Operations)
            //{
            //    foreach (MessageDescription messageDescription in op.Messages)
            //    {
            //       var exist =  messageDescription.Headers.Any(x => x.Equals(headerDescription));
            //       if(!exist)
            //          messageDescription.Headers.Add(headerDescription);
            //    }
            //}
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }

        void IContractBehavior.ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            // Attach the message inspector to the endpoint.
            var inspector = new CustomHeaderMessageInspector();
            dispatchRuntime.MessageInspectors.Add(inspector);
        }

        void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
        {
            // Build the custom header description
            var headerDescription = new MessageHeaderDescription(CustomHeader.Name, CustomHeader.Namespace);
            headerDescription.Type = typeof(CustomHeader);

            // Loop through all the operations defined for the contract and add custom SOAP header to the WSDL
            foreach (OperationDescription op in context.Contract.Operations)
            {
                foreach (MessageDescription messageDescription in op.Messages)
                {
                    messageDescription.Headers.Add(headerDescription);
                }
            }
        }
    }
}