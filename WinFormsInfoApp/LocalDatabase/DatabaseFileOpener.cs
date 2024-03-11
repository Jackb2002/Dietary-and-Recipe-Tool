using System.Diagnostics;

namespace WinFormsInfoApp.LocalDatabase
{
    internal class DatabaseFileOpener
    {
        private readonly string Path; // Path to the database file

        public DatabaseFileOpener(string path)
        {
            Path = path;
        }

        public DatabaseManager CreateOrOpen()
        {
            if (File.Exists(Path))
            {
                // File already exists, open it
                return new DatabaseManager(Path);
            }
            else
            {
                // File does not exist, create it
                using (FileStream fileStream = File.Create(Path))
                {
                    Debug.WriteLine("Created new database file at " + Path);
                }
                // Return a DatabaseManager instance associated with the newly created file
                return new DatabaseManager(Path);
            }
        }

        public DatabaseManager ResetFile()
        {
            if (File.Exists(Path))
            {
                // File exists, delete it
                File.Delete(Path);
            }
            // Create a new file
            using (FileStream fileStream = File.Create(Path))
            {
                Debug.WriteLine("Created new database file at " + Path);
            }
            // Return a DatabaseManager instance associated with the new file
            return new DatabaseManager(Path);
        }
    }
}
