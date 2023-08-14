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
//is there some factor of size that matters here? something fishy about the spheres not being on the center of hexes
//^tested and the hizmos are not on the same places as the meshes.....
//*2????
        //For 9 players, identify root hexes for two "rows" of islands; each row will either have 4 or 5 islands in it:
        islandRootHexes = new List<Hex>();
        int northernColumnCount = Random.Range(4,6); //randomly select either 4 or 5
        Debug.Log("Northern columns count: " + northernColumnCount);
        for (int i = 0; i < 9; i++) {
            int r = (int)Mathf.Round(mapHeight*(i < northernColumnCount ? 2 : 5)/7); //either 2/7ths or 5/7ths of mapHeight
            float columnSpacing = (float)mapWidth/(i < northernColumnCount ? (float)northernColumnCount: 9f - (float)northernColumnCount);
            Debug.Log("Column spacing = " + columnSpacing);
            int r_offset = (int)Mathf.Floor((float)r/2.0f); // or r>>1
            int q = (int)Mathf.Round((columnSpacing/2f) - r_offset + (columnSpacing*(i < northernColumnCount ? (float)i : (float)i - northernColumnCount))); //start from offset to ensure islands do not go to edges of map width- we want only oceans on width
            islandRootHexes.Add(new Hex(q, r, -q-r));
            Debug.Log("New root island hex created with qrs = " + q + ", " + r + ", " + (-q-r));
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
        for (int r = top; r < bottom; r++) { // pointy top
            int r_offset = (int)Mathf.Floor((float)r/2.0f); // or r>>1
            for (int q = left - r_offset; q < right - r_offset; q++) {
                Tile tile = Instantiate<Tile>(tilePrefab);
                Hex hex = new Hex(q,r,-q-r);
                tile.hex = hex;
                hex.tile = tile;
                Debug.Log("Adding new hex to map with index " + index + " with current position equal to " + tile.gameObject.transform.position + "and Hex q r s = " + tile.hex.q +","+tile.hex.r + "," + tile.hex.s);
                PositionTile(tile);
                map.Add(index, tile);
                /*foreach(Hex rootHex in islandRootHexes) {
                    if (hex.Equals(rootHex)) {
                        hex.height = 2;
                    }
                }*/
                index++;
            }
        }
    }

    //Debugging option to draw hex locations when gameObject is selected:
    void OnDrawGizmosSelected(){
        if (map != null) {
            foreach (DictionaryEntry kvPair in map) {
                bool root = false;
                Tile tile = (Tile)kvPair.Value;
                foreach (Hex hex in islandRootHexes) {
                    if (tile.hex.Equals(hex)) {root = true;}
                }
                Gizmos.color = root ? Color.red : Color.green;
                Point point = layout.HexToPixel(tile.hex);
                Vector3 pixel = new Vector3((float)point.x,(float)point.y,0f);
                Gizmos.DrawSphere(pixel,0.5f);
            }
        }
    }

    void PositionTile(Tile tile) {
        Point point = layout.HexToPixel(tile.hex);
        tile.gameObject.transform.position = new Vector3((float)point.x,(float)point.y,0f);
        Debug.Log("Repositioned tile to position " + tile.gameObject.transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Height colour 11 = " + Height.hexColours[11]);
        Debug.Log("Height colour -3 = " + Height.hexColours[-3]);
        Debug.Log("Height land type for -4 = " + Height.landTypes[-4]);
        //UnityEditor.SceneView.RepaintAll();
    }
}
