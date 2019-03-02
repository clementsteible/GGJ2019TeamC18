using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    public float lifeTime = 10;
    [HideInInspector]
    public int damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        else
            Destroy(gameObject);
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<EnemiScript>().degatsRecus(true, damage);
        else if (collision.gameObject.tag == "Boss")
            collision.gameObject.GetComponent<BossScripts>().degatsRecus(true, damage);
    }
}
