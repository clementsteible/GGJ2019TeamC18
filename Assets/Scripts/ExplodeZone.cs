using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeZone : MonoBehaviour
{
    [HideInInspector]
    public bool isLethal = true;
    [HideInInspector]
    public int damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<EnemiScript>().degatsRecus(isLethal, damage);
    }
}
