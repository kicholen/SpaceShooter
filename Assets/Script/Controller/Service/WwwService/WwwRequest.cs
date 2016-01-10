using System.Collections.Generic;
using UnityEngine;

public abstract class WwwRequest {

    WWW www;
    public Dictionary<string, string> postData = new Dictionary<string, string>();
    public List<string> urlData = new List<string>();

    public abstract void ParseResult();

    protected string result { get { return www.text; } }

    public void SetWww(WWW www) {
        this.www = www;
    }

    public WWW Process() {
        return www;
    }

    public bool Successful() {
        return www.error == null;
    }

    public string GetErrorMessage() {
        return www.error;
    }
}
