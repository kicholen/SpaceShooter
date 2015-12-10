namespace Entitas {
    public partial class Pool {
        public ISystem CreateCreateShipSystem() {
            return this.CreateSystem<CreateShipSystem>();
        }
    }
}