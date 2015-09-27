namespace Entitas {
    public partial class Pool {
        public ISystem CreateWeaponSystem() {
            return this.CreateSystem<WeaponSystem>();
        }
    }
}