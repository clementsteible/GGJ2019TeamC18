using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maison : MonoBehaviour
{
    /// <summary>
    /// Points de vie de la maison
    /// </summary>
    public int pointsDeVie;
   
    /// <summary>
    /// Compteur des poussins dans la maison
    /// </summary>
    public int compteurPoussinsDansMaison;

    /// <summary>
    /// Si la maison n'a pas subit de dégats
    /// </summary>
    public bool intacte = true;

    /// <summary>
    /// GameObject qui émet des particules de fumées quand la maison recoit les premiers dégats
    /// </summary>
    public GameObject particuleFumee;

    private void Awake()
    {
        compteurPoussinsDansMaison = 0;
    }

    public void degatsRecus(int nbrDegats)
    {
        pointsDeVie -= nbrDegats;

        Debug.Log("PV restants : " + pointsDeVie);

        if (pointsDeVie < 0)
        {
            Debug.Log("Perdu !");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Poussin")
        {
            Debug.Log("Je suis un " + collision.gameObject.name + " et je rentre à ma maison !");
            compteurPoussinsDansMaison += 1;
            Debug.Log("Compteur : " + compteurPoussinsDansMaison);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Enemy" && intacte)
        {
            intacte = false;
            particuleFumee.GetComponent<ParticleSystem>().enableEmission = true;
        }
        
    }
    

    void Update()
    {
        if (compteurPoussinsDansMaison >= 5)
        {
            if (GameObject.Find("RespawnManager").GetComponent<SpawnEnemisManager>().compteurEnnemisTues == 0)
            {
                Application.LoadLevel(3);
            }
            else
            {
                Application.LoadLevel(4);
            }

        }
        else if (pointsDeVie < 0)
        {
            Application.LoadLevel(5);
        }
    }

}
