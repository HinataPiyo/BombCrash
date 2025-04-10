using UnityEngine;

public class DropScrap : MonoBehaviour
{
    EnemySO enemySO;
    [SerializeField] float strength;

    void Start()
    {
        enemySO = GetComponent<EnemyStatus>().EnemySO;
    }

    public void SpawnScrap()
    {
        Range rangeX = new Range { min = -1.5f, max = 1.5f };
        Range rangeY = new Range { min = -1.5f, max = 1.5f };
        for(int ii = 0; ii < enemySO.DropScrapAmount; ii++)
        {
            GameObject scrap = Instantiate(enemySO.Scrap_Prefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = scrap.GetComponent<Rigidbody2D>();

            Vector3 randomDir = new Vector2(Random.Range(rangeX.min, rangeX.max), Random.Range(rangeY.min, rangeY.max));
            rb.AddForce(randomDir * strength, ForceMode2D.Impulse);
        }
    }
}
