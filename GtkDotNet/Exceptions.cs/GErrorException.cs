using System;

namespace GtkDotNet
{
    public class GErrorException : Exception 
    {
        public uint Domain { get; }
        public int Code { get; }

        internal static Exception New(GError error)
            => error.Domain switch
            {
                226 => new FileException(error) as GErrorException,
                _ => new GErrorException(error)
            };

        internal GErrorException(GError error) : base(error.Message)
        {
            Domain = error.Domain;
            Code = error.Code;
        }
    }
}
