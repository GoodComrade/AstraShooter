using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;

    public float size = 1f;
    public float minSize = 0.35f;
    public float maxSize = 1.65f;
    public float movementSpeed = 50f;
    public float maxLifetime = 20f;

    private AsteroidSpawner spawner;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        spawner = FindObjectOfType<AsteroidSpawner>();
        // Присваиваем рандомные значения для придания каждому астероиду уникальности.
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);

        // Задаем значение и массу астероида, основываясь на размере
        // для более реалистичного физического отображения.
        transform.localScale = Vector3.one * size;
        rigidbody.mass = size;

        // Уничтожаем астероид после достижения им максимального времени жизни.
        Destroy(gameObject, maxLifetime);
    }

    public void SetTrajectory(Vector2 direction)
    {
        rigidbody.AddForce(direction * movementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // Проверка на достаточный размер астероида
            // (обе части должны быть больше минимального допустимого размера)
            if ((size * 0.5f) >= minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            // Уничтожаем текущий астероид после создания частей поменьше
            FindObjectOfType<GameController>().AsteroidDestroyed(this);
            spawner.asteroids.Remove(this);
            Destroy(gameObject);
        }
    }

    private Asteroid CreateSplit()
    {
        // Задаем позицию для нового астероида. Такую же как у родителя,
        // но с небольшим офсетом, что бы они не создавались внутри друг-друга.
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        // Создаем новый астероид, в двое еньше родителя.
        Asteroid half = Instantiate(this, position, transform.rotation);
        spawner.asteroids.Add(half);
        half.size = size * 0.5f;

        // Задаем рандомную траекторию.
        half.SetTrajectory(Random.insideUnitCircle.normalized);

        return half;
    }
}
