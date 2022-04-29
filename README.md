# GtkDotNet
.NET 6 Bindings for GTK 4.0 

## Prerequisites

### Ubuntu
* sudo apt install libgtk-4-dev

### Cambalache
* Install Cambalache via flatpak. It is the GTK 4 replacement of glade

## Installation of GTK Schema
```
    sudo install -D org.gtk.exampleapp.gschema.xml /usr/share/glib-2.0/schemas/
    sudo glib-compile-schemas /usr/share/glib-2.0/schemas/
```     