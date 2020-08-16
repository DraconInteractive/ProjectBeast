using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics
{

}

public class DamageStat
{
    public ElementalStat Type;
    public int Amount;
}

public class EffectStat
{
    public CharacterStat Effect;
    public int Amount;
}
public enum ElementalStat
{
    None,
    Fire,
    Ice,
    Air,
    Energy,
    Light,
    Dark,
    Earth,
    All
}

public enum CharacterStat
{
    None,
    Speed,
    Agility,
    Strength,
    Dexterity
}

