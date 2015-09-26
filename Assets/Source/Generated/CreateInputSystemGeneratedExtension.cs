namespace Entitas {
    public partial class Pool {
        public ISystem CreateCreateInputSystem() {
            return this.CreateSystem<CreateInputSystem>();
        }
    }
}