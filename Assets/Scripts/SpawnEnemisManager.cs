using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemisManager : MonoBehaviour
{
    /// <summary>
    /// Si un boss est sur la map
    /// </summary>
    public bool bossExiste = false;

    /// <summary>
    /// Nombre de compteur de boss en stock
    /// </summary>
    public int compteurBossEnStock = 0;

    /// <summary>
    /// Définit le nombre d'ennemis à tuer qu'il faut pour pop un boss
    /// </summary>
    public int frequenceboss = 7;

    /// <summary>
    /// Nombre total d'enemis répartis sur l'ensemble de la map
    /// </summary>
    public int nbrTotalEnemis;
    
    /// <summary>
    /// Nombre maximum d'enemis qui peuvent être sur la map en simultané
    /// </summary>
    public int nbrMaxEnemis;

    /// <summary>
    /// Définit si le respawn est possible
    /// </summary>
    public bool respawnIsPossible = true;

    /// <summary>
    /// Nombre d'ennemis tués par le joueur
    /// </summary>
    public int compteurEnnemisTues = 0;

    /// <summary>
    /// Nombre de Boss tués par le joueur
    /// </summary>
    public int compteurBossTues = 0;

    /// <summary>
    /// Compteur Multiplicateur qui augmente de 1 tous les X ennemis tués
    /// </summary>
    public int compteurMultiplicateurStats = 0;

    /// <summary>
    /// Détermine le palier du nombre de tués qu'il faut pour incrémenter le compteurMultiplicateurStats de 1
    /// </summary>
    public int seuilMultiplicateur;

    /// <summary>
    /// Détermine la puissance du facteur multiplicateur
    /// </summary>
    public float forceMultiplicateur = 0.1f;

    /// <summary>
    /// Points de vie de départ des ennemis
    /// </summary>
    public float healthOrigine = 100;

    /// <summary>
    /// Force de départ des ennemis
    /// </summary>
    public float forceOrigine = 10;

    /// <summary>
    /// Sert de standard pour instancier les points de vie de départ des ennemis
    /// </summary>
    public float healthStandard;

    /// <summary>
    /// Force de départ des ennemis
    /// </summary>
    public float forceStandard;

    /// <summary>
    /// Points de stamina de départ des ennemis
    /// </summary>
    public float staminaStandard;

    public float delaiRespawn = 1f;
    public GameObject[] spawner;


    void Awake()
    {

        forceStandard = forceOrigine;
        healthStandard = healthOrigine;
        Random.InitState((int)System.DateTime.Now.Ticks);
        InvokeRepeating("ChooseSpawner", 0, delaiRespawn);
    }

    void ChooseSpawner()
    {
        if (nbrTotalEnemis < nbrMaxEnemis)
            spawner[(int)Random.Range(0, spawner.Length)].GetComponent<SpawnEnemisScript>().PopEnemy();
    }

    /// <summary>
    /// Fonction appellée par chaque ennemi qui meurt, on applique le nouveau coefficient aux points de vie standard et à la force standard
    /// </summary>
    public void CalculNouveauMultiplicateur()
    {
        compteurMultiplicateurStats = compteurEnnemisTues / seuilMultiplicateur;

        if (compteurMultiplicateurStats>0)
        {
            forceStandard = forceOrigine + (forceOrigine * compteurMultiplicateurStats * forceMultiplicateur);
            healthStandard = healthOrigine + (healthOrigine * compteurMultiplicateurStats * forceMultiplicateur);
        }
    }
}
