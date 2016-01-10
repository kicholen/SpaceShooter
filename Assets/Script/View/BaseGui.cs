using UnityEngine;

public class BaseGui {

    protected GameObject go;

    public void removeChildren() {
        for (int i = go.transform.childCount - 1; i >= 0; i--) {
            Object.Destroy(go.transform.GetChild(i).gameObject);
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

    public virtual void Destroy() {
        Object.Destroy(go);
    }
}
