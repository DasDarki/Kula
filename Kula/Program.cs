using Kula.CodeAnalysis.Syntax;

var testFile = Path.Join(Environment.CurrentDirectory, "..", "..", "..", "test.kula");
var testContent = File.ReadAllText(testFile);

var lexer = new Lexer(testContent, testFile);
var tokens = lexer.Tokenize();

foreach (var token in tokens)
    Console.WriteLine(token);