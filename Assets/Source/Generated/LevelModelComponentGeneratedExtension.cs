using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public LevelModelComponent levelModel { get { return (LevelModelComponent)GetComponent(ComponentIds.LevelModel); } }

        public bool hasLevelModel { get { return HasComponent(ComponentIds.LevelModel); } }

        static readonly Stack<LevelModelComponent> _levelModelComponentPool = new Stack<LevelModelComponent>();

        public static void ClearLevelModelComponentPool() {
            _levelModelComponentPool.Clear();
        }

        public Entity AddLevelModel(long newId, string newName, System.Collections.Generic.List<WaveModel> newWaves, int newWaveIndex, System.Collections.Generic.List<EnemyModel> newEnemies, int newEnemyIndex, UnityEngine.Vector2 newPosition, UnityEngine.Vector2 newSize) {
            var component = _levelModelComponentPool.Count > 0 ? _levelModelComponentPool.Pop() : new LevelModelComponent();
            component.id = newId;
            component.name = newName;
            component.waves = newWaves;
            component.waveIndex = newWaveIndex;
            component.enemies = newEnemies;
            component.enemyIndex = newEnemyIndex;
            component.position = newPosition;
            component.size = newSize;
            return AddComponent(ComponentIds.LevelModel, component);
        }

        public Entity ReplaceLevelModel(long newId, string newName, System.Collections.Generic.List<WaveModel> newWaves, int newWaveIndex, System.Collections.Generic.List<EnemyModel> newEnemies, int newEnemyIndex, UnityEngine.Vector2 newPosition, UnityEngine.Vector2 newSize) {
            var previousComponent = hasLevelModel ? levelModel : null;
            var component = _levelModelComponentPool.Count > 0 ? _levelModelComponentPool.Pop() : new LevelModelComponent();
            component.id = newId;
            component.name = newName;
            component.waves = newWaves;
            component.waveIndex = newWaveIndex;
            component.enemies = newEnemies;
            component.enemyIndex = newEnemyIndex;
            component.position = newPosition;
            component.size = newSize;
            ReplaceComponent(ComponentIds.LevelModel, component);
            if (previousComponent != null) {
                _levelModelComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveLevelModel() {
            var component = levelModel;
            RemoveComponent(ComponentIds.LevelModel);
            _levelModelComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherLevelModel;

        public static IMatcher LevelModel {
            get {
                if (_matcherLevelModel == null) {
                    _matcherLevelModel = Matcher.AllOf(ComponentIds.LevelModel);
                }

                return _matcherLevelModel;
            }
        }
    }
}
