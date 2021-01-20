using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void Update()
    {
        if (this.gameObject.transform.position.y - Camera.main.transform.position.y > 20)
        {
            Destroy(this.gameObject);
        }
    }
}
