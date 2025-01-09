using Business_Library.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Business_Library.Helpers;
/// <summary>
/// I used chat to generate this converter for me
/// Custom JSON converter for the <see cref="UserBase"/> class.
/// Provides serialization and deserialization logic for <see cref="UserBase"/> objects.
/// </summary>
public class UserBaseJsonConverter : JsonConverter<UserBase>
{
    /// <summary>
    /// Reads and deserializes a <see cref="UserBase"/> object from JSON.
    /// </summary>
    /// <param name="reader">The <see cref="Utf8JsonReader"/> to read JSON data from.</param>
    /// <param name="typeToConvert">The type of the object to convert (expected to be <see cref="UserBase"/>).</param>
    /// <param name="options">Serialization options to use during deserialization.</param>
    /// <returns>A deserialized <see cref="UserBase"/> object.</returns>
    public override UserBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<UserBase>(ref reader, options);
    }

    /// <summary>
    /// Writes a <see cref="UserBase"/> object to JSON.
    /// </summary>
    /// <param name="writer">The <see cref="Utf8JsonWriter"/> to write JSON data to.</param>
    /// <param name="value">The <see cref="UserBase"/> object to serialize.</param>
    /// <param name="options">Serialization options to use during serialization.</param>
    public override void Write(Utf8JsonWriter writer, UserBase value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}