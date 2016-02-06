using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class SliderAutoUpdateBehaviour : MonoBehaviour {
    Camera mainCamera;
    MethodInfo sliderSetMethod;

    Func<bool> isGameInProgress;
    float endPosition;

    public void setInProgressAction(Func<bool> isGameInProgress) {
        this.isGameInProgress = isGameInProgress;
    }

    void Start() {
        mainCamera = Camera.main;
        sliderSetMethod = getSliderSilentSetterMethod();
    }

    public void setEndPosition(float endPosition) {
        this.endPosition = endPosition;
    }

    void Update() {
        if (isGameInProgress())
            sliderSetMethod.Invoke(GetComponent<Slider>(), new object[] {  mainCamera.transform.position.y / endPosition, false });
    }

    MethodInfo getSliderSilentSetterMethod() {
        MethodInfo[] methods = typeof(Slider).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
        for (var i = 0; i < methods.Length; i++) {
            if (methods[i].Name == "Set" && methods[i].GetParameters().Length == 2) {
                return methods[i];
            }
        }
        return null;
    }
}