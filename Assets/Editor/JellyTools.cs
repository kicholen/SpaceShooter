using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class JellyTools {
    static RequestBuilder builder = new RequestBuilder();

    [MenuItem("Jelly/DownloadAll")]
    public static void DownloadAll() {
        downloadPaths(savePaths);
        downloadLevels(saveLevels);
    }

    static void downloadPaths(Action<List<PathModelComponent>> onPathsLoaded) {
        GetPaths request = new GetPaths();
        builder.Build(request);
        while (!request.Process().isDone) { }

        if (request.Successful()) {
            request.ParseResult();
            onPathsLoaded(request.Paths);
        }
        else {
            onRequestFailed(request.GetErrorMessage());
        }
    }

    static void downloadLevels(Action<List<LevelModelComponent>> onLevelsLoaded) {
        GetLevels request = new GetLevels();
        builder.Build(request);
        while (!request.Process().isDone) { }

        if (request.Successful()) {
            request.ParseResult();
            onLevelsLoaded(request.Levels);
        }
        else {
            onRequestFailed(request.GetErrorMessage());
        }
    }

    static void saveLevels(List<LevelModelComponent> levels) {
        foreach (LevelModelComponent level in levels)
            Utils.Serialize(level, level.name);
    }

    static void savePaths(List<PathModelComponent> paths) {
        foreach (PathModelComponent path in paths)
            Utils.Serialize(path, path.name);
    }

    static void onRequestFailed(string message) {
        Debug.Log(message);
    }
}