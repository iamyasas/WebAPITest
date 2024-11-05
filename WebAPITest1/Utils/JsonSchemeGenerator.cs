using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace WebAPITest1.Utils;

public class JsonSchemeGenerator
{
    public static string GenerateJsonSchema(Type type)
    {
        var schemaGenerator = new JSchemaGenerator
        {
            DefaultRequired = Required.Always,
        };

        // Generate schema for the provided type
        JSchema schema = schemaGenerator.Generate(type);
        schema.AllowAdditionalProperties = false;

        //Console.WriteLine("===Schema: " + schema.ToString() + "===");

        return schema.ToString();
    }
}
