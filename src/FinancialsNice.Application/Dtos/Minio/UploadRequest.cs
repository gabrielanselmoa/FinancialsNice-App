using System.ComponentModel.DataAnnotations;

namespace FinancialsNice.Application.Dtos.Minio;

public record UploadRequest([Required] string FileName, [Required] string ContentType);