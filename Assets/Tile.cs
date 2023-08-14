using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Tile : MonoBehaviour
{
    public Hex hex;
    Mesh tileMesh;
	List<Vector3> vertices;
	List<int> triangles;
    List<Color> colours;

	void Awake () {
		GetComponent<MeshFilter>().mesh = tileMesh = new Mesh();
		tileMesh.name = "Tile Mesh";
		vertices = new List<Vector3>();
		triangles = new List<int>();
        colours = new List<Color>();
	}

    void Start() {
        //Happens after Awake() because tile positioning occurs there
        Triangulate();
    }

    public void Triangulate () {
		tileMesh.Clear();
		vertices.Clear();
		triangles.Clear();
        colours.Clear();
        Vector3 center = new Vector3(0,0,0); //gameObject.transform.position incorrect because Mesh seems to be local position in respect to its gameObject!;
		for (int i = 0; i < 6; i++) {
			AddTriangle(center, center + Hex.corners[i], center + Hex.corners[i+1]);
            AddTriangleColor();
            //Debug.Log("i = " + i + ". Added triangle with center = " + center + " and two corners " + (center + Hex.corners[i]) + " and " + (center + Hex.corners[i+1]));
		}
		tileMesh.vertices = vertices.ToArray();
		tileMesh.triangles = triangles.ToArray();
        tileMesh.colors = colours.ToArray();
        //Debug.Log("Vertices count = " + vertices.Count);
        //Debug.Log("Triangles count = " + triangles.Count);
		tileMesh.RecalculateNormals(); 
	}

	void AddTriangle (Vector3 v1, Vector3 v2, Vector3 v3) {
		int vertexIndex = vertices.Count;
		vertices.Add(v1);
		vertices.Add(v2);
		vertices.Add(v3);
		triangles.Add(vertexIndex);
		triangles.Add(vertexIndex + 1);
		triangles.Add(vertexIndex + 2);
	}

    void AddTriangleColor () {
        Color newCol;
        if (ColorUtility.TryParseHtmlString(hex.height.ToString(), out newCol)) {
            colours.Add(newCol);
            colours.Add(newCol);
            colours.Add(newCol);
        }
	}
}
