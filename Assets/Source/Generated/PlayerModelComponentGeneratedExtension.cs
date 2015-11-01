using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public PlayerModelComponent playerModel { get { return (PlayerModelComponent)GetComponent(ComponentIds.PlayerModel); } }

        public bool hasPlayerModel { get { return HasComponent(ComponentIds.PlayerModel); } }

        static readonly Stack<PlayerModelComponent> _playerModelComponentPool = new Stack<PlayerModelComponent>();

        public static void ClearPlayerModelComponentPool() {
            _playerModelComponentPool.Clear();
        }

        public Entity AddPlayerModel(string newName, UnityEngine.Vector2 newVelocityLimit, int newHealth, bool newHasLaser, float newLaserDamage, bool newHasMissile, float newMissileVelocity, float newMissileSpawnDelay, float newMissileDamage, float newHasSecondaryMissiles, float newSecondaryMissileVelocity, float newSecondaryMissileSpawnDelay, float newSecondaryMissileDamage, bool newHasHomeMissile, float newHomeMissileVelocity, float newHomeMissileSpawnDelay, float newHomeMissileDamage, bool newHasMagnetField, float newMagnetRadius) {
            var component = _playerModelComponentPool.Count > 0 ? _playerModelComponentPool.Pop() : new PlayerModelComponent();
            component.name = newName;
            component.velocityLimit = newVelocityLimit;
            component.health = newHealth;
            component.hasLaser = newHasLaser;
            component.laserDamage = newLaserDamage;
            component.hasMissile = newHasMissile;
            component.missileVelocity = newMissileVelocity;
            component.missileSpawnDelay = newMissileSpawnDelay;
            component.missileDamage = newMissileDamage;
            component.hasSecondaryMissiles = newHasSecondaryMissiles;
            component.secondaryMissileVelocity = newSecondaryMissileVelocity;
            component.secondaryMissileSpawnDelay = newSecondaryMissileSpawnDelay;
            component.secondaryMissileDamage = newSecondaryMissileDamage;
            component.hasHomeMissile = newHasHomeMissile;
            component.homeMissileVelocity = newHomeMissileVelocity;
            component.homeMissileSpawnDelay = newHomeMissileSpawnDelay;
            component.homeMissileDamage = newHomeMissileDamage;
            component.hasMagnetField = newHasMagnetField;
            component.magnetRadius = newMagnetRadius;
            return AddComponent(ComponentIds.PlayerModel, component);
        }

        public Entity ReplacePlayerModel(string newName, UnityEngine.Vector2 newVelocityLimit, int newHealth, bool newHasLaser, float newLaserDamage, bool newHasMissile, float newMissileVelocity, float newMissileSpawnDelay, float newMissileDamage, float newHasSecondaryMissiles, float newSecondaryMissileVelocity, float newSecondaryMissileSpawnDelay, float newSecondaryMissileDamage, bool newHasHomeMissile, float newHomeMissileVelocity, float newHomeMissileSpawnDelay, float newHomeMissileDamage, bool newHasMagnetField, float newMagnetRadius) {
            var previousComponent = hasPlayerModel ? playerModel : null;
            var component = _playerModelComponentPool.Count > 0 ? _playerModelComponentPool.Pop() : new PlayerModelComponent();
            component.name = newName;
            component.velocityLimit = newVelocityLimit;
            component.health = newHealth;
            component.hasLaser = newHasLaser;
            component.laserDamage = newLaserDamage;
            component.hasMissile = newHasMissile;
            component.missileVelocity = newMissileVelocity;
            component.missileSpawnDelay = newMissileSpawnDelay;
            component.missileDamage = newMissileDamage;
            component.hasSecondaryMissiles = newHasSecondaryMissiles;
            component.secondaryMissileVelocity = newSecondaryMissileVelocity;
            component.secondaryMissileSpawnDelay = newSecondaryMissileSpawnDelay;
            component.secondaryMissileDamage = newSecondaryMissileDamage;
            component.hasHomeMissile = newHasHomeMissile;
            component.homeMissileVelocity = newHomeMissileVelocity;
            component.homeMissileSpawnDelay = newHomeMissileSpawnDelay;
            component.homeMissileDamage = newHomeMissileDamage;
            component.hasMagnetField = newHasMagnetField;
            component.magnetRadius = newMagnetRadius;
            ReplaceComponent(ComponentIds.PlayerModel, component);
            if (previousComponent != null) {
                _playerModelComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovePlayerModel() {
            var component = playerModel;
            RemoveComponent(ComponentIds.PlayerModel);
            _playerModelComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherPlayerModel;

        public static IMatcher PlayerModel {
            get {
                if (_matcherPlayerModel == null) {
                    _matcherPlayerModel = Matcher.AllOf(ComponentIds.PlayerModel);
                }

                return _matcherPlayerModel;
            }
        }
    }
}
