namespace GtkDotNet
{
    public class Settings : GObject
    {
        public Settings(string schemaId) : base(Raw.Settings.New(schemaId)) { }

        public new bool GetBool(string key) => Raw.Settings.GetBool(handle, key);
        public new void SetBool(string key, bool value) => Raw.Settings.SetBool(handle, key, value);

        public new int GetInt(string key) => Raw.Settings.GetInt(handle, key);
        public new void SetInt(string key, int value) => Raw.Settings.SetInt(handle, key, value);
    }
}

