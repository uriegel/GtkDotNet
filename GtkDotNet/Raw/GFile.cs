using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class GFile
    {
        /// <summary>
        /// Creates a new GFile object. Free it with GObject.Unref
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [DllImport(Globals.LibGtk, EntryPoint = "g_file_new_for_path", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr New(string path);

        [DllImport(Globals.LibGtk, EntryPoint = "g_file_trash", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool FileTrash(IntPtr file, IntPtr cancellable, ref IntPtr error);
    }
}