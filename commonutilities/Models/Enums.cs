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
        ROTATECW = 90,
        ROTATECCW = 270,
        ROTATE180 = 180,
        OPTIMIZE,
        CONVERT,
        IMAGETOPDF,
        PDFTOIMAGE
    }
}
