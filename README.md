# ExtendedStringBuilder
Thread safe .Net class library that facilitate logging text to a file.
The library can handle big files with ease, and have been tested with 10GB of data with no issues encoutered.

How to use it? 
Just like the standard StringBuilder of .Net framework except that when you append any text to the builder it will write it directly to the file.

```c#
ExtendedStringBuilder.ExStringBuilder logger = new ExtendedStringBuilder.ExStringBuilder("log.txt");

// simulating multithreading logging
List<string> list = new List<string>();
for (int i = 0; i < 10000000; i++)
list.Add($"Testing {i.ToString()}");

Parallel.ForEach(list, str =>
{
// AppendingLine Just like the standard StringBuilder
    logger.AppendLine(str);
});
//Don't forget to close the logger inorder to dispose resources, and realase filelock
logger.Close();           
 ```
