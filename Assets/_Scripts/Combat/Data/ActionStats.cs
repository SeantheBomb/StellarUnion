using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ActionStats
{

    public Vitals vitals;
    public Farnecc farnecc;
    public Capabilities capabilities;
    public Skills skills;

    public ActionStats()
    {

    }

    public ActionStats(Vitals v, Farnecc f, Capabilities c, Skills s)
    {
        this.vitals = v;
        this.farnecc = f;
        this.capabilities = c;
        this.skills = s;
    }

    public static ActionStats Roll(int totalFarnecc = 35)
    {
        ActionStats a = new ActionStats();
        a.farnecc.Roll();
        a.skills.LoadFarnecc(a.farnecc);
        a.vitals.LoadSkills(a.skills);
        a.capabilities.LoadSkills(a.skills);
        return a;
    }

}



/// <summary>
/// Farnecc ranges between 1-10?
/// If you have 1-2, they shouldnt have any skills for that stat
/// if you have 3-4 they should have skills for that state be around 1-12
/// If you have 5-7, they should have skiils for that stat be around 13-24
/// If you have 8-10, they should have skills for that state be around 25-50
/// </summary>
[System.Serializable]
public struct Farnecc
{
    public int fit;
    public int aware;
    public int rigor;
    public int nimble;
    public int educated;
    public int charm;
    public int chance;

    public void Roll()
    {
        this = GetRoll();
    }


    public static Farnecc GetDefault()
    {
        return new Farnecc()
        {
            fit = 5,
            aware = 5,
            rigor = 5,
            nimble = 5,
            educated = 5,
            charm = 5,
            chance = 5
        };
    }


    public static int GetRandom(ref int remainingPoints)
    {
        int min = Mathf.Max(1, 10 - remainingPoints);
        int max = Mathf.Min(10, remainingPoints);
        int result = Random.Range(min, max);
        remainingPoints -= result;
        return result;
    }

    public static Farnecc GetRoll(int totalPoints = 35)
    {
        return new Farnecc()
        {
            fit = GetRandom(ref totalPoints),
            aware = GetRandom(ref totalPoints),
            rigor = GetRandom(ref totalPoints),
            nimble = GetRandom(ref totalPoints),
            educated = GetRandom(ref totalPoints),
            charm = GetRandom(ref totalPoints),
            chance = GetRandom(ref totalPoints),
        };
    }
}


/// <summary>
/// Skills range between 0 -> 100? 
/// At zero you are unable to use the skill...
/// 1-5 you would be useless, 6-12 you would be sometimes lucky, 13-24 you would be starting to get it. 
/// 25-50 you would be starting to get skilled.
/// 50-100 you will be mastering this skill
/// </summary>
[System.Serializable]
public struct Skills
{
    public int trade;
    public int sidearms;
    public int rifles;
    public int demolitions;
    public int infiltration;
    public int infirmary;
    public int battery;
    public int mechanic;
    public int stealth;
    public int influence;
    public int martialarts;
    public int armor;
    public int survival;
    public int pickpocket;
    public int evasion;
    public int intimidation;
    public int computers;
    public int perception;
    public int leadership;
    public int endurance;
    public int woo;


    public void LoadFarnecc(Farnecc f)
    {
        this = GetFarnecc(f);
    }


    public static int GetSkill(int lstat, int rstat, int baseValue)
    {
        return (int)((lstat + rstat) / 3f) * baseValue;
    }

    public static Skills GetFarnecc(Farnecc f)
    {
        int baseValue = 10;

        return new Skills()
        {
            rifles = GetSkill(f.fit, f.aware, baseValue),
            armor = GetSkill(f.fit, f.rigor, baseValue),
            martialarts = GetSkill(f.fit, f.nimble, baseValue),
            mechanic = GetSkill(f.fit, f.educated, baseValue),
            intimidation = GetSkill(f.charm, f.fit, baseValue),
            survival = GetSkill(f.rigor, f.aware, baseValue),
            sidearms = GetSkill(f.nimble, f.aware, baseValue),
            demolitions = GetSkill(f.educated, f.aware, baseValue),
            trade = GetSkill(f.charm, f.aware, baseValue),
            evasion = GetSkill(f.rigor, f.nimble, baseValue),
            infirmary = GetSkill(f.rigor, f.educated, baseValue),
            woo = GetSkill(f.charm, f.rigor, baseValue),
            infiltration = GetSkill(f.nimble, f.educated, baseValue),
            battery = GetSkill(f.fit, f.fit, baseValue),
            stealth = GetSkill(f.nimble, f.nimble, baseValue),
            computers = GetSkill(f.educated, f.educated, baseValue),
            pickpocket = GetSkill(f.charm, f.nimble, baseValue),
            influence = GetSkill(f.charm, f.educated, baseValue),
            perception = GetSkill(f.aware, f.aware, baseValue),
            leadership = GetSkill(f.charm, f.charm, baseValue),
            endurance = GetSkill(f.rigor, f.rigor, baseValue),
        };
    }

  
}


[System.Serializable]
public struct Vitals
{
    public int HealthPoints;
    public int ActionPoints;
    public int VitalPoints;


    public int GetVitalFromSkill(int skill, float expValue, int minValue)
    {
        return (int)Mathf.Sqrt(Mathf.Pow(skill, expValue)) + minValue;
    }

    public void LoadSkills(Skills skills)
    {
        HealthPoints = GetVitalFromSkill(skills.endurance, 1.5f, 10);
        ActionPoints = GetVitalFromSkill(skills.evasion, 1.5f,10);
        VitalPoints = GetVitalFromSkill(skills.survival, 1.5f, 10);
    }
}


[System.Serializable]
public struct Capabilities
{
    public int initiative;
    public int speed;
    public int defense;
    public int offense;
    public int deescalate;


    public int GetVitalFromSkill(int divisor, params int[] skills)
    {
        return (int)skills.Average() / divisor;
    }

    public void LoadSkills(Skills skills)
    {
        initiative = GetVitalFromSkill(10, skills.perception);
        speed = GetVitalFromSkill(10, skills.evasion);
        defense = GetVitalFromSkill(10, skills.armor, skills.endurance, skills.evasion);
        offense = GetVitalFromSkill(10, skills.rifles, skills.sidearms, skills.martialarts, skills.battery);
        deescalate = GetVitalFromSkill(10, skills.intimidation, skills.influence, skills.woo, skills.trade);
    }

}