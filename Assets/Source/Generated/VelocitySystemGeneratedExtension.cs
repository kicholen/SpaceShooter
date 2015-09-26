namespace Entitas {
    public partial class Pool {
        public ISystem CreateVelocitySystem() {
            return this.CreateSystem<VelocitySystem>();
        }
    }
}