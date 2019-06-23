using CommonUtilities.Models;
using System.Collections.Generic;
using System.Linq;

namespace CommonUtilities
{
    public class ActionUtilities
    {
        public class ActionInfo
        {
            public ActionType actionType { get; set; }
            public FileType actionTargetType
            {
                get
                {
                    return actionTarget.Any(f => f.FType == FileType.PDF) ? FileType.PDF : FileType.IMAGE;
                }
            }
            public List<FileUtilities.FileDetails> actionTarget { get; set; }
        }
    }
}
