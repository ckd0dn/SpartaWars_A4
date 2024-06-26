using JetBrains.Annotations;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Slot", menuName = "SOData/SlotData")]
public class SlotData : ScriptableObject
{
    [Header("Slot")]
    public string slotName;
    public Sprite image;

    [Header("Upgrade")]
    public int level;
    public int gold;
}