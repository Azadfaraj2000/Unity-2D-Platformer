using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameskull : MonoBehaviour
{
    Animator animator;
    public float speed;
    public float explodeDistance = 0.5f;
    private GameObject player;
    private bool chase = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        if (Vector2.Distance(transform.position, player.transform.position) < explodeDistance)
        {
            gameObject.GetComponent<SimpleFlash>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            chase = false;

            animator.SetBool("Explode", true);
        }
        if (chase == true){
            Chase();
            CheckFlip();
        }

    }
    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    private void CheckFlip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

}
