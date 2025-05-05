using UnityEngine;

public class LevelUp : MonoBehaviour
{
    Vector3 initialLocalPosition;
    float initialOffset;

    [SerializeField] float frequency = 1f;
    [SerializeField] float amplitude = 0.5f;
    [SerializeField] Vector3 direction = Vector3.up;

    void Start()
    {
        initialLocalPosition = transform.localPosition; // Lưu localPosition
        initialOffset = Random.Range(0f, frequency);
    }

    void Update()
    {
        float offset = Mathf.Sin((Time.time + initialOffset) * frequency) * amplitude;
        transform.localPosition = initialLocalPosition + direction.normalized * offset;
    }
}
