using System;

namespace BlazorJinji.Shared.Condition
{
    public class PregnancyReportCondition
    {
        public DateTime? SelectedDate { get; set; }

        public string AppliDate { get; set; }

        public bool MultipleFlg { get; set; } = false;
    }
}