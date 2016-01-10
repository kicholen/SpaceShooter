using System;

public interface IWwwService {
    void Send<T>(T request, Action<T> onSuccess, Action<string> onFailure) where T : WwwRequest;
}
