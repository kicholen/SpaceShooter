using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _indicators;
	
	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.IndicatorPanel, Matcher.GameObject));
		_indicators = pool.GetGroup(Matcher.AllOf(Matcher.Indicator));
	}
	
	public void Execute() {
		for (int i = 0; i < _group.count; i++) {
			Entity panelEntity = _group.GetEntities()[i];
			GameObject panelGO = panelEntity.gameObject.gameObject;
			for (int j = 0; j < _indicators.count; j++) {
				Entity e = _indicators.GetEntities()[j];
				IndicatorComponent component = e.indicator;
				float ratio = component.currentValue / component.totalValue;
				GameObject go = panelGO.transform.Find(component.type).gameObject;
				if (!go.activeSelf) {
					go.SetActive(true);
				}
				go.GetComponent<Image>().fillAmount = ratio;
				if (ratio <= 0.001f) {
					go.SetActive(false);
					e.isDestroyEntity = true;
				}
			}
		}
	}
}