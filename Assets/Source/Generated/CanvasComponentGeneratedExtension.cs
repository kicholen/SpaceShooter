//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Entitas {
    public partial class Entity {
        public CanvasComponent canvas { get { return (CanvasComponent)GetComponent(ComponentIds.Canvas); } }

        public bool hasCanvas { get { return HasComponent(ComponentIds.Canvas); } }

        public Entity AddCanvas(UnityEngine.Canvas newCanvas) {
            var component = CreateComponent<CanvasComponent>(ComponentIds.Canvas);
            component.canvas = newCanvas;
            return AddComponent(ComponentIds.Canvas, component);
        }

        public Entity ReplaceCanvas(UnityEngine.Canvas newCanvas) {
            var component = CreateComponent<CanvasComponent>(ComponentIds.Canvas);
            component.canvas = newCanvas;
            ReplaceComponent(ComponentIds.Canvas, component);
            return this;
        }

        public Entity RemoveCanvas() {
            return RemoveComponent(ComponentIds.Canvas);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherCanvas;

        public static IMatcher Canvas {
            get {
                if (_matcherCanvas == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Canvas);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherCanvas = matcher;
                }

                return _matcherCanvas;
            }
        }
    }
}
