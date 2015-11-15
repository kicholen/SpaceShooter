using Entitas;
using System.Collections.Generic;

public interface IServices : IUpdateable {
	Controller Controller { get; }
	Pool Pool { get;}
	List<IUpdateable> Updateables { get; }
	ILoadService LoadService { get; }
	IGameService GameService { get; }
	EventService EventService { get; }
}
