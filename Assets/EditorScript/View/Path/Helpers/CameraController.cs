using UnityEngine;

public class CameraController : MonoBehaviour {
    public Camera mainCamera;
	
    void Start() {
        mainCamera = Camera.main;
    }

	void Update () {
		float offsetX = 0.0f;
		float offsetY = 0.0f;

		if (Input.GetKey(KeyCode.S)) {
			offsetY -= 0.1f;
		}
		else if (Input.GetKey(KeyCode.W)) {
			offsetY += 0.1f;
		}
		else if (Input.GetKey(KeyCode.A)) {
			offsetX -= 0.1f;
		}
		else if (Input.GetKey(KeyCode.D)) {
			offsetX += 0.1f;
		}
		Vector3 position = mainCamera.transform.position;
		mainCamera.transform.position = new Vector3(position.x + offsetX, position.y + offsetY, position.z);

		if(Input.GetKey(KeyCode.KeypadPlus)) {
			mainCamera.orthographicSize -= .1f;
		}
		if(Input.GetKey(KeyCode.KeypadMinus)) {
			mainCamera.orthographicSize += .1f;
		}
	}
}
