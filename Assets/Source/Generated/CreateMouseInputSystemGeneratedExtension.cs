namespace Entitas {
    public partial class Pool {
        public ISystem CreateCreateMouseInputSystem() {
            return this.CreateSystem<CreateMouseInputSystem>();
        }
    }
}