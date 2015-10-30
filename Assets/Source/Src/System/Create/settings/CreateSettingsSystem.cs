using Entitas;
using System.Xml;
using UnityEngine;
using System;

public class CreateSettingsSystem : IInitializeSystem, ISetPool {
	Pool _pool;
	
	const string settingsPath = "Settings";
	
	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Initialize() {
		XmlNode node = Utils.loadXml(settingsPath);
		
		XmlNode settings = node.FirstChild;
		SettingsModelComponent model = new SettingsModelComponent();
		while(settings!=null) {

			switch (settings.Name) {
				case "difficulty":
					model.difficulty = Convert.ToInt16(settings.Attributes[0].Value);
					break;
				case "sound":
					model.sound = Convert.ToBoolean(settings.Attributes[0].Value);
					break;
				case "music":
					model.music = Convert.ToBoolean(settings.Attributes[0].Value);
					break;
				case "langugage":
					model.language = settings.Attributes[0].Value;
					break;
				default:
					throw new UnityException("CreateSettingsSystem:: Unexcepted setting");
				break;
			}
			settings = settings.NextSibling;
		}

		_pool.CreateEntity()
			.AddComponent(ComponentIds.SettingsModel, model);
	}
}