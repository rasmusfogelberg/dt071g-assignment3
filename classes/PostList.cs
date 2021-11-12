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
      Console.WriteLine("\nCurrent posts in the guestbook\n");
      for (int i = 0; i < list.Count; i++)
      {
        Console.WriteLine($"[{i}] Author: {list[i].author} : Message: {list[i].content}");
      }
    }

    public void AddPost(List<Post> list, string author, string content)
    {
      Post newPost = new Post() { author = author, content = content };

      list.Add(newPost); // adds item to the end of the collection

      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("\nPost successfully created!");
      Console.ForegroundColor = ConsoleColor.Gray;

      UpdateJson(list);

      Console.WriteLine("\nPress any key to continue");
      Console.ReadKey();
      Console.Clear();

      
    }

    public void DeletePost(int index, List<Post> list)
    {
      list.RemoveAt(index); // Removes item on provided index
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("\nPost successfully removed!");
      Console.ForegroundColor = ConsoleColor.Gray;

      UpdateJson(list);

      Console.WriteLine("\nPress any key to continue");
      Console.ReadKey();
      Console.Clear();
    }

    public void UpdateJson(List<Post> list)
    {
      string json = JsonConvert.SerializeObject(list, Formatting.Indented);
      File.WriteAllText("posts.json", json, Encoding.UTF8);
    }
  }
}