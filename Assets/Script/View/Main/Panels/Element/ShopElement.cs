using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopElement : BaseGui
{
    IServices services;
    ShopModel model;

    Text title;
    Text count;
    Text price;

    public ShopElement(Transform transform, IServices services, ShopModel model)
    {
        go = transform.gameObject;
        this.services = services;
        this.model = model;
        setReferences();
        setTexts();
    }

    public void AddClickListener(Action<ShopModel> onClick)
    {
        go.GetComponent<Button>().onClick.AddListener(() => onClick(model));
    }

    void setReferences()
    {
        title = getChild("TitleText").GetComponent<Text>();
        count = getChild("Slot/Count").GetComponent<Text>();
        price = getChild("PriceText").GetComponent<Text>();
    }

    void setTexts()
    {
        title.text = services.TranslationService.Translate(model.title);
        count.text = model.count.ToString();
        price.text = getPriceText();
    }

    string getPriceText()
    {
        return model.type == ShopType.COINS ? model.price.ToString() : model.currency + model.priceCurrency;
    }
}