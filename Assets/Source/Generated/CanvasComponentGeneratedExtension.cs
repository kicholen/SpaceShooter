using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public CanvasComponent canvas { get { return (CanvasComponent)GetComponent(ComponentIds.Canvas); } }

        public bool hasCanvas { get { return HasComponent(ComponentIds.Canvas); } }

        static readonly Stack<CanvasComponent> _canvasComponentPool = new Stack<CanvasComponent>();

        public static void ClearCanvasComponentPool() {
            _canvasComponentPool.Clear();
        }

        public Entity AddCanvas(UnityEngine.Canvas newCanvas) {
            var component = _canvasComponentPool.Count > 0 ? _canvasComponentPool.Pop() : new CanvasComponent();
            component.canvas = newCanvas;
            return AddComponent(ComponentIds.Canvas, component);
        }

        public Entity ReplaceCanvas(UnityEngine.Canvas newCanvas) {
            var previousComponent = hasCanvas ? canvas : null;
            var component = _canvasComponentPool.Count > 0 ? _canvasComponentPool.Pop() : new CanvasComponent();
            component.canvas = newCanvas;
            ReplaceComponent(ComponentIds.Canvas, component);
            if (previousComponent != null) {
                _canvasComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCanvas() {
            var component = canvas;
            RemoveComponent(ComponentIds.Canvas);
            _canvasComponentPool.Push(component);
            return this;
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
