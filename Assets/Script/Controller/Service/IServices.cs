public interface IServices : IUpdateable {
	ILoadService LoadService { get; }
	IGameService GameService { get; }
	EventService EventService { get; }
}
