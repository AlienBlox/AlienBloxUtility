using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using static BCrypt.Net.BCrypt;

namespace AlienBloxUtility.Utilities.DataStructures
{
    /// <summary>
    /// A group chat that you can use to chat right from the debug console
    /// </summary>
    public class Groupchat : IDisposable
    {
        /// <summary>
        /// The password hash to the groupchat
        /// </summary>
        private string Password;

        public List<Player> Members;

        public BinaryWriter ConnectedWriter;

        private bool disposedValue;

        // Method to create a group chat with a password
        public void CreateGroupChat(string password)
        {
            // Hash the password using BCrypt
            Password = HashPassword(password);
            Console.WriteLine("Group chat created with a secure password.");
        }

        // Method to verify the password for the group chat
        public bool VerifyPassword(string enteredPassword)
        {
            // Check if the entered password matches the stored hashed password
            if (Verify(enteredPassword, Password))
            {
                Console.WriteLine("Access granted! You can join the chat.");
                return true;
            }
            else
            {
                Console.WriteLine("Incorrect password. Access denied.");
                return false;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}