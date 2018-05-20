using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AmmoStorage : MonoBehaviour
{

    public GameObject GUIObject;

    void Start()
    {
        GUIObject.SetActive(false);
    }

    void Update()
    {


    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
            Debug.Log("TRIGGER");
        {
            GUIObject.SetActive(true);

            if (GUIObject.activeInHierarchy == true && Input.GetButtonDown("Use"))
                other.GetComponent<Player>().Ammo = + 540;
        }

    }

    void OnTriggerExit(Collider other)
    {
        GUIObject.SetActive(false);
    }
}
