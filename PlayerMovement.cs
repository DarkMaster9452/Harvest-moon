using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject Manager;
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    ///Pickup jablka
    private void OnTriggerEnter2D(Collider2D other)
    {
        var manager = Manager.GetComponent<FruitSpawner>();
        if (other.CompareTag("Apple"))
        {
            Destroy(other.gameObject);
            manager.spawnute = false;
            print("dosdosaff");
        }
    }


}
