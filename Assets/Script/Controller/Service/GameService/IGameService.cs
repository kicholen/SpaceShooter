using Entitas;

public interface IGameService : IUpdateable {
	Systems CreateSystems(Pool pool);
}
