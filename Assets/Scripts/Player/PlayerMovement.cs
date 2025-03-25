using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Range range = new Range { min = -5f, max = 5f };

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Transform pos = transform;

        pos.position += new Vector3(horizontal, 0) * PlayerStatusSO.MoveSpeed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(pos.position.x, range.min, range.max), pos.position.y, pos.position.z);
    }
}
