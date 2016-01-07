using Entitas;
using System.Collections.Generic;

public interface IServices : Updateable {
	void Init();
	IController Controller { get; }
	Pool Pool { get;}
	List<Updateable> Updateables { get; }
	ILoadService LoadService { get; }
	EventService EventService { get; }
	IGameService GameService { get; }
	IViewService ViewService { get; }
	IViewFactoryService ViewFactoryService { get; }
	IUIFactoryService UIFactoryService { get; }
}
