

using System;
using System.Collections.Generic;

static class Delegates
{
    public static void Add(Delegate delegat) => delegates.Add(delegat);
    static List<Delegate> delegates = new List<Delegate>();
}