namespace Entitas {
    public partial class Pool {
        public ISystem CreateDefaultCameraSystem() {
            return this.CreateSystem<DefaultCameraSystem>();
        }
    }
}