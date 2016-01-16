using System.Collections.Generic;
using UnityEngine;

public class ApplicationController : MonoBehaviour {

    Dictionary<string, string> parameters = new Dictionary<string, string>();

    void Start() {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
            Invoke("retrieveUrlParameters", 0.5f);
        else
            init();
    }

    void retrieveUrlParameters() {
        Application.ExternalEval("SendMessage('" + name + "', 'SetRequestParameters', document.location.search);");
    }

    void init() {
        string value = "false";
        if (parameters.TryGetValue("editor", out value) && value=="true")
            gameObject.AddComponent<EditorController>();
        else
            gameObject.AddComponent<Controller>();
        Destroy(this);
    }

    public void SetRequestParameters(string parametersString) {
        char[] parameterDelimiters = new char[] { '?', '&' };
        string[] urlParamaters = parametersString.Split(parameterDelimiters, System.StringSplitOptions.RemoveEmptyEntries);
        char[] keyValueDelimiters = new char[] { '=' };

        for (int i = 0; i < urlParamaters.Length; ++i) {
            string[] keyValue = urlParamaters[i].Split(keyValueDelimiters, System.StringSplitOptions.None);
            if (keyValue.Length >= 2) {
                parameters.Add(WWW.UnEscapeURL(keyValue[0]), WWW.UnEscapeURL(keyValue[1]));
            }
            else if (keyValue.Length == 1) {
                parameters.Add(WWW.UnEscapeURL(keyValue[0]), "");
            }
        }
        init();
    }
}