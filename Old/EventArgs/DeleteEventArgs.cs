using System;

namespace GtkDotNet
{
    public class DeleteEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
   }
}