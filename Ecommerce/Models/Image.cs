namespace Ecommerce.Models;

public class Image
{
    public string Filename { get; set; }
    public string Base64 { get; set; }

    public async Task SaveToFile(string path) =>
        await File.WriteAllBytesAsync(path, Convert.FromBase64String(
            Base64.Remove(0, 23)));
    // remove "data:image/jpeg;base64,"
}