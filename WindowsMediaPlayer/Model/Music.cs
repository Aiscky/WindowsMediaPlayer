using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsMediaPlayer.Model
{
    public class Music : Media
    {
        public String Artist { get; set; }
        public String Album { get; set; }
        public String Title { get; set; }

        public Music(String path) : base(path)
        {
            this.type = Media.MediaType.MUSIC;

            Title = "";
            Artist = "";
            Album = "";

            getMusicInfos();
        }

        public void getMusicInfos()
        {
            if (System.IO.File.Exists(Path))
            {
                /* USING DISPOSE FROM THE OBJ ONCE THE CODE SECTION IS OVER */

                using (System.IO.FileStream fs = new System.IO.FileStream(Path, System.IO.FileMode.Open))
                {
                    byte[] b = new byte[128];

                    fs.Seek(-128, System.IO.SeekOrigin.End);
                    fs.Read(b, 0, 128);

                    if (System.Text.Encoding.Default.GetString(b, 0, 3).CompareTo("TAG") == 0)
                    {
                        Title = System.Text.Encoding.Default.GetString(b, 3, 30).TrimEnd('\0');
                        Artist = System.Text.Encoding.Default.GetString(b, 33, 30).TrimEnd('\0');
                        Album = System.Text.Encoding.Default.GetString(b, 63, 30).TrimEnd('\0');
                    }
                    fs.Close();
                }
            }
        }
    }
}
