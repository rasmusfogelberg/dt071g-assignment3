/* 
 * Class for PostList. Here are methods for Reading (listing all posts), Creating and Deleting.
 * 
 */

using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace assignment3
{
  public class PostList
  {
    // Method to print all posts in the JSON file
    public void PrintPosts(List<Post> list)
    {
      Console.WriteLine("\nCurrent posts in the guestbook\n");
      for (int i = 0; i < list.Count; i++)
      {
        Console.WriteLine($"[{i}] Author: {list[i].author} : Message: {list[i].content}");
      }
    }

    // Method that calls the Post class to store a post
    public void AddPost(List<Post> list, string author, string content)
    {
      Post newPost = new Post() { author = author, content = content };

      list.Add(newPost); // adds item to the end of the collection

      Console.ForegroundColor = ConsoleColor.Green;
      // Conformation message that the post was created
      Console.WriteLine("\nPost successfully created!");
      Console.ForegroundColor = ConsoleColor.Gray;

      UpdateJson(list);

      Console.WriteLine("\nPress any key to continue");
      Console.ReadKey();
      Console.Clear();
    }

    // Method to delete a post
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

    // Method to serialize (update) the JSON file with what was done to it (delte or create a post)
    public void UpdateJson(List<Post> list)
    {
      string json = JsonConvert.SerializeObject(list, Formatting.Indented);
      File.WriteAllText("posts.json", json, Encoding.UTF8);
    }
  }
}