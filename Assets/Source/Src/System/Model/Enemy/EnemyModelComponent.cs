﻿using Entitas;
using UnityEngine;

public class EnemyModelComponent : IComponent {
    public int id;
    public int type;
    public string resource;
    public int weapon;

    public int amount;
    public float time;
    public float spawnDelay;
    public string weaponResource;
    public float velocity;
    public int angle;
    public int waves;
    public int angleOffset;
    public Vector2 startVelocity;
    public float followDelay;
    public float selfDestructionDelay;
    public float timeDelay;
    public float delay;
    public float randomPositionOffsetX;
}