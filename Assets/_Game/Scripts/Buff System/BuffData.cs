using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Buff Data", menuName = "Dark-Hunter-s-League/Buff Data")]
public class BuffData : ScriptableObject
{
    public new string name = "New Buff";
    public Sprite icon;

    [System.Flags]
    public enum Type : byte { buff = 1, debuff = 2, freeze = 4, strong = 8 }
    public Type type;

    public enum StackType : byte { refreshDurationOnly, stacksFully, doesNotStack }
    public enum ModifierType : byte { additive, multiplicative }

    [System.Serializable]
    public class Stats
    {
        public string name;

        [Header("Visuals")]
        [Tooltip("Effect that is attached to the GameObject with the buff.")]
        public ParticleSystem effect;
        [Tooltip("the tint colour of units affected by this buff.")]
        public Color tint = new Color(0, 0, 0, 0);
        [Tooltip("Whether this buff slows down or speeds up the animation of the affected GameObject.")]
        public float animationSpeed = 1f;

        [Header("Stats")]
        public float duration;
        public float damagePerSecond, healPerSecond;

        [Tooltip("Controls how frequetly the damage / heal per second applies. ")]
        public float tickInterval = 0.25f;

        public StackType stackType;
        public ModifierType modifierType;

        public Stats()
        {
            duration = 10f;
            damagePerSecond = 1f;
            healPerSecond = 1f;
            tickInterval = 0.25f;
        }
        public CharacterData.Stats playerModifier;
        public EnemyState.Stats enemyModifier;
    }
    public Stats[] variations = new Stats[1]
    {
        new Stats{name = "Level 1"}
    };
    public float GetTickDamage(int variant = 0)
    {
        Stats s = Get(variant);
        return s.damagePerSecond * s.tickInterval;
    }
    public float GetTickHeal(int variant = 0)
    {
        Stats s = Get(variant);
        return s.healPerSecond * s.tickInterval;
    }
    public Stats Get(int variant = -1)
    {
        return variations[Mathf.Max(0, variant)];
    }
}
