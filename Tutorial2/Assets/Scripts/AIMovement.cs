using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public float speed = 1.50f;

    private float startTime;

    private float objectJourney;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        objectJourney = Vector3.Distance(startPoint.position, endPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceCovered = (Time.time - startTime) * speed;

        float fracJourney = distanceCovered/objectJourney;

        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, Mathf.PingPong(fracJourney, 1));
    }
}
