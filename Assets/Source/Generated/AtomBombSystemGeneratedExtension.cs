namespace Entitas {
    public partial class Pool {
        public ISystem CreateAtomBombSystem() {
            return this.CreateSystem<AtomBombSystem>();
        }
    }
}