using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace ProductService.Auth
{
    public class CustomHeaderMessageInspector : IDispatchMessageInspector
    {
        object IDispatchMessageInspector.AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            // Grab the header from the request
            // TODO: Handle missing or invalid header
            CustomHeader header = header = request.Headers.GetHeader<CustomHeader>(CustomHeader.Name, CustomHeader.Namespace);

            // Stash the header in the OperationContext so the service code can reference it if necessary
            OperationContext.Current.IncomingMessageProperties[CustomHeader.OperationContextKey] = header;

            // The object returned from this method is passed as the "correlationState" parameter to the BeforeSendReply
            // method.  We can use this to "echo" back the header to the caller.

            if (!string.IsNullOrEmpty(header.UserName) && (!string.IsNullOrEmpty(header.Password)))
            {
                if (IsRequestValid(header.Password, header.UserName))
                    return header;
                else
                    throw new UnauthorizedAccessException("Wrong credentials");
            }
            else
            {
                throw new MessageHeaderException("Missing credentials from request");
            }
        }

        void IDispatchMessageInspector.BeforeSendReply(ref Message reply, object correlationState)
        {
            // correlationState should be the CustomHeader (passed to this method from AfterReceiveRequest)
            // If an exception occured before or during the header processing, this may be null.
            if (correlationState != null)
            {
                // Get the custom header object passed in on the request
                var headerObject = (CustomHeader)correlationState;

                // Build the MessageHeader object from our custom header
                var headerMessage = MessageHeader.CreateHeader(CustomHeader.Name, CustomHeader.Namespace, headerObject);

                // Inject our custom header into the reply.
                reply.Headers.Add(headerMessage);
            }
        }

        private bool IsRequestValid(string headerPassword, string headerUsername)
        {
            bool isAuthorised = new bool();

            try
            {
                string authUser = "ABB";
                string authPassword = "BBCR";

                DateTime dateTimeUTCnow = DateTime.UtcNow;

                if (authPassword == headerPassword && authUser == headerUsername)
                    isAuthorised = true;

                return isAuthorised;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}