using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePoussin : MonoBehaviour
{
    public GameObject dialoguePoussin;

    // Update is called once per frame
    void Update()
    {
        if (dialoguePoussin.activeSelf)
        {
            if (Input.GetButtonDown("Use"))
            {
                dialoguePoussin.SetActive(false);
            }
        }
    }
}

