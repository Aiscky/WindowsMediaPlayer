using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsMediaPlayer.Model
{
    class Media
    {
        /* ATTRIBUTES */

        public String Name { get; set; }
        private String path;
        public String Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
                if (System.IO.File.Exists(path))
                {
                    Name = System.IO.Path.GetFileName(value);
                    ModificationDate = System.IO.File.GetLastWriteTime(value);
                }
                else
                {
                    Name = Path;
                    ModificationDate = new DateTime();
                }
            }
        }
        public enum MediaType { IMAGE, MUSIC, VIDEO };
        public MediaType type { get; set; }
        public DateTime ModificationDate { get; set; }

        /* CTOR */

        public Media(String path)
        {
            Path = path;
        }
    }
}
