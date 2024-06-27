namespace Todo.Logic.Domain.Models.Base {
    public abstract class BaseEntityDto<TKey>
        where TKey : IEquatable<TKey> {
        private Dictionary<string, object> _metadata;

        public TKey Id { get; set; }

        public void SetMetadata<T>(string key, T value) {
            if (_metadata == null) {
                _metadata = new Dictionary<string, object>();
            }

            if (_metadata.ContainsKey(key)) {
                _metadata[key] = value;
            }
            else {
                _metadata.Add(key, value);
            }
        }

        public T GetMetadata<T>(string key) {
            if (_metadata == null) {
                return default(T);
            }

            if (!_metadata.ContainsKey(key)) {
                return default(T);
            }

            try {
                return (T)Convert.ChangeType(_metadata[key], typeof(T));
            }
            catch {
                return default(T);
            }
        }
    }
}