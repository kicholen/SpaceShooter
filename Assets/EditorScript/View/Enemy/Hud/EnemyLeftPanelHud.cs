using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyLeftPanelHud : EditorViewUpdaterBase {
    Transform content;
    EventService eventService;
    EnemyModelCmpActionExecutor executor;
    EnemyWeaponActionExecutor weaponExecutor;
    Entity entity;

    public EnemyLeftPanelHud(Transform content, EventService eventService, EnemyModelCmpActionExecutor executor, Entity entity) {
        go = content.gameObject;
        this.content = content;
        this.eventService = eventService;
        this.executor = executor;
        this.entity = entity;
        weaponExecutor = new EnemyWeaponActionExecutor(entity);
        setData();
    }

    void setData() {
        createChangeEnemyResource().transform.SetParent(content, false);
        createChangeEnemyWeapon().transform.SetParent(content, false);
    }

    GameObject createChangeEnemyWeapon() {
        List<string> types = typeof(WeaponTypes).GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
                    .Select(fieldInfo => ((int)fieldInfo.GetRawConstantValue()).ToString()).ToList<string>();
        return createDropdownElement("type", executor.getType().ToString(), types, (value) => {
            weaponExecutor.Execute(getActionBasedOnType(Convert.ToInt16(value)));
            executor.Execute(new ChangeEnemyModelWeaponAction(value));
        });
    }

    GameObject createChangeEnemyResource() {
        List<string> types = typeof(ResourceWithColliders).GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
                    .Select(fieldInfo => fieldInfo.GetRawConstantValue().ToString()).ToList<string>();
        types.Remove(ResourceWithColliders.Player);
        types.Remove(ResourceWithColliders.PlayerShield);
        types.Remove(ResourceWithColliders.Blockade);
        return createDropdownElementOfString("type", executor.getType().ToString(), types, (value) => {
            setResource(value, types[Convert.ToInt16(value)]);
            executor.Execute(new ChangeEnemyModelResourceAction(value));
        });
    }

    void setResource(string value, string resource) {
        if (entity.hasGameObject)
            entity.RemoveGameObject();
        entity.ReplaceResource(resource);
    }

    IEnemyWeaponAction getActionBasedOnType(int type) {
        switch (type) {
            case WeaponTypes.Circle:
                return new CircleWeaponAction();
            case WeaponTypes.CircleRotated:
                return new CircleRotatedWeaponAction();
            case WeaponTypes.Dispersion:
                return new DispersionWeaponAction();
            case WeaponTypes.Home:
                return new HomeWeaponAction();
            case WeaponTypes.Laser:
                return new LaserWeaponAction();
            case WeaponTypes.Multiple:
                return new MultipleWeaponAction();
            case WeaponTypes.Single:
                return new SingleWeaponAction();
            case WeaponTypes.Target:
                return new TargetWeaponAction();
            case WeaponTypes.None:
            default:
                return new RemoveWeaponAction();
        }
    }
}