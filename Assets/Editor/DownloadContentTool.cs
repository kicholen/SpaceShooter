using RSG;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DownloadContentTool {
    static RequestBuilder builder = new RequestBuilder();

    [MenuItem("Jelly/DownloadAll")]
    public static void DownloadAll() {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneWindows64);
        Promise.All(
            downloadPaths().Then(models => savePaths(models)),
            downloadLevels().Then(models => saveLevels(models)),
            downloadEnemies().Then(models => saveEnemies(models)),
            downloadBonuses().Then(models => saveBonuses(models)),
            downloadDifficulties().Then(models => saveDifficulties(models)),
            downloadLanguages().Then(models => saveLanguages(models))
        )
        .Catch(exception => Debug.Log(exception.Message))
        .Done(() => Debug.Log("Download Completed"));
    }

    static IPromise<List<PathModel>> downloadPaths() {
        Promise<List<PathModel>> promise = new Promise<List<PathModel>>();
        GetPaths request = new GetPaths();
        builder.Build(request);
        while (!request.Process().isDone) { }

        if (request.Successful()) {
            request.ParseResult();
            promise.Resolve(request.Paths);
        }
        else {
            promise.Reject(new Exception(request.GetErrorMessage()));
        }
        return promise;
    }

    static IPromise<List<LevelModelComponent>> downloadLevels() {
        Promise<List<LevelModelComponent>> promise = new Promise<List<LevelModelComponent>>();
        GetLevels request = new GetLevels();
        builder.Build(request);
        while (!request.Process().isDone) { }

        if (request.Successful()) {
            request.ParseResult();
            promise.Resolve(request.Levels);
        }
        else {
            promise.Reject(new Exception(request.GetErrorMessage()));
        }
        return promise;
    }

    static IPromise<List<EnemyModel>> downloadEnemies() {
        Promise<List<EnemyModel>> promise = new Promise<List<EnemyModel>>();
        GetEnemies request = new GetEnemies();
        builder.Build(request);
        while (!request.Process().isDone) { }

        if (request.Successful()) {
            request.ParseResult();
            promise.Resolve(request.Enemies);
        }
        else {
            promise.Reject(new Exception(request.GetErrorMessage()));
        }
        return promise;
    }

    static IPromise<List<BonusModelComponent>> downloadBonuses() {
        Promise<List<BonusModelComponent>> promise = new Promise<List<BonusModelComponent>>();
        GetBonuses request = new GetBonuses();
        builder.Build(request);
        while (!request.Process().isDone) { }

        if (request.Successful()) {
            request.ParseResult();
            promise.Resolve(request.Bonuses);
        }
        else {
            promise.Reject(new Exception(request.GetErrorMessage()));
        }
        return promise;
    }

    static IPromise<List<DifficultyModelComponent>> downloadDifficulties() {
        Promise<List<DifficultyModelComponent>> promise = new Promise<List<DifficultyModelComponent>>();
        GetDifficulties request = new GetDifficulties();
        builder.Build(request);
        while (!request.Process().isDone) { }

        if (request.Successful()) {
            request.ParseResult();
            promise.Resolve(request.Difficulties);
        }
        else {
            promise.Reject(new Exception(request.GetErrorMessage()));
        }
        return promise;
    }

    static IPromise<List<LanguageModel>> downloadLanguages() {
        Promise<List<LanguageModel>> promise = new Promise<List<LanguageModel>>();
        GetLanguages request = new GetLanguages();
        builder.Build(request);
        while (!request.Process().isDone) { }

        if (request.Successful()) {
            request.ParseResult();
            promise.Resolve(request.Languages);
        }
        else {
            promise.Reject(new Exception(request.GetErrorMessage()));
        }
        return promise;
    }

    static IPromise saveLevels(List<LevelModelComponent> levels) {
        Promise promise = new Promise();
        foreach (LevelModelComponent level in levels)
            Utils.Serialize(level, level.id.ToString());
        promise.Resolve();
        return promise;
    }

    static IPromise savePaths(List<PathModel> paths) {
        Promise promise = new Promise();
        for (int i = 0; i < paths.Count; i++)
            Utils.Serialize(paths[i], i.ToString());
        promise.Resolve();
        return promise;
    }

    static IPromise saveEnemies(List<EnemyModel> enemies) {
        Promise promise = new Promise();
        for (int i = 0; i < enemies.Count; i++)
            Utils.Serialize(enemies[i], i.ToString());
        promise.Resolve();
        return promise;
    }

    static IPromise saveDifficulties(List<DifficultyModelComponent> difficulties) {
        Promise promise = new Promise();
        foreach (DifficultyModelComponent difficulty in difficulties)
            Utils.Serialize(difficulty, difficulty.type.ToString());
        promise.Resolve();
        return promise;
    }

    static IPromise saveBonuses(List<BonusModelComponent> bonuses) {
        Promise promise = new Promise();
        for (int i = 0; i < bonuses.Count; i++)
            Utils.Serialize(bonuses[i], i.ToString());
        promise.Resolve();
        return promise;
    }

    static IPromise saveLanguages(List<LanguageModel> languages) {
        Promise promise = new Promise();
        foreach (LanguageModel language in languages)
            Utils.Serialize(language, language.name);
        promise.Resolve();
        return promise;
    }
}