using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public string mapSeed; //need to regenerate on seed being editted in Unity editor
    public int mapHeight = 75;
    public int mapWidth = 130;
    public int mapTerritorySize = 35;
    public Tile tilePrefab;

    private Hashtable map;
    private Layout layout;
    private HexMesh hexMesh;
    private TestMesh testMesh;
    //private int[,,] islandRootHexes;

    void Awake()
    {
        Point size = new Point(1.0,1.0);
        Point origin = new Point (0.0,0.0);
        layout = new Layout(Layout.pointy,size,origin);
        Debug.Log("Added layout...");

        int left = 0;
        int right = mapWidth;
        int top = 0;
        int bottom = mapHeight;
        int index = 0;
        map = new Hashtable();
        for (int r = top; r <= bottom; r++) { // pointy top
            int r_offset = (int)Mathf.Floor((float)r/2.0f); // or r>>1
            for (int q = left - r_offset; q <= right - r_offset; q++) {
                Tile tile = Instantiate<Tile>(tilePrefab);
                Hex hex = new Hex(q,r,-q-r);
                tile.hex = hex;
                PositionTile(tile);
                map.Add(index, tile);
                Debug.Log("Added new hex to map with index " + index + " with x position equal to " + tile.transform.localPosition);
                index++;
            }
        }

		hexMesh = GetComponentInChildren<HexMesh>();
        testMesh = GetComponentInChildren<TestMesh>();
    }

    /*void OnDrawGizmos(){
        if (map != null) {
            Gizmos.color = Color.green;
            foreach (DictionaryEntry kvPair in map) {
                Tile tile = (Tile)kvPair.Value;
                Point point = layout.HexToPixel(tile.hex);
                Vector3 pixel = new Vector3((float)point.x,(float)point.y,0f);
                Gizmos.DrawSphere(pixel,1);
            }
        }
    }*/

    void PositionTile(Tile tile) {
        Point point = layout.HexToPixel(tile.hex);
        tile.transform.position = new Vector3((float)point.x,(float)point.y,0f);
    }

    // Start is called before the first frame update
    void Start()
    {

        //testMesh.Triangulate();
        hexMesh.Triangulate(map);
    }
}
