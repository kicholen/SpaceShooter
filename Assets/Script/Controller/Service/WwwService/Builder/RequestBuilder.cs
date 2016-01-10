using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RequestBuilder {
    
    public void Build(WwwRequest request) {
        WWW www = createWww(request.urlData, request.postData);
        request.SetWww(www);
    }

    WWW createWww(List<string> urlData, Dictionary<string, string> postData) {
        string url = createUrl(urlData);
        return isGETRequest(postData) ? createGETWww(url) : createPOSTWww(url, postData);
    }

    string createUrl(List<string> urlData) {
        StringBuilder builder = new StringBuilder();
        builder.Append("http://localhost:8080/");
        attachUrlData(builder, urlData);
        return builder.ToString();
    }

    void attachUrlData(StringBuilder builder, List<string> urlData) {
        foreach (string value in urlData) {
            builder.Append(value).Append("/");
        }
    }

    WWW createGETWww(string url) {
        return new WWW(url);
    }

    WWW createPOSTWww(string url, Dictionary<string, string> postData) {
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, string> pair in postData) {
            form.AddField(pair.Key, pair.Value);
        }
        return new WWW(url, form);
    }

    bool isGETRequest(Dictionary<string, string> postData) {
        return postData.Count == 0;
    }
}
