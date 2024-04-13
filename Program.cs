using System;
using FileDbGroup.App.Brokers.Loggings;
using FileDbGroup.App.Brokers.Storages;
using FileDbGroup.App.Services.Identities;
using FileDbGroup.App.Services.UserProcessing;
using FileDbGroup.App.Services.UserServices;

internal class Program
{
    private static void Main(string[] args)
    {
        IUserProcessingService userProcessingService = InitializeServices();

        string userChoice;
        do
        {
            PrintMenu();
            Console.Write("Enter your choice: ");
            userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "1":
                    Console.Clear();
                    Console.Write("Enter your name: ");
                    string userName = Console.ReadLine();
                    userProcessingService.CreateNewUser(userName);
                    break;

                case "2":
                    {
                        Console.Clear();
                        userProcessingService.DisplayUsers();
                    }
                    break;

                case "3":
                    {
                        Console.Clear();
                        Console.WriteLine("Enter an ID which you want to delete");
                        Console.Write("Enter ID: ");
                        int deleteWithId = Convert.ToInt32(Console.ReadLine());
                        userProcessingService.DeleteUser(deleteWithId);
                    }
                    break;

                case "4":
                    {
                        Console.Clear();
                        Console.WriteLine("Enter an id which you want to edit");
                        Console.Write("Enter an ID: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter new name: ");
                        string newName = Console.ReadLine();
                        userProcessingService.UpdateUser(id, newName);
                    }
                    break;

                case "0": break;

                default:
                    Console.WriteLine("You entered wrong input, please try again!");
                    break;
            }
        }
        while (userChoice != "0");
        Console.Clear();
        Console.WriteLine("The app has been finished, thanks bye!");
    }

    private static IUserProcessingService InitializeServices()
    {
        IStoragesBroker storagesBroker = new FileStorageBroker();
        ILoggingBroker loggingBroker = new LoggingBroker();
        IUserService userService = new UserService(storagesBroker, loggingBroker);
        IIdentityService identityService = IdentityService.GetIdentityService();
        IUserProcessingService userProcessingService = new UserProcessingService(userService, identityService);

        return userProcessingService;
    }

    private static void PrintMenu()
    {
        Console.WriteLine("1.Create User");
        Console.WriteLine("2.Display User");
        Console.WriteLine("3.Delete User by ID");
        Console.WriteLine("4.Update User by ID");
        Console.WriteLine("0.Exit");
    }
}