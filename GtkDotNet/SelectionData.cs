using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace GtkDotNet;

public class SelectionData 
{
    public SelectionData(IntPtr handle)
    => this.handle = handle;

    public void SetUris(string[] uris)
    {
        Raw.SelectionData.DataSetUris(handle, uris);
    }

    [StructLayout(LayoutKind.Sequential)] 
    public struct ArrayOfString 
    { 
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=100)] 
        public IntPtr[] listOfStrings; 
    }


    IntPtr handle;
}
