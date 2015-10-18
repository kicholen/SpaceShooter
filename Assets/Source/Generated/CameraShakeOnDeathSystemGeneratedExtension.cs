namespace Entitas {
    public partial class Pool {
        public ISystem CreateCameraShakeOnDeathSystem() {
            return this.CreateSystem<CameraShakeOnDeathSystem>();
        }
    }
}