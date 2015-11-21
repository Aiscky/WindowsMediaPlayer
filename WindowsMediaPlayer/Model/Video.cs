using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsMediaPlayer.Model
{
    public class Video : Media
    {
        public Video(String path) : base(path)
        {
            this.type = Model.Media.MediaType.VIDEO;
        }
    }
}
