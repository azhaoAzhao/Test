using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameMgr", menuName = "GameMgrSpcriptable")]
public class BattleMgr : ScriptableObject
{
    /// <summary>
    /// 战斗事件监听
    /// </summary>
    public Action _battleListening;
    /// <summary>
    /// 战斗伤害监听
    /// </summary>
    public Action<long> _battleDamage;
    /// <summary>
    /// 战斗技能伤害监听
    /// </summary>
    public Action<string, long> _battleSkillDamage;
    /// <summary>
    /// 战斗控制伤害监听 技能名称/控制类型 /伤害（无则填0）/控制时长
    /// </summary>
    public Action<string, string, long, DateTime> _battleSkillControlDamage;

    /*
     1.技能是否存在持续伤害?
     2.技能是否要使用ID 这样可以省区很多麻烦
     */


    /// <summary>
    /// 战斗监听事件 所有战斗都要走
    /// </summary>
    public void Battle()
    {
        if (_battleListening != null)
        {
            _battleListening.Invoke();
        }
    }

    /// <summary>
    /// 战斗伤害（平A）事件监听
    /// </summary>
    /// <param name="Damage">伤害数值</param>
    public void BattleDamage(long Damage)
    {
        Debug.Log($"BattleInfo:伤害{Damage}");//TODO 这里需要不需要在展示出玩家的信息？？？

        if (_battleDamage != null)
        {
            _battleDamage.Invoke(Damage);
        }
        else
        {
            NotBattleEvent();
        }
    }

    /// <summary>
    /// 战斗伤害 事件监听
    /// </summary>
    /// <param name="skillName">技能名称</param> //TODO 这里是不是改成技能ID最好？
    /// <param name="skillDamage">技能伤害</param>
    public void BattleSkillDamage(string skillName, long skillDamage)
    {
        if (_battleSkillDamage != null)
        {
            _battleSkillDamage.Invoke(skillName, skillDamage);
        }
        else
        {
            NotBattleEvent();
        }
    }
    /// <summary>
    /// 战斗控制伤害 事件监听  主要作用于控制技能 只有控制没有伤害填0
    /// </summary>
    /// <param name="skillName">技能名称</param>
    /// <param name="ControlType">技能类型</param>
    /// <param name="Damage">技能伤害</param>
    /// <param name="ControlDateTime">控制时长</param>
    public void BattleSkillControlDamage(string skillName, string ControlType, long Damage, DateTime ControlDateTime)
    {

        if (_battleSkillControlDamage != null)
        {
            _battleSkillControlDamage.Invoke(skillName, ControlType, Damage, ControlDateTime);
        }
        else
        {
            NotBattleEvent();
        }
    }

    public void NotBattleEvent() => Debug.LogError("BattleInfo:缺少战斗事件监听");
}
