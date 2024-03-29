namespace Exo.Rp.Serialization
{
    public abstract class Serializable<T> where T : Serializable<T>, new()
    {
        public string GetClassName()
        {
            return typeof(T).Name;
        }

        public abstract string SerializeObject();

        public abstract T DeserializeObject(string value);

        public static T Deserialize(string value)
        {
            return new T().DeserializeObject(value);
        }

        public override string ToString()
        {
            return SerializeObject();
        }
    }
}