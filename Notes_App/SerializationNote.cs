using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Notes_App
{
    static class SerializationNote
    {
        static public Note LoadFromBinaryFile(string fileName)
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), fileName);
            using (Stream fStream = File.OpenRead(backingFile))
            {
                Note note = (Note)binFormat.Deserialize(fStream);
                return note;
            }
        }
        static public void SaveAsBinaryFormat(object objGraph, string fileName)
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), fileName);
            using (Stream fStream = new FileStream(backingFile,
            FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, objGraph);
            }

        }
    }
}