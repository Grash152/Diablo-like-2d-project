using UnityEngine;

public class orbsOfXP : MonoBehaviour
{
    public int xpValue;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            Leveling.GainXP(xpValue);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.position = Vector2.MoveTowards(transform.position, other.transform.position, Time.deltaTime);
        }
    }
}
