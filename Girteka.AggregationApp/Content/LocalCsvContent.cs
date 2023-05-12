// namespace Girteka.AggregationApp.Content;
//
// public class LocalCsvContent : IContent
// {
//     private readonly string _filePath;
//
//     public LocalCsvContent(string filePath)
//     {
//         _filePath = filePath;
//     }
//
//     public async Task GetCsvContent(string fileName)
//     {
//         string filePath = @"/Users/sabakoghuashvili/Desktop/Data/2022-05.csv";
//
//         try
//         {
//             using (StreamReader sr = new StreamReader(filePath))
//             {
//                 string line;
//                 while ((line = sr.ReadLine()) != null)
//                 {
//                     Console.WriteLine(line);
//                 }
//             }
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine("Error: " + e.Message);
//         }
//     }
//
//     public Task<Stream> DownloadCsvAsync(string fileName)
//     {
//         throw new NotImplementedException();
//     }
// }