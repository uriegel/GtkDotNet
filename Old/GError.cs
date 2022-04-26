using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    [StructLayout(LayoutKind.Sequential)]
    public struct GError  
    {
        public uint Domain;
        public int Code;
        public string Message;

        internal GError(uint domain, int code, string message)
        {
            Domain = domain;
            Code = code;
            Message = message;
        }

        internal GError(IntPtr error)
        {
            if (error != IntPtr.Zero)
            {
                this = Marshal.PtrToStructure<GError>(error);
                Raw.GError.Free(error);
            }
            else
            {
                Domain = 0;
                Code = 0;
                Message = null;
            }
        }
    }
}
