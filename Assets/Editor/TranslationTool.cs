using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using RSG;
using System;

public class TranslationTool
{
    const string path = "/Script/View/";
    const string prefix = "Translate(\"";
    const string sufix = "\")";

    static RequestBuilder builder = new RequestBuilder();
    static Dictionary<string, string> result = new Dictionary<string, string>();
    static List<Translation> translations = new List<Translation>();

    [MenuItem("Jelly/RefreshTranslations")]
    public static void RefreshTranslations() {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneWindows64);
        parse(new System.IO.DirectoryInfo(Application.dataPath + path));

        getLanguageIds().Then<long>(ids => {
            Promise<long> promise = new Promise<long>();
            foreach (KeyValuePair<long, string> entry in ids) {
                if (entry.Value.Equals("pl"))
                    promise.Resolve(entry.Key);
            }
            return promise;
        })
        .Catch(exception => Debug.Log(exception.Message))
        .Done(remove);
    }

    static void remove(long id) {
        removeLanguage(id)
        .Catch(exception => Debug.Log(exception.Message))
        .Done(create);
    }

    static void create() {
        createLanguage()
        .Catch(exception => Debug.Log(exception.Message))
        .Done(upload);
    }

    static void upload(long id) {
        foreach (KeyValuePair<string, string> entry in result) {
            translations.Add(new Translation(entry.Key, entry.Key));
        }
        updateLanguage(id)
        .Catch(exception => Debug.Log(exception.Message))
        .Done(() => Debug.Log("Upload Completed"));
    }

    static Promise<Dictionary<long, string>> getLanguageIds() {
        Promise<Dictionary<long, string>> promise = new Promise<Dictionary<long, string>>();
        GetLanguageIds request = new GetLanguageIds();
        builder.Build(request);
        while (!request.Process().isDone) { }

        if (request.Successful()) {
            request.ParseResult();
            promise.Resolve(request.LanguageIds);
        }
        else {
            promise.Reject(new Exception(request.GetErrorMessage()));
        }
        return promise;
    }

    static Promise removeLanguage(long id) {
        Promise promise = new Promise();
        DeleteLanguage request = new DeleteLanguage(id);
        builder.Build(request);
        while (!request.Process().isDone) { }

        if (request.Successful()) {
            promise.Resolve();
        }
        else {
            promise.Reject(new Exception(request.GetErrorMessage()));
        }
        return promise;
    }

    static Promise<long> createLanguage() {
        Promise<long> promise = new Promise<long>();
        CreateLanguage request = new CreateLanguage();
        builder.Build(request);
        while (!request.Process().isDone) { }

        if (request.Successful()) {
            request.ParseResult();
            promise.Resolve(request.Model.id);
        }
        else {
            promise.Reject(new Exception(request.GetErrorMessage()));
        }
        return promise;
    }

    static Promise updateLanguage(long id) {
        Promise promise = new Promise();
        UpdateLanguage request = new UpdateLanguage(new LanguageModel(id, "pl", translations));
        builder.Build(request);
        while (!request.Process().isDone) { }

        if (request.Successful()) {
            promise.Resolve();
        }
        else {
            promise.Reject(new Exception(request.GetErrorMessage()));
        }
        return promise;
    }

    static void parse(DirectoryInfo directory) {
        parseFiles(directory);
        parseDirectories(directory.GetDirectories());
    }

    static void parseDirectories(DirectoryInfo[] directories) {
        foreach (System.IO.DirectoryInfo info in directories)
            parse(info);
    }

    static List<string> parseFiles(DirectoryInfo directory) {
        List<string> list = new List<string>();
        System.IO.FileInfo[] infos = directory.GetFiles();
        foreach (System.IO.FileInfo info in infos) {
            if (info.Name.EndsWith(".cs")) {
                parseFile(info);
            }
        }
        return list;
    }

    static void parseFile(FileInfo info) {
        using (StreamReader reader = info.OpenText()) {
            while (!reader.EndOfStream) {
                string line = reader.ReadLine();
                if (line != null && line.Length > 0) {
                    int index = line.IndexOf(prefix);
                    if (index >= 0) {
                        line = line.Substring(index + prefix.Length);
                        line = line.Substring(0, line.IndexOf(sufix));
                        result[line] = line;
                    }
                }
            }
        }
    }
}