namespace Entitas {
    public partial class Pool {
        public ISystem CreateAddCollisionPositionToGOSystem() {
            return this.CreateSystem<AddCollisionPositionToGOSystem>();
        }
    }
}