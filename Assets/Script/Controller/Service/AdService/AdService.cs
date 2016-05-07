using System;
using UnityEngine.Advertisements;

public class AdService : IAdService
{
    ICurrencyService currencyService;

    Action<bool> onFinished;

    public AdService(ICurrencyService currencyService)
    {
        this.currencyService = currencyService;
    }

    public void ShowIfShould(Action<bool> onFinished)
    {
    #if UNITY_ANDROID
        this.onFinished = onFinished;
        if (shoudShowAd())
            Advertisement.Show("rewardedVideo", new ShowOptions { resultCallback = onShown });
        else
            onFinished(false);
    #endif
    }
    #if UNITY_ANDROID

    void onShown(ShowResult result)
    {
        onFinished(result == ShowResult.Finished);
    }

    void addReward()
    {
        currencyService.IncreaseCoins(10);
    }

    bool shoudShowAd()
    {
        return Advertisement.IsReady("rewardedVideo");
    }
    #endif
}
