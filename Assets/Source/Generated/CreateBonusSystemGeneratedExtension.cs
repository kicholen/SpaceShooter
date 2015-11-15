namespace Entitas {
    public partial class Pool {
        public ISystem CreateCreateBonusSystem() {
            return this.CreateSystem<CreateBonusSystem>();
        }
    }
}