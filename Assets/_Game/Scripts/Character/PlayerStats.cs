﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : EntityStats
{
    public UpgradeStats upgradeStats; 
    public CharacterData charactedData;
    public CharacterData.Stats baseStats;
    [SerializeField] CharacterData.Stats actualStats;

    public CharacterData.Stats Stats
    {
        get { return actualStats; }
        set { actualStats = value; }
    }
    public CharacterData.Stats Actual
    {
        get { return actualStats; }
    }


    #region Current Stats Properties
    public float CurrentHealth
    {
        get { return health; }
        set
        {
            if (health != value)
            {
                health = value;
                UpdateHealthBar();
            }
        }
    }


    #endregion

    [Header("Visuals")]
    public ParticleSystem damageEffect;
    public ParticleSystem blockeEffect;
    public ParticleSystem lvUpEffect;
    public ParticleSystem targetEffect;

    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;
    public int coin = 100;

    [System.Serializable]
    public class LevelRange
    {
        public int statsLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;



    [Header("I - Frames")]
    public float invincibilityDuration;
    float invincibilityTime;
    bool isInvincible;

    PlayerInventory inventory;
    PlayerCollector collector;

    [Header("UI")]
    public Image healthBar;
    public Image expBar;
    public TMP_Text levelText;
    public TMP_Text coinText;

    PlayerAnimator playerAnimator;

    private void Awake()
    {
        charactedData = UICharactedSelector.GetData();

        inventory = GetComponent<PlayerInventory>();
        collector = GetComponentInChildren<PlayerCollector>();
        
        baseStats = actualStats = charactedData.stats;
        collector.SetRadius(actualStats.magnet);
        health = actualStats.maxHealth;

        playerAnimator = GetComponent<PlayerAnimator>();
        if (charactedData.controller)
        {
            playerAnimator.SetAnimatorController(charactedData.controller);
        }

    }
    protected override void Start()
    {
        base.Start();
        
        if (UILevelSelector.globaBuff && !UILevelSelector.globalBuffAffectsPlayer)
            ApplyBuff(UILevelSelector.globaBuff);
        
        inventory.Add(charactedData.StartingWeapon);

        experienceCap = levelRanges[0].experienceCapIncrease;

        GameManager.instance.AssignChosenCharacterUI(charactedData);
        Invoke(nameof(UpdateHealth1s),0.2f);
        UpdateHealthBar();
        UpdateExpBar();
        UpdateLevelText();
    }
    protected override void Update()
    {
        base.Update();
        if (invincibilityTime > 0)
        {
            invincibilityTime -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }
        Recover();
        if (Input.GetKeyDown(KeyCode.P))
        {
            experience += 1500;
            LevelUpChecker();

            UpdateExpBar();
        }
        InputPLvUp();
    }
    public override void RecalculateStats()
    {
        actualStats = baseStats;

        foreach (PlayerInventory.Slot s in inventory.passiveSlots)
        {
            Passive p = s.item as Passive;
            if (p)
            {
                actualStats += p.GetBoosts();
            }
        }

        if (upgradeStats != null)
        {
            actualStats.maxHealth += upgradeStats.maxHealthBonus;
            actualStats.recovery += upgradeStats.recoveryBonus;
            actualStats.armor += upgradeStats.armorBonus;
            actualStats.moveSpeed += upgradeStats.moveSpeedBonus;
            actualStats.might += upgradeStats.mightBonus;
            actualStats.amount += upgradeStats.amountBonus;
            actualStats.area += upgradeStats.areaBonus;
            actualStats.speed += upgradeStats.speedBonus;
            actualStats.duration += upgradeStats.durationBonus;
            actualStats.cooldown += upgradeStats.cooldownBonus;
            actualStats.luck += upgradeStats.luckBonus;
        }

        CharacterData.Stats multiplier = new CharacterData.Stats
        {
            maxHealth = 1f,
            recovery = 1f,
            armor = 1f,
            moveSpeed = 1f,
            might = 1f,
            area = 1f,
            speed = 1f,
            duration = 1f,
            amount = 1,
            cooldown = 1f,
            luck = 1f,
            growth = 1f,
            greed = 1f,
            curse = 1f,
            magnet = 1f,
            revival = 1
        };

        foreach (Buff b in activeBuffs)
        {
            BuffData.Stats bd = b.GetData();
            switch (bd.modifierType)
            {
                case BuffData.ModifierType.additive:
                    actualStats += bd.playerModifier;
                    break;
                case BuffData.ModifierType.multiplicative:
                    multiplier *= bd.playerModifier;
                    break;
            }
        }

        actualStats *= multiplier;

        if (CurrentHealth > actualStats.maxHealth)
        {
            CurrentHealth = actualStats.maxHealth;
        }

        collector.SetRadius(actualStats.magnet);

    }

    public void UpdateHealth1s()
    {
        CurrentHealth += actualStats.maxHealth;
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        coin += amount;
        LevelUpChecker();
        UpdateCoinText();
        UpdateExpBar();
    }
    private void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;
            int experienceCapIncrease = 0;
            if (lvUpEffect) Destroy(Instantiate(lvUpEffect, transform.position, Quaternion.identity), 5f);
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.statsLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
            UpdateLevelText();
            GameManager.instance.StartLevelUp();

            if (experience >= experienceCap)
            {
                LevelUpChecker();
            }
        }
    }
    private void UpdateExpBar()
    {
        expBar.fillAmount = (float)experience / experienceCap;
    }
    private void UpdateLevelText()
    {
        levelText.text = level.ToString();
    }
    private void UpdateCoinText()
    {
        coinText.text = coin.ToString();
    }
    public override void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            damage = Mathf.Max(damage - actualStats.armor, 0);
            if (damage > 0)
            {
                CurrentHealth -= damage;

                if (damageEffect) Destroy(Instantiate(damageEffect, transform.position, Quaternion.identity), 5f);


                if (CurrentHealth <= 0)
                {
                    Kill();
                }
                else
                {
                    if (blockeEffect) Destroy(Instantiate(blockeEffect, transform.position, Quaternion.identity), 5f);
                }
                invincibilityTime = invincibilityDuration;
                isInvincible = true;
                UpdateHealthBar();
            }
        }
    }
    private void UpdateHealthBar()
    {
        healthBar.fillAmount = CurrentHealth / actualStats.maxHealth;
    }
    public override void Kill()
    {
        if (!GameManager.instance.isGameOver)
        {
            GameManager.instance.AssignLevelReacheUI(level);
            GameManager.instance.GameOver();
        }
    }
    public override void RestoreHealth(float amount)
    {
        if (CurrentHealth < actualStats.maxHealth)
        {
            CurrentHealth += amount;
            if (CurrentHealth > actualStats.maxHealth)
            {
                CurrentHealth = actualStats.maxHealth;
            }
            UpdateHealthBar();
        }
    }
    private void Recover()
    {
        if (CurrentHealth < actualStats.maxHealth)
        {
            CurrentHealth += Stats.recovery * Time.deltaTime;
            if (CurrentHealth > actualStats.maxHealth)
            {
                CurrentHealth = actualStats.maxHealth;
            }
            UpdateHealthBar();
        }
    }
    private void InputPLvUp()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            experience += 50;
            LevelUpChecker();

            UpdateExpBar();
        }
    }
    public void EnableEffect()
    {
        if (targetEffect != null && !targetEffect.isPlaying)
        {
            targetEffect.Play();
        }
    }

    public void DisableEffect()
    {
        if (targetEffect != null && targetEffect.isPlaying)
        {
            targetEffect.Stop();
        }
    }
}
