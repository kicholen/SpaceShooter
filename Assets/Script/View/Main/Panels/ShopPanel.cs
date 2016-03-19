using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : BaseGui, IPanel
{
    public PanelType PanelType { get { return PanelType.SHOP; } }

    IServices services;

    public ShopPanel(Transform content, IServices services)
    {
        go = content.gameObject;
        this.services = services;
        setReferences();
        createElements();
    }

    public void Disable()
    {
    }

    public void Enable()
    {
    }

    void setReferences()
    {
        getChild("GemInfoPanel/Text").GetComponent<Text>().text = services.TranslationService.Translate("Klejnoty");
        getChild("CoinInfoPanel/Text").GetComponent<Text>().text = services.TranslationService.Translate("Monety");
    }

    void createElements()
    {
        createGems();
        createCoins();
    }

    void createCoins()
    {
        List<ShopModel> models = services.ShopService.CoinItems;
        for (int i = 0; i < models.Count; i++)
        {
            ShopElement element = new ShopElement(getChild("CoinPanel/Element" + (i + 1).ToString()), services, models[i]);
            element.AddClickListener(onButtonClick);
        }
    }

    void createGems()
    {
        List<ShopModel> models = services.ShopService.GemItems;
        for (int i = 0; i < models.Count; i++)
        {
            ShopElement element = new ShopElement(getChild("GemPanel/Element" + (i + 1).ToString()), services, models[i]);
            element.AddClickListener(onButtonClick);
        }
    }

    void onButtonClick(ShopModel model)
    {
        bool added = false;
        services.ShopService.Buy(model)
            .Then(result => { added = true; })
            .Catch(exception => { added = false; });// show floating Text
    }
}
