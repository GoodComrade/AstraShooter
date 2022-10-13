using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject rock;
    private float maxRotation;
 
    private float rotationZ;
    private Rigidbody2D rb;
    private float maxSpeed;
    private int _generation;

    void Start()
    {
        maxRotation = 25f;
        rotationZ = Random.Range(-maxRotation, maxRotation);

        rb = rock.GetComponent<Rigidbody2D>();

        float speedX = Random.Range(200f, 800f);
        int selectorX = Random.Range(0, 2);
        float dirX = 0;
        if (selectorX == 1) 
            dirX = -1;
        else 
            dirX = 1;
        float finalSpeedX = speedX * dirX;
        rb.AddForce(transform.right * finalSpeedX);

        float speedY = Random.Range(200f, 800f);
        int selectorY = Random.Range(0, 2);
        float dirY = 0;
        if (selectorY == 1) 
            dirY = -1;
        else 
            dirY = 1;
        float finalSpeedY = speedY * dirY;
        rb.AddForce(transform.up * finalSpeedY);

    }

    void Update()
    {
        rock.transform.Rotate(new Vector3(0, 0, rotationZ) * Time.deltaTime);
        CheckPosition();
        float dynamicMaxSpeed = 3f;
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -dynamicMaxSpeed, dynamicMaxSpeed), Mathf.Clamp(rb.velocity.y, -dynamicMaxSpeed, dynamicMaxSpeed));
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.name == "PlayerLaser(Clone)")
        {
            if (_generation < 3)
            {
                CreateSmallAsteriods(2);
            }
            collisionInfo.gameObject.GetComponent<Projectile>().Hit();
            Destroy(gameObject);
        }
    }

    void CreateSmallAsteriods(int asteroidsNum)
    {
        int newGeneration = _generation + 1;
        for (int i = 1; i <= asteroidsNum; i++)
        {
            float scaleSize = 0.5f;
            GameObject AsteroidClone = Instantiate(rock, new Vector3(transform.position.x, transform.position.y, 0f), transform.rotation);
            AsteroidClone.transform.localScale = new Vector3(AsteroidClone.transform.localScale.x * scaleSize, AsteroidClone.transform.localScale.y * scaleSize, AsteroidClone.transform.localScale.z * scaleSize);
            AsteroidClone.GetComponent<AsteroidController>().SetGeneration(newGeneration);
            AsteroidClone.SetActive(true);
        }
    }
    public void SetGeneration(int generation)
    {
        _generation = generation;
    }
    private void CheckPosition()
    {

        float sceneWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
        float sceneHeight = Camera.main.orthographicSize * 2;
        float sceneRightEdge = sceneWidth / 2;
        float sceneLeftEdge = sceneRightEdge * -1;
        float sceneTopEdge = sceneHeight / 2;
        float sceneBottomEdge = sceneTopEdge * -1;

        float rockOffset = 1.0f;
        if (rock.transform.position.x > sceneRightEdge + rockOffset)
        {
            rock.transform.position = new Vector2(sceneLeftEdge - rockOffset, rock.transform.position.y);
        }

        if (rock.transform.position.x < sceneLeftEdge - rockOffset)
        {
            rock.transform.position = new Vector2(sceneRightEdge + rockOffset, rock.transform.position.y);
        }
        if (rock.transform.position.y > sceneTopEdge + rockOffset)
        {
            rock.transform.position = new Vector2(rock.transform.position.x, sceneBottomEdge - rockOffset);
        }

        if (rock.transform.position.y < sceneBottomEdge - rockOffset)
        {
            rock.transform.position = new Vector2(rock.transform.position.x, sceneTopEdge + rockOffset);
        }
    }
}

