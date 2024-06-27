using Todo.Logic.Domain.Models.Base;

namespace Todo.Logic.Domain.Models {
    public class EmailTemplateDto : BaseEntityDto<int> {
        public string SubjectFormat { get; set; }
        public string BodyFormat { get; set; }
    }
}