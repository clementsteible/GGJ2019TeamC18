using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStat : MonoBehaviour
{
    public double fireRate = 1;
    public int damage = 10;
    public float attackLenght = 0.5f;
    public GameObject physicalForm;
    private double nextAttack = 0;
    private bool colliderActive = false;
    private float timer = 0;
    private Vector3 stickRotate;
    private float toAddRotate;
    private bool animLeft = false;
    private float animTimer;

    // Start is called before the first frame update
    void Start()
    {
        stickRotate = physicalForm.transform.rotation.eulerAngles;
        toAddRotate = 90 / (float)fireRate;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (colliderActive) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                colliderActive = false;
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
        if ((Input.GetButton("Fire2") || Input.GetAxis("Fire2") > 0) && Time.time > nextAttack) {
            nextAttack = Time.time + fireRate;
            Attack();
        }
        if (Input.GetButton("Fire2") || Input.GetAxis("Fire2") > 0) {
            Anim();
        }
    }

    void Attack()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
        colliderActive = true;
        timer = attackLenght;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<EnemiScript>().degatsRecus(false, damage);
    }

    void Anim()
    {
        animTimer -= Time.deltaTime;
        if (animTimer <= 0) {
            if (animLeft)
                physicalForm.transform.localRotation = Quaternion.Euler(stickRotate.x, stickRotate.y - 90, stickRotate.z);
            else
                physicalForm.transform.localRotation = Quaternion.Euler(stickRotate.x, stickRotate.y, stickRotate.z);
            animLeft = !animLeft;
            animTimer = (float)fireRate;
        } else {
            if (animLeft)
                physicalForm.transform.Rotate(Vector3.up * toAddRotate * Time.deltaTime * -1);
            else
                physicalForm.transform.Rotate(Vector3.up * toAddRotate * Time.deltaTime);
        }
    }
}
