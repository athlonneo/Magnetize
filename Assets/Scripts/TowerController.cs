using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    public PlayerController player;
    private Rigidbody2D playerRb2D;
    public float pullForce = 100f;
    public float rotateSpeed = 360f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb2D = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        Vector3 pullDirection = (transform.position - player.transform.position).normalized;
        float newPullForce = Mathf.Clamp(pullForce / distance, 30, 100);
        playerRb2D.AddForce(pullDirection * newPullForce);
        playerRb2D.angularVelocity = -rotateSpeed / distance;
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void OnMouseUp()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        playerRb2D.angularVelocity = 0;
    }
}
