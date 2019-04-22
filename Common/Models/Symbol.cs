using Common.Interfaces;
using System.Drawing;
using System.IO;

namespace Common.Models
{
    public class Symbol : ISymbol
    {
        private int id;

        private string name;

        private string fileName;

        private Bitmap image;

        public int Id { get => id; set => id = value; }

        public string Name
        {
            get => name;
            set => name = value;
        }
        public string FileName { get => fileName; set => fileName = value; }

        /// <summary>
        /// LiteDB is not compatible with Bitmap class, therefore I moved 
        /// image load from constructor to method Load()
        /// and moved Image property to GetImage() method
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fileName"></param>
        //public Bitmap Image
        //{
        //    get => image;
        //    set => image = value;
        //}
        //public Symbol(string name, string fileName)
        //{
        //    this.name = name;

        //    this.image = Load(fileName);
        //}

        public void Load(string name, string fileName)
        {
            this.name = name;
            this.fileName = fileName;
            Load();
        }

        public void Load()
        {
            using (Stream BitmapStream = System.IO.File.Open(fileName, System.IO.FileMode.Open))
            {
                Image img = System.Drawing.Image.FromStream(BitmapStream);

                image = new Bitmap(img);
            }
        }
        public Bitmap GetImage()
        {
            return image;
        }
    }
}
