namespace Entitas {
    public partial class Pool {
        public ISystem CreateGhostSystem() {
            return this.CreateSystem<GhostSystem>();
        }
    }
}