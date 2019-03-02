using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiScript : MonoBehaviour
{
    /// <summary>
    /// Etat de stun de l'enemi
    /// </summary>
    public bool stun = false;

    /// <summary>
    /// Points de vie en cours d'un enemi
    /// </summary>
    public int health;

    /// <summary>
    /// Points d'attaque d'un E.T.
    /// </summary>
    public int force;

    /// <summary>
    /// Points de stamina d'un E.T.
    /// </summary>
    public int stamina;

    /// <summary>
    /// Vitesse de base de l'ennemi
    /// </summary>
    public float vitesseLimiteBasse = 150f;
    
    /// <summary>
    /// Vitesse de base de l'ennemi
    /// </summary>
    public float vitesseLimiteHaute = 300f;

    /// <summary>
    /// Vitesse de base de l'ennemi
    /// </summary>
    public float vitesseOrigine;

    /// <summary>
    /// GameObject contenant le temps de stun des ennemis
    /// </summary>
    public GameObject niveauStun;

    /// <summary>
    /// Cherche le respawnManager
    /// </summary>
    public GameObject respawnManager;

    /// <summary>
    /// Cooldown entre 2 attaques en secondes
    /// </summary>
    public float cooldownAttaque;


    // Start is called before the first frame update
    void Awake()
    {
        niveauStun = GameObject.FindGameObjectWithTag("TempsStun");

        respawnManager = GameObject.FindGameObjectWithTag("RespawnManager");

        stamina = (int) respawnManager.GetComponent<SpawnEnemisManager>().staminaStandard;
        health = (int) respawnManager.GetComponent<SpawnEnemisManager>().healthStandard;
        force = (int) respawnManager.GetComponent<SpawnEnemisManager>().forceStandard;

        vitesseOrigine = Random.Range(vitesseLimiteBasse, vitesseLimiteHaute);
    }

    public void Death()
    {
        Debug.Log("Mort !");
        respawnManager.GetComponent<SpawnEnemisManager>().compteurEnnemisTues += 1;
        respawnManager.GetComponent<SpawnEnemisManager>().nbrTotalEnemis -= 1;
        respawnManager.GetComponent<SpawnEnemisManager>().CalculNouveauMultiplicateur();
        CancelInvoke("AttaqueMaison");
        Debug.Log("Je suis mort donc j'arrête d'attaquer la maison !");
        if (respawnManager.GetComponent<SpawnEnemisManager>().compteurEnnemisTues % respawnManager.GetComponent<SpawnEnemisManager>().frequenceboss == 0 && respawnManager.GetComponent<SpawnEnemisManager>().compteurBossEnStock == 0 && !respawnManager.GetComponent<SpawnEnemisManager>().bossExiste)
        {
            respawnManager.GetComponent<SpawnEnemisManager>().compteurBossEnStock += 1;
        }
        Destroy(gameObject);
    }

    public void Stun()
    {
        Debug.Log("Stun !");
        stun = true;

        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;

        gameObject.GetComponent<NPCController>().agentSpeed = 0;

        Invoke("Unstun", niveauStun.GetComponent<StunTime>().tempsStunEnSecondes);
    }

    private void Unstun()
    {
        Debug.Log("Unstun !");
        stun = false;

        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;

        stamina = (int) respawnManager.GetComponent<SpawnEnemisManager>().staminaStandard;
        gameObject.GetComponent<NPCController>().agentSpeed = vitesseOrigine;
    }


    /// <summary>
    /// Degats recus par l'ennemi
    /// </summary>
    /// <param name="isLetal">True si l'arme est létale, False si non-létale</param>
    /// <param name="nbrDegats">Nombre de dégats infligés</param>
    public void degatsRecus(bool isLetal, int nbrDegats)
    {
        if (isLetal)
        {
            health -= nbrDegats;
            if (health <= 0)
            {
                Death();
            }
        }
        else
        {
            stamina -= nbrDegats;
            if (stamina <= 0)
            {
                Stun();
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Maison")
        {
            Debug.Log("J'attaque la " + collision.gameObject.name + " !");
            InvokeRepeating("AttaqueMaison", 0, cooldownAttaque);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        CancelInvoke("AttaqueMaison");
        Debug.Log("J'arrête d'attaquer la " + collision.gameObject.name + " !");
    }

    private void AttaqueMaison()
    {
        if (!stun)
        {
            GameObject.FindGameObjectWithTag("Maison").GetComponent<Maison>().degatsRecus(force);
            //Debug.Log("Points de vie maison : " + GameObject.FindGameObjectWithTag("Maison").GetComponent<Maison>().pointsDeVie);
        }
        
    }


}
