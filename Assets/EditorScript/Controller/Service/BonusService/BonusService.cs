using Entitas;
using System;
using System.Collections.Generic;

public class BonusService : IBonusService
{
    IWwwService wwwService;
    EventService eventService;
    Pool pool;

    List<BonusModelComponent> bonuses;

    public BonusService(Pool pool, IWwwService wwwService, EventService eventService) {
        this.pool = pool;
        this.wwwService = wwwService;
        this.eventService = eventService;
    }

    public void CreateNewBonus(Action<BonusModelComponent> onCreated) {
        wwwService.Send<CreateBonus>(new CreateBonus(), (request) => { onCreated(request.Component); }, onRequestFailed);
    }

    public void DeleteBonus(long id, Action onDeleted) {
        wwwService.Send<DeleteBonus>(new DeleteBonus(id), (request) => onDeleted(), onRequestFailed);
    }

    public void LoadBonusById(long id, Action<BonusModelComponent> onLoaded) {
        wwwService.Send<GetBonus>(new GetBonus(id), (request) => { onLoaded(request.Component); }, onRequestFailed);
    }

    public void LoadBonuses(Action onLoaded) {
        wwwService.Send<GetBonuses>(new GetBonuses(), (request) => {
            bonuses = request.Bonuses;
            replaceOrAddBonuses(bonuses);
            onLoaded();
        }, onRequestFailed);
    }

    public void LoadBonusIds(Action<Dictionary<long, string>> onLoaded) {
        wwwService.Send<GetBonusIds>(new GetBonusIds(), (request) => { onLoaded(request.BonusIds); }, onRequestFailed);
    }

    public void UpdateBonus(BonusModelComponent component, Action onUpdated) {
        wwwService.Send<UpdateBonus>(new UpdateBonus(component), (request) => onUpdated(), onRequestFailed);
    }

    void onRequestFailed(string message) {
        eventService.Dispatch<InfoBoxShowEvent>(new InfoBoxShowEvent(message));
    }

    void replaceOrAddBonuses(List<BonusModelComponent> bonuses) {
        Entity[] entities = pool.GetGroup(Matcher.BonusModel).GetEntities();

        foreach (BonusModelComponent bonus in bonuses) {
            bool found = false;
            foreach (Entity e in entities) {
                if (bonus.type == e.bonusModel.type) {
                    e.ReplaceBonusModel(bonus.id, bonus.type, bonus.minAmount, bonus.maxAmount, bonus.probability, bonus.resource);
                    found = true;
                }
            }
            if (!found)
                pool.CreateEntity()
                    .AddComponent(ComponentIds.BonusModel, bonus);
        }
    }
}