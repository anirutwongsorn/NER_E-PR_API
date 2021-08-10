namespace ner_pr_api.Dtos.InputDtos
{
    public class CancelPrDto
    {
        public string prNo { get; set; }
        public string oldStatus { get; set; }

        public string newStatus { get; set; }

        public string operationName { get; set; }

    }
}