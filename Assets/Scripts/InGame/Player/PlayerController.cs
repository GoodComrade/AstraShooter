using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float thrust = 6f;
    private float rotationSpeed = 180f;
    private float MaxSpeed = 4.5f;
    public float padding = 0;
    public GameObject projectile;
    public float projectileSpeed;
    public float firingRate = 0.2f;
    public int health = 3;

    public AudioClip fireSound;

    private Rigidbody2D rb;

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject missile = collider.gameObject;
        if (missile.tag == "Asteroid")
        {
            health -= 1;
            //TODO: Implement AsteroidController with this function
            //missile.GetComponent<AsteroidController>().Hit();
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        //LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        //man.LoadLevel("Win Screen");
        Destroy(gameObject);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Fire()
    {
        GameObject beam = Instantiate(projectile, transform.GetChild(0).position, transform.rotation) as GameObject;
        beam.GetComponent<Rigidbody2D>().AddForce(transform.up * projectileSpeed);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

        ControlRocket();
        CheckPosition();
    }
    private void ControlRocket()
    {
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        rb.AddForce(transform.up * thrust * Input.GetAxis("Vertical"));
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -MaxSpeed, MaxSpeed), Mathf.Clamp(rb.velocity.y, -MaxSpeed, MaxSpeed));
    }
    private void CheckPosition()
    {

        float sceneWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
        float sceneHeight = Camera.main.orthographicSize * 2;

        float sceneRightEdge = sceneWidth / 2;
        float sceneLeftEdge = sceneRightEdge * -1;
        float sceneTopEdge = sceneHeight / 2;
        float sceneBottomEdge = sceneTopEdge * -1;

        if (transform.position.x > sceneRightEdge)
        {
            transform.position = new Vector2(sceneLeftEdge, transform.position.y);
        }
        if (transform.position.x < sceneLeftEdge) 
        { 
            transform.position = new Vector2(sceneRightEdge, transform.position.y); 
        }
        if (transform.position.y > sceneTopEdge)
        {
            transform.position = new Vector2(transform.position.x, sceneBottomEdge);
        }
        if (transform.position.y < sceneBottomEdge)
        {
            transform.position = new Vector2(transform.position.x, sceneTopEdge);
        }
    }
}
