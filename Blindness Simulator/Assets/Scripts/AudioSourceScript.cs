using UnityEngine;

public class AudioSourceScript : MonoBehaviour
{
    private AudioSource audioSource;
    public float maxPointRadius;
    public float pointDelay;
    private float count;
    public Transform Player;

    public UnityEngine.Object lidarPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.GetComponent<PlayerController>().startTimer <= 0)
        {
            if (audioSource.isPlaying && Vector3.Distance(transform.position, Player.position) <= audioSource.maxDistance)
            {
                if(count >= pointDelay)
                {
                    count = 0;
                    Vector3 randomDirection = Random.onUnitSphere;

                    Vector3 pointPos = transform.position + (randomDirection * Random.Range(0, maxPointRadius));
                    Quaternion quaternion = Quaternion.LookRotation(randomDirection);
                    Instantiate(lidarPoint, pointPos, quaternion);
                }
                else
                {
                    count++;
                }
            }
        }
        
    }
}
