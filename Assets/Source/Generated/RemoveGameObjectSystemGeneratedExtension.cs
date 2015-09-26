namespace Entitas {
    public partial class Pool {
        public ISystem CreateRemoveGameObjectSystem() {
            return this.CreateSystem<RemoveGameObjectSystem>();
        }
    }
}