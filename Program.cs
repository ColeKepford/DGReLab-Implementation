using DGReLab_Implementation;
using System.Xml;

bool IsRunning = true;
String SWISSPROT = "./res/SwissProt.xml";
String NASA = "./res/Nasa.xml";
String AUCTIONDATA = "./res/Auction_Data.xml";
string urlString = "";
var utility = new Utility();

while (IsRunning)
{
    InputDatabase();
    InputQuery();
}

void InputDatabase()
{
    Console.WriteLine("\nSelect which database to load.\n1. Swiss Protein Database"
        + "\n2. Nasa Database" + "\n3. Auction Database");
    string input = Console.ReadLine();
    if (input == null || input == string.Empty)
    {
        input = RedoInput();
    }
    input = input.Trim();
    CheckForEnd(input);
    if (input != "1" && input != "2" && input != "3" && input!= "END")
    {
        Console.WriteLine("\nInvalid Input please try again.");
        InputDatabase();
    }
    switch (input)
    {
        case "1":
            urlString = SWISSPROT;
            break;
        case "2":
            urlString = NASA;
            break;
        case "3":
            urlString = AUCTIONDATA; break;
        case "END":
            IsRunning = false;
            break;
    }
    Console.WriteLine("\nLoading Database...");
    utility.LoadDocument(urlString);
}

void InputQuery()
{
    Console.WriteLine("\nInput Query:");
    string query = Console.ReadLine();
    if (query == null || query == string.Empty)
    {
        query = RedoInput();
    }
    query = query.Trim();
    CheckForEnd(query);
    Console.WriteLine("\nQuery Running");
    var queryWatch = new System.Diagnostics.Stopwatch();
    queryWatch.Start();
    try 
    {
        var result = utility.Query(query);
        queryWatch.Stop();
        Console.WriteLine($"\nQuery Time: {queryWatch.ElapsedMilliseconds} ms");
        Console.WriteLine(result.Count + " Elements returned for query " + query);
    }
    catch(Exception e )
    {
        Console.WriteLine("\nInvalid Query or no matching path found. Please Input again");
        InputQuery();
    }

    Console.WriteLine("\nSelect from the following.\n1. Input Another Query.\nEnter to select another database."
        + "\nEnd Program by entering END");
    string input = Console.ReadLine();
    if (input == null)
    {
        input = RedoInput();
    }
    switch (input)
    {
        case "1":
            InputQuery();
            break;
        case "END":
            IsRunning = false;
            break;
    }
}

static string RedoInput()
{
    Console.WriteLine("Invalid Input. Please Input Again");
    string input = Console.ReadLine();
    if (input == null || input == string.Empty)
    {
        RedoInput();
    }
    return input.Trim();
}

void CheckForEnd(string input)
{
    if (input == "END")
    {
        IsRunning = false;
    }
}

void CheckForEqual(string url, string elementName)
{
    XmlDocument doc = new XmlDocument();
    doc.Load(url);
    var list = doc.GetElementsByTagName(elementName);
    Console.WriteLine(list.Count);
}