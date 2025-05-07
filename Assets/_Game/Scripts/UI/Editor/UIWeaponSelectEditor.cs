using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Events;

[DisallowMultipleComponent]
[CustomEditor(typeof(UIWeapon))]
public class UIWeaponSelectEditor : Editor
{
    UIWeapon selector;

    private void OnEnable()
    {
        selector = target as UIWeapon;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate Selectable Weapon"))
        {
            CreateTogglesForCharacterData();
        }
    }

    public void CreateTogglesForCharacterData()
    {
        if (!selector.toggleTemplate)
        {
            Debug.LogWarning("Please assign a toggle Template for the ui Character Selector first.");
            return;
        }
        for (int i = selector.toggleTemplate.transform.parent.childCount - 1; i >= 0; i--)
        {
            Toggle tog = selector.toggleTemplate.transform.parent.GetChild(i).GetComponent<Toggle>();
            if (tog == selector.toggleTemplate) continue;

            Undo.DestroyObjectImmediate(tog.gameObject);
        }

        Undo.RecordObject(selector, "Updates to UICharacterSelect. ");
        selector.selectableToggles.Clear();
        WeaponData[] characters = UIWeapon.GetAllWeaponDataAssets();

        for (int i = 0; i < characters.Length; i++)
        {
            Toggle tog;
            if (i == 0)
            {
                tog = selector.toggleTemplate;
                Undo.RecordObject(tog, "Modifying the template. ");
            }
            else
            {
                tog = Instantiate(selector.toggleTemplate, selector.toggleTemplate.transform.parent);
                Undo.RegisterCreatedObjectUndo(tog.gameObject, "Created a new toggle. ");
            }

            Transform iconWeapon = tog.transform.Find(selector.weaponIconPath);
            if (iconWeapon && iconWeapon.TryGetComponent(out Image lvlImageweapon))
            {
                lvlImageweapon.sprite = characters[i].icon;
            }
            selector.selectableToggles.Add(tog);

            for (int j = 0; j < tog.onValueChanged.GetPersistentEventCount(); j++)
            {
                if (tog.onValueChanged.GetPersistentMethodName(j) == "Select")
                {
                    UnityEventTools.RemovePersistentListener(tog.onValueChanged, j);
                }
            }
            UnityEventTools.AddObjectPersistentListener(tog.onValueChanged, selector.Select, characters[i]);
        }
        EditorUtility.SetDirty(selector);
    }
}
