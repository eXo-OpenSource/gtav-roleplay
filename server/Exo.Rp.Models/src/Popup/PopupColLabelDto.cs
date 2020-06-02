using AltV.Net;

namespace models.Popup
{
	public class PopupColLabelDto : PopupItemDto
	{
		public string LeftText { get; set; }

		public string RightText { get; set; }

		public void OnWrite(IMValueWriter writer)
		{
			writer.BeginObject();
			writer.Name("id");
			writer.Value(3);
			writer.Name("left");
			writer.Value(LeftText);
			writer.Name("right");
			writer.Value(RightText);
			writer.EndObject();
		}
	}
}
