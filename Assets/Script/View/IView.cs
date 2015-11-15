using UnityEngine;

public interface IView {
	GameObject Go { get; }
	void SetParent(Transform parent);
	void Destroy();
	void Hide();
	void Show();
}