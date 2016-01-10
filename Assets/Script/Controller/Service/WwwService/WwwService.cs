using System;
using System.Collections;
using UnityEngine;

public class WwwService : MonoBehaviour, IWwwService {

    RequestBuilder builder = new RequestBuilder();

    public void Send<T>(T request, Action<T> onSuccess, Action<string> onFailure) where T : WwwRequest {
        builder.Build(request);
        StartCoroutine(waitForRequest(request, onSuccess, onFailure));
    }

    IEnumerator waitForRequest<T>(T request, Action<T> onSuccess, Action<string> onFailure) where T : WwwRequest {
        yield return request.Process();

        if (request.Successful()) {
            request.ParseResult();
            onSuccess.Invoke(request);
        }
        else {
            onFailure.Invoke(request.GetErrorMessage());
        }
    }
}