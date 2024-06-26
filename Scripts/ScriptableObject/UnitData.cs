using JetBrains.Annotations;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Unit", menuName = "SOData/UnitData")]
public class UnitData : ScriptableObject
{
    [Header("UnitName")]
    public string unitName;
    
    [Header("Stat")]
    public int attack;
    public int health;
    public float attackSpeed;
    public float moveSpeed;
    public float Range;
    public int cost;

    [Header("Drop")]
    public int dropGold;
    public int dropMana;

    [Header("Bullet")]
    public GameObject bullet;

    [Header("Icon")]
    public Sprite icon;

    [Header("UnitID")]
    public int ID;
}