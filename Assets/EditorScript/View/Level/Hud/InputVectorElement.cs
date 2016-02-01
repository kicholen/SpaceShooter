using System;
using UnityEngine;
using UnityEngine.UI;

public class InputVectorElement : BaseGui {

    EventService eventService;

    public GameObject Go { get { return go; } }

    public InputVectorElement(string name, EventService eventService, Vector2 startValue, Action<float> onChangeX, Action<float> onChangeY) {
        go = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/InputVectorElement"));
        getChild("Text").GetComponent<Text>().text = name;
        this.eventService = eventService;
        addListenerToInputFieldAndSetStartValue("InputFieldX", onChangeX, startValue.x);
        addListenerToInputFieldAndSetStartValue("InputFieldY", onChangeY, startValue.y);
    }

    void addListenerToInputFieldAndSetStartValue(string childPath, Action<float> onChange, float startValue) {
        InputField input = getChild(childPath).GetComponentInChildren<InputField>();
        input.onValueChanged.AddListener((value) => {
            onChange(getFloatValueFromString(value));
        });
        input.text = startValue.ToString();
    }

    float getFloatValueFromString(string value) {
        float result = 0.0f;
        try {
            result = (float)Convert.ToDouble(value);
        }
        catch {
            showMessage();
        }
        return result;
    }

    void showMessage() {
        eventService.Dispatch<InfoBoxShowEvent>(new InfoBoxShowEvent("Value will not be properly saved"));
    }
}