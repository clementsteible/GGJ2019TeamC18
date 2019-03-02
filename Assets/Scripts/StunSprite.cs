using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunSprite : MonoBehaviour
{
    public int nbrFramesTilt;

    public int compteur = 0;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        compteur++;

        if(compteur > nbrFramesTilt)
        {
            if(!gameObject.GetComponent<SpriteRenderer>().flipX)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }

            compteur = 0;
        }
    }
}
