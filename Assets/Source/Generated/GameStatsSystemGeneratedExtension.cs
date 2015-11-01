namespace Entitas {
    public partial class Pool {
        public ISystem CreateGameStatsSystem() {
            return this.CreateSystem<GameStatsSystem>();
        }
    }
}