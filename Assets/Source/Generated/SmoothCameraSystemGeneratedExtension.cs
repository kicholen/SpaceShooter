namespace Entitas {
    public partial class Pool {
        public ISystem CreateSmoothCameraSystem() {
            return this.CreateSystem<SmoothCameraSystem>();
        }
    }
}