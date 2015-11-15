using Entitas;

public interface IGameService : IUpdateable {
	void Init();
	void InitGame(int level);
	void PlayGame();
	void EndGame();
}
