﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;
using System.Linq;
using ICSharpCode.NRefactory.PrettyPrinter;

[CustomEditor(typeof(Beast))]
public class BeastInspector : Editor
{
    public override void OnInspectorGUI()
    {
        var beast = (Beast)target;
        serializedObject.Update();
        bool setDirty = false;
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        beast.ID = EditorGUILayout.TextField("Name", beast.ID);
        beast.level = EditorGUILayout.IntField("Level", beast.level);
        EditorGUILayout.Space(46);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
        {
            beast.exp += 10;
            setDirty = true;
        }
        if (GUILayout.Button("Retract"))
        {
            beast.exp -= 10;
            setDirty = true;
        }
        EditorGUILayout.Space(26);
        EditorGUILayout.EndHorizontal();
        EditorGUI.ProgressBar(GUILayoutUtility.GetRect(1, 16), (beast.exp * 0.01f), "EXP: " + beast.exp + "%");
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        beast.icon = (Sprite)EditorGUILayout.ObjectField(beast.icon, typeof(Sprite), false, GUILayout.Width(128), GUILayout.Height(128));
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(16);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Damage"))
        {
            beast.hp -= 10;
            setDirty = true;
        }
        if (GUILayout.Button("Heal"))
        {
            beast.hp += 10;
            setDirty = true;
        }
        if (GUILayout.Button("Exhaust"))
        {
            beast.mp -= 10;
            setDirty = true;
        }
        if (GUILayout.Button("Refresh"))
        {
            beast.mp += 10;
            setDirty = true;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(16);
        float h = beast.hp * 0.01f;
        float m = beast.mp * 0.01f;
        EditorGUI.ProgressBar(GUILayoutUtility.GetRect(1, 32), h, "Health: " + beast.hp + "%");
        EditorGUI.ProgressBar(GUILayoutUtility.GetRect(1, 32), m, "Mana: " + beast.mp + "%");
        EditorGUILayout.Space(16);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Damages");
        if (GUILayout.Button("Add"))
        {
            beast.damages.Add(new DamageStat());
            setDirty = true;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(8);
        for (int i = 0; i < beast.damages.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.LabelField("\t" + beast.stats[i].type.ToString());
            EditorGUILayout.Space(16);
            beast.damages[i].Type = (ElementalStat)EditorGUILayout.EnumPopup(beast.damages[i].Type);
            beast.damages[i].Amount = EditorGUILayout.IntField(beast.damages[i].Amount);
            if (GUILayout.Button("Remove"))
            {
                beast.damages.Remove(beast.damages[i]);
                setDirty = true;
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.Space(16);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Effects");
        if (GUILayout.Button("Add"))
        {
            beast.effects.Add(new EffectStat());
            setDirty = true;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(8);
        for (int i = 0; i < beast.effects.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(16);
            beast.effects[i].Effect = (CharacterStat)EditorGUILayout.EnumPopup(beast.effects[i].Effect);
            beast.effects[i].Amount = EditorGUILayout.IntField(beast.effects[i].Amount);
            if (GUILayout.Button("Remove"))
            {
                beast.effects.Remove(beast.effects[i]);
                setDirty = true;
            }
            EditorGUILayout.EndHorizontal();
        }
        if (setDirty)
        {
            Debug.Log("Set dirty");
            EditorUtility.SetDirty(beast);
            Undo.RecordObject(beast, "Test");
            AssetDatabase.SaveAssets();
            // Refresh to show changes in the editor
            AssetDatabase.Refresh();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
