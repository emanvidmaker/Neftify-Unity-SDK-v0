using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGen : MonoBehaviour
{
    [Range(0f,1f)]
    public float isoLevel;
    public List<Triangulator.TRIANGLE> tri;
    public MeshFilter filter;
    // Start is called before the first frame update
    void Start()
    {
        Triangulator.GRIDCELL v = new Triangulator.GRIDCELL();

        for (int i = 0; i < v.value.Length; i++)
        {
            v.value[i] = Random.Range(0f, 1f);
        }

        tri = Triangulator.Polygonise(v, isoLevel);
        var mesh = new Mesh();
        var vx = new List<Vector3>();
        var tr = new List<int>();
        int it = 0;
        foreach (var t in tri)
        {
            vx.Add(t.p[0]);
            tr.Add(it);
            it++;

            vx.Add(t.p[1]);
            tr.Add(it);
            it++;

            vx.Add(t.p[2]);
            tr.Add(it);
            it++;
        }
        mesh.vertices = vx.ToArray();
        mesh.triangles = tr.ToArray();

        filter.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
