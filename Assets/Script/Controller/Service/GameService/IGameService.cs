using Entitas;

public interface IGameService : IUpdateable {
	void Init();
	void InitGame(int level);
	void InitPool(string resource, int count);
	void PlayGame();
	void EndGame(Entity e);
}
