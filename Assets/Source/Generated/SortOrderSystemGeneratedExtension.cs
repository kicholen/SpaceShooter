namespace Entitas {
    public partial class Pool {
        public ISystem CreateSortOrderSystem() {
            return this.CreateSystem<SortOrderSystem>();
        }
    }
}