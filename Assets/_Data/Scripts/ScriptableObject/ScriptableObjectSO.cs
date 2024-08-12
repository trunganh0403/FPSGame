using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "SO/ScriptableObjectSO")]
public class ScriptableObjectSO : ScriptableObject
{
    public AmoType amoType;
    public int dame;
    public int range;
    public int ammoCount;
}
