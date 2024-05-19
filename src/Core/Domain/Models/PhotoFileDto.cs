namespace Domain.Models;

public class PhotoFileDto
{
    public string FileName { get; set; } = null!;
    public byte[] Content { get; set; } = null!;
}
