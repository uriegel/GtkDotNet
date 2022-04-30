﻿using System;
using System.Diagnostics;
using System.Linq;
using GtkDotNet;

var app = Application.New("org.gtk.example");
Action onActivate = () => 
{
    var settings = Settings.New("org.gtk.exampleapp");

    Application.RegisterResources();
    var builder = Builder.FromResource("/org/gtk/example/window.ui");
    Builder.AddFromFile(builder, "builder.ui");
    var window = Builder.GetObject(builder, "window");
    var stack = Builder.GetObject(builder, "stack");
    var gears = Builder.GetObject(builder, "gears");
    var search = Builder.GetObject(builder, "search");
    var searchBar = Builder.GetObject(builder, "searchbar");
    var searchEntry = Builder.GetObject(builder, "searchentry");
    var sidebar = Builder.GetObject(builder, "sidebar");
    var wordsListbox = Builder.GetObject(builder, "words");
    Window.SetApplication(window, app);
    GObject.Unref(builder);

    var action = Settings.CreateAction(settings, "show-words");
    ActionMap.AddAction(window, action);
    GObject.Unref(action);

    var menuBuilder = Builder.FromResource("/org/gtk/example/menu.ui");
    var menu = Builder.GetObject(menuBuilder, "menu");
    MenuButton.SetModel(gears, menu);
    GObject.Unref(menuBuilder);

    GObject.BindProperty(search, "active", searchBar, "search-mode-enabled", BindingFlags.Bidirectional);

    Settings.Bind(settings, "transition", stack, "transition-type", BindFlags.Default);
    Settings.Bind(settings, "show-words", sidebar, "reveal-child", BindFlags.Default);
    var openPreferences = () =>
    {
        var dialogBuilder = Builder.FromResource("/org/gtk/example/dialog.ui");
        var dialog = Builder.GetObject(dialogBuilder, "dialog");
        var transition = Builder.GetObject(dialogBuilder, "transition");
        var font = Builder.GetObject(dialogBuilder, "font");

        Window.SetTransientFor(dialog, window);
        Widget.Show(dialog);
        GObject.Unref(dialogBuilder);
        Settings.Bind(settings, "font", font, "font", BindFlags.Default);
        Settings.Bind(settings, "transition", transition, "active-id", BindFlags.Default);
    };

    var actions = new GtkAction[] 
    {
        new GtkAction("preferences", openPreferences),
        new GtkAction("quit", () => Window.Close(window), "<Ctrl>Q")
    };
    Application.AddActions(app, actions);

    void searchTextChanged(IntPtr z1, IntPtr z2)
    {
        var text = Editable.GetText(searchEntry);
        if (text.Length == 0)
            return;

        var tab = Stack.GetVisibleChild(stack);
        var textView = ScrolledWindow.GetChild(tab);
        var buffer = TextView.GetBuffer(textView);
        
        TextBuffer.GetStartIter(buffer, out var startIter);
        if (TextIter.ForwardSearch(ref startIter, text, SearchFlags.CaseInsensitive, out var matchStart, out var matchEnd, IntPtr.Zero))
        {
            TextBuffer.SelectRange(buffer, ref matchStart, ref matchEnd);
            TextView.ScrollToIter(textView, ref matchStart, 0.0, false, 0.0, 0.0);
        }
    }

    void visibleChildChanged(IntPtr stack, IntPtr pec,  IntPtr z)
    {
        SearchBar.SetSearchMode(searchBar, false);
        updateWords();
    }

    void FindWord(IntPtr button)
    {
        var word = Button.GetLabel(button);
        Editable.SetText(searchEntry, word);
    }

    void updateWords()
    {
        var tab = Stack.GetVisibleChild(stack);
        if (tab == IntPtr.Zero)
            return;

        var textView = ScrolledWindow.GetChild(tab);
        var buffer = TextView.GetBuffer(textView);
        TextBuffer.GetStartIter(buffer, out var startIter);
        TextBuffer.GetEndIter(buffer, out var endIter);
        var text = TextBuffer.GetText(buffer, ref startIter, ref endIter, false);
        var words = text
            .Split(new[] {' ', '\n', '.', '"', '(', ')', ';', '}', '{', '/', ',', '<', '>', '=' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(n => n.Trim())
            .Where(n => n.Length > 0)
            .Distinct();

        IntPtr first;
        while ((first = Widget.GetFirstChild(wordsListbox)) != IntPtr.Zero)
            Listbox.Remove(wordsListbox, first);
        foreach (var word in words)
        {
            var row = Button.NewWithLabel(word);
            Gtk.SignalConnect(row, "clicked", () => FindWord(row));
            Listbox.Insert(wordsListbox, row);
        }
    }

    void wordsChanged(IntPtr obj, IntPtr spec, IntPtr z) => updateWords();

    Gtk.SignalConnect<TwoIntPtr>(searchEntry, "search-changed", searchTextChanged);
    Gtk.SignalConnect<ThreeIntPtr>(stack, "notify::visible-child", visibleChildChanged);
    Gtk.SignalConnect<ThreeIntPtr>(sidebar, "notify::reveal-child", wordsChanged);
    Gtk.SignalConnect(window, "destroy", () => GObject.Clear(settings));

    Widget.Show(window);

    var currentDirectory = Directory.GetCurrentDirectory();
    var files = System.Environment.CommandLine.Split(' ').Skip(1).Select(n => GFile.New(Path.Combine(currentDirectory, n)));
    foreach (var file in files)
    {
        var name = GFile.GetBasename(file);
        var scrolled = ScrolledWindow.New ();
        Widget.SetHExpand(scrolled, true);
        Widget.SetVExpand(scrolled, true);
        var textView = TextView.New();
        TextView.SetEditable(textView, false);
        TextView.SetCursorVisible(textView, false);
        ScrolledWindow.SetChild(scrolled, textView);
        Stack.AddTitled(stack, scrolled, name, name);
        var content = GFile.LoadContents(file);
        if (content.HasValue)
        {
            var buffer = TextView.GetBuffer(textView);
            TextBuffer.SetText(buffer, content.Value.content, (int)content.Value.length);
            GObject.Free(content.Value.content);

            var tag = TextBuffer.CreateTag(buffer);
            Settings.Bind(settings, "font", tag, "font", BindFlags.Default);

            TextBuffer.GetStartIter(buffer, out var startIter);
            TextBuffer.GetEndIter(buffer, out var endIter);
            TextBuffer.ApplyTag(buffer, tag, ref startIter, ref endIter);
        }
    }
    Widget.SetSensitive(search, true);
    updateWords();
};

var status = Application.Run(app, onActivate);

GObject.Unref(app);

return status;

delegate void TwoIntPtr(IntPtr z1, IntPtr z2);
delegate void ThreeIntPtr(IntPtr z1, IntPtr z2, IntPtr z3);
