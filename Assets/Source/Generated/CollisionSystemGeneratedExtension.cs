namespace Entitas {
    public partial class Pool {
        public ISystem CreateCollisionSystem() {
            return this.CreateSystem<CollisionSystem>();
        }
    }
}