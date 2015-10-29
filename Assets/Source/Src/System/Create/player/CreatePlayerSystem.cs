using Entitas;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System;

public class CreatePlayerSystem : IInitializeSystem, ISetPool {
	Pool _pool;
	Group _group;

	const string playerSettingsPath = "Player";

	public void SetPool(Pool pool) {
		_pool = pool;
		pool.GetGroup(Matcher.PlayerModel).OnEntityAdded += create;
		_group = pool.GetGroup(Matcher.PlayerModel);
	}
	
	public void Initialize() {
		PlayerModelComponent component = new PlayerModelComponent();

		XmlNode node = loadXml(playerSettingsPath);

		XmlNode playerNode = node.FirstChild;
		XmlNode mainNode = playerNode.FirstChild;

		XmlAttributeCollection attributes = mainNode.Attributes;
		
		component.name = attributes[0].Value;
		mainNode = mainNode.NextSibling;
		attributes = mainNode.Attributes;
		component.velocityLimit = new Vector2((float)Convert.ToDouble(attributes[0].Value), (float)Convert.ToDouble(attributes[1].Value));

		mainNode = mainNode.NextSibling;
		attributes = mainNode.Attributes;
		component.health = Convert.ToInt32(attributes[0].Value);

		XmlNode weapon = mainNode.NextSibling.FirstChild;

		while (weapon != null) {
			attributes = weapon.Attributes;
			if (weapon.Name == "missile") {
				component.missileVelocity = (float)Convert.ToDouble(attributes[0].Value);
				component.missileSpawnDelay = (float)Convert.ToDouble(attributes[1].Value);
				component.missileDamage = (float)Convert.ToDouble(attributes[2].Value);
			}
			else if (weapon.Name == "homeMissile") {
				component.hasHomeMissile = true;
				component.homeMissileVelocity = (float)Convert.ToDouble(attributes[0].Value);
				component.homeMissileSpawnDelay = (float)Convert.ToDouble(attributes[1].Value);
				component.homeMissileDamage = (float)Convert.ToDouble(attributes[2].Value);
			}
			else {
				//throw UnityException("Player System weapon wasn't detected");
			}
			weapon = weapon.NextSibling;
		}

		Entity e = _pool.CreateEntity();
		e.AddComponent(ComponentIds.PlayerModel, component);
	}

	void create(Group group, Entity entity, int index, IComponent component) {
		PlayerModelComponent playerModel = (PlayerModelComponent)component;
		
		Entity player = _pool.CreateEntity()
				.AddPlayer(playerModel.name)
				.AddPosition(0.0f, 0.0f)
				.AddVelocity(0.0f, 0.0f)
				.AddVelocityLimit(playerModel.velocityLimit.x, playerModel.velocityLimit.y, 0.0f, 0.0f)
				.AddCollision(CollisionTypes.Player)
				.AddHealth(playerModel.health)
				.AddResource(Resource.Player);
		
		player.AddParent(getChildren(player, playerModel));
	}

	List<Entity> getChildren(Entity parent, PlayerModelComponent component) {
		List<Entity> children = new List<Entity>();
		if (component.hasHomeMissile) {
	         children.Add(_pool.CreateEntity()
		         .AddRelativePosition(0.5f, 0.5f)
		         .AddPosition(0.0f, 0.0f)
		         .AddChild(parent)
	             .AddHomeMissileSpawner(0.0f, component.homeMissileSpawnDelay, Resource.Missile, component.homeMissileVelocity, CollisionTypes.Player)
		         .AddResource(Resource.Weapon));
	         children.Add(_pool.CreateEntity()
		         .AddRelativePosition(-0.5f, 0.5f)
		         .AddPosition(0.0f, 0.0f)
		         .AddChild(parent)
	             .AddHomeMissileSpawner(0.0f, component.homeMissileSpawnDelay, Resource.Missile, component.homeMissileVelocity, CollisionTypes.Player)
		         .AddResource(Resource.Weapon));
		}

		addNonRemovable(children);
		return children;
	}

	void addNonRemovable(List<Entity> entities) {
		foreach (Entity e in entities) {
			e.isNonRemovable = true;
		}
	}

	XmlNode loadXml(string path) {
		XmlDocument doc = new XmlDocument();
		TextAsset textFile = Resources.Load<TextAsset>(path);
		doc.LoadXml(textFile.text);
		return doc.FirstChild;
	}
}