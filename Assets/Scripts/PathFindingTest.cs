using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingTest : MonoBehaviour {

	public GameObject mapGroup;
	// Use this for initialization
	void Start () {
		
		int[,] map = new int[5, 5]{
			{0,1,0,0,0},
			{0,1,0,0,0},
			{0,1,0,0,0},
			{0,1,0,0,0},
			{0,0,0,0,0}
		};

		Graph graph = new Graph(map);

		Search search = new Search(graph);

		search.Start(graph.nodes[0], graph.nodes[2]);

		while(!search.finished){
			search.Step();
		}

		print("Search done, path count is " + search.path.Count + " and iterations were " + search.iterations);

		ResetMapGroup(graph);

		foreach(Node node in search.path){
			GetImage(node.label).color = Color.red;
		}
	}
	
	Image GetImage(string label){

		int id = Int32.Parse(label);
		GameObject square = mapGroup.transform.GetChild(id).gameObject;

		return square.GetComponent<Image>();
	}

	void ResetMapGroup(Graph graph){

		foreach(Node node in graph.nodes){
			GetImage(node.label).color = node.adjacent.Count == 0 ? Color.white : Color.gray;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
