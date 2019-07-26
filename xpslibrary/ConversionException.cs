using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace XpsLibrary
{
    [Serializable]
    public class ConversionException : Exception
    {
        public readonly int ContextData;

        public ConversionException(string message, int contextData, Exception innerException = null) :
            base(message, innerException)
        {
            ContextData = contextData;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ContextData", ContextData);
        }
    }
}
