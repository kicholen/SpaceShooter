namespace Entitas {
    public partial class Pool {
        public ISystem CreatePlayerInputSystem() {
            return this.CreateSystem<PlayerInputSystem>();
        }
    }
}