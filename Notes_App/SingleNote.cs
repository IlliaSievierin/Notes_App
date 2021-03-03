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
using Android.Views;
using System.Reflection.Emit;
using System.Threading;


namespace Notes_App
{
    [Activity(Label = "SongleNote")]
    public class SingleNote : Activity
    {
        private Note     currentNode = new Note();
        private Button   saveButton ;
        private Button   deleteButton;
        private Button   backButton;
        private EditText titleView ;
        private EditText textView ;
     
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SingleNote);

            saveButton   = FindViewById<Button>(Resource.Id.button1);
            deleteButton = FindViewById<Button>(Resource.Id.button2);
            backButton   = FindViewById<Button>(Resource.Id.button3);
            titleView    = FindViewById<EditText>(Resource.Id.autoCompleteTextView1);
            textView     = FindViewById<EditText>(Resource.Id.autoCompleteTextView2);
            

            currentNode.Id    = Intent.GetIntExtra("Id", 0);
            currentNode.Title = Intent.GetStringExtra("Title");
            currentNode.Text  = Intent.GetStringExtra("Text");

          
            if (currentNode.Id != 0)
            {
                titleView.Text = currentNode.Title;
                textView.Text  = currentNode.Text;
            }

            backButton.Click += (o, e) =>
            {
                ToMainPage();
            };

            deleteButton.Click += (o, e) =>
             {
                 DeleteNote();
                 ToMainPage();
             };
            
            saveButton.Click += (o, e) =>
            {
                if(titleView.Text!="" && textView.Text !="")
                {
                    if (currentNode.Id == 0)
                    {
                        CreateNewNote();
                    }
                    else
                    {
                        SaveNote();
                    }
                }
                else
                {
                    Toast.MakeText(this, "Not saved. The note is empty.", ToastLength.Short).Show();
                }
            };

        }
        

        public override void OnBackPressed()
        {
            ToMainPage();
        }
       
      
        private void DeleteNote()
        {
            File.Delete(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Note" + currentNode.Id));
            Toast.MakeText(this, "Deleted", ToastLength.Short).Show();
        }
        private void SaveNote()
        {
            File.Delete(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Note" + currentNode.Id));
            Note note = new Note(currentNode.Id, textView.Text, titleView.Text);
            SerializationNote.SaveAsBinaryFormat(note, String.Format("Note" + note.Id));
            currentNode = note;
            Toast.MakeText(this, "Saved",ToastLength.Short).Show();
        }

        private void ToMainPage()
        {
            Intent intent = new Intent(this, typeof(MainActivity));

            StartActivity(intent);
        }

        private void CreateNewNote()
        {
            Note note = new Note(MainActivity.countNotes + 1, textView.Text, titleView.Text);
            SerializationNote.SaveAsBinaryFormat(note, String.Format("Note" + note.Id));
            currentNode = note;
        }
    }
}