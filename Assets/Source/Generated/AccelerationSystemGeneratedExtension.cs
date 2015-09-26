namespace Entitas {
    public partial class Pool {
        public ISystem CreateAccelerationSystem() {
            return this.CreateSystem<AccelerationSystem>();
        }
    }
}