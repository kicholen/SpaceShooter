namespace Entitas {
    public partial class Pool {
        public ISystem CreateCreateEnemySystem() {
            return this.CreateSystem<CreateEnemySystem>();
        }
    }
}