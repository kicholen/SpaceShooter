namespace Entitas {
    public partial class Pool {
        public ISystem CreateBonusSpawnerSystem() {
            return this.CreateSystem<BonusSpawnerSystem>();
        }
    }
}