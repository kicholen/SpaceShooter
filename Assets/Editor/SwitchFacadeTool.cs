using System.IO;
using UnityEditor;
using UnityEngine;

public class SwitchFacadeTool
{
    const string facadePath = "/Script/Controller/Facade/Facade.cs";

    [MenuItem("Jelly/Facade/Locale")]
    public static void SetLocalFacade()
    {
        replaceTextInFacadeFile("HerokuFacadeParameters", "LocalFacadeParameters");
    }

    [MenuItem("Jelly/Facade/Heroku")]
    public static void SetHerokuFacade()
    {
        replaceTextInFacadeFile("LocalFacadeParameters", "HerokuFacadeParameters");
    }

    static void replaceTextInFacadeFile(string from, string to)
    {
        string path = Application.dataPath + facadePath;
        string content = File.ReadAllText(path).Replace(from, to);
        File.WriteAllText(path, content);
    }
}