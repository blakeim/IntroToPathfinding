using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingTest : MonoBehaviour {

	public GameObject mapGroup;
	Graph graph;
	private int start = 0, goal = 14;
	Search search;

	// Use this for initialization
	void Start () {
		

		//TODO make capable of accepting arbitrary maps
		int[,] map = new int[5, 5]{
			{0,1,0,0,0},
			{0,1,0,0,0},
			{0,1,0,0,0},
			{0,1,0,0,0},
			{0,0,0,0,0}
		};

		graph = new Graph(map);
		search = new Search(graph);

		search.Start(graph.nodes[start], graph.nodes[goal]);
		
		while(!search.finished){
			search.Step();
		}

		ResetMapGroup(graph);
		StartCoroutine(TrackRoutine());
	}
	
	Image GetImage(string label){

		int id = Int32.Parse(label);
		GameObject square = mapGroup.transform.GetChild(id).gameObject;

		return square.GetComponent<Image>();
	}

	void ResetMapGroup(Graph graph){

		for(int i = 0; i < graph.nodes.Length; i++){

			if(i == goal){
				GetImage(graph.nodes[i].label).color = Color.green;
			}
			else{
				GetImage(graph.nodes[i].label).color = graph.nodes[i].adjacent.Count == 0 ? Color.white : Color.gray;
			}
		}


	}

	IEnumerator<WaitForSeconds> TrackRoutine () {

		foreach(Node node in search.path){
			GetImage(node.label).color = Color.red;
			yield return new WaitForSeconds(0.5f);
		}
	}

	void Update(){

	}
}
