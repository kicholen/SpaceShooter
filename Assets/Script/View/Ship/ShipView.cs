using Entitas;
using System.Reflection;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShipView : View, IView {

	ShipModelComponent shipModel;

	public ShipView(Pool pool, IUIFactoryService uiFactoryService, EventService eventService, IViewService viewService)
	: base(pool, uiFactoryService, eventService, "View/ShipView") {
		shipModel = pool.GetGroup(Matcher.ShipModel).GetEntities()[0].shipModel;

		GameObject button = uiFactoryService.CreatePrefab("Element/SimpleButton");
		button.name = "Save";
		button.transform.SetParent(go.transform, false);
		uiFactoryService.AddText(button.transform, "Text", "Save");
		uiFactoryService.AddButton(button, () => viewService.SetView(ViewTypes.LANDING));
		setData();
	}

	void setData() {
		FieldInfo[] fields = shipModel.GetType().GetFields();
		for (int i = 0; i < fields.Length; i++) {
			Type fieldType = fields[i].GetValue(shipModel).GetType();

			if (fieldType == typeof(int) || fieldType == typeof(float)) {
				GameObject slider = uiFactoryService.CreatePrefab("Element/SimpleSlider");
				slider.name = fields[i].ToString();
				slider.transform.SetParent(go.transform, false);
				uiFactoryService.AddText(slider.transform, "Label", slider.name);
				uiFactoryService.AddText(slider.transform, "Min", ((float)Convert.ToDouble(fields[i].GetValue(shipModel)) * 0.5f).ToString());
				uiFactoryService.AddText(slider.transform, "Max", ((float)Convert.ToDouble(fields[i].GetValue(shipModel)) * 1.5f).ToString());
				slider.GetComponent<Slider>().minValue = (float)Convert.ToDouble(fields[i].GetValue(shipModel)) * 0.5f;
				slider.GetComponent<Slider>().maxValue = (float)Convert.ToDouble(fields[i].GetValue(shipModel)) * 1.5f;
				slider.GetComponent<Slider>().value = (float)Convert.ToDouble(fields[i].GetValue(shipModel));
				slider.GetComponent<Slider>().onValueChanged.AddListener(onSliderValueChanged);
			}
			else if (fieldType == typeof(Boolean)) {
				GameObject toggle = uiFactoryService.CreatePrefab("Element/SimpleToggle");
				toggle.name = fields[i].ToString();
				toggle.transform.SetParent(go.transform, false);
				uiFactoryService.AddText(toggle.transform, "Label", toggle.name);
				toggle.GetComponent<Toggle>().isOn = (bool)fields[i].GetValue(shipModel);
				toggle.GetComponent<Toggle>().onValueChanged.AddListener(onToggleValueChanged);
			}
		}
	}
	
	void onSliderValueChanged(float value) {
		FieldInfo[] fields = shipModel.GetType().GetFields();
		string name = EventSystem.current.currentSelectedGameObject.name;
		foreach (FieldInfo field in fields) {
			if (field.ToString() == name) {
				if (field.GetValue(shipModel).GetType() == typeof(int)) {
					field.SetValue(shipModel, Convert.ToInt16(value));
				}
				else {
					field.SetValue(shipModel, value);
				}
			}
		}
	}

	void onToggleValueChanged(bool value) {
		FieldInfo[] fields = shipModel.GetType().GetFields();
		string name = EventSystem.current.currentSelectedGameObject.name;
		foreach (FieldInfo field in fields) {
			if (field.ToString() == name) {
				field.SetValue(shipModel, value);
			}
		}
	}
}