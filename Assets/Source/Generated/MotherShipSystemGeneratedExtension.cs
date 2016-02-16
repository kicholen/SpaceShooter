namespace Entitas {
    public partial class Pool {
        public ISystem CreateMotherShipSystem() {
            return this.CreateSystem<MotherShipSystem>();
        }
    }
}