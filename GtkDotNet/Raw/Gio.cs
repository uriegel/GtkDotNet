using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class Gio
    {
        [DllImport(Globals.LibGtk, EntryPoint="g_settings_new", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr NewSettings(string schemaId);

        [DllImport(Globals.LibGtk, EntryPoint="g_resource_new_from_data", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr NewResourceFromData(IntPtr gbytes, IntPtr zero);

        [DllImport(Globals.LibGtk, EntryPoint="g_resources_register", CallingConvention = CallingConvention.Cdecl)]
        public extern static void RegisterResources(IntPtr res);

        [DllImport(Globals.LibGtk, EntryPoint="g_resources_get_info", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool ResourcesGetInfo(string path, int none, out long size, out int flags, IntPtr zero);

        [DllImport(Globals.LibGtk, EntryPoint="g_resources_open_stream", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr ResourcesOpenStream(string path, int none, IntPtr zero);
       
        [DllImport(Globals.LibGtk, EntryPoint="g_settings_set_boolean", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SettingsSetBool(IntPtr obj, string name, bool value);

        [DllImport(Globals.LibGtk, EntryPoint="g_settings_get_boolean", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SettingsGetBool(IntPtr obj, string name);

        [DllImport(Globals.LibGtk, EntryPoint="g_settings_set_int", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SettingsSetInt(IntPtr obj, string name, int value);

        [DllImport(Globals.LibGtk, EntryPoint="g_settings_get_int", CallingConvention = CallingConvention.Cdecl)]
        public extern static int SettingsGetInt(IntPtr obj, string name);
    }
}