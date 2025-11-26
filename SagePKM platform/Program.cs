using System;
using System.Collections.Generic;

namespace SagePKM
{
    //Start of program. 
    public class Program
    {
        public static void Main(string[] args)
        {
            //Create a new user with ID=1, name "Alice", and role "Researcher".
            var user = new User(1, "Alice", "Researcher");

            //Create a new empty knowledge graph.
            var graph = new KnowledgeGraph();

            //Creates a new node with title, content, and tags.
            var node = user.CreateNode(
                title: "New book added",
                content: "Beginners guide to Java.",
                tags: new List<string> { "Guide", "Java" }
            );

            //Add the created node to the knowledge graph.
            graph.AddNode(node);

            //Search for nodes in the graph with the tag "Java".
            var results = user.SearchNodes(graph, "Java");

            //Print how many nodes were found. 
            Console.WriteLine($"Found {results.Count} node(s):");

            //Loop through each found node and print its title and summary.
            foreach (var result in results)
            {
                Console.WriteLine($"- {result.Title}: {result.GetSummary()}");
            }
        }
    }
    
    // Represents a user who interacts with the knowledge graph.
    public class User
    {
        public int UserId { get; } //Unique identifier for the user.
        public string Name { get; } //User's name.
        public string Role { get; } //user's role (e.g., Researcher, Admin).

        // Constructor to initialize a new user with ID, name, and role.
        public User(int userId, string name, string role)
        {
            UserId = userId;
            Name = name;
            Role = role;
        }
        
        //Method for creating a new node with given title, content, and tags in the knowledge graph.
        public Node CreateNode(string title, string content, List<string> tags)
        {
            return new Node(title, content, tags);
        }
        
        //Method for searching nodes in the knowledge graph that contain a specific keyword in their tags.
        public List<Node> SearchNodes(KnowledgeGraph graph, string keyword)
        {
            return graph.Nodes.FindAll(n => n.Tags.Contains(keyword));
        }
    }
   
    // Represents a knowledge graph containing multiple nodes.
    public class KnowledgeGraph
    {
        // List of all nodes in the knowledge graph.
        public List<Node> Nodes { get; } = new List<Node>();

        //Method to add a new node to the knowledge graph.
        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }
    }
    
    // Represents a single node in the knowledge graph.
    public class Node
    {
        public string Title { get; } // Title of the node.
        public string Content { get; } // Full content of the node.
        public List<string> Tags { get; } // Tags for categorization/Search.

        // Constructor to initialize a node
        public Node(string title, string content, List<string> tags)
        {
            Title = title;
            Content = content;
            Tags = tags;
        }
        
        // Returns a short summary of the content (first 50 chars)
        public string GetSummary() 
        {
            return Content.Length > 50 ? Content.Substring(0, 50) + "..." : Content;
        }

        // Combines this node's content with another node's content
        // Returns a new node with merged content but same title/tags
        public Node add(Node other)
        {
            return new Node(Title, Content + other.Content, Tags);
        }
    }
}

