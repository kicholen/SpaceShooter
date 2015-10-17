namespace Entitas {
    public partial class Pool {
        public ISystem CreateParticleSpawnSystem() {
            return this.CreateSystem<ParticleSpawnSystem>();
        }
    }
}