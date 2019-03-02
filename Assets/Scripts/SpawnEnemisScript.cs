using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemisScript : MonoBehaviour
{
    GameObject respawnManager;
    public GameObject prefabEnemi;
    public GameObject prefabBoss;
    public float delaiRespawn;
    public bool isDisponible;

    // Start is called before the first frame update
    void Awake()
    {
        respawnManager = GameObject.Find("RespawnManager");

        delaiRespawn = 1f;

        isDisponible = true;
    }

    public bool PopEnemy()
    {
        if (respawnManager.GetComponent<SpawnEnemisManager>().nbrTotalEnemis < respawnManager.GetComponent<SpawnEnemisManager>().nbrMaxEnemis && isDisponible) {
            if (respawnManager.GetComponent<SpawnEnemisManager>().compteurBossEnStock > 0 && !respawnManager.GetComponent<SpawnEnemisManager>().bossExiste)
            {
                if (respawnManager.GetComponent<SpawnEnemisManager>().compteurBossEnStock != 0)
                {
                    respawnManager.GetComponent<SpawnEnemisManager>().compteurBossEnStock -= 1;
                }
                Instantiate(prefabBoss, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(prefabEnemi, transform.position, Quaternion.identity);               
            }
            ChangeNbrTotalEnemis(1);
            return true;
        } else
            return false;
    }

    /// <summary>
    /// Fait varier le compteur de nbrTotalEnemis
    /// </summary>
    public void ChangeNbrTotalEnemis(int nbr)
    {
        if (nbr > 0)
        {
            respawnManager.GetComponent<SpawnEnemisManager>().nbrTotalEnemis += nbr;
        }
        else if (nbr < 0)
        {
            respawnManager.GetComponent<SpawnEnemisManager>().nbrTotalEnemis -= nbr;
        }
    }


}
