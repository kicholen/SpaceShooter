﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EditorViewUpdaterBase : BaseGui {
    protected GameObject createInputElement(string infoText, string defaultText, UnityAction<string> onValueChange) {
        GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/InputElement"));
        gameObject.GetComponentInChildren<Text>().text = infoText;
        InputField input = gameObject.GetComponentInChildren<InputField>();
        input.onValueChanged.AddListener(onValueChange);
        input.text = defaultText;

        return gameObject;
    }

    protected GameObject createDropdownElement(string infoText, string defaultText, List<string> names, UnityAction<string> onValueChange) {
        GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/DropdownElement"));
        names.Sort((x, y) => Convert.ToInt16(x).CompareTo(Convert.ToInt16(y)));
        List<Dropdown.OptionData> options = names.Select(name => new Dropdown.OptionData(name)).ToList<Dropdown.OptionData>();
        gameObject.GetComponentInChildren<Dropdown>().options = options;
        gameObject.GetComponentInChildren<Dropdown>().value = names.IndexOf(defaultText);
        gameObject.transform.FindChild("Label").GetComponent<Text>().text = infoText;
        gameObject.GetComponentInChildren<Dropdown>().onValueChanged.AddListener(value => onValueChange(value.ToString()));

        return gameObject;
    }

    protected GameObject createDropdownElementOfString(string infoText, string defaultText, List<string> names, UnityAction<string> onValueChange) {
        GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/DropdownElement"));
        List<Dropdown.OptionData> options = names.Select(name => new Dropdown.OptionData(name)).ToList<Dropdown.OptionData>();
        gameObject.GetComponentInChildren<Dropdown>().options = options;
        gameObject.GetComponentInChildren<Dropdown>().value = names.IndexOf(defaultText);
        gameObject.transform.FindChild("Label").GetComponent<Text>().text = infoText;
        gameObject.GetComponentInChildren<Dropdown>().onValueChanged.AddListener(value => onValueChange(value.ToString()));

        return gameObject;
    }

    protected GameObject createLanguageElement(string defaultText, string translatedText, UnityAction<string> onValueChange)
    {
        GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Language/LanguageElement"));
        gameObject.GetComponentInChildren<Text>().text = defaultText;
        InputField input = gameObject.GetComponentInChildren<InputField>();
        input.onValueChanged.AddListener(onValueChange);
        input.text = translatedText;

        return gameObject;
    }

    protected GameObject createToggleElement(string text, bool value, UnityAction<bool> onValueChange)
    {
        GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/ToggleElement"));
        gameObject.GetComponentInChildren<Text>().text = text;
        Toggle toggle = gameObject.GetComponentInChildren<Toggle>();
        toggle.isOn = value;
        toggle.onValueChanged.AddListener(onValueChange);

        return gameObject;
    }
}
