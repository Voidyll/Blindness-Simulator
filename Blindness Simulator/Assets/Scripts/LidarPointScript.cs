using UnityEngine;

public class LidarPointScript : MonoBehaviour
{
    public float decayTimerMin;
    public float decayTimerMax;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = Random.Range(decayTimerMin, decayTimerMax);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
    }
}
