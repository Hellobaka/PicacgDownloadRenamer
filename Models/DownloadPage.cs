using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicacgDownloadRenamer.Models
{
    [SugarTable("DOWNLOAD_COMIC_PAGE_OBJECT")]
    public class DownloadPage
    {
        public int ID { get; set; }
      
        public string COMIC_ID { get; set; }
      
        public string COMIC_PAGE_ID { get; set; }
      
        public string EPISODE_ID { get; set; }
      
        public string MEDIA_FILE_SERVER { get; set; }
      
        public string MEDIA_ORIGINAL_NAME { get; set; }
      
        public string MEDIA_PATH { get; set; }
      
        public string STORAGE_FOLDER { get; set; }

        public static async Task<List<DownloadPage>> FindPagesByEpisodeId(SqlSugarClient db, string episodeId)
        {
            return await db.Queryable<DownloadPage>()
                .Where(x => x.EPISODE_ID.Equals(episodeId, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }
    }
}
