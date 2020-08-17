using AltV.Net;

namespace models.Popup
{
    public class PopupLabelDto : PopupItemDto
    {
        public string Name { get; set; }

        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();
            writer.Name("id");
            writer.Value(2);
            writer.Name("name");
            writer.Value(Name);
            writer.EndObject();
        }
    }
}