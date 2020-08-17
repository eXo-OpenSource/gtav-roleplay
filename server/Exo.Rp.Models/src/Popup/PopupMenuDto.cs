using System.Collections.Generic;
using AltV.Net;

namespace models.Popup
{
    public class PopupMenuDto : IWritable
    {

        public string Title { get; set; }
        public List<PopupItemDto> Items { get; set; }

        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("title");
            writer.Value(Title);

            writer.Name("items");
            writer.BeginArray();
            foreach (var item in Items)
            {
                item?.OnWrite(writer);
            }
            writer.EndArray();

            writer.EndObject();
        }
    }
}