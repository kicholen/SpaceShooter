using Entitas;
using System.Reflection;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShipView : View, IView {

	PlayerModelComponent playerModel;

	public ShipView(Pool pool, IUIFactoryService uiFactoryService, EventService eventService, IViewService viewService)
	: base(pool, uiFactoryService, eventService, "View/ShipView") {
		playerModel = pool.GetGroup(Matcher.PlayerModel).GetSingleEntity().playerModel;

		GameObject button = uiFactoryService.CreatePrefab("Element/SimpleButton");
		button.name = "Save";
		button.transform.SetParent(go.transform, false);
		uiFactoryService.AddText(button.transform, "Text", "Save");
		uiFactoryService.AddButton(button, () => viewService.SetView(ViewTypes.LANDING));
		setData();
	}

	void setData() {
		FieldInfo[] fields = playerModel.GetType().GetFields();
		for (int i = 0; i < fields.Length; i++) {
			Type fieldType = fields[i].GetValue(playerModel).GetType();

			if (fieldType == typeof(int) || fieldType == typeof(float)) {
				GameObject slider = uiFactoryService.CreatePrefab("Element/SimpleSlider");
				slider.name = fields[i].ToString();
				slider.transform.SetParent(go.transform, false);
				uiFactoryService.AddText(slider.transform, "Label", slider.name);
				uiFactoryService.AddText(slider.transform, "Min", ((float)Convert.ToDouble(fields[i].GetValue(playerModel)) * 0.5f).ToString());
				uiFactoryService.AddText(slider.transform, "Max", ((float)Convert.ToDouble(fields[i].GetValue(playerModel)) * 1.5f).ToString());
				slider.GetComponent<Slider>().minValue = (float)Convert.ToDouble(fields[i].GetValue(playerModel)) * 0.5f;
				slider.GetComponent<Slider>().maxValue = (float)Convert.ToDouble(fields[i].GetValue(playerModel)) * 1.5f;
				slider.GetComponent<Slider>().value = (float)Convert.ToDouble(fields[i].GetValue(playerModel));
				slider.GetComponent<Slider>().onValueChanged.AddListener(onSliderValueChanged);
			}
			else if (fieldType == typeof(Boolean)) {
				GameObject toggle = uiFactoryService.CreatePrefab("Element/SimpleToggle");
				toggle.name = fields[i].ToString();
				toggle.transform.SetParent(go.transform, false);
				uiFactoryService.AddText(toggle.transform, "Label", toggle.name);
				toggle.GetComponent<Toggle>().isOn = (bool)fields[i].GetValue(playerModel);
				toggle.GetComponent<Toggle>().onValueChanged.AddListener(onToggleValueChanged);
			}
		}
	}
	
	void onSliderValueChanged(float value) {
		FieldInfo[] fields = playerModel.GetType().GetFields();
		string name = EventSystem.current.currentSelectedGameObject.name;
		foreach (FieldInfo field in fields) {
			if (field.ToString() == name) {
				if (field.GetValue(playerModel).GetType() == typeof(int)) {
					field.SetValue(playerModel, Convert.ToInt16(value));
				}
				else {
					field.SetValue(playerModel, value);
				}
			}
		}
	}

	void onToggleValueChanged(bool value) {
		FieldInfo[] fields = playerModel.GetType().GetFields();
		string name = EventSystem.current.currentSelectedGameObject.name;
		foreach (FieldInfo field in fields) {
			if (field.ToString() == name) {
				field.SetValue(playerModel, value);
			}
		}
	}
}