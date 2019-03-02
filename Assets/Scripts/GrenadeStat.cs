using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeStat : MonoBehaviour
{
    public GameObject explodeZone;
    public bool isLethal = true;
    [HideInInspector]
    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        explodeZone.GetComponent<ExplodeZone>().isLethal = isLethal;
        explodeZone.GetComponent<ExplodeZone>().damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody explode = Instantiate(explodeZone.GetComponent<Rigidbody>(), transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
