using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public static int size = 16;
    static float randOffest = 0.3563565716f;
    static int randOffest2 = 255;
    static float scale = 8;
    static int SeaLevel = 32;
    public GameObject UnitPrefab;
    public class ChunkData
    {
        public Vector2Int chords;
        public GameObject gameObject;
        public bool active;
        public BitArrayUint32[,] grid = new BitArrayUint32[size, size];
        public ChunkData(int x,int y)
        {
            chords.x = x ;
            chords.y = y ;

           
            PerlinNoise();
        }
        void PerlinNoise()
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    float noise = 0;
                    noise = Mathf.PerlinNoise(
                        (((float)x) / (float)scale) + chords.x * randOffest + randOffest2,
                        (((float)y) / (float)scale) + chords.y * randOffest + randOffest2);
                    noise = Mathf.Floor(noise * SeaLevel);

                    grid[x, y] = new BitArrayUint32(0);
                    for (int i = 0; i < noise; i++)
                    {
                        
                        grid[x, y].SetBit(i, true);
                    }


                }
            }
        }
        public void PlaceBlocks(GameObject UnitPrefab)
        {
            gameObject = new GameObject($"Chunk { chords.x} { chords.y}");
            gameObject.transform.position = new Vector3(chords.x * size, 0, chords.y * size);
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < grid[x, y].Length; z++)
                    {
                        if (grid[x, y].GetBit(z))
                        {
                            bool hasCornerVisible = true;
                            for (int x1 = -1; x1 < 2; x1++)
                            {
                                for (int y1 = -1; y1 < 2; y1++)
                                {
                                    for (int z1 = -1; z1 < 2; z1++)
                                    {
                                        try
                                        {
                                        hasCornerVisible = hasCornerVisible && grid[(x + x1), y + y1].GetBit(z + z1);
                                        }
                                        catch (System.Exception)
                                        {

                                            hasCornerVisible = false;
                                        }
                                        if (hasCornerVisible == false) break;

                                    }
                                    if (hasCornerVisible == false) break;

                                }
                                if (hasCornerVisible == false) break;

                            }
                            if (hasCornerVisible == false)
                            {
                            GameObject cube = Instantiate(UnitPrefab);
                            cube.transform.parent = gameObject.transform;
                            cube.transform.localPosition = new Vector3(x, z, y);
                                
                            }
                        }
                    }
                }
            }
        }
        public string ToBase()
        {
            string s = "";
            foreach (var data in grid)
            {
                s += data.GetBase64() + " ";
            }
            return s;
        }

        public void FromBase(string s)
        {
            var trim = s.Split(' ');
            for (int i = 0; i < trim.Length; i++)
            {
                grid[i,i].SetFromBase64(trim[i]);

               
            }
            foreach (var data in grid)
            {
            }
        }
    }
   // public ChunkData[,] Chunks = new ChunkData[size,size];
    public Dictionary<Vector2Int,ChunkData> Chunks = new Dictionary<Vector2Int, ChunkData>();
    Vector3Int offset;
    public bool done = true;
    public void UpdateOffset(Vector3 p)
    {
        Vector3Int offset = Vector3Int.zero;
        var v = p;
        offset.x = (int)v.x;
        offset.z = (int)v.z;
        offset /= 100;
        for (int x = worldSize / -2; x < worldSize / 2; x++)
        {
            for (int y = worldSize / -2; y < worldSize / 2; y++)
            {
                if (Chunks.ContainsKey(new Vector2Int(x, y)) == false)
                {
                    ChunkData data = PlaceChunk(x + offset.x, y + offset.z);
                    Chunks.Add(new Vector2Int(x, y), data);
                }
            }
        }
        StartCoroutine("PlaceChunks");
    }
    public ChunkData PlaceChunk(int x, int y)
    {
        ChunkData data = new ChunkData(x , y);
        data.PlaceBlocks(UnitPrefab);
       // Chunks.Add(new Vector2Int(x, y), data);
        return data;
    }
    // Start is called before the first frame update
    static int worldSize = 6;
    void Start()
    {

        UpdateOffset(Vector3.zero);

    }
    IEnumerator PlaceChunks()
    {
        //yield return StartCoroutine("_CleanUpChunks");


        for (int x = worldSize / -2; x < worldSize / 2; x++)
        {
            for (int y = worldSize / -2; y < worldSize / 2; y++)
            {
               
                if ( Chunks.TryGetValue(new Vector2Int(x, y), out ChunkData data))
                {
                    data.PlaceBlocks(UnitPrefab);
                    data.active = true;
                    yield return new WaitForEndOfFrame();
                }

            }
        }
    
    }
    public void CleanUpChunks() => StartCoroutine("_CleanUpChunks");
    IEnumerator _CleanUpChunks()
    {
        
        foreach (ChunkData data in Chunks.Values)
        {
            Destroy(data.gameObject);
            data.active = false;
            yield return null;//new WaitForEndOfFrame();
        }
        done = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
