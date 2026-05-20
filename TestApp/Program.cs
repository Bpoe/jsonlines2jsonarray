using var writer = new StreamWriter(Console.OpenStandardOutput());
writer.AutoFlush = false;

var counter = 0;
while (counter < 9999999)
{
    writer.Write("{ \"Hello\": \"World!\", \"iteration\": ");
    writer.Write(counter);
    writer.WriteLine(" }");
    counter++;
}

writer.Flush();