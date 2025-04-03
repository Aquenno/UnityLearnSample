using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[HelpURL("https://docs.google.com/document/d/1GP4_m0MzOF8L5t5pZxLChu3V_TFIq1czi1oJQ2X5kpU/edit?usp=sharing")]
public class GameObjectActivator : MonoBehaviour
{
    [Tooltip("List of target objects and their states")]
    [SerializeField] private List<StateContainer> targets = new List<StateContainer>();

    [Tooltip("Enable to visualize connections in Scene view")]
    [SerializeField] private bool debug = false;

    private void Awake()
    {
        foreach (var item in targets)
        {
            item.defaultValue = item.targetGO.activeSelf;
        }
    }

    [ContextMenu("Активировать модуль")]
    public void ActivateModule()
    {
        SetStateForAll();
    }

    public void ReturnToDefaultState()
    {
        foreach (var item in targets)
        {
            if (item.targetGO != null)
            {
                item.targetState = item.defaultValue;
                item.targetGO.SetActive(item.defaultValue);
            }
        }
    }

    private void SetStateForAll()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null && targets[i].targetGO != null)
            {
                targets[i].targetGO.SetActive(targets[i].targetState);
                targets[i].targetState = !targets[i].targetState;
            }
            else
            {
                UnityEngine.Debug.LogError("Element " + i + " is null. Reference might be lost. Source: " + gameObject.name);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawSphere(transform.position, 0.3f);

            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] != null && targets[i].targetGO != null)
                {
                    Gizmos.color = targets[i].targetState ? Color.green : Color.red;
                    Gizmos.DrawLine(transform.position, targets[i].targetGO.transform.position);
                }
            }
        }
    }
}

[System.Serializable]
public class StateContainer
{
    [Tooltip("Target GameObject to change active state")]
    public GameObject targetGO;

    [Tooltip("Target state. If checked, object will be activated")]
    public bool targetState = false;

    [HideInInspector]
    public bool defaultValue;
}