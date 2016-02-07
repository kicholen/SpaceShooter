using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRightPanelHud : EditorViewUpdaterBase {
    Transform content;
    EventService eventService;
    EnemyModelComponent component;

    EnemyWeaponParameterActionExecutor executor;

    public EnemyRightPanelHud(Transform content, EventService eventService, Entity entity, EnemyModelComponent component) {
        go = content.gameObject;
        this.content = content;
        this.eventService = eventService;
        this.component = component;
        create(entity, component);
    }

    void create(Entity entity, EnemyModelComponent component) {
        executor = new EnemyWeaponParameterActionExecutor(entity, component);
        addListeners();
    }

    public override void Destroy() {
        base.Destroy();
        eventService.RemoveListener<EnemyWeaponChangedEvent>(onWeaponChanged);
    }

    void addListeners() {
        eventService.AddListener<EnemyWeaponChangedEvent>(onWeaponChanged);
    }

    void onWeaponChanged(EnemyWeaponChangedEvent e) {
        removeChildren(content.gameObject);
        createFieldsBasedOnWeaponType();
    }

    void createFieldsBasedOnWeaponType() {
        switch (component.weapon) {
            case WeaponTypes.Circle:
                createChangeAmountField().transform.SetParent(content, false);
                createChangeVelocityField().transform.SetParent(content, false);
                createChangeWeaponResourceField().transform.SetParent(content, false);
                break;
            case WeaponTypes.CircleRotated:
                createChangeAmountField().transform.SetParent(content, false);
                createChangeWavesField().transform.SetParent(content, false);
                createChangeAngleField().transform.SetParent(content, false);
                createChangeAngleOffsetField().transform.SetParent(content, false);
                createChangeVelocityField().transform.SetParent(content, false);
                createChangeWeaponResourceField().transform.SetParent(content, false);
                break;
            case WeaponTypes.Dispersion:
                createChangeVelocityField().transform.SetParent(content, false);
                createChangeAngleField().transform.SetParent(content, false);
                createChangeWeaponResourceField().transform.SetParent(content, false);
                break;
            case WeaponTypes.Home:
                createChangeVelocityField().transform.SetParent(content, false);
                createChangeStartVelocityField().transform.SetParent(content, false);
                createChangeFollowDelayField().transform.SetParent(content, false);
                createChangeSelfDestructDelayField().transform.SetParent(content, false);
                break;
            case WeaponTypes.Laser:
                //todo
                break;
            case WeaponTypes.Multiple:
                createChangeAmountField().transform.SetParent(content, false);
                createChangeTimeDelayField().transform.SetParent(content, false);
                createChangeDelayField().transform.SetParent(content, false);
                createChangeRandomPositionOffsetXField().transform.SetParent(content, false);
                createChangeStartVelocityField().transform.SetParent(content, false);
                createChangeWeaponResourceField().transform.SetParent(content, false);
                break;
            case WeaponTypes.Single:
                createChangeStartVelocityField().transform.SetParent(content, false);
                createChangeWeaponResourceField().transform.SetParent(content, false);
                break;
            case WeaponTypes.Target:
                createChangeVelocityField().transform.SetParent(content, false);
                createChangeWeaponResourceField().transform.SetParent(content, false);
                break;
            case WeaponTypes.None:
            default:
                return;
        }

        createChangeTimeField().transform.SetParent(content, false);
        createChangeSpawnDelayAction().transform.SetParent(content, false);
    }

    GameObject createChangeAmountField() {
        return createInputElement("amount", component.amount.ToString(), (value) => {
            executor.Execute(new EnemyChangeAmountAction(value));
        });
    }

    GameObject createChangeAngleField() {
        return createInputElement("angle", component.angle.ToString(), (value) => {
            executor.Execute(new EnemyChangeAngleAction(value));
        });
    }

    GameObject createChangeAngleOffsetField() {
        return createInputElement("angleOffset", component.angleOffset.ToString(), (value) => {
            executor.Execute(new EnemyChangeAngleOffsetAction(value));
        });
    }

    GameObject createChangeDelayField() {
        return createInputElement("delay", component.delay.ToString(), (value) => {
            executor.Execute(new EnemyChangeDelayAction(value));
        });
    }

    GameObject createChangeFollowDelayField() {
        return createInputElement("followDelay", component.followDelay.ToString(), (value) => {
            executor.Execute(new EnemyChangeFollowDelayAction(value));
        });
    }

    GameObject createChangeRandomPositionOffsetXField() {
        return createInputElement("randomPositionOffsetX", component.randomPositionOffsetX.ToString(), (value) => {
            executor.Execute(new EnemyChangeRandomPositionOffsetXAction(value));
        });
    }

    GameObject createChangeSelfDestructDelayField() {
        return createInputElement("selfDestructionDelay", component.selfDestructionDelay.ToString(), (value) => {
            executor.Execute(new EnemyChangeSelfDestructDelayAction(value));
        });
    }

    GameObject createChangeSpawnDelayAction() {
        return createInputElement("spawnDelay", component.spawnDelay.ToString(), (value) => {
            executor.Execute(new EnemyChangeSpawnDelayAction(value));
        });
    }

    GameObject createChangeStartVelocityField() {
        return new InputVectorElement("startVelocity", eventService, component.startVelocity, (value) => {
            executor.Execute(new EnemyChangeStartVelocityAction(new Vector2(value, component.startVelocity.y)));
        }, (value) => {
            executor.Execute(new EnemyChangeStartVelocityAction(new Vector2(component.startVelocity.x, value)));
        }).Go;
    }

    GameObject createChangeTimeField() {
        return createInputElement("time", component.time.ToString(), (value) => {
            executor.Execute(new EnemyChangeTimeAction(value));
        });
    }

    GameObject createChangeTimeDelayField() {
        return createInputElement("timeDelay", component.timeDelay.ToString(), (value) => {
            executor.Execute(new EnemyChangeTimeDelayAction(value));
        });
    }

    GameObject createChangeVelocityField() {
        return createInputElement("velocity", component.velocity.ToString(), (value) => {
            executor.Execute(new EnemyChangeVelocityAction(value));
        });
    }

    GameObject createChangeWavesField() {
        return createInputElement("waves", component.waves.ToString(), (value) => {
            executor.Execute(new EnemyChangeWavesAction(value));
        });
    }

    GameObject createChangeWeaponResourceField() {
        List<string> types = new List<string>();
        types.Add(ResourceWithColliders.MissileEnemyHoming);
        types.Add(ResourceWithColliders.MissileEnemy);
        return createDropdownElementOfString("weaponResource", component.weaponResource, types, (value) => {
            executor.Execute(new EnemyChangeWeaponResourceAction(types[Convert.ToInt16(value)]));
        });
    }
}