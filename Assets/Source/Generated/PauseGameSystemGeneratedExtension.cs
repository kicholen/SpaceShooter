namespace Entitas {
    public partial class Pool {
        public ISystem CreatePauseGameSystem() {
            return this.CreateSystem<PauseGameSystem>();
        }
    }
}