using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicacgDownloadRenamer.Models
{
    [SugarTable("DOWNLOAD_COMIC_EPISODE_OBJECT")]
    public class DownloadEpisode
    {
        public int ID { get; set; }
       
        public string COMIC_ID { get; set; }
       
        public string EPISODE_ID { get; set; }
       
        public int EPISODE_ORDER { get; set; }
       
        public int STATUS { get; set; }
       
        public string TITLE { get; set; }
       
        public int TOTAL { get; set; }
       
        public DateTime UPDATED_AT { get; set; }
    }
}
