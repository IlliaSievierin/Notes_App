using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Notes_App;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Android.Content;
using System.Runtime.Serialization;


namespace Notes_App
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        List<Note> Notes = new List<Note>();

        public static int countNotes = 0;

        private Note note;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            ListView listViewNotes = FindViewById<ListView>(Resource.Id.listView1);
            Button   button        = FindViewById<Button>(Resource.Id.button1);
            

            int id = 1;
            while(id<1000)
            {
                var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Note" + id);
                if (File.Exists(backingFile))
                {
                    Note note = SerializationNote.LoadFromBinaryFile("Note" + id);
                    Notes.Add(note);
                    countNotes = id;
                }
                id++;
            }
         
            ArrayAdapter<Note> adapter = new ArrayAdapter<Note>(this,Android.Resource.Layout.SimpleListItem1,Notes);
            listViewNotes.Adapter = adapter;

            listViewNotes.ItemClick += (s, e) =>
            {
                note = Notes[e.Position];
                Intent intent = new Intent(this, typeof(SingleNote));
                intent.PutExtra("Id", note.Id);
                intent.PutExtra("Title", note.Title);
                intent.PutExtra("Text", note.Text);
                StartActivity(intent);
            };

            button.Click += (s, e) =>
            { 
                Intent intent = new Intent(this, typeof(SingleNote));
                StartActivity(intent);
            };
            
        }
      
       
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}