using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public float gridSize = 1f; // Size of the grid

    private Rigidbody2D rb;
    private GameObject nearbyItem; 
    private GameObject heldItem;   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (nearbyItem != null && heldItem == null) 
            {
                PickUpItem();
            }
            else if (heldItem != null) 
            {
                DropItem();
            }
        }

    }

    void FixedUpdate()
    {
    }

    void PickUpItem()
    {
        heldItem = nearbyItem; 
        heldItem.transform.SetParent(transform); 
        heldItem.transform.localPosition = new Vector3(0, 0.2f, 0); 
        heldItem.GetComponent<Collider2D>().enabled = false; 
    }

    void DropItem()
    {
        Vector3 dropPosition = SnapToGrid(heldItem.transform.position);
        if (!IsPositionOccupied(dropPosition))
        {
            heldItem.transform.SetParent(null); 
            heldItem.GetComponent<Collider2D>().enabled = true; 
            heldItem.transform.position = dropPosition;
            heldItem = null; 
        }
    }

    Vector3 SnapToGrid(Vector3 position)
    {
        position.x = Mathf.Round(position.x / gridSize) * gridSize;
        position.y = Mathf.Round(position.y / gridSize) * gridSize;
        return position;
    }

    bool IsPositionOccupied(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Item"))
            {
                return true;
            }
        }
        return false;
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


