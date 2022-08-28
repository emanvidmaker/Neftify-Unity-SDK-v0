using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PositionTracker : MonoBehaviour
{
    public UnityEvent<Vector3> UpdatePos = new UnityEvent<Vector3>();
    CubeSpawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<CubeSpawner>();
    }
    void ping() {
    }
    Vector3 lastPos;
    // Update is called once per frame
    void Update()
    {
        //evry 16 steps launch an event 
        Vector3Int pos = Vector3Int.zero;
            pos.x = (int)transform.position.x % CubeSpawner.size;
           // pos.y = (int)transform.position.y % CubeSpawner.size;
            pos.z = (int)transform.position.z % CubeSpawner.size;
        //if (spawner.done)
        if (Input.GetAxisRaw("Fire1") != 0)
        {
           /* if( (lastPos.x == 16 && pos.x == 0 )|| (lastPos.x == 0 && pos.x == 16)
                ||
                (lastPos.y == 16 && pos.y == 0 )|| (lastPos.y == 0 && pos.y == 16))*/
            {
                UpdatePos.Invoke(transform.position);

                spawner.UpdateOffset(transform.position);
                spawner.done = false;
            }
        }
        lastPos = pos; 
    }
}
