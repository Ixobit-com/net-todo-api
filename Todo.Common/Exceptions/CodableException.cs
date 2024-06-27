namespace Todo.Common.Exceptions {
    public class CodableException : Exception {
        public string Code { get; set; }

        public CodableException(string code, string message)
            : base(message) {
            Code = code;
        }
    }
}