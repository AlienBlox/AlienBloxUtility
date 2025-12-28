using BCrypt.Net;
using static BCrypt.Net.BCrypt;

namespace AlienBloxUtility.Utilities.DataStructures
{
    /// <summary>
    /// A group chat that you can use to chat right from the debug console
    /// </summary>
    public class Groupchat
    {
        /// <summary>
        /// The password hash to the groupchat
        /// </summary>
        private string Password;

        // Hash the password when the user creates an account or sets a password
        public static string CheckPassword(string password)
        {
            // bcrypt automatically generates a salt and hashes the password
            return HashPassword(password);
        }

        // Verify the password when the user logs in
        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // bcrypt compares the entered password with the stored hash
            return Verify(enteredPassword, storedHash);
        }
    }
}