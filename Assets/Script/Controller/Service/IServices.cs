using Entitas;
using System.Collections.Generic;

public interface IServices : IUpdateable {
	Controller Controller { get; }
	Pool Pool { get;}
	List<IUpdateable> Updateables { get; }
	ILoadService LoadService { get; }
	EventService EventService { get; }
	IGameService GameService { get; }
	IViewService ViewService { get; }
	IViewFactoryService ViewFactoryService { get; }
	IUIFactoryService UIFactoryService { get; }
}
