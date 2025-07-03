using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Abstract
{
    public interface IToxicityService
    {
        Task<(bool IsToxic, float Score)> AnalyzeCommentAsync(string commentText);
    }
}
