using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Crop : MonoBehaviour
{
    public Sprite[] growthStages;
    public float growthIntervalMin = 1f;
    public float growthIntervalMax = 3f;
    public int maxGrowthStages = 5;
    public TextMeshProUGUI harvestCountText;
    public List<Policko> plots;

    private int harvestCount = 0;

    void Start()
    {
        foreach (var plot in plots)
        {
            plot.Initialize(this);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (var plot in plots)
            {
                if (plot.playerCollider != null)
                {
                    plot.HarvestCrop();
                    // No break here to allow harvesting multiple crops
                }
            }
        }
    }

    public void UpdateHarvestCount()
    {
        harvestCount++;
        harvestCountText.text = "Harvested: " + harvestCount.ToString();
    }

    public void RestartGrowth(Policko plot)
    {
        StartCoroutine(RestartGrowthCoroutine(plot));
    }

    private IEnumerator RestartGrowthCoroutine(Policko plot)
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        plot.ResetCrop();
        plot.Initialize(this);
    }
}

[System.Serializable]
public class Policko
{
    public GameObject plotObject;
    private Crop crop;
    private int currentStage = 0;
    private bool isGrowing = false;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    public Collider2D playerCollider;

    public void Initialize(Crop crop)
    {
        this.crop = crop;
        spriteRenderer = plotObject.GetComponent<SpriteRenderer>();
        boxCollider = plotObject.GetComponent<BoxCollider2D>();

        if (crop.growthStages.Length > 0)
        {
            crop.StartCoroutine(GrowCrop());
        }
    }

    private IEnumerator GrowCrop()
    {
        while (currentStage < crop.maxGrowthStages)
        {
            float waitTime = Random.Range(crop.growthIntervalMin, crop.growthIntervalMax);
            yield return new WaitForSeconds(waitTime);

            currentStage++;
            UpdateCropVisuals();
        }

        yield return new WaitUntil(() => !isGrowing);
    }

    private void UpdateCropVisuals()
    {
        if (currentStage < crop.growthStages.Length)
        {
            spriteRenderer.sprite = crop.growthStages[currentStage];
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollider = other;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollider = null;
        }
    }

    public void OnDrawGizmosSelected()
    {
        if (boxCollider == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
    }

    public void HarvestCrop()
    {
        if (currentStage >= crop.maxGrowthStages && playerCollider != null)
        {
            float distance = Vector2.Distance(playerCollider.transform.position, plotObject.transform.position);
            if (distance <= 0.5f) // Assuming 1.5 units is the close distance
            {
                currentStage = 0;
                isGrowing = false;

                crop.UpdateHarvestCount();

                // Reset the crop without destroying the plotObject
                spriteRenderer.sprite = null; // Clear the sprite to simulate harvesting
                crop.RestartGrowth(this);
            }
        }
    }

    public void ResetCrop()
    {
        currentStage = 0;
        isGrowing = false;
        spriteRenderer.sprite = null;
    }
}