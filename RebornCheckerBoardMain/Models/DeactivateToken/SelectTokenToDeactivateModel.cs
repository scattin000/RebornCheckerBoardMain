using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RebornCheckerBoardMain.Models.DeactivateToken
{
    public class SelectTokenToDeactivateModel
    {
        public SelectListItem[] TokensToChooseFrom { get; set; }

        public SelectListItem[] ReasonChoices { get; set; }

        public string SelectedTokenCode { get; set; }

        public string SelectedReason { get; set; }
    }
}