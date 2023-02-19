using Kula.CodeAnalysis.Syntax;

var testFile = Path.Join(Environment.CurrentDirectory, "..", "..", "..", "test.kula");

var parser = new Parser();
parser.ParseFile(testFile);

Console.Read();

