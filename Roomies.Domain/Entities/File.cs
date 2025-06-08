using Roomies.Domain.Interfaces;

namespace Roomies.Domain.Entities;

public class File : IAuditableEntity
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public byte[] Content { get; set; }
    public string Extension { get; set; }
    public string MimeType { get; set; }
    public int FileSize { get; set; }
    public string Hash { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public ICollection<ExpenseAttachment> Attachments { get; set; } = new List<ExpenseAttachment>();
    public User Creator { get; set; }
}
