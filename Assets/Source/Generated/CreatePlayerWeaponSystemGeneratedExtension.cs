namespace Entitas {
    public partial class Pool {
        public ISystem CreateCreatePlayerWeaponSystem() {
            return this.CreateSystem<CreatePlayerWeaponSystem>();
        }
    }
}