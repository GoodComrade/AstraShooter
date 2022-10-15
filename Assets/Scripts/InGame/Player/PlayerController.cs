using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private PlayerModel model;
    [SerializeField]
    private float thrust = 6f;
    [SerializeField]
    private float rotationSpeed = 180f;
    [SerializeField]
    private float MaxSpeed = 4.5f;
    [SerializeField]
    private int health = 3;

    public Projectile projectile;
    public float firingRate = 0.2f;
    public AudioClip fireSound;

    private int score = 0;
    private Rigidbody2D rb;

    float sceneWidth;
    float sceneHeight;

    float sceneRightEdge;
    float sceneLeftEdge;
    float sceneTopEdge;
    float sceneBottomEdge;


    void Start()
    {
        model = new PlayerModel();
        rb = GetComponent<Rigidbody2D>();
        model.SetCharacterData(thrust, rotationSpeed, MaxSpeed, health, score);

        sceneWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
        sceneHeight = Camera.main.orthographicSize * 2;

        sceneRightEdge = sceneWidth / 2;
        sceneLeftEdge = sceneRightEdge * -1;
        sceneTopEdge = sceneHeight / 2;
        sceneBottomEdge = sceneTopEdge * -1;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            InvokeRepeating("Fire", 0.000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            CancelInvoke("Fire");
        }

        ControlRocket();
        CheckPosition();
    }
    void Fire()
    {
        Projectile beam = Instantiate(projectile, transform.position, transform.rotation);
        beam.Project(transform.up);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    private void ControlRocket()
    {
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * model.playerRotationSpeed * Time.deltaTime);
        rb.AddForce(transform.up * model.playerThrust * Input.GetAxis("Vertical"));
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -model.playerMaxSpeed, model.playerMaxSpeed), Mathf.Clamp(rb.velocity.y, -model.playerMaxSpeed, model.playerMaxSpeed));
    }
    private void CheckPosition()
    {
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
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        
        if (collisionInfo.collider.tag == "Asteroid")
        {
            model.playerHP -= 1;
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0f);
            rb.angularVelocity = 0f;
            Debug.Log(model.playerHP);
            if (model.playerHP <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
