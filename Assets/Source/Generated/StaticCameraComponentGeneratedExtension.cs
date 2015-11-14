using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public StaticCameraComponent staticCamera { get { return (StaticCameraComponent)GetComponent(ComponentIds.StaticCamera); } }

        public bool hasStaticCamera { get { return HasComponent(ComponentIds.StaticCamera); } }

        static readonly Stack<StaticCameraComponent> _staticCameraComponentPool = new Stack<StaticCameraComponent>();

        public static void ClearStaticCameraComponentPool() {
            _staticCameraComponentPool.Clear();
        }

        public Entity AddStaticCamera(UnityEngine.Vector3 newOffset) {
            var component = _staticCameraComponentPool.Count > 0 ? _staticCameraComponentPool.Pop() : new StaticCameraComponent();
            component.offset = newOffset;
            return AddComponent(ComponentIds.StaticCamera, component);
        }

        public Entity ReplaceStaticCamera(UnityEngine.Vector3 newOffset) {
            var previousComponent = hasStaticCamera ? staticCamera : null;
            var component = _staticCameraComponentPool.Count > 0 ? _staticCameraComponentPool.Pop() : new StaticCameraComponent();
            component.offset = newOffset;
            ReplaceComponent(ComponentIds.StaticCamera, component);
            if (previousComponent != null) {
                _staticCameraComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveStaticCamera() {
            var component = staticCamera;
            RemoveComponent(ComponentIds.StaticCamera);
            _staticCameraComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherStaticCamera;

        public static IMatcher StaticCamera {
            get {
                if (_matcherStaticCamera == null) {
                    _matcherStaticCamera = Matcher.AllOf(ComponentIds.StaticCamera);
                }

                return _matcherStaticCamera;
            }
        }
    }
}
