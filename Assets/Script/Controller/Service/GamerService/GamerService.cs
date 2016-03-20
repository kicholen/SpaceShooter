using System.Collections.Generic;

public class GamerService : IGamerService
{
    EventService eventService;
    GamerModel model;
    List<ProgressModel> progressModels = new List<ProgressModel>();
    int level = 0;

    public GamerModel Model { get { return model; } }
    public ProgressModel ProgressModel { get { return progressModels[level]; } }
    public int Level { get { return level; } }
    public long PreviousLevelExp { get { return (level - 1) >= 0 ? progressModels[level - 1].neededExp : 0; } }

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

    public void IncreaseExp(long exp)
    {
        model.experience += exp;
        advanceLevelIfCan();
        eventService.Dispatch<ExpChangedEvent>(new ExpChangedEvent(exp));
    }

    public string GetTopPanelFormattedText()
    {
        if (isMaxLevel())
            return "MAX";
        else
            return (model.experience - PreviousLevelExp).ToString() + "/" + (progressModels[level].neededExp - PreviousLevelExp).ToString();
    }

    public float GetNextLevelRatio()
    {
        return isMaxLevel() ? 1.0f : (float)(model.experience - PreviousLevelExp) / (float)(progressModels[level].neededExp - PreviousLevelExp);
    }

    public bool WillLevelUp(long exp)
    {
        return !isMaxLevel() && (model.experience + exp) >= progressModels[level].neededExp;
    }

    void advanceLevelIfCan()
    {
        if (canAdvance())
            level += 1;
    }

    bool canAdvance()
    {
        return (level + 1) < progressModels.Count && model.experience >= progressModels[level].neededExp;
    }

    void calculateCurrentLevel()
    {
        for (int i = 0; i < Config.MAX_LEVEL; i++)
            if (progressModels[i].neededExp <= model.experience)
                level += 1;
    }

    bool isMaxLevel()
    {
        return level == Config.MAX_LEVEL;
    }

    void Save()
    {
        Utils.Serialize(model);
    }
}
