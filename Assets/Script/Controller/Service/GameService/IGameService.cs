using Entitas;

public interface IGameService : Updateable {
	void Init(IServices services);
	void InitGame(int level);
	void InitPool(string resource, int count);
	void PlayGame();
	void EndGame(Entity e);
}
