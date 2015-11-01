using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public SoundOnDeathComponent soundOnDeath { get { return (SoundOnDeathComponent)GetComponent(ComponentIds.SoundOnDeath); } }

        public bool hasSoundOnDeath { get { return HasComponent(ComponentIds.SoundOnDeath); } }

        static readonly Stack<SoundOnDeathComponent> _soundOnDeathComponentPool = new Stack<SoundOnDeathComponent>();

        public static void ClearSoundOnDeathComponentPool() {
            _soundOnDeathComponentPool.Clear();
        }

        public Entity AddSoundOnDeath(string newResource, float newVolume) {
            var component = _soundOnDeathComponentPool.Count > 0 ? _soundOnDeathComponentPool.Pop() : new SoundOnDeathComponent();
            component.resource = newResource;
            component.volume = newVolume;
            return AddComponent(ComponentIds.SoundOnDeath, component);
        }

        public Entity ReplaceSoundOnDeath(string newResource, float newVolume) {
            var previousComponent = hasSoundOnDeath ? soundOnDeath : null;
            var component = _soundOnDeathComponentPool.Count > 0 ? _soundOnDeathComponentPool.Pop() : new SoundOnDeathComponent();
            component.resource = newResource;
            component.volume = newVolume;
            ReplaceComponent(ComponentIds.SoundOnDeath, component);
            if (previousComponent != null) {
                _soundOnDeathComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSoundOnDeath() {
            var component = soundOnDeath;
            RemoveComponent(ComponentIds.SoundOnDeath);
            _soundOnDeathComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSoundOnDeath;

        public static IMatcher SoundOnDeath {
            get {
                if (_matcherSoundOnDeath == null) {
                    _matcherSoundOnDeath = Matcher.AllOf(ComponentIds.SoundOnDeath);
                }

                return _matcherSoundOnDeath;
            }
        }
    }
}
