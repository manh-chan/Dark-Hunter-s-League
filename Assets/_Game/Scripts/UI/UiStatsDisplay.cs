using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class UiStatsDisplay : UIPropertyDisplay
{
    public PlayerStats player;
    public CharacterData character;
    public bool displayCurrentHealth = false;

    public override object GetReadObject()
    {
        if(player) return player.Stats;
        else if (character) return character.stats;
        return new CharacterData.Stats();
    }
    public override void UpdateFields()
    {
        if (!player && !character) return;
        
        StringBuilder[] allStats = GetProperties(
            BindingFlags.Public | BindingFlags.Instance,
            "CharacterData+Stats");

        if (!propertyNames) propertyNames = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (!propertyValues) propertyValues = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        if (displayCurrentHealth)
        {
            allStats[0].Insert(0, "Health\n");
            allStats[0].Insert(0, player.CurrentHealth + "\n");
        }
        if(propertyNames) propertyNames.text = allStats[0].ToString();
        if(propertyValues) propertyValues.text = allStats[1].ToString();

        //propertyValues.fontSize = propertyNames.fontSize;
    }

    private void Reset()
    {
        player = FindObjectOfType<PlayerStats>();
    }
}
