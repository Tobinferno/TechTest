using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    #region Panels: Action,Move & Messg.
    [SerializeField] private GameObject mainPanel;
    #endregion

    private float playerHealth = 100.0f;
    private float meleeDamage = 35.0f;
    private float rangeDamage = 29.0f;
    public int baseMovs = 3;

    [HideInInspector] public float currentHealthPlayer;
    [HideInInspector] public float currentMeleeDamP;
    [HideInInspector] float currentRangeDamP;
    public int currentMovs;

    [HideInInspector] public float healthBuff = 1f;
    [HideInInspector] public float rangeBuff = 1f;
    [HideInInspector] public float meleeBuff = 1f;
    [HideInInspector] public int movsBuff = 0;

    public bool playerTurn = true;

    [SerializeField] private GameObject enemy;

    private EnemyBehavior enemyBehavior;
    
    private int action;

    #region Movement: Vectors
    private Vector3 mouseWorldPos;
    [SerializeField] private Vector3 vecPlayerToMouse;
    [SerializeField] private Vector3 enemy2Player;
    [SerializeField] private float magnitudDistance;
    #endregion

    private void Start()
    {
        enemyBehavior = enemy.GetComponent<EnemyBehavior>();

        currentHealthPlayer = playerHealth * healthBuff;
        currentMeleeDamP = meleeDamage * meleeBuff;
        currentRangeDamP = rangeDamage * rangeBuff;
        currentMovs = baseMovs + movsBuff;
    }



    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);

        // Transform the mousePos to screen space mouse position
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        ChangeTurn();
        
        //distanceToMouse measure the distance from player to the mouse
        vecPlayerToMouse = mouseWorldPos - transform.position;

        if (playerTurn)
        {
            switch (action)
            {
                case 1:
                    ApplyMelee();
                    break;
                case 2:
                    ApplyMovement();
                    break;
                case 3:
                    ApplyRange();
                    break;
                default:
                    break;
            }
        }

        
    }

    private void ApplyMelee()
    {
        enemy2Player = enemy.transform.position - transform.position;
        magnitudDistance = enemy2Player.magnitude;

        if (currentMovs > 0 && (enemy2Player).magnitude < 2.5f)
        {
            Debug.Log("Hit by" + currentMeleeDamP);
            enemyBehavior.currentHealth -= currentMeleeDamP;
            currentMovs--;
            mainPanel.SetActive(true);
            action = 4;
            return;
        }
        else
        {
            Debug.Log("You are out of range");
            mainPanel.SetActive(true);
            return;
        }
    }

    private void ApplyRange()
    {
        enemy2Player = enemy.transform.position - transform.position;
        magnitudDistance = enemy2Player.magnitude;

        float hitProb = Random.Range(0f, 1f);

        if (hitProb < 0.82f && magnitudDistance > 3.88f && currentMovs > 0)
        {
            Debug.Log("Hit by" + currentRangeDamP);
            enemyBehavior.currentHealth -= currentRangeDamP;
            currentMovs -= 2;
            mainPanel.SetActive(true);
            action = 4;
            return;
        }
        else if(hitProb < 0.18f && magnitudDistance < 3.88f && currentMovs > 0)
        {
            Debug.Log("Hit by" + currentRangeDamP);
            enemyBehavior.currentHealth -= currentRangeDamP;
            currentMovs -= 2;
            mainPanel.SetActive(true);
            action = 4;
            return;
        }
        else
        {
            Debug.Log("You missed!");
            currentMovs -= 2;
            mainPanel.SetActive(true);
            action = 4;
            return;
        }
        
    }

    private void ApplyMovement()
    {
        

        float distanceToMouseX = vecPlayerToMouse.x;
        float distanceToMouseY = vecPlayerToMouse.y;

        if (Input.GetMouseButtonDown(0))
        {
            if(((0.5f < distanceToMouseX && distanceToMouseX < 1.5f) || (-0.5f > distanceToMouseX && distanceToMouseX > -1.5f)) && currentMovs > 0)
            {

                if (((0.73f < distanceToMouseY && distanceToMouseY < 1.73f) || (-0.25f > distanceToMouseY && distanceToMouseY > -1.25f)) && currentMovs > 0)
                {

                    currentMovs -= 1;
                    if (distanceToMouseY > 0)
                    {
                        transform.Translate(0, 1, 0);
                    }
                    else
                    {
                        transform.Translate(0, -1, 0);
                    }
                }

                if (((1.75f < distanceToMouseY && distanceToMouseY < 2.75f) || (-1.25f > distanceToMouseY && distanceToMouseY > -2.25f)) && currentMovs > 0)
                {
                    currentMovs -= 2;

                    if (distanceToMouseY > 0)
                    {
                        transform.Translate(0, 2, 0);
                    }
                    else
                    {
                        transform.Translate(0, -2, 0);
                    }
                }

                currentMovs -= 1;

                if (distanceToMouseX > 0)
                {
                    transform.Translate(1, 0, 0);
                }
                else
                {
                    transform.Translate(-1 , 0, 0);
                }
                mainPanel.SetActive(true);
                action = 4;
                return;
            }
            else if (((1.5f < distanceToMouseX && distanceToMouseX < 2.5f) || (-1.5f > distanceToMouseX && distanceToMouseX > -2.5f)) && currentMovs > 1)
            {
                if (((0.73f < distanceToMouseY && distanceToMouseY < 1.73f) || (-0.25f > distanceToMouseY && distanceToMouseY > -1.25f)) && currentMovs > 0)
                {

                    currentMovs -= 1;
                    if (distanceToMouseY > 0)
                    {
                        transform.Translate(0, 1, 0);
                    }
                    else
                    {
                        transform.Translate(0, -1, 0);
                    }
                }

                currentMovs -= 2;

                if (distanceToMouseX > 0)
                {
                    transform.Translate(2, 0, 0);
                }
                else
                {
                    transform.Translate(-2, 0, 0);
                }

                mainPanel.SetActive(true);
                action = 4;
                return;
            }
            else if ((2.5f < distanceToMouseX || distanceToMouseX < -2.5f) && currentMovs == 3)
            {
                currentMovs -= 3;
                if (distanceToMouseX > 0)
                {
                    transform.Translate(3, 0, 0);
                }
                else
                {
                    transform.Translate(-3, 0, 0);
                }
                mainPanel.SetActive(true);
                action = 4;
                return;
            }

            else if ((3.75f < distanceToMouseY || distanceToMouseY < -3.25f) && currentMovs == 3)
            {
                currentMovs -= 3;
                if (distanceToMouseX > 0)
                {
                    transform.Translate(0, 3, 0);
                }
                else
                {
                    transform.Translate(0, -3, 0);
                }
                mainPanel.SetActive(true);
                action = 4;
                return;
            }

        }

        if (currentMovs == 0)
        {
            mainPanel.SetActive(true);
            return;
        }
    }

    public void Melee()
    {
        action = 1;
        return;
    }

    public void Move()
    {
        action = 2;
        return;
    }

    public void Range()
    {
        action = 3;
        return;
    }

    public void ChangeTurn()
    {
        if(currentMovs > 0)
        {
            return;
        }
        else if(currentMovs == 0)
        {
            playerTurn = false;
            enemyBehavior.enemyTurn = true;
            currentMovs = baseMovs + movsBuff;
            mainPanel.SetActive(false);
        }
    }

}
