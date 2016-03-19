using System.Collections.Generic;
using System;

public class InitPhase : Phase, IPhase
{
	IServices services;

	public InitPhase(IServices services) {
		this.services = services;
	}

    public override void CreateActions() {
        actions = new Queue<Action>();
        actions.Enqueue(() => addViewListener());
        actions.Enqueue(() => services.ViewService.SetView(ViewTypes.INIT));
        actions.Enqueue(() => { services.GameService.Init(services); nextAction(); });
        actions.Enqueue(() => { services.ShipService.Init(services); nextAction(); });
        actions.Enqueue(() => { services.SettingsService.Init(); nextAction(); });
        actions.Enqueue(() => { services.ShopService.Init(); nextAction(); });
        actions.Enqueue(() => { services.TranslationService.Init(); nextAction(); });
        actions.Enqueue(() => { services.GamerService.Init(); nextAction(); });
        actions.Enqueue(() => { services.ViewService.CreateTopPanel(services); nextAction(); });
        actions.Enqueue(() => { services.ViewService.CreateBottomPanel(services); nextAction(); });
        actions.Enqueue(() => addViewListener());
        actions.Enqueue(() => services.ViewService.SetView(ViewTypes.MAIN));
    }
}

