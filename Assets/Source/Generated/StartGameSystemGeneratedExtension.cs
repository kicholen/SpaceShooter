namespace Entitas {
    public partial class Pool {
        public ISystem CreateStartGameSystem() {
            return this.CreateSystem<StartGameSystem>();
        }
    }
}