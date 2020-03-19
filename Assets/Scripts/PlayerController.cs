using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float moveSpeed = 5f;

    public float pullForce = 100f;
    public float rotateSpeed = 360f;
    private GameObject closestTower;
    private GameObject hookedTower;
    private bool isPulled = false;

    private UIController uiControl;

    private AudioSource myAudio;
    private bool isCrashed = false;

    public Vector3 startPosition;

    void Start()
    {
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
        myAudio = this.gameObject.GetComponent<AudioSource>();
        uiControl = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    void Update()
    {
        if (isCrashed)
        {
            if (!myAudio.isPlaying)
            {
                //Restart scene
            }
        }
        else
        {
            rb2D.velocity = -transform.up * moveSpeed;
            //rb2D.angularVelocity = 0f;
        }

        if (Input.GetKey(KeyCode.Z) && !isPulled)
        {
            if (closestTower != null && hookedTower == null)
            {
                hookedTower = closestTower;
            }
            if (hookedTower)
            {
                float distance = Vector2.Distance(transform.position, hookedTower.transform.position);
                Vector3 pullDirection = (hookedTower.transform.position - transform.position).normalized;
                float newPullForce = Mathf.Clamp(pullForce / distance, 30, 100);
                rb2D.AddForce(pullDirection * newPullForce);
                //Debug.Log(pullDirection+" "+newPullForce);
                rb2D.angularVelocity = -rotateSpeed / distance;
                isPulled = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            rb2D.angularVelocity = 0;
            isPulled = false;
        }


    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            myAudio.Play();
            rb2D.velocity = new Vector3(0f, 0f, 0f);
            rb2D.angularVelocity = 0f;
            isCrashed = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            //Debug.Log("Levelclear!");
            uiControl.endGame();
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tower")
        {
            closestTower = collision.gameObject;
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        //if (isPulled) return;

        if (collision.gameObject.tag == "Tower")
        {
            closestTower = null;
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void restartPosition()
    {
        this.transform.position = startPosition;

        this.transform.rotation = Quaternion.Euler(0f, 0f, 90f);

        isCrashed = false;

        if (closestTower)
        {
            closestTower.GetComponent<SpriteRenderer>().color = Color.white;
            closestTower = null;
        }

    }
}
