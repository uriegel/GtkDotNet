namespace GtkDotNet
{
    public class Settings : GObject
    {
        public Settings(string schemaId) : base(Raw.Gio.NewSettings(schemaId)) { }

        public new bool GetBool(string key) => Raw.Gio.SettingsGetBool(handle, key);
        public new void SetBool(string key, bool value) => Raw.Gio.SettingsSetBool(handle, key, value);

        public new int GetInt(string key) => Raw.Gio.SettingsGetInt(handle, key);
        public new void SetInt(string key, int value) => Raw.Gio.SettingsSetInt(handle, key, value);
    }
}

