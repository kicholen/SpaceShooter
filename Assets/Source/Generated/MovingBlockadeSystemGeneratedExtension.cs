namespace Entitas {
    public partial class Pool {
        public ISystem CreateMovingBlockadeSystem() {
            return this.CreateSystem<MovingBlockadeSystem>();
        }
    }
}