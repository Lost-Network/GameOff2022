using Photon.Pun;
using UnityEngine;

public class EnemyMovementRandomCompassDirection : MonoBehaviour
{
    [SerializeField]
    private float speed = 2;
    private float timer = 0f;
    private float timerCap = 3f;
    private int chosenDirection = 0;
    private bool waitFlip = false;

    private void FixedUpdate()
    {
        //if (!PhotonNetwork.IsMasterClient)
        //{
        //    return;
        //}
        if (timer < timerCap)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            if(waitFlip == false)
            {
                //Every other movement will have the enemy wait
                chosenDirection = 4;
                waitFlip = true;
            }
            else
            {
                //Every other movement will have the enemy moving in a random compass direction
                chosenDirection = Random.Range(0, 4);
                waitFlip = false;
            }
        }
        switch (chosenDirection)
        {
            case 0:
                this.transform.Translate(Vector2.up * speed * Time.deltaTime);
                GetComponent<EnemyStats>().combatState = 1;
                break;

            case 1:
                this.transform.Translate(Vector2.down * speed * Time.deltaTime);
                GetComponent<EnemyStats>().combatState = 1;
                break;

            case 2:
                this.transform.Translate(Vector2.right * speed * Time.deltaTime);
                GetComponent<EnemyStats>().combatState = 1;
                break;

            case 3:
                this.transform.Translate(Vector2.left * speed * Time.deltaTime);
                GetComponent<EnemyStats>().combatState = 1;
                break;
            case 4:
                GetComponent<EnemyStats>().combatState = 2;
                break;
            default:
                GetComponent<EnemyStats>().combatState = 2;
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        timer = 0;
        chosenDirection = Random.Range(0, 4);
    }
}