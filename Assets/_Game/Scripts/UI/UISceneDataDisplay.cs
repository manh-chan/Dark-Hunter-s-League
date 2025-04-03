using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine;

public class UISceneDataDisplay : UIPropertyDisplay
{
    public UILevelSelector levelSelector;
    TMP_Text extraStageInfo;

    public override object GetReadObject()
    {
        if(levelSelector && UILevelSelector.selectedLevel >= 0)
            return levelSelector.levels[UILevelSelector.selectedLevel];
        return new UILevelSelector.SceneData();
    }
    public override void UpdateFields()
    {
        if (!propertyNames) propertyNames = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (!propertyValues) propertyValues = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        if (!extraStageInfo) extraStageInfo = transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        StringBuilder[] allStats = GetProperties(
            BindingFlags.Public | BindingFlags.Instance, "UILevelSelector+SceneData");

        UILevelSelector.SceneData dat = (UILevelSelector.SceneData)GetReadObject();

        allStats[0].AppendLine("Move Speed").AppendLine("Gold Bonus").AppendLine("Luck Bonus").AppendLine("XP Bonus").AppendLine("Enemy Healdth");

        Type CharacterDataStats = typeof(CharacterData.Stats);
        ProcessValue(dat.playerModifiers.moveSpeed, allStats[1], CharacterDataStats.GetField("moveSpeed"));
        ProcessValue(dat.playerModifiers.greed, allStats[1], CharacterDataStats.GetField("greed"));
        ProcessValue(dat.playerModifiers.luck, allStats[1], CharacterDataStats.GetField("luck"));
        ProcessValue(dat.playerModifiers.growth, allStats[1], CharacterDataStats.GetField("growth"));

        Type enemyStats = typeof(EnemyState.Stats);

        ProcessValue(dat.playerModifiers.maxHealth, allStats[1], enemyStats.GetField("maxHealth"));

        if(propertyNames) propertyNames.text = allStats[0].ToString();
        if(propertyValues) propertyValues.text = allStats[1].ToString();
    }
    protected override bool IsFieldShown(FieldInfo field)
    {
        switch(field.Name)
        {
            default: return false;
            case "timeLimit":
            case "clockspeed":
            case "movespeed":
            case "greed":
            case "luck":
            case "growth":
            case "maxHealth":
                return true;
        }
    }
    protected override StringBuilder ProcessName(string name, StringBuilder output, FieldInfo field)
    {
        if(field.Name == "extraNotes") return output;
        return base.ProcessName(name, output, field);
    }
    protected override StringBuilder ProcessValue(object value, StringBuilder output, FieldInfo field)
    {
        float fval;
        switch (field.Name)
        {
            case "timelimit":
                fval = value is int ? (int)value : (float)value;
                if(fval == 0)
                {
                    output.Append(DASH).Append('\n');
                }
                else
                {
                    string minutes = Mathf.FloorToInt(fval / 60).ToString();
                    string seconds = (fval % 60).ToString();
                    if(fval % 60 < 10)
                    {
                        seconds += "0";
                    }
                    output.Append(minutes).Append(":").Append(seconds).Append('\n');
                }
                return output;
            case "clockSpped":
                fval = value is int ? (int)value: (float)value;
                output.Append(fval).Append("x").Append('\n');
                return output;
            case "maxHealth":
            case "moveSpeed":
            case "greed":
            case "luck":
            case "growth":
                fval = value is int ? (int)value : (float)value;
                float percentage = Mathf.Round(fval * 100);

                if(Mathf.Approximately(percentage, 0))
                {
                    output.Append(DASH).Append('\n');
                }
                else
                {
                    if (percentage > 0)
                        output.Append('\n');
                    output.Append(percentage).Append("%").Append("\n");
                }
                return output;
            case "extraNotes":
                if(value == null) return output;
                string msg = value.ToString();
                extraStageInfo.text = string.IsNullOrWhiteSpace(msg) ? DASH : msg;
                return output;
        }
        return base.ProcessValue(value, output, field);
    }
    private void Reset()
    {
        levelSelector = FindObjectOfType<UILevelSelector>();
    }
}
