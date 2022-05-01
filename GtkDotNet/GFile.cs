using System;
using System.Runtime.InteropServices;

namespace GtkDotNet;

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
    public extern static bool Trash(IntPtr file, IntPtr cancellable, ref IntPtr error);

    [DllImport(Globals.LibGtk, EntryPoint = "g_file_copy", CallingConvention = CallingConvention.Cdecl)]
    public extern static bool Copy(IntPtr source, IntPtr destination, FileCopyFlags flags, IntPtr cancellable, FileProgressCallback progress, IntPtr data, ref IntPtr error);

    [DllImport(Globals.LibGtk, EntryPoint = "g_file_move", CallingConvention = CallingConvention.Cdecl)]
    public extern static bool Move(IntPtr source, IntPtr destination, FileCopyFlags flags, IntPtr cancellable, FileProgressCallback progress, IntPtr data, ref IntPtr error);

    [DllImport(Globals.LibGtk, EntryPoint = "g_file_get_basename", CallingConvention = CallingConvention.Cdecl)]
    public extern static string GetBasename(IntPtr file);

    [DllImport(Globals.LibGtk, EntryPoint = "g_file_get_path", CallingConvention = CallingConvention.Cdecl)]
    public extern static string GetPath(IntPtr file);
    
    public static (IntPtr content, long length)? LoadContents(IntPtr file)
    {
        var result = LoadContents(file, IntPtr.Zero, out var content, out var length, IntPtr.Zero, IntPtr.Zero);    
        if (result)
            return (content, length);
        else
            return null;
    }

    [DllImport(Globals.LibGtk, EntryPoint = "g_file_enumerate_children_async", CallingConvention = CallingConvention.Cdecl)]   
    public extern static bool EnumerateChildrenAsync(IntPtr file, string attributes, FileQueryInfoFlags flags, int ioPriority, IntPtr cancellable, AsyncReadyCallback cb, IntPtr zero);

    [DllImport(Globals.LibGtk, EntryPoint = "g_file_enumerate_children_finish", CallingConvention = CallingConvention.Cdecl)]   
    public extern static IntPtr EnumerateChildrenFinish(IntPtr file, IntPtr asyncResult, IntPtr error);

    [DllImport(Globals.LibGtk, EntryPoint = "g_file_load_contents", CallingConvention = CallingConvention.Cdecl)]
    extern static bool LoadContents(IntPtr file, IntPtr cancellable, out IntPtr content, out int length, IntPtr etagOut, IntPtr error);
    
    public delegate void FileProgressCallback(long current, long total, IntPtr zero);
    public delegate void AsyncReadyCallback(IntPtr source, IntPtr result, IntPtr zero);
}
