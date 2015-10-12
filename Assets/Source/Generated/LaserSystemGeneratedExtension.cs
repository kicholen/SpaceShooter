namespace Entitas {
    public partial class Pool {
        public ISystem CreateLaserSystem() {
            return this.CreateSystem<LaserSystem>();
        }
    }
}