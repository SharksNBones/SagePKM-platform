using Xunit;
using SagePKM;
using System.Collections.Generic;

namespace SagePKM.Tests
{
    public class SagePKMTests
    {
        [Fact]
        public void CreateNode_ShouldReturnNodeWithCorrectProperties()
        {
            var user = new User(1, "Alice", "Researcher");
            var node = user.CreateNode("Test Title", "Test Content", new List<string> { "Tag1" });

            Assert.Equal("Test Title", node.Title);
            Assert.Equal("Test Content", node.Content);
            Assert.Contains("Tag1", node.Tags);
        }

        [Fact]
        public void SearchNodes_ShouldFindMatchingTag()
        {
            var user = new User(1, "Alice", "Researcher");
            var graph = new KnowledgeGraph();
            graph.AddNode(user.CreateNode("Java Book", "Intro to Java", new List<string> { "Java" }));
            graph.AddNode(user.CreateNode("Python Book", "Intro to Python", new List<string> { "Python" }));

            var results = user.SearchNodes(graph, "Java");

            Assert.Single(results);
            Assert.Equal("Java Book", results[0].Title);
        }

        [Fact]
        public void SearchNodes_ShouldReturnEmptyListForMissingTag()
        {
            var user = new User(1, "Alice", "Researcher");
            var graph = new KnowledgeGraph();
            graph.AddNode(user.CreateNode("Java Book", "Intro to Java", new List<string> { "Java" }));

            var results = user.SearchNodes(graph, "C++");

            Assert.Empty(results);
        }

        [Fact]
        public void GetSummary_ShouldReturnFullContentIfShort()
        {
            var node = new Node("Short", "Quick note.", new List<string> { "Note" });
            var summary = node.GetSummary();

            Assert.Equal("Quick note.", summary);
        }

        [Fact]
        public void GetSummary_ShouldTruncateLongContent()
        {
            var longText = new string('A', 100);
            var node = new Node("Long", longText, new List<string> { "Long" });
            var summary = node.GetSummary();

            Assert.StartsWith(new string('A', 50), summary);
            Assert.EndsWith("...", summary);
        }

        [Fact]
        public void AddNode_ShouldIncreaseNodeCount()
        {
            var graph = new KnowledgeGraph();
            var node = new Node("Title", "Content", new List<string> { "Tag" });

            graph.AddNode(node);

            Assert.Single(graph.Nodes);
            Assert.Equal("Title", graph.Nodes[0].Title);
        }
    }
}
