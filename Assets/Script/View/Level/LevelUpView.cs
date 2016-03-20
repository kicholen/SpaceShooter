using Entitas;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpView : View, IView
{
    IServices services;

    public override bool TopPanelVisible() { return false; }
    public override bool BottomPanelVisible() { return false; }

    public LevelUpView(IServices services) : base("View/LevelUpView")
    {
        this.services = services;
    }

    public override void Init()
    {
        base.Init();
        translateTexts();
        addListeners();
        setRewards();
    }

    void translateTexts()
    {
        getChild("StateText").GetComponent<Text>().text = services.TranslationService.Translate("Gratulacje!");
        getChild("RewardText").GetComponent<Text>().text = services.TranslationService.Translate("Nagrody:");
        getChild("LevelText").GetComponent<Text>().text = services.GamerService.Level.ToString();
    }

    void addListeners()
    {
        getChild("OkButton").GetComponent<Button>().onClick.AddListener(onOkClick);
    }

    void setRewards()
    {
        setReward("RewardPanel/SecondReward/", services.GamerService.ProgressModel.coins, "Star");
        setReward("RewardPanel/ThirdReward/", services.GamerService.ProgressModel.gems, "Star");
        setBonusRewardIfExist();
    }

    void setBonusRewardIfExist()
    {
        Entity[] entities = pool.GetGroup(Matcher.BonusModel).GetEntities();
        foreach (KeyValuePair<int, int> entry in services.GamerService.ProgressModel.bonusRewards)
            for (int i = 0; i < entities.Length; i++)
                if (entities[i].bonusModel.type == entry.Key)
                    setReward("RewardPanel/FirstReward/", entry.Value, entities[i].bonusModel.resource.Replace("Collider/", ""));
    }

    void setReward(string name, int count, string resource)
    {
        getChild(name + "Image").GetComponent<Image>().sprite = uiFactoryService.CreateSprite(resource);
        getChild(name + "Text").GetComponent<Text>().text = "x" + count;
    }

    void onOkClick()
    {
        services.CurrencyService.IncreaseCoins(services.GamerService.ProgressModel.coins);
        services.CurrencyService.IncreaseGems(services.GamerService.ProgressModel.gems);
        services.GameService.EndGame(null);
    }

    public override void OnShown(Entity e = null)
    {
        base.OnShown(e);
    }
}
