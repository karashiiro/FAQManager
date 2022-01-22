using Newtonsoft.Json;
using System;

namespace FAQManager
{
    public class FaqStore
    {
        private readonly IFaqBackend _backend;

        public FaqStore(IFaqBackend backend)
        {
            _backend = backend;
        }

        public void Save<T>(string key, T data)
        {
            var type = typeof(T);
            var serializedData = JsonConvert.SerializeObject(data);
            var record = new FaqRecord(type, serializedData);
            _backend.Set(key, JsonConvert.SerializeObject(record));
        }

        public T Load<T>(string key)
        {
            var (type, data) = JsonConvert.DeserializeObject<FaqRecord>(_backend.Get(key));
            if (!typeof(T).IsAssignableFrom(type))
            {
                throw new InvalidCastException($"Unable to deserialize object of type {type} into a receiver of type {typeof(T)}");
            }

            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
