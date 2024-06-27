using Todo.Common.Email.Data;

namespace Todo.Common.Email.Providers {
    public class CreateNewClientSampleDataProvider : ISampleDataProvider {
        public object GetSampleData() {
            return new CreateNewClientEmailData {
                Name = "Test Client",
                Key = "28d0zg5jbwy4edxi3hnuvw4j9kb3c1ex",
                Secret = "xb6ib94rbj6ox8rhd7n36u5125qxefs2l83shsej5tmioa23nttzmkz86m1h3n8bv79lfen5mie91hkhjry9g585r1kk906y"
            };
        }
    }
}