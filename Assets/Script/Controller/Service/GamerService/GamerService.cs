using System.Collections.Generic;

public class GamerService : IGamerService
{
    EventService eventService;
    GamerModel model;
    List<ProgressModel> progressModels = new List<ProgressModel>();
    int level = 0;

    public GamerModel Model { get { return model; } }
    public int Level { get { return level; } }
    public long NextLevelExp { get { return level < Config.MAX_LEVEL ? progressModels[level].neededExp : -1; } }
    public long PreviousLevelExp { get { return (level - 1) > 0 ? progressModels[level - 1].neededExp : 0; } }

    public GamerService(EventService eventService)
    {
        this.eventService = eventService;
        model = Utils.Deserialize<GamerModel>();
    }

    public void Init()
    {
        for (int i = 0; i < Config.MAX_LEVEL; i++)
            progressModels.Add(Utils.Deserialize<ProgressModel>(i.ToString()));
        progressModels.Sort((progX, progY) => progX.level.CompareTo(progY.level));
        calculateCurrentLevel();
    }

    public void IncreaseExp(long count)
    {
        model.experience += count;
        eventService.Dispatch<ExpChangedEvent>(new ExpChangedEvent(model.experience));
        advanceLevelIfCan();
    }

    public string GetTopPanelFormattedText()
    {
        if (NextLevelExp < 0)
            return "MAX";
        else
            return (model.experience - PreviousLevelExp).ToString() + "/" + (NextLevelExp - PreviousLevelExp).ToString();
    }

    void advanceLevelIfCan()
    {
        if (canAdvance())
            level += 1;
    }

    bool canAdvance()
    {
        return (level + 1) < progressModels.Count && model.experience >= progressModels[level + 1].neededExp;
    }

    void calculateCurrentLevel()
    {
        for (int i = 0; i < Config.MAX_LEVEL; i++)
            if (progressModels[i].neededExp <= model.experience)
                level += 1;
    }

    void Save()
    {
        Utils.Serialize(model);
    }
}
