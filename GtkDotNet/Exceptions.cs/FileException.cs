using System;

namespace GtkDotNet
{
    public class FileException : GErrorException 
    {
        public enum ErrorCode 
        {
            Exist,
            IsDir,
            Access,
            NameTooLong,
            NoEnt,
            NotDir,
            NxIO,
            NoDev,
            ROFS,
            TXTBSY,
            Fault,
            Loop,
            NoSpc,
            NoMem,
            MFile,
            NFile,
            BadF,
            Inval,
            Pipe,
            Again,
            Intr,
            IO,
            Perm,
            NoSys,
            Failed
        }
        public new ErrorCode Code { get => (ErrorCode)base.Code; }
        internal FileException(GError error) : base(error) { }
    }
}