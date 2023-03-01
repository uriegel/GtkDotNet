# GtkDotNet
.NET 6 Bindings for GTK+ 

## Prerequisites

### Ubuntu
* sudo apt install libwebkit2gtk-4.0-dev
### Fedora
* sudo dnf install webkit2gtk3-devel.x86_64

## Installation of GTK Schema
```
    sudo install -D de.uriegel.test.gschema.xml /usr/share/glib-2.0/schemas/
    sudo glib-compile-schemas /usr/share/glib-2.0/schemas/
```     