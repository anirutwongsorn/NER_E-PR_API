using ner_api.Entities;

namespace ner_pr_api.Dtos.OutputDtos
{
    public class NerDepartmentDto
    {
        public string DeptCode { get; set; }
        public string DeptSymbol { get; set; }
        public string Division { get; set; }

        public static NerDepartmentDto FromTbDepartment(TbDepartment model) => new NerDepartmentDto
        {
            DeptCode = model.DeptCode,
            DeptSymbol = model.DeptSymbol,
            Division = model.Division,
        };

    }
}