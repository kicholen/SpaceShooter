namespace Entitas {
    public partial class Pool {
        public ISystem CreateSpeedBonusSystem() {
            return this.CreateSystem<SpeedBonusSystem>();
        }
    }
}