namespace Entitas {
    public partial class Pool {
        public ISystem CreateFindTargetSystem() {
            return this.CreateSystem<FindTargetSystem>();
        }
    }
}