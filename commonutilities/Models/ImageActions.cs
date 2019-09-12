using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.CommonUtilities.Models
{
    public class ImageActions
    {
        private readonly Interfaces.IImageActions _imageActions;
        public ImageActions(Interfaces.IImageActions imageActions)
        {
            _imageActions = imageActions;
        }

        public object Run(ActionUtilities.ActionInfo actinf)
        {
            switch (actinf.ActionType)
            {
                case ActionType.OPTIMIZE:
                    actinf.ActionTarget.ForEach(f => _imageActions.Optimize(f.Filepath, actinf.resolution));
                    break;
                case ActionType.ROTATE:
                case ActionType.ROTATECW:
                case ActionType.ROTATECCW:
                case ActionType.ROTATE180:
                    actinf.ActionTarget.ForEach(f => _imageActions.Rotate(f.Filepath, (float)actinf.ActionType));
                    break;
                case ActionType.ROTATEOTH:
                    if (actinf.retobj)
                        return _imageActions.Rotate(actinf.ActionTarget.First().Filepath, actinf.rotation, actinf.retobj);
                    else
                        actinf.ActionTarget.ForEach(f => _imageActions.Rotate(f.Filepath, actinf.rotation));
                    break;
            }

            return null;
        }
    }
}
