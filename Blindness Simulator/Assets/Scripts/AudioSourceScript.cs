using UnityEngine;

public class AudioSourceScript : MonoBehaviour
{
    private AudioSource audioSource;
    public float maxPointRadius;

    public UnityEngine.Object lidarPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying)
        {
            Vector3 randomDirection = Random.onUnitSphere;

            Vector3 pointPos = transform.position + (randomDirection * Random.Range(0, maxPointRadius));
            Quaternion quaternion = Quaternion.LookRotation(randomDirection);
            Instantiate(lidarPoint, pointPos, quaternion);
        }
    }
}
