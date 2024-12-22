using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; 

    private Rigidbody2D rb;
    private Vector2 movement;
    private GameObject nearbyItem; 
    private GameObject heldItem;   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

       
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (nearbyItem != null && heldItem == null) 
            {
                PickUpItem();
            }
        }

        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (heldItem != null) 
            {
                DropItem();
            }
        }
    }

    void FixedUpdate()
    {
        
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void PickUpItem()
    {
        heldItem = nearbyItem; 
        heldItem.transform.SetParent(transform); 
        heldItem.transform.localPosition = new Vector3(0, 1, 0); 
        heldItem.GetComponent<Collider2D>().enabled = false; 
    }

    void DropItem()
    {
        heldItem.transform.SetParent(null); 
        heldItem.GetComponent<Collider2D>().enabled = true; 
        heldItem = null; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item")) 
        {
            nearbyItem = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Item")) 
        {
            if (nearbyItem == collision.gameObject)
            {
                nearbyItem = null;
            }
        }
    }
}


