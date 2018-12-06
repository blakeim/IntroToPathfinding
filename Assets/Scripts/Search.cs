using System.Collections.Generic;
using UnityEngine;

public class Search  {

	public Graph graph;
	public List<Node> reachable, explored, path;

	public Node goalNode;
	public int iterations;
	public bool finished;

	public Search(Graph graph){
		
		this.graph = graph;
	}

	public void Start(Node start, Node goal){

		reachable = new List<Node>();
		reachable.Add(start);

		goalNode = goal;

		explored = new List<Node>();
		path = new List<Node>();
		iterations = 0;

		foreach(Node n in graph.nodes){
			n.Clear();
		}
	}

	public void Step(){

		if(path.Count > 0){
			return;
		}

		if(reachable.Count == 0){
			finished = true;
			return;
		}

		iterations++;

		Node node = ChoseNode();
		if(node == goalNode){
			while(node != null){
				path.Insert(0, node);
				node = node.previous;
			}

			finished = true;
			return;
		}

		reachable.Remove(node);
		explored.Add(node);

		foreach(Node n in node.adjacent){
			AddAdjacent(node, n);
		}
	}

	public void AddAdjacent(Node node, Node adjacent){

		if(FindNode(adjacent, explored) || FindNode(adjacent, reachable)){
			return;
		}

		if(adjacent == goalNode){
			node.distanceToGoal = 1;
		}
		else{
			node.distanceToGoal += adjacent.distanceToGoal;
		}
		adjacent.previous = node;
		reachable.Add(adjacent);
	}

	public bool FindNode(Node node, List<Node> list){

		return GetNodeIndex(node, list) >= 0;
	}

	public int GetNodeIndex(Node node, List<Node> list){

		for(int i = 0; i < list.Count; i++){
			if(node == list[i]){
				return i;
			}
		}

		return -1;
	}

	public Node ChoseNode(){

		int distance = -1;
		Node closestNode = null;

		foreach(Node n in reachable){
			
			if(distance == -1 || n.distanceToGoal < distance){
				distance = n.distanceToGoal;
				closestNode = n;
			}
		} 
		return closestNode;
	}
}
