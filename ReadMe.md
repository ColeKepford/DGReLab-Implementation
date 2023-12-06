Instructions to Run

Run from Repo
1. Clone Repository
2. Paste res folder containing .xml files into source directory
3. enter "dotnet run"


Run from .exe file
1. Ensure res folder containing .xml files is on same path as .exe file
2. Run .exe


Query Instructions
1. Queries will return all elements with the same tag name and Parent => child heirarchy
2. "//" Specifies the root node
3. Start queries with "//"
4. Every additional child node appended to query must be divided by "/"
5. Example queries that all return the same results
    //Level2
    //Level1/Level2
    //Root/Level1/Level2
