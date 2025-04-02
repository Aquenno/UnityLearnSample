using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("Kirill", LoadSceneMode.Additive);
        SceneManager.LoadScene("Misha", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
