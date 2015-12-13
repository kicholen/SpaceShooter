namespace Entitas {
    public partial class Pool {
        public ISystem CreateHelperShipSystem() {
            return this.CreateSystem<HelperShipSystem>();
        }
    }
}