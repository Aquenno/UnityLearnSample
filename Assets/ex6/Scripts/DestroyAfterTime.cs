using UnityEngine;
using UnityEngine.Events;

public class DestroyAfterTime : MonoBehaviour
{
    public float time = 2f;
    void Start() => Destroy(gameObject, time);
}