using System;
using System.Collections.Generic;

public class LanguageService : ILanguageService
{
    IWwwService wwwService;
    EventService eventService;

    public LanguageService(IWwwService wwwService, EventService eventService)
    {
        this.wwwService = wwwService;
        this.eventService = eventService;
    }

    public void LoadLanguageById(long id, Action<LanguageModel> onLanguageLoaded)
    {
        wwwService.Send<GetLanguage>(new GetLanguage(id), (request) => { onLanguageLoaded(request.Model); }, onRequestFailed);
    }

    public void LoadLanguageIds(Action<Dictionary<long, string>> onLanguageIdsLoaded)
    {
        wwwService.Send<GetLanguageIds>(new GetLanguageIds(), (request) => { onLanguageIdsLoaded(request.LanguageIds); }, onRequestFailed);
    }

    public void UpdateLanguage(LanguageModel model, Action onLanguageUpdated)
    {
        wwwService.Send<UpdateLanguage>(new UpdateLanguage(model), (request) => onLanguageUpdated(), onRequestFailed);
    }

    void onRequestFailed(string message)
    {
        eventService.Dispatch<InfoBoxShowEvent>(new InfoBoxShowEvent(message));
    }
}
