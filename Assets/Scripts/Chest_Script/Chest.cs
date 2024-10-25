using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("Open", true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("Open", false);
        }
    }
}
