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
		actions.Enqueue(() => addViewListener());
		actions.Enqueue(() => services.ViewService.SetView(ViewTypes.LANDING));
	}
}
