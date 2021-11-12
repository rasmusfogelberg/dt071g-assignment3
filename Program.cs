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
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
            Console.Clear();
          }
          else
          {
            Console.WriteLine("\nNo posts in the guestbook yet, want to add one? \n");
          }

          return true;
        case "2":
          Console.Clear();
          string authorName = "";
          string message = "";
          Console.ForegroundColor = ConsoleColor.DarkCyan;
          Console.WriteLine("\nPlease input your name:");

          authorName = Console.ReadLine();

          while (String.IsNullOrEmpty(authorName))
          {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You have to provide a name!\n");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            authorName = Console.ReadLine();
          }

          Console.WriteLine("\nInput your message:");
          message = Console.ReadLine().Trim();

          while (String.IsNullOrEmpty(message))
          {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nYou have to provide a message!");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            message = Console.ReadLine().Trim();
          }

          list.AddPost(posts, authorName, message);
          Console.ForegroundColor = ConsoleColor.Gray;
          return true;
        case "3":
          Console.Clear();

          if (posts.Count > 0)
          {
            list.PrintPosts(posts);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nPlease provide the number of the post you want to delete:");

            string postIndexInput = Console.ReadLine();
            int postIndex;
            Console.ForegroundColor = ConsoleColor.Gray;
            bool isANumber = int.TryParse(postIndexInput, out postIndex);

            if (!isANumber)
            {
              Console.Clear();
              Console.ForegroundColor = ConsoleColor.Yellow;
              Console.WriteLine("\nOh dear, your input wasn't a number, try again please\n");
              Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
              int inputCheck = Convert.ToInt32(postIndexInput) + 1;

              if (inputCheck > posts.Count)
              {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nOh dear, your input wasn't a valid post number, try again please\n");
                Console.ForegroundColor = ConsoleColor.Gray;
              }
              else
              {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Are you sure you want to remove post {postIndex}? Y/n:\n");
                string answer = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Gray;

                if (answer == "y" || answer == "Y" || answer == "Yes" || answer == "yes")
                {
                  list.DeletePost(postIndex, posts);
                }
                else
                {
                  Console.ForegroundColor = ConsoleColor.DarkGreen;
                  Console.WriteLine("\nNo posts were deleted!");
                  Console.ForegroundColor = ConsoleColor.Gray;
                  Console.WriteLine("\nPress any key to continue");
                  Console.ReadKey();
                  Console.Clear();
                }

              }
            }
          }
          else
          {
            Console.WriteLine("\nNo posts in the guestbook yet, want to add one? \n");
          }

          return true;
        case "0":
          Console.Clear();
          Console.WriteLine("\nThanks for visiting. Goodbye!\n");
          Environment.Exit(0);
          return false;

        default:
          Console.Clear();
          Console.WriteLine("\nNot a valid input. Please try again");
          return true;
      }
    }
    static void WelcomeText()
    {
      Console.ForegroundColor = ConsoleColor.DarkBlue;
      Console.WriteLine("\n### R A S M U S ~ G U E S T B O O K! ###");
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
