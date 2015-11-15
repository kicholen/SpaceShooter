using Entitas;

public interface IGameService : IUpdateable {
	Systems CreateSystems();
	void Init();
	void StartGame(int level);
	void EndGame();
}
