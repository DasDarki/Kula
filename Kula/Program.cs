using Kula.LuaGen;
using Kula.LuaGen.Generatables;

var builder = LuaSourceBuilder.Create();

builder.LocalVar("a", 16);

var gender = builder.LocalVar("Gender", 
    LuaSourceBuilder
        .CreateTable()
        .AddKeyValue("Male", 0)
        .AddKeyValue("Female", 1)
        .AddKeyValue("Divers", 2)
        .Build()
);

builder
    .Function()
    .Local()
    .Name("getGender")
    .AddParameter("id")
    .DescribeBody(scope =>
    {
        scope.ForEach("k", "v", gender, forScope =>
        {
            forScope.If("v == id", ifScope =>
            {
                ifScope.Return(LuaSourceBuilder.CreateCustomValue("k"));
            });
        });
        
        scope.Return(LuaValue.Nil);
    });

builder
    .Function()
    .Name("CreatePerson")
    .AddParameter("name")
    .AddParameter("age")
    .AddParameter("gender")
    .AddVarargs()
    .DescribeBody(body =>
    {
        var genderVar = body.LocalVar("genderName", LuaSourceBuilder.CreateCustomValue("getGender(gender)"));
        
        var ret = body.LocalVar("ret", LuaSourceBuilder.CreateTable().Build());
        
        body.AssignTable(ret, "name", LuaSourceBuilder.CreateCustomValue("name"));
        body.AssignTable(ret, "age", LuaSourceBuilder.CreateCustomValue("age"));
        body.AssignTable(ret, "gender", genderVar);

        body.AssignTable(ret, "meta", LuaSourceBuilder.CreateCustomValue("{...}"));
        
        body.Return(ret);
    });

Console.WriteLine(builder.Generate());