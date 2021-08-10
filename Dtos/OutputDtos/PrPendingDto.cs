namespace ner_pr_api.Dtos.OutputDtos
{
    public class PrPendingDto
    {
        public string PrYear { get; set; }

        public int DeptCode { get; set; }

        public string DeptSymbol { get; set; }

        public string Division { get; set; }

        public string PrStatus { get; set; }

        public int PrPending { get; set; } = 0;

        public int PrCanceled { get; set; } = 0;

        public int PrChecked { get; set; } = 0;

        public int PrApproved { get; set; } = 0;

        public int PrCompleted { get; set; } = 0;

        public int PrAll { get; set; } = 0;
        public double PrKpi { get; set; } = 0;
        public double AvgKpi { get; set; } = 0;
    }
}