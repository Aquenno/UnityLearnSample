using System.Collections.Generic;
using UnityEngine;

public class SampleScriptManager : MonoBehaviour
{
    [SerializeField] private List<SampleScript> scripts = new List<SampleScript>();

    public void AddScript(SampleScript script)
    {
        if (!scripts.Contains(script))
        {
            scripts.Add(script);
        }
    }

    public void RemoveScript(SampleScript script)
    {
        if (scripts.Contains(script))
        {
            scripts.Remove(script);
        }
    }

    [ContextMenu("Активировать всё")]
    public void UseAllScripts()
    {
        foreach (var script in scripts)
        {
            if (script != null)
            {
                script.Use();
            }
        }
    }
}