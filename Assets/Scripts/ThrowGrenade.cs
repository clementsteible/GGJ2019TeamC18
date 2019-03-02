using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    public GameObject grenadeLethal;
    public GameObject grenadeNotLethal;
    public GameObject cheese;
    public GameObject checkCheese;
    public double grenadeRate = 5;
    public float grenadeSpeed = 100;
    public int grenadeDamage = 10;
    public double cheeseRate = 30;
    private double nextGrenade = 0;
    private double nextCheese = 0;
    private float timer = 0;
    private bool timerActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("GrenadeLethal") && Time.time > nextGrenade) {
            nextGrenade = Time.time + grenadeRate;
            grenadeLethal.GetComponent<GrenadeStat>().damage = grenadeDamage;
            Rigidbody grenade = Instantiate(grenadeLethal.GetComponent<Rigidbody>(), transform.position, transform.rotation);
            grenade.velocity = transform.TransformDirection((Vector3.left * grenadeSpeed) + (Vector3.up * (grenadeSpeed / 2)));
        }
        if (Input.GetButton("GrenadeNotLethal") && Time.time > nextGrenade) {
            nextGrenade = Time.time + grenadeRate;
            grenadeNotLethal.GetComponent<GrenadeStat>().damage = grenadeDamage;
            Rigidbody grenade = Instantiate(grenadeNotLethal.GetComponent<Rigidbody>(), transform.position, transform.rotation);
            grenade.velocity = transform.TransformDirection((Vector3.left * grenadeSpeed) + (Vector3.up * (grenadeSpeed / 2)));
        }
        if (Input.GetButton("Cheese") && Time.time > nextCheese) {
            nextCheese = Time.time + cheeseRate;
            Rigidbody cheeze = Instantiate(cheese.GetComponent<Rigidbody>(), transform.position, transform.rotation);
            checkCheese.GetComponent<FromageBool>().fromageExiste = true;
            timer = 20;
            timerActive = true;
        }
        if (timerActive) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                checkCheese.GetComponent<FromageBool>().fromageExiste = false;
                timerActive = false;
            }
        }
    }

    public void GetNewCheese()
    {
        nextCheese = 0;
    }
}
