using Microsoft.AspNetCore.Http;

namespace appServicosCostura.API.Helpers;

public static class FotoUploadHelper
{
    private const long TamanhoMaximo = 2 * 1024 * 1024;

    private static readonly string[] TiposPermitidos =
    [
        "image/jpeg",
        "image/png",
        "image/webp"
    ];

    public static async Task<(byte[]? Foto, string? ContentType)>
        ConverterAsync(
            IFormFile? arquivo,
            CancellationToken cancellationToken = default)
    {
        if (arquivo is null || arquivo.Length == 0)
            return (null, null);

        if (arquivo.Length > TamanhoMaximo)
            throw new ArgumentException(
                "A foto deve possuir no máximo 2 MB.");

        var contentType = arquivo.ContentType.ToLowerInvariant();

        if (!TiposPermitidos.Contains(contentType))
            throw new ArgumentException(
                "Formato inválido. Envie JPG, PNG ou WebP.");

        await using var memoryStream = new MemoryStream();

        await arquivo.CopyToAsync(
            memoryStream,
            cancellationToken);

        return (memoryStream.ToArray(), contentType);
    }
}