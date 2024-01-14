using System.Text.Json;
using Az.Subscriptions;
using Terminal.Gui;

Application.Run<ExampleWindow>();

// TODO
// 1. see if searching / filtering is possible
// 2. fix box height,
// 3. a box on the right that showed the rest of the info would be nice.
// 4. Update models

Application.Shutdown();

public class ExampleWindow : Window
{
    // TODO: check how cross platform this is, other OS's might not put the file in the home directory
    public static string Path = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.azure\\azureProfile.json";

    public ExampleWindow()
    {
        ColorScheme = Colors.Menu;
        Title = "Azure subscriptions (Ctrl+Q to quit)";

        string json = File.ReadAllText(Path);
        var account = JsonSerializer.Deserialize<AzureAccount>(json)
            ?? throw new Exception("didn't parse json");

        var list = new CustomList(account)
        {
            Height = Dim.Fill(10),
            Width = Dim.Fill(10),
            AllowsMarking = true,
            AllowsMultipleSelection = false,
            SelectedItem = account.subscriptions.ToList().FindIndex(x => x.isDefault)
        };

        list.MarkUnmarkRow();


        Add(list);
    }
}
