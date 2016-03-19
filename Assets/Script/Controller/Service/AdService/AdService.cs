using System;
using UnityEngine.Advertisements;

public class AdService : IAdService
{
    ICurrencyService currencyService;

    Action onFinished;

    public AdService(ICurrencyService currencyService)
    {
        this.currencyService = currencyService;
    }

    public void ShowIfShould(Action onFinished)
    {
        this.onFinished = onFinished;
        if (shoudShowAd())
            Advertisement.Show("rewardedVideo", new ShowOptions { resultCallback = onShown });
        else
            onFinished();
    }

    void onShown(ShowResult result)
    {
        if (result == ShowResult.Finished)
            addReward();

        onFinished();
    }

    void addReward()
    {
        currencyService.IncreaseCoins(10);
    }

    bool shoudShowAd()
    {
        return Advertisement.IsReady("rewardedVideo");
    }
}
