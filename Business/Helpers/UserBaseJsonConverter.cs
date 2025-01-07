using Business_Library.Models;
using System.Text.Json.Serialization;
using System.Text.Json;


public class UserBaseJsonConverter : JsonConverter<UserBase>
{
    public override UserBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        
        return JsonSerializer.Deserialize<UserBase>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, UserBase value, JsonSerializerOptions options)
    {
        
        JsonSerializer.Serialize(writer, value, options);
    }
}