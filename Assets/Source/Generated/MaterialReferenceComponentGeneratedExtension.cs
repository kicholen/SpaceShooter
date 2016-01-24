using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public MaterialReferenceComponent materialReference { get { return (MaterialReferenceComponent)GetComponent(ComponentIds.MaterialReference); } }

        public bool hasMaterialReference { get { return HasComponent(ComponentIds.MaterialReference); } }

        static readonly Stack<MaterialReferenceComponent> _materialReferenceComponentPool = new Stack<MaterialReferenceComponent>();

        public static void ClearMaterialReferenceComponentPool() {
            _materialReferenceComponentPool.Clear();
        }

        public Entity AddMaterialReference(MaterialStorage newStorage) {
            var component = _materialReferenceComponentPool.Count > 0 ? _materialReferenceComponentPool.Pop() : new MaterialReferenceComponent();
            component.storage = newStorage;
            return AddComponent(ComponentIds.MaterialReference, component);
        }

        public Entity ReplaceMaterialReference(MaterialStorage newStorage) {
            var previousComponent = hasMaterialReference ? materialReference : null;
            var component = _materialReferenceComponentPool.Count > 0 ? _materialReferenceComponentPool.Pop() : new MaterialReferenceComponent();
            component.storage = newStorage;
            ReplaceComponent(ComponentIds.MaterialReference, component);
            if (previousComponent != null) {
                _materialReferenceComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMaterialReference() {
            var component = materialReference;
            RemoveComponent(ComponentIds.MaterialReference);
            _materialReferenceComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherMaterialReference;

        public static IMatcher MaterialReference {
            get {
                if (_matcherMaterialReference == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.MaterialReference);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherMaterialReference = matcher;
                }

                return _matcherMaterialReference;
            }
        }
    }
}
