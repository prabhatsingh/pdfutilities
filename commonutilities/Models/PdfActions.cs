using System.Linq;

namespace Libraries.CommonUtilities.Models
{
    public class PdfActions
    {
        private readonly Interfaces.IPdfActions _pdfActions;
        public PdfActions(Interfaces.IPdfActions pdfActions)
        {
            _pdfActions = pdfActions;
        }

        public void Run(ActionUtilities.ActionInfo actinf)
        {
            switch (actinf.ActionType)
            {
                case ActionType.SPLIT:
                    actinf.ActionTarget.ForEach(f => _pdfActions.Split(f.Filepath));
                    break;
                case ActionType.LOOKSCANNED:
                    actinf.ActionTarget.ForEach(f => _pdfActions.ConvertToScanned(f.Filepath));
                    break;
                case ActionType.PDFTOIMAGE:
                    actinf.ActionTarget.ForEach(f => _pdfActions.PdfToImage(f.Filepath));
                    break;
                case ActionType.MERGE:
                    _pdfActions.Merge(actinf.ActionTarget.Select(f => f.Filepath).ToList());
                    break;
                case ActionType.IMAGETOPDF:
                    _pdfActions.ImageToPdf(actinf.ActionTarget.Select(f => f.Filepath).ToList());
                    break;
                case ActionType.COMBINE:
                    _pdfActions.Combine(actinf.ActionTarget.Select(f => f.Filepath).ToList());
                    break;
                case ActionType.ROTATE:
                case ActionType.ROTATECW:
                case ActionType.ROTATECCW:
                case ActionType.ROTATE180:
                    actinf.ActionTarget.ForEach(f => _pdfActions.Rotate(f.Filepath, (float)actinf.ActionType));
                    break;

            }
        }
    }
}
