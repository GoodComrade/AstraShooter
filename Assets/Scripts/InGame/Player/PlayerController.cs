using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float thrust = 2f;
    [SerializeField]
    private float rotationSpeed = 180f;
    [SerializeField]
    private float MaxSpeed = 4.5f;

    public Projectile projectile;
    public float firingRate = 0.2f;
    public AudioClip fireSound;
    private Rigidbody2D rb;

    float sceneWidth;
    float sceneHeight;

    float sceneRightEdge;
    float sceneLeftEdge;
    float sceneTopEdge;
    float sceneBottomEdge;

    bool IsInvul = false;
    float invulTime = 3f;

    private void Awake()
    {
        gameObject.SetActive(true);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        sceneWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
        sceneHeight = Camera.main.orthographicSize * 2;

        sceneRightEdge = sceneWidth / 2;
        sceneLeftEdge = sceneRightEdge * -1;
        sceneTopEdge = sceneHeight / 2;
        sceneBottomEdge = sceneTopEdge * -1;
    }
    void Update()
    {
        // Управление стрельбой
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            InvokeRepeating(nameof(Fire), 0.000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            CancleFire();
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
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        rb.AddForce(transform.up * thrust * Input.GetAxis("Vertical"));
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -MaxSpeed, MaxSpeed), Mathf.Clamp(rb.velocity.y, -MaxSpeed, MaxSpeed));
    }
    private void CheckPosition()
    {
        // В этом методе происходит проверка на вылет за край экрана
        // и возвращение обратно на экран
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
        
        if (collisionInfo.collider.tag == "Asteroid" && !IsInvul)
        {
            // При столкновении с астероидом вызывается метод игрового контролера для пересчета оставшихся жизней.
            // Затем на время включается неуязвимость к столкновениям, что бы у игрока была возможность отлететь от точки контакта.
            FindObjectOfType<GameController>().PlayerTakeDamage(this);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;
            rb.angularDrag = 0f;
            StartCoroutine(InvulTimer());
        }
    }

    public void CancleFire()
    {
        CancelInvoke(nameof(Fire));
    }

    // Костыль, добавленный в последние мгновения
    // Без него слишком быстро кончаются жизни.
    //(С ним в редакторе выдает ошибку т.к. корутина вызывается даже после перехода на фазу конца уровня, когда кораблик не активен)
    IEnumerator InvulTimer()
    {
        IsInvul = true;
        yield return new WaitForSeconds(invulTime);
        IsInvul = false;
    }
}
