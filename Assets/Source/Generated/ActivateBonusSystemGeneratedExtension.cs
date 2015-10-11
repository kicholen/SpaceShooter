namespace Entitas {
    public partial class Pool {
        public ISystem CreateActivateBonusSystem() {
            return this.CreateSystem<ActivateBonusSystem>();
        }
    }
}