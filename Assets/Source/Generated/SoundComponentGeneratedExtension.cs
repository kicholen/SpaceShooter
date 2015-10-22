using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public SoundComponent sound { get { return (SoundComponent)GetComponent(ComponentIds.Sound); } }

        public bool hasSound { get { return HasComponent(ComponentIds.Sound); } }

        static readonly Stack<SoundComponent> _soundComponentPool = new Stack<SoundComponent>();

        public static void ClearSoundComponentPool() {
            _soundComponentPool.Clear();
        }

        public Entity AddSound(string newPath, float newVolume, UnityEngine.GameObject newGo) {
            var component = _soundComponentPool.Count > 0 ? _soundComponentPool.Pop() : new SoundComponent();
            component.path = newPath;
            component.volume = newVolume;
            component.go = newGo;
            return AddComponent(ComponentIds.Sound, component);
        }

        public Entity ReplaceSound(string newPath, float newVolume, UnityEngine.GameObject newGo) {
            var previousComponent = hasSound ? sound : null;
            var component = _soundComponentPool.Count > 0 ? _soundComponentPool.Pop() : new SoundComponent();
            component.path = newPath;
            component.volume = newVolume;
            component.go = newGo;
            ReplaceComponent(ComponentIds.Sound, component);
            if (previousComponent != null) {
                _soundComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSound() {
            var component = sound;
            RemoveComponent(ComponentIds.Sound);
            _soundComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherSound;

        public static AllOfMatcher Sound {
            get {
                if (_matcherSound == null) {
                    _matcherSound = new Matcher(ComponentIds.Sound);
                }

                return _matcherSound;
            }
        }
    }
}
