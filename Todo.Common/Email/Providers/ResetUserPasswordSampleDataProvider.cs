using Todo.Common.Email.Data;

namespace Todo.Common.Email.Providers {
    public class ResetUserPasswordSampleDataProvider : ISampleDataProvider {
        public object GetSampleData() {
            return new ResetUserPasswordEmailData {
                Token = "23er72xwsfp1b2d86fh7g9io5pyr62wb212czwg6z8olzry48m1pi4lv14h6puzoslf3e75iyp54b5v3p1yubi2j64w1qhtqc2t7"
            };
        }
    }
}