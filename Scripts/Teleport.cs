using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour {

    public string sceneName;
    public GameObject GUIObject;

    void Start () {
        GUIObject.SetActive(false);
    }
	
	void Update () {
 

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GUIObject.SetActive(true);

            if (GUIObject.activeInHierarchy == true && Input.GetButtonDown("Use"))
                SceneManager.LoadScene(sceneName);
        }

    }
    
    void OnTriggerExit(Collider other)
    {
        GUIObject.SetActive(false);
    }
}
