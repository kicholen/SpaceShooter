﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RightBottomPanelViewUpdaterBase {
    protected GameObject createInputElement(string infoText, string defaultText, UnityAction<string> onValueChange) {
        GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/InputElement"));
        gameObject.GetComponentInChildren<Text>().text = infoText;
        InputField input = gameObject.GetComponentInChildren<InputField>();
        input.onValueChange.AddListener(onValueChange);
        input.text = defaultText;

        return gameObject;
    }

    protected GameObject createDropdownElement(string infoText, string defaultText, List<string> names, UnityAction<string> onValueChange) {
        GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/DropdownElement"));
        gameObject.GetComponentInChildren<Dropdown>().options = names.Select(name => new Dropdown.OptionData(name)).ToList<Dropdown.OptionData>();
        gameObject.GetComponentInChildren<Dropdown>().value = names.IndexOf(defaultText);
        gameObject.transform.FindChild("Label").GetComponent<Text>().text = infoText;
        gameObject.GetComponentInChildren<Dropdown>().onValueChanged.AddListener(value => onValueChange(value.ToString()));

        return gameObject;
    }
}
