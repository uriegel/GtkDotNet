using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class TreeModel
    {
        [DllImport(Globals.LibGtk, EntryPoint = "gtk_tree_model_get", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr ModelGetInt(IntPtr treemodel, IntPtr iter, int pos, ref int value, int minusOne);
    }
}
