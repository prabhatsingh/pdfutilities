namespace CommonUtilities.Models
{
    public enum FileType
    {
        PDF,
        IMAGE,
        UNKNOWN
    }

    public enum FileFormat
    {
        PDF,
        TXT,
        JPEG,
        GIF,
        PNG,
        JPG,
        BMP,
        TIFF
    }

    public enum ActionType
    {
        SPLIT,
        MERGE,
        ROTATE,
        OPTIMIZE,
        CONVERT,
        IMAGETOPDF,
        PDFTOIMAGE
    }
}
