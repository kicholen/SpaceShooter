namespace Entitas {
    public partial class Pool {
        public ISystem CreateShakeSystem() {
            return this.CreateSystem<ShakeSystem>();
        }
    }
}