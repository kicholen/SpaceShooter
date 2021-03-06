﻿using UnityEngine;
using UnityEngine.UI;

public class BaseGui {

    protected GameObject go;

    public void removeChildren() {
        removeChildren(go);
    }

    protected void removeChildren(GameObject gameObject) {
        for (int i = gameObject.transform.childCount - 1; i >= 0; i--) {
            Object.Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }

    public void removeChildByName(string name) {
        Object.Destroy(go.transform.FindChild(name).gameObject);
    }

    protected void addChild(GameObject child) {
        child.transform.SetParent(go.transform, false);
    }

    protected void addChildToChild(string childName, GameObject child) {
        child.transform.SetParent(getChild(childName), false);
    }

    protected Transform getChild(string childName) {
        return go.transform.FindChild(childName);
    }

    protected void setText(string childName, string text) {
        getChild(childName).GetComponent<Text>().text = text;
    }

    public virtual void Destroy() {
        Object.Destroy(go);
    }
}
