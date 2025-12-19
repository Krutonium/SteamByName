namespace SteamByName;

class Program
{
    // Users Home Directory + Steam...
    private static readonly string SteamApps =
        $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.local/share/Steam/steamapps";

    static void Main(string[] args)
    {
        Console.WriteLine("Steam By Name now creating Symlinks...");
        string[] LibraryFile = File.ReadAllLines(Path.Combine(SteamApps, "libraryfolders.vdf"));
        List<string> LibraryLocations = new List<string>();
        foreach (string library in LibraryFile)
        {
            if (library.Contains("path"))
            {
                string[] pathCombo = library.Split('\t');
                string path = pathCombo[4]; // Gets the Path which is, by pure chance, 4
                path = path.Replace("\"", "");
                LibraryLocations.Add(Path.Combine(path, "steamapps"));
#if  DEBUG
                Console.WriteLine(path);
#endif
                
            }
        }
        
        // Make the By-Name folder
        string compatDataByName = Path.Combine(SteamApps, "compatdata", "by-name");
        if (!Directory.Exists(compatDataByName))
        {
            Directory.CreateDirectory(compatDataByName);
        }
        else
        {
            foreach (var file in Directory.GetDirectories(compatDataByName))
            {
                File.Delete(file);
            }
        }
        
        foreach (var location in LibraryLocations)
        {
            List<string> Files = Directory.GetFiles(location, "appmanifest_*.acf", SearchOption.TopDirectoryOnly)
                .ToList();
            foreach (var file in Files)
            {
                String[] acfFile = File.ReadAllLines(file);
                string ID = "";
                string NAME = "";
                foreach (string acf in acfFile)
                {
                    if (acf.Contains("appid"))
                    {
                        string[] appIdBundle = acf.Split('\t');
                        string appId = appIdBundle[3];
                        appId = appId.Replace("\"", "");
                        if (appId == "dlcappid") // DLC APP ID - We can safely ignore this.
                        {
                            continue;
                        }

                        ID = appId;
#if DEBUG
                        Console.WriteLine($"Found {ID}");
#endif            
                    }

                    if (acf.Contains("name"))
                    {
                        string[] nameBundle = acf.Split('\t');
                        string nameString = nameBundle[3];
                        nameString = nameString.Replace("\"", "");
                        nameString = nameString.Replace("/", "_");
                        nameString = nameString.Replace("\\", "_");
                        NAME = nameString;
#if DEBUG
                        Console.WriteLine($"Found {nameString}"); 
#endif
                    }
                }
                string compatData = Path.Combine(location, "compatdata");
                string compatDataGamePath = Path.Combine(compatData, ID);
                string byNamePath = (Path.Combine(compatDataByName, NAME));

                if (!Directory.Exists(compatDataGamePath))
                {
                    continue;
                }
                File.CreateSymbolicLink(byNamePath, compatDataGamePath);
            }
        }
        Console.WriteLine("Finished!");
    }
}