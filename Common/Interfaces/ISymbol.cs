using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Common.Interfaces
{
    public interface ISymbol
    {
        int Id { get; set; }

        string Name { get; set; }

        /// <summary>
        /// LiteDB is not compatible with Bitmap class, 
        /// therefore image load is moved from constructor to method Load()
        /// and is moved Image property to GetImage() method
        /// </summary>
        //Bitmap Image { get; set; }
                string FileName { get; set; }

        void Load(string name, string fileName);

        void Load();

        Bitmap GetImage();
    }
}
