using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private float baseEnemyHealth = 100.0f;
    private int baseEnemyMovs = 3;
    private float baseEnemyMelee = 25.0f;
    private float baseEnemyRange = 18.0f;

    public GameObject player;
    [SerializeField] private GameObject mainPanel;

    private PlayerMovements playerMovement;

    public float meleeBuff = 1f;
    public float rangeBuff = 1f;
    public float healthBuff = 1f;
    public int movsBuff = 0;

    public bool enemyTurn = false;

    public float currentHealth;
    public float currentMeleeDam;
    public float currentRangeDam;
    public int currentEnemyMovs;

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovements>();
    }

    private void Awake()
    {
        currentHealth = baseEnemyHealth * healthBuff;
        currentMeleeDam = baseEnemyMelee * meleeBuff;
        currentRangeDam = baseEnemyRange * rangeBuff;
        currentEnemyMovs = baseEnemyMovs + movsBuff;
    }

    private void Update()
    {
        int randomAction = Random.Range(1, 4);

        ChangeTurn();
        if(enemyTurn)
        {
            switch (randomAction)
            {
                case 1:
                    Debug.Log("Melee attk selected");
                    EnemyMelee();
                    break;
                case 2:
                    EnemyMovement();
                    break;
                case 3:
                    EnemyRange();
                    break;
            }
        }
        
    }


    private void EnemyMovement()
    {
        Vector3 enemy2Player = transform.position - player.transform.position;
        int direction = Random.Range(1, 3);

        if (currentEnemyMovs > 0 && (enemy2Player).magnitude > 2.8f)
        {
            if (direction == 1)
            {
                currentEnemyMovs -= 1;
                if (enemy2Player.x < 0)
                {
                    transform.Translate(-1, 0, 0);
                    return;
                }
                else
                {
                    transform.Translate(1, 0, 0);
                    return;
                }
                
            }
            else
            {
                currentEnemyMovs -= 1;

                if(enemy2Player.y < 0)
                {
                    transform.Translate(0, -1, 0);
                    return;
                }
                else
                {
                    transform.Translate(0, 1, 0);
                    return;
                }
            }
        }
        else if (enemy2Player.magnitude < 2.5f)
        {
            EnemyMelee();
        }
        else
        {
            return;
        }
    }

    private void EnemyMelee()
    {
        Vector3 enemy2Player = transform.position - player.transform.position;

        float enemyHit = Random.Range(0f, 1f);

        if (currentEnemyMovs > 0 && enemy2Player.magnitude < 2.5f && enemyHit > 0.3f)
        {
            playerMovement.currentHealthPlayer -= currentMeleeDam;
            currentEnemyMovs -= 1;
            return;
        }
        else if(currentEnemyMovs > 0 && enemy2Player.magnitude > 2.5f)
        {
            Debug.Log("Enemy far, let's shoot him!");
            EnemyRange();
        }
        else
        {
            currentEnemyMovs -= 1;
            return;
        }
    }

    private void EnemyRange()
    {
        Vector3 enemy2Player = transform.position - player.transform.position;

        float enemyHit = Random.Range(0f, 1f);

        if (currentEnemyMovs > 0 && enemy2Player.magnitude > 3.88f && enemyHit > 0.2f)
        {
            playerMovement.currentHealthPlayer -= currentRangeDam;
            currentEnemyMovs -= 1;
            return;
        }
        else if(currentEnemyMovs > 0 && enemy2Player.magnitude < 3.88f && enemyHit < 0.2f)
        {
            playerMovement.currentHealthPlayer -= currentRangeDam;
            currentEnemyMovs -= 1;
            return;
        }
    }

    private void ChangeTurn()
    {
        if(currentEnemyMovs == 0)
        {
            enemyTurn = false;
            playerMovement.playerTurn = true;
            currentEnemyMovs = baseEnemyMovs + movsBuff;
            mainPanel.SetActive(true);
        }
        else
        {
            return;
        }
    }
}
