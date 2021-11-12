using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace assignment3
{

  class Program
  {
    static void Main(string[] args)
    {

      string fileName = "posts.json";
      string jsonString = File.ReadAllText(fileName);
      List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(jsonString);

      bool displayMenu = true;

      while (displayMenu)
      {
        displayMenu = MainMenu(posts);
      }
    }

    static bool MainMenu(List<Post> posts)
    {
      WelcomeText();
      string choice = Console.ReadLine();
      PostList list = new PostList();

      switch (choice)
      {
        case "1":
          Console.Clear();

          if (posts.Count > 0)
          {
            list.PrintPosts(posts);
          }
          else
          {
            Console.WriteLine("No posts in the guestbook yet, want to add one? \n");
          }

          return true;
        case "2":
          Console.Clear();
          string authorName = "";
          string message = "";

          Console.WriteLine("Please input your name:\n");
          authorName = Console.ReadLine();

          Console.WriteLine("Input your message:\n");
          message = Console.ReadLine().Trim();

          do
          {
            Console.Clear();
            Console.WriteLine("You forgot to write your name or message down. Please try again. \n");
          } while (String.IsNullOrEmpty(authorName) || String.IsNullOrEmpty(message));

          list.AddPost(posts, authorName, message);

          Console.Clear();
      
          return true;
        case "3":
          Console.Clear();
          list.PrintPosts(posts);
          Console.WriteLine("Please provide the number of the post you want to delete:");

          string postIndexInput = Console.ReadLine();
          int postIndex;

          bool isANumber = int.TryParse(postIndexInput, out postIndex);

          if (!isANumber)
          {
            Console.Clear();
            Console.WriteLine("Oh dear, you didn't put in a number, try again please\n");
          } else {
            list.DeletePost(postIndex, posts);
          }

          return true;

        case "0":
          Console.Clear();
          Console.WriteLine("\nThanks for visiting. Goodbye!\n");
          Environment.Exit(0);
          return false;

        default:
          Console.Clear();
          Console.WriteLine("Not a valid input. Please try again");
          return true;
      }
    }
    static void WelcomeText()
    {
      Console.ForegroundColor = ConsoleColor.DarkBlue;
      Console.WriteLine("\n\n### R A S M U S ~ G U E S T B O O K! ###");
      Console.WriteLine(" ");
      Console.WriteLine("What would you like to do?");
      Console.WriteLine(" ");
      Console.WriteLine("[1] Read a post");
      Console.WriteLine("[2] Create a post");
      Console.WriteLine("[3] Delete a post");
      Console.WriteLine("[0] Exit guestbook");
      Console.Write("\r\nSelect an option: ");
      Console.ForegroundColor = ConsoleColor.Gray; 
    }
  }
}
