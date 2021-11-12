/* 
 * Author: Rasmus Fogelberg
 * This is a simple guestbook application in C#. A user can Read, Create and Delete posts when using the console for this application.
 *
 */
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
      // Checking if a JSON file exists on starting the application
      Console.Write("Starting up. Checking if JSON-file exists...\n");
      string fileName = @"posts.json";

      bool fileExist = File.Exists(fileName);
      if (fileExist)
      {
        Console.WriteLine("File exists. Starting application.");
      }
      else 
      { // If no JSON file exists one is created
        Console.WriteLine("File does not exist. Creating file..");
        using (StreamWriter sw = File.CreateText(fileName)) {
         sw.WriteLine("[]");
        }
        Console.WriteLine("File created. Starting application.");
      }

      // Reading the contents on the JSON file by Deserializing it and storing it in posts
      string jsonString = File.ReadAllText(fileName);
      List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(jsonString);

      // To show the menu
      bool displayMenu = true;

      while (displayMenu)
      { // Calling MainMenu method and sending the posts argument with is
        displayMenu = MainMenu(posts);
      }
    }

    static bool MainMenu(List<Post> posts)
    {
      // Calling WelcomeText method to display welcome text
      WelcomeText();
      // Reading the user input
      string choice = Console.ReadLine();

      // Instantiating the PostList class in the variable list
      PostList list = new PostList();

      // Depending on user input different cases are entered
      switch (choice)
      {
        case "1":
          Console.Clear();

          // If there are posts in the JSON file this statment is entered
          if (posts.Count > 0)
          {
            // Calling the PrintPosts method from PostList class via list
            list.PrintPosts(posts);
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
            Console.Clear();
          }
          else
          { // If there are no posts this message is shown
            Console.WriteLine("\nNo posts in the guestbook yet, want to add one? \n");
          }

          return true;
        case "2":
          Console.Clear();
          // Setting variables to empty
          string authorName = "";
          string message = "";
          // Changing color on text
          Console.ForegroundColor = ConsoleColor.DarkCyan;
          Console.WriteLine("\nPlease input your name:");

          // User input is stored in authorName
          authorName = Console.ReadLine();

          // If the user didn't enter any input, this code is run while authorName variable is empty
          while (String.IsNullOrEmpty(authorName))
          {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You have to provide a name!\n");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            authorName = Console.ReadLine();
          }

          Console.WriteLine("\nInput your message:");
          // User input is stored in message variable
          message = Console.ReadLine().Trim();

          // Same with this while-loop as for authorName
          while (String.IsNullOrEmpty(message))
          {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nYou have to provide a message!");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            message = Console.ReadLine().Trim();
          }

          // Calling the AddPost method from PostList class
          list.AddPost(posts, authorName, message);
          Console.ForegroundColor = ConsoleColor.Gray;
          return true;
        case "3":
          Console.Clear();

          // Checking if there are posts in the JSON file
          if (posts.Count > 0)
          {
            list.PrintPosts(posts);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nPlease provide the number of the post you want to delete:");

            // This will check if the input was a int or not and return true or false
            string postIndexInput = Console.ReadLine();
            int postIndex;
            Console.ForegroundColor = ConsoleColor.Gray;
            bool isANumber = int.TryParse(postIndexInput, out postIndex);

            // If it's not a int the user is sent back to main menu
            if (!isANumber)
            { 
              Console.Clear();
              Console.ForegroundColor = ConsoleColor.Yellow;
              Console.WriteLine("\nOh dear, your input wasn't a number, try again please\n");
              Console.ForegroundColor = ConsoleColor.Gray;
            }
            else 
            {
              // This stores the input and makes sure it's an integer and adds 1 to make it match index on array
              int inputCheck = Convert.ToInt32(postIndexInput) + 1;

              // If the input is lower than number of posts the user is sent to main menu
              if (inputCheck > posts.Count)
              {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nOh dear, your input wasn't a valid post number, try again please\n");
                Console.ForegroundColor = ConsoleColor.Gray;
              }
              else
              { // If the input is valid a the user is asked to confirm the deletion of a post
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Are you sure you want to remove post {postIndex}? Y/n:\n");
                string answer = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Gray;

                // If the user enters anything else then listed answer s/he will be sent to main menu
                if (answer == "y" || answer == "Y" || answer == "Yes" || answer == "yes")
                { // If user confirmed to delete the DeletePost method is called
                  list.DeletePost(postIndex, posts);
                }
                else
                { // Conformation message that nothing was deleted
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
          { // If there are no posts this message is shown
            Console.WriteLine("\nNo posts in the guestbook yet, want to add one? \n");
          }

          return true;
        case "0": // Case that is run if users choose to exit application
          Console.Clear();
          Console.WriteLine("\nThanks for visiting. Goodbye!\n");
          Environment.Exit(0);
          return false;


        default: // If the users input is invalid this is shown
          Console.Clear();
          Console.WriteLine("\nNot a valid input. Please try again");
          return true;
      }
    }

    // Welcome text that is shown when the application is started
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
