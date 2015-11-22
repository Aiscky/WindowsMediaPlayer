using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsMediaPlayer.XML
{
    class XMLMedia
    {
        System.Xml.Linq.XElement XElement = null;

        /* LOAD XML MEDIAS FILE */

        public void LoadXML(String FileName)
        {
            try
            {
                XElement = System.Xml.Linq.XElement.Load(FileName);
            }
            /* CREATING XML IF NOT EXISTS */
            catch
            {
                XElement = new System.Xml.Linq.XElement("Medias");
            }
        }

        /* Extract Medias From XML */

        public List<String> ExtractMedias()
        {
            IEnumerable<System.Xml.Linq.XElement> medias = XElement.Elements();
            List<String> MediasList = new List<String>();

            if (XElement == null)
                return null;

            try
            {
                foreach (var media in medias)
                    MediasList.Add(media.Element("Path").Value);
            }
            catch
            { }

            return MediasList;
        }

        /* ADD MEDIA OBJ TO XML */

        public void AddMedia(Model.Media aMedia)
        {
            if (XElement == null)
                return;
            try
            {
                /* TESTING IF THE SAME MEDIA ALREADY EXISTS */

                var medias = from media in XElement.Elements() where (string)media.Element("Path").Value == media.Value select media;

                if (medias.Count() != 0)
                {
                    return;
                }

                /* ADDING MEDIA */

                XElement.Add(new System.Xml.Linq.XElement("Media", new System.Xml.Linq.XElement("Path", aMedia.Path)));
            }
            catch
            { }
        }

        /*REMOVE MEDIA OBJ FROM XML */

        public void RemoveMedia(Model.Media rMedia)
        {
            var medias = from media in XElement.Elements() where (string)media.Element("Path").Value == rMedia.Path select media;

            foreach (var media in medias)
            {
                media.Remove();
            }
        }

        /* REMOVE ALL MEDIAS XML FILE */

        public void RemoveAllMedias()
        {
            IEnumerable<System.Xml.Linq.XElement> medias = XElement.Elements();
            List<String> MediasList = new List<String>();

            if (XElement == null)
                return;

            try
            {
                foreach (var media in medias)
                    media.Remove();
            }
            catch
            { }
        }

        /* WRITE THE CORRESPONDING XML FILE TO XELEMENT*/

        public void WriteXML(String XMLFile)
        {
            if (XElement != null)
            {
                XElement.Save(XMLFile);
            }
        }
    }
}
