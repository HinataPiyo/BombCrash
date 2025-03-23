using UnityEngine;

public class DropScrap : MonoBehaviour
{
    [SerializeField] EnemySO enemySO;
    [SerializeField] GameObject scrap_Prefab;
    [SerializeField] float strength;
    
    public void SpawnScrap()
    {
        Range rangeX = new Range { min = -1.5f, max = 1.5f };
        Range rangeY = new Range { min = -1.5f, max = 1.5f };
        for(int ii = 0; ii < enemySO.DropScrapAmount; ii++)
        {
            GameObject scrap = Instantiate(scrap_Prefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = scrap.GetComponent<Rigidbody2D>();

            Vector3 randomDir = new Vector2(Random.Range(rangeX.min, rangeX.max), Random.Range(rangeY.min, rangeY.max));
            rb.AddForce(randomDir * strength, ForceMode2D.Impulse);
        }
    }
}
