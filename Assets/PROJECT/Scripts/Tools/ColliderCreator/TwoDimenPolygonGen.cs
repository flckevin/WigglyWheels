using Kamgam.BikeAndCharacter25D.Helpers;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//REFERNECE: https://www.h3xed.com/programming/automatically-create-polygon-collider-2d-from-2d-mesh-in-unity

namespace QuocAnh.Tool
{


    public class TwoDimenPolygonGen : MonoBehaviour
    {
        public GameObject[] target; // all colliders for target
        public GameObject parentToColGroup; // parent to hold all those collision
        public int layer; // layer to assign
        public int pathSize = 1; // path amount for pollygon collider ususally 1 would be the best
        public bool sameScale;

        public void GenerateInitiation()
        {
            //if there is nothing in array then stop
            if (target.Length == 0) return;
            //loop all target
            for (int i = 0; i < target.Length; i++)
            {
                //generate collider for those target
                Generate(target[i]);
            }

            //clear array
            target.Clear();
            target = new GameObject[0];
            Debug.Log("DONE");
        }


        /// <summary>
        /// function to generate 2d polygon collider
        /// </summary>
        /// <param name="_targetToAddCol"> target to add collider </param>
        public void Generate(GameObject _targetToAddCol)
        {
            // Stop if no mesh filter exists or there's already a collider
            if (_targetToAddCol.GetComponent<PolygonCollider2D>() || _targetToAddCol.GetComponent<MeshFilter>() == null)
            {
                Debug.Log("ALREADY CONTAIN POLYGON 2D");
                return;
            }



            // Get triangles and vertices from mesh
            int[] triangles = _targetToAddCol.GetComponent<MeshFilter>().mesh.triangles;
            Vector3[] vertices = _targetToAddCol.GetComponent<MeshFilter>().mesh.vertices;

            // Get just the outer edges from the mesh's triangles (ignore or remove any shared edges)
            Dictionary<string, KeyValuePair<int, int>> edges = new Dictionary<string, KeyValuePair<int, int>>();
            for (int i = 0; i < triangles.Length; i += 3)
            {
                for (int e = 0; e < 3; e++)
                {
                    int vert1 = triangles[i + e];
                    int vert2 = triangles[i + e + 1 > i + 2 ? i : i + e + 1];
                    string edge = Mathf.Min(vert1, vert2) + ":" + Mathf.Max(vert1, vert2);
                    if (edges.ContainsKey(edge))
                    {
                        edges.Remove(edge);
                    }
                    else
                    {
                        edges.Add(edge, new KeyValuePair<int, int>(vert1, vert2));
                    }
                }
            }

            // Create edge lookup (Key is first vertex, Value is second vertex, of each edge)
            Dictionary<int, int> lookup = new Dictionary<int, int>();
            foreach (KeyValuePair<int, int> edge in edges.Values)
            {
                if (lookup.ContainsKey(edge.Key) == false)
                {
                    lookup.Add(edge.Key, edge.Value);
                }
            }

            GameObject _targetStorage = null;

            //if target not going to be parent into a group
            if (parentToColGroup == null)
            {
                _targetStorage = new GameObject();
                _targetStorage.name = _targetToAddCol.name + " 2D COL";
                _targetStorage.transform.position = _targetToAddCol.transform.position;
                _targetStorage.transform.rotation = _targetToAddCol.transform.rotation;
                if (sameScale == true) { _targetStorage.transform.localScale = _targetToAddCol.transform.localScale; }
                _targetStorage.transform.parent = _targetToAddCol.transform;

            }
            else
            {
                _targetStorage = new GameObject();
                _targetStorage.name = _targetToAddCol.name + " 2D COL";
                _targetStorage.transform.parent = parentToColGroup.transform;
                _targetStorage.transform.position = _targetToAddCol.transform.position;
                _targetStorage.transform.rotation = _targetToAddCol.transform.rotation;
                if (sameScale == true) { _targetStorage.transform.localScale = _targetToAddCol.transform.localScale; }
            }

            //set layer
            _targetStorage.layer = layer;
            // Create empty polygon collider
            PolygonCollider2D polygonCollider = _targetStorage.AddComponent<PolygonCollider2D>();
            polygonCollider.pathCount = 0;

            // Loop through edge vertices in order
            int startVert = 0;
            int nextVert = startVert;
            int highestVert = startVert;
            List<Vector2> colliderPath = new List<Vector2>();
            while (true)
            {

                // Add vertex to collider path
                colliderPath.Add(vertices[nextVert]);

                // Get next vertex
                nextVert = lookup[nextVert];

                // Store highest vertex (to know what shape to move to next)
                if (nextVert > highestVert)
                {
                    highestVert = nextVert;
                }

                // Shape complete
                if (nextVert == startVert)
                {

                    // Add path to polygon collider
                    polygonCollider.pathCount++;
                    polygonCollider.SetPath(polygonCollider.pathCount - 1, colliderPath.ToArray());
                    colliderPath.Clear();

                    // Go to next shape if one exists
                    if (lookup.ContainsKey(highestVert + 1))
                    {

                        // Set starting and next vertices
                        startVert = highestVert + 1;
                        nextVert = startVert;

                        // Continue to next loop
                        continue;
                    }

                    // No more verts
                    break;
                }
            }

            if (pathSize == -1) return;
            //set path to be size of 1 in order to works
            polygonCollider.pathCount -= (polygonCollider.pathCount - pathSize);
        }
    }

}