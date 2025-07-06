using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicacgDownloadRenamer.Models
{
    [SugarTable("DB_COMIC_VIEW_RECORD_OBJECT")]
    public class CacheComicEP
    {
        public int ID { get; set; }
       
        public string COMIC_ID { get; set; }
       
        public int EPISODE_ORDER { get; set; }
       
        public string EPISODE_TITLE { get; set; }
       
        public int EPISODE_TOTAL { get; set; }
       
        public int LAST_VIEW_TIMESTAMP { get; set; }
       
        public int PAGE { get; set; }
    }
}
