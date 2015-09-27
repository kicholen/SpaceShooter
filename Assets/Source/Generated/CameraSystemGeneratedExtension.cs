namespace Entitas {
    public partial class Pool {
        public ISystem CreateCameraSystem() {
            return this.CreateSystem<CameraSystem>();
        }
    }
}