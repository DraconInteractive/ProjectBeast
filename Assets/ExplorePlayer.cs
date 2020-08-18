using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorePlayer : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input *= speed * Time.deltaTime;
        transform.position = transform.position + Vector3.forward * input.y + Vector3.right * input.x;
    }
}
