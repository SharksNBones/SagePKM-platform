using System;
using System.Collections.Generic;
using System.Linq;

namespace SagePKM
{
    // Represents a user in the system (e.g., researcher, student, academic)
    public class User
    {
        // Unique ID number for each user
        public int UserId { get; set; }

        // The user's name
        public string Name { get; set; } = string.Empty;

        // The user's role (e.g., "Researcher", "Student")
        public string Role { get; set; } = string.Empty;

        // Method: Create a new knowledge node (piece of information)
        public KnowledgeNode CreateNode(string title, string content, List<string> tags, ExternalSource? source = null)
        {
            var node = new KnowledgeNode
            {
                NodeId = Guid.NewGuid(),      // Generate a unique identifier
                Title = title,
                Content = content,
                CreatedDate = DateTime.Now,
                Tags = tags,
                Source = source
            };

            return node;
        }

        // Method: Search for nodes in the knowledge graph by keyword
        public List<KnowledgeNode> SearchNodes(KnowledgeGraph graph, string keyword)
        {
            return graph.SearchGraph(keyword);
        }

        // Method: Link two knowledge nodes together with a relationship type
        public void LinkNodes(KnowledgeGraph graph, KnowledgeNode source, KnowledgeNode target, string type)
        {
            var link = new Link
            {
                LinkId = Guid.NewGuid(),
                SourceNode = source,
                TargetNode = target,
                Type = type
            };

            graph.AddLink(link);
        }
    }

    // Knowledge Node
    public class KnowledgeNode
    {
        public Guid NodeId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<string> Tags { get; set; } = new();
        public ExternalSource? Source { get; set; }

        public void EditNode(string newContent)
        {
            Content = newContent;
        }
        public void AddLink(Link link)
        {
            // Could store links locally if needed
        }
        public string GetSummary()
        {
            return Content.Length > 100 ? Content.Substring(0, 100) + "..." : Content;
        }
    }

    // External source (e.g., website, database, document)
    public class ExternalSource
    {
        public string Name { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
    }

    // Link between two knowledge nodes
    public class Link
    {
        public Guid LinkId { get; set; }
        public KnowledgeNode SourceNode { get; set; } = new KnowledgeNode();
        public KnowledgeNode TargetNode { get; set; } = new KnowledgeNode();
        public string Type { get; set; } = string.Empty;
    }

    // Knowledge graph that stores nodes and links
    public class KnowledgeGraph
    {
        private readonly List<KnowledgeNode> nodes = new();
        private readonly List<Link> links = new();
        public void AddNode(KnowledgeNode node) => nodes.Add(node);
        public void AddLink(Link link) => links.Add(link);
        public List<KnowledgeNode> SearchGraph(string keyword)
        {
            return nodes.Where(n =>
                n.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                n.Content.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                n.Tags.Any(t => t.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }
    }
}
