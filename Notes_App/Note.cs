using System;


namespace Notes_App
{
	[Serializable]
	public class Note
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Text { get; set; }

		public Note()
        {

        }
		public Note(int id, string Text, string Title)
		{
			this.Id = id;
			this.Text = Text;
			this.Title = Title;
		}
        public override string ToString()
        {
			return Title;
        }
    }
}

