using System.Collections.Generic;
using UnityEngine;

public class LanguagePanelHud : EditorViewUpdaterBase
{
    Transform content;
    LanguageModel model;
    LanguageModel defaultModel;

    public LanguagePanelHud(Transform content, LanguageModel model, LanguageModel defaultModel)
    {
        this.content = content;
        this.model = model;
        this.defaultModel = defaultModel;
        setData();
    }

    void setData()
    {
        foreach (KeyValuePair<string, string> entry in defaultModel.data)
        {
            if (!model.data.ContainsKey(entry.Key))
                createTranslationField(entry.Key, "").transform.SetParent(content, false);
            else
                createTranslationField(entry.Key, model.data[entry.Key]).transform.SetParent(content, false);
        }
    }

    GameObject createTranslationField(string defaultText, string translatedText)
    {
        return createLanguageElement(defaultText, translatedText, (value) => {
            model.data[defaultText] = value;
        });
    }
}
