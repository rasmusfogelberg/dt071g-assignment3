using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace assignment3
{
  public class PostList
  {
    public void PrintPosts(List<Post> list)
    {
      for (int i = 0; i < list.Count; i++)
      {
        Console.WriteLine($"[{i}] Author: {list[i].author} : Message: {list[i].content}");
      }
    }

    public void AddPost(List<Post> list, string author, string content)
    {
      Post newPost = new Post() { author = author, content = content };

      list.Add(newPost); // adds item to the end of the collection

      // TODO: Break this into method
      string json = JsonConvert.SerializeObject(list, Formatting.Indented);
      File.WriteAllText("posts.json", json, Encoding.UTF8);
    }

    public void DeletePost(int index, List<Post> list)
    {
      list.RemoveAt(index);

      // TODO: Break this into method
      string json = JsonConvert.SerializeObject(list, Formatting.Indented);
      File.WriteAllText("posts.json", json, Encoding.UTF8);
    }
  }
}