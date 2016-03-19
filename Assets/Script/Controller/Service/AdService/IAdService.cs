using System;

public interface IAdService
{
    void ShowIfShould(Action<bool> onFinished);
}