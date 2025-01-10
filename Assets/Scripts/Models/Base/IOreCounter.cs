using System;
using System.Collections.Generic;

public interface IOreCounter
{
    event Action<Dictionary<string, int>> OreAdded;
}
