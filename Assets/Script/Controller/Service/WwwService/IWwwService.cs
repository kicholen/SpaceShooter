using System;

public interface IWwwService {
    void Send<T>(T request, Action<T> onSuccess, Action onFailure) where T : WwwRequest;
}
