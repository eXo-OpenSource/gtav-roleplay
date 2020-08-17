using System.Collections.Generic;
using AltV.Net;

namespace models.Popup
{
    public class PopupButtonDto : PopupItemDto
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public string Callback { get; set; }

        public List<object> CallbackArgs { get; set; }

        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();
            writer.Name("id");
            writer.Value(1);
            writer.Name("name");
            writer.Value(Name);
            writer.Name("color");
            writer.Value(Color);
            writer.Name("callback");
            writer.Value(Callback);
            writer.Name("callbackArgs");
            writer.BeginArray();
            if (CallbackArgs != null)
                foreach (var arg in CallbackArgs)
                {
                    switch (arg)
                    {
                        case int i:
                            writer.Value(i);
                            break;
                        case string s:
                            writer.Value(s);
                            break;
                    }
                }

            writer.EndArray();
            writer.EndObject();
        }
    }
}