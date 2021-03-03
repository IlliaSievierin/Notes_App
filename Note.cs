using System;
using System.Drawing;

namespace Notes_App
{
	public class Note
	{
		public int id { get; set; }

		public string Text { get; set; }

		public Color color { get; set; }

		public string Title { get; set; }

		public Note(int id, string Text, string Title, Color color)
		{
			this.id = id;
			this.Text = Text;
			this.Title = Title;
			this.color = color;
		}
	}
}

