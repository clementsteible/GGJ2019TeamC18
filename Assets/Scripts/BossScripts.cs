using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScripts : MonoBehaviour
{
    /// <summary>
    /// Points de vie en cours d'un enemi
    /// </summary>
    public int health = 400;

    /// <summary>
    /// Points d'attaque d'un E.T.
    /// </summary>
    public int force = 10;

    /// <summary>
    /// Vitesse de base de l'ennemi
    /// </summary>
    public float vitesseOrigine;

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
        respawnManager = GameObject.FindGameObjectWithTag("RespawnManager");
        
        respawnManager.GetComponent<SpawnEnemisManager>().bossExiste = true;

        health += health * respawnManager.GetComponent<SpawnEnemisManager>().compteurBossTues;

        GameObject.FindGameObjectWithTag("Musique_Boss").GetComponent<AudioSource>().mute = false;
        GameObject.FindGameObjectWithTag("Musique_Theme").GetComponent<AudioSource>().mute = true;

        Debug.Log("BOSS ARRIVE");
        Debug.Log("MES PV SONT A : " + health);
    }

    public void Death()
    {
        Debug.Log(" BOSS MORT !");
        respawnManager.GetComponent<SpawnEnemisManager>().compteurBossTues += 1;
        respawnManager.GetComponent<SpawnEnemisManager>().nbrTotalEnemis -= 1;
        respawnManager.GetComponent<SpawnEnemisManager>().CalculNouveauMultiplicateur();
        respawnManager.GetComponent<SpawnEnemisManager>().bossExiste = false;
        GameObject.FindGameObjectWithTag("Musique_Boss").GetComponent<AudioSource>().mute = true;
        GameObject.FindGameObjectWithTag("Musique_Theme").GetComponent<AudioSource>().mute = false;
        GameObject.Find("LethalWeapon").GetComponent<WeaponStat>().damage += 20;
        Destroy(gameObject);
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

    }
    
}
