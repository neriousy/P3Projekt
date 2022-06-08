using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace SerializacjaGniazdaAsync
{
    class Formatter
    {
        private BinaryFormatter bt;
        public Formatter()
        {
            bt = new BinaryFormatter();
        }
        public Formatter(BinaryFormatter formatter)
        {
            bt = formatter;
        }

        public void Serialize<T>(Stream s, T data)
        {
            bt.Serialize(s, data);
        }
        public T Deserialize<T>(Stream s)
        {
            return (T)bt.Deserialize(s);
        }
        public Task SerializeAsync<T>(Stream s, T data)
        {
            return Task.Factory.StartNew(() => Serialize(s, data));
        }

        public Task<T> DeserializeAsync<T>(Stream s)
        {
            return Task<T>.Factory.StartNew(() => Deserialize<T>(s));
        }
    }
}
