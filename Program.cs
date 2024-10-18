using System.Net;
using System.Net.Sockets;
using System.Text;

var ip = IPAddress.Parse("192.168.100.115");
var port = 27001;
var endPoint = new IPEndPoint(ip, port);

var path = @"C:\Users\User\Desktop\images.jpg";

FileInfo file = new FileInfo(path);

using var client = new TcpClient();
client.Connect(endPoint);

using var stream = client.GetStream();


var fileNameBytes = Encoding.UTF8.GetBytes(file.Name);
stream.Write(fileNameBytes, 0, fileNameBytes.Length);

var fileLengthBytes = Encoding.UTF8.GetBytes(file.Length.ToString());
stream.Write(fileLengthBytes, 0, fileLengthBytes.Length);

using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
var buffer = new byte[5000];
int bytesRead;

while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
{
    stream.Write(buffer, 0, bytesRead);
}
Console.WriteLine("File sent");
Console.ReadKey();