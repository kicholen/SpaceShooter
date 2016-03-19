using Entitas;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsView : View, IView
{
    const int DOUBLE_COINS = 2;

    IServices services;

    public override bool TopPanelVisible() { return false; }
    public override bool BottomPanelVisible() { return false; }

    public ResultsView(IServices services) : base("View/ResultsView")
    {
        this.services = services;
    }

    public override void Init()
    {
        base.Init();
        translateTexts();
        addListeners();
    }

    public override void OnShown(Entity e = null)
    {
        base.OnShown(e);
        animateTexts();
    }

    void animateTexts()
    {
        GameStatsComponent gameStats = pool.GetGroup(Matcher.GameStats).GetSingleEntity().gameStats;
        animateText(getChild("EnemiesText").gameObject, gameStats.shipsDestroyed);
        animateText(getChild("ScoreText").gameObject, gameStats.score);
        animateText(getChild("LevelBonusText").gameObject, services.GamerService.Level);
        animateText(getChild("ScoreTotalText").gameObject, gameStats.score + gameStats.starsPicked * services.GamerService.Level);
        animateText(getChild("CoinsText").gameObject, gameStats.starsPicked);
    }

    void animateText(GameObject goWithText, float to)
    {
        Entity e = pool.CreateEntity()
                    .AddGameObject(goWithText, "", false)
                    .AddTween(false, new List<Tween>());
        e.tween.AddTween(e.gameObject, EaseTypes.linear, GameObjectAccessorType.TEXT, Config.TEXT_ANIM_DURATION)
            .From(0.0f)
            .To(to);
    }

    void addListeners()
    {
        getChild("ExitButton").GetComponent<Button>().onClick.AddListener(() => onExit());
        getChild("AdButton").GetComponent<Button>().onClick.AddListener(onDoubleReward);
    }

    void onExit(bool shouldDoubleCoins = false)
    {
        GameStatsComponent gameStats = pool.GetGroup(Matcher.GameStats).GetSingleEntity().gameStats;
        services.CurrencyService.IncreaseCoins(shouldDoubleCoins ? gameStats.starsPicked * 2 : gameStats.starsPicked);
        services.GameService.EndGame(null);
    }

    void onDoubleReward()
    {
        services.AdService.ShowIfShould(onExit);
    }

    void translateTexts()
    {
        getChild("StateText").GetComponent<Text>().text = hasPlayerWon() ? translationService.Translate("Zwycięstwo!") : translationService.Translate("Porażka");
        getChild("EnemiesInfoText").GetComponent<Text>().text = translationService.Translate("Zniszczonych przeciwników:");
        getChild("ScoreInfoText").GetComponent<Text>().text = translationService.Translate("Zdobytych punktów:");
        getChild("LevelBonusInfoText").GetComponent<Text>().text = translationService.Translate("Bonus za poziom:");
        getChild("ScoreTotalInfoText").GetComponent<Text>().text = translationService.Translate("Punkty totalne:");
        getChild("CoinsInfoText").GetComponent<Text>().text = translationService.Translate("Zdobyte Monety:");
    }

    public override void Destroy()
    {
        base.Destroy();
    }

    bool hasPlayerWon()
    {
        return true;
    }
}