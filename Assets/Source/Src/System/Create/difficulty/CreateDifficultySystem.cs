using Entitas;
using UnityEngine;
using System.Xml;
using System;

public class CreateDifficultySystem : IInitializeSystem, ISetPool {
	Pool _pool;

	const string difficultyPath = "Difficulty";

	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Initialize() {
		XmlNode node = Utils.loadXml(difficultyPath);

		XmlNode difficulty = node.FirstChild.FirstChild;

		while(difficulty != null) {
			_pool.CreateEntity()
				.AddDifficultyModel(Convert.ToInt16(difficulty.Attributes[0].Value),
				                    Convert.ToInt16(difficulty.Attributes[1].Value),
				                    Convert.ToInt16(difficulty.Attributes[2].Value),
				                    Convert.ToInt16(difficulty.Attributes[3].Value));
			difficulty = difficulty.NextSibling;
		}
	}
}