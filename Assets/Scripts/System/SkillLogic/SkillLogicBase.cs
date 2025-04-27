using System.Collections;
using UnityEngine;

public abstract class SkillLogicBase : ScriptableObject
{
    public abstract IEnumerator ExecuteFlow();
}
