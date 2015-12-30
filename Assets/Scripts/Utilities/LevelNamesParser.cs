using UnityEngine;
using System.Collections;

public static class LevelNameParser
{
    //public TextAsset quoteList;

    public static string[] ParseNames()
    {
        TextAsset quoteList = Resources.Load<TextAsset>("Texts/LevelNames");
        string[] lines = quoteList.text.Split("\n"[0]);
        Resources.UnloadAsset(quoteList);
        return lines;
    }
}
