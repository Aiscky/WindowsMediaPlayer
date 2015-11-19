using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsMediaPlayer.Model
{
    class Image : Media
    {
        public Image(String path) : base(path)
        {
            this.type = Media.MediaType.IMAGE;
        }
    }
}
