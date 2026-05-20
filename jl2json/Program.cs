using System.Text;
using System.Text.Json;

await using var stdin = Console.OpenStandardInput();

// Reads one line at a time from stdin.
// This does NOT buffer the whole input in memory.
using var reader = new StreamReader(
    stdin,
    new UTF8Encoding(encoderShouldEmitUTF8Identifier: false),
    detectEncodingFromByteOrderMarks: false,
    bufferSize: 4096,
    leaveOpen: false);

using var stdout = Console.OpenStandardOutput();

using var jsonWriter = new Utf8JsonWriter(stdout, new JsonWriterOptions
{
    Indented = false
});

jsonWriter.WriteStartArray();

string? line;
while ((line = await reader.ReadLineAsync()) is not null)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        continue; // skip blank lines
    }

    // Treat each line as a complete JSON value and append it to the array.
    // Validation stays ON so bad input fails immediately.
    jsonWriter.WriteRawValue(line, skipInputValidation: false);

    // Flush periodically so output is streamed to disk incrementally.
    jsonWriter.Flush();
}

jsonWriter.WriteEndArray();
jsonWriter.Flush();

return 0;