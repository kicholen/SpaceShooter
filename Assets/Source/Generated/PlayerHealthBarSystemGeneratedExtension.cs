namespace Entitas {
    public partial class Pool {
        public ISystem CreatePlayerHealthBarSystem() {
            return this.CreateSystem<PlayerHealthBarSystem>();
        }
    }
}