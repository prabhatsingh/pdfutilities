namespace CommonUtilities.Models
{
    public enum FileType { PDF, IMAGE, XPS, COMBINED, UNSUPPORTED }
    public enum ActionType
    {
        SPLIT, MERGE, ROTATE,
        OPTIMIZE, CONVERT, IMAGETOPDF,
        PDFTOIMAGE, COMBINE, XPSTOIMAGE, XPSTOPDF,
        ROTATECW = 90, ROTATECCW = 270, ROTATE180 = 180,
    }
}
