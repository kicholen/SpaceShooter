namespace Entitas {
    public partial class Pool {
        public ISystem CreateEndGameSystem() {
            return this.CreateSystem<EndGameSystem>();
        }
    }
}