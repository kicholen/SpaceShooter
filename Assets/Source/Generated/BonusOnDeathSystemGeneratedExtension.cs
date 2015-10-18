namespace Entitas {
    public partial class Pool {
        public ISystem CreateBonusOnDeathSystem() {
            return this.CreateSystem<BonusOnDeathSystem>();
        }
    }
}