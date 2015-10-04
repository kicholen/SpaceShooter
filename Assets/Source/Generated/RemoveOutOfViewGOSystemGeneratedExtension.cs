namespace Entitas {
    public partial class Pool {
        public ISystem CreateRemoveOutOfViewGOSystem() {
            return this.CreateSystem<RemoveOutOfViewGOSystem>();
        }
    }
}