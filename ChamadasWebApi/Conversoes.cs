using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;


namespace ChamadasWebApi
{
    public class Conversoes
    {
        public string ObjectToXml<T>(T objeto)
        {
            MemoryStream oStream = new MemoryStream();
            XmlSerializer serializador = new XmlSerializer(typeof(T));
            serializador.Serialize(oStream, objeto);
            string retorno = Encoding.UTF8.GetString(oStream.ToArray());
            return retorno;
        }


        public T XmlToObject<T>(string Xml)
        {

            MemoryStream oStream = new MemoryStream(Encoding.UTF8.GetBytes(Xml));
            XmlSerializer deserializador = new XmlSerializer(typeof(T));
            T retorno = (T)deserializador.Deserialize(oStream);
            return retorno;
        }


        public string ObjectToJson<T>(T objeto)
        {
            MemoryStream oStream = new MemoryStream();
            DataContractJsonSerializer serializador = new DataContractJsonSerializer(typeof(T));
            serializador.WriteObject(oStream, objeto);
            string retorno = Encoding.UTF8.GetString(oStream.ToArray());
            return retorno;
        }

        public T JsonToObject<T> (string json)
        {

            MemoryStream oStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer deserializador = new DataContractJsonSerializer(typeof(T));
            T retorno = (T)deserializador.ReadObject(oStream);
            return retorno;

        }


    }
}




