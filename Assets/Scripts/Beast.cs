using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Beast", menuName = "Beast", order = 0)]
public class Beast : ScriptableObject
{
    public string ID;
    public int level;
    public Sprite icon;
    private int _exp, _hp, _mp;
    public int exp
    {
        get
        {
            return _exp;
        }

        set
        {
            int v = value;
            if (v > 100)
            {
                while (v > 100)
                {
                    level++;
                    v -= 100;
                }
            }
            _exp = v;
        }
    }
    public int hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = Mathf.Clamp(value, 0, 100);
        }
    }
    public int mp
    {
        get
        {
            return _mp;
        }
        set
        {
            _mp = Mathf.Clamp(value, 0, 100);
        }
    }
    public List<DamageStat> damages = new List<DamageStat>();
    public List<EffectStat> effects = new List<EffectStat>();
}
