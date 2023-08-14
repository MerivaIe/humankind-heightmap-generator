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
    private List<Hex> islandRootHexes;

    void Awake()
    {
        //For 9 players, identify root hexes for two "rows" of islands; each row will either have 4 or 5 islands in it:
        int northernColumnCount = Random.Range(4,6); //randomly select either 4 or 5
        for (int i = 0; i < 9; i++) {
            int q = mapHeight*(i < northernColumnCount? 5 : 2)/7; //either 5/7ths or 4/7ths of mapHeight
            int columnSpacing = mapWidth/(i < northernColumnCount? northernColumnCount : 9 - northernColumnCount);
            int r = (columnSpacing/2) + (i*columnSpacing); //start from offset to ensure islands do not go to edges of map width- we want only oceans on width
            islandRootHexes.Add(new Hex(q, r, -q-r));
        }

        //Create a new layout
        Point size = new Point(1.0,1.0);
        Point origin = new Point (0.0,0.0);
        layout = new Layout(Layout.pointy,size,origin);
        Debug.Log("Added layout...");

        //Create map
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
                hex.tile = tile;
                PositionTile(tile);
                map.Add(index, tile);
                foreach(Hex rootHex in islandRootHexes) {
                    if (hex.Equals(rootHex)) {
                        hex.height = 2;
                    }
                }
                Debug.Log("Added new hex to map with index " + index + " with x position equal to " + tile.transform.localPosition);
                index++;
            }
        }
    }

    //Debugging option to draw hex locations:
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
        Debug.Log("Height colour 11 = " + Height.hexColours[11]);
        Debug.Log("Height colour -3 = " + Height.hexColours[-3]);
        Debug.Log("Height land type for -4 = " + Height.landTypes[-4]);
    }
}
