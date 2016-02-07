using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public EnemyModelComponent enemyModel { get { return (EnemyModelComponent)GetComponent(ComponentIds.EnemyModel); } }

        public bool hasEnemyModel { get { return HasComponent(ComponentIds.EnemyModel); } }

        static readonly Stack<EnemyModelComponent> _enemyModelComponentPool = new Stack<EnemyModelComponent>();

        public static void ClearEnemyModelComponentPool() {
            _enemyModelComponentPool.Clear();
        }

        public Entity AddEnemyModel(int newId, int newType, string newResource, int newWeapon, int newAmount, float newTime, float newSpawnDelay, string newWeaponResource, float newVelocity, int newAngle, int newWaves, int newAngleOffset, UnityEngine.Vector2 newStartVelocity, float newFollowDelay, float newSelfDestructionDelay, float newTimeDelay, float newDelay, float newRandomPositionOffsetX) {
            var component = _enemyModelComponentPool.Count > 0 ? _enemyModelComponentPool.Pop() : new EnemyModelComponent();
            component.id = newId;
            component.type = newType;
            component.resource = newResource;
            component.weapon = newWeapon;
            component.amount = newAmount;
            component.time = newTime;
            component.spawnDelay = newSpawnDelay;
            component.weaponResource = newWeaponResource;
            component.velocity = newVelocity;
            component.angle = newAngle;
            component.waves = newWaves;
            component.angleOffset = newAngleOffset;
            component.startVelocity = newStartVelocity;
            component.followDelay = newFollowDelay;
            component.selfDestructionDelay = newSelfDestructionDelay;
            component.timeDelay = newTimeDelay;
            component.delay = newDelay;
            component.randomPositionOffsetX = newRandomPositionOffsetX;
            return AddComponent(ComponentIds.EnemyModel, component);
        }

        public Entity ReplaceEnemyModel(int newId, int newType, string newResource, int newWeapon, int newAmount, float newTime, float newSpawnDelay, string newWeaponResource, float newVelocity, int newAngle, int newWaves, int newAngleOffset, UnityEngine.Vector2 newStartVelocity, float newFollowDelay, float newSelfDestructionDelay, float newTimeDelay, float newDelay, float newRandomPositionOffsetX) {
            var previousComponent = hasEnemyModel ? enemyModel : null;
            var component = _enemyModelComponentPool.Count > 0 ? _enemyModelComponentPool.Pop() : new EnemyModelComponent();
            component.id = newId;
            component.type = newType;
            component.resource = newResource;
            component.weapon = newWeapon;
            component.amount = newAmount;
            component.time = newTime;
            component.spawnDelay = newSpawnDelay;
            component.weaponResource = newWeaponResource;
            component.velocity = newVelocity;
            component.angle = newAngle;
            component.waves = newWaves;
            component.angleOffset = newAngleOffset;
            component.startVelocity = newStartVelocity;
            component.followDelay = newFollowDelay;
            component.selfDestructionDelay = newSelfDestructionDelay;
            component.timeDelay = newTimeDelay;
            component.delay = newDelay;
            component.randomPositionOffsetX = newRandomPositionOffsetX;
            ReplaceComponent(ComponentIds.EnemyModel, component);
            if (previousComponent != null) {
                _enemyModelComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveEnemyModel() {
            var component = enemyModel;
            RemoveComponent(ComponentIds.EnemyModel);
            _enemyModelComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherEnemyModel;

        public static IMatcher EnemyModel {
            get {
                if (_matcherEnemyModel == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.EnemyModel);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherEnemyModel = matcher;
                }

                return _matcherEnemyModel;
            }
        }
    }
}
