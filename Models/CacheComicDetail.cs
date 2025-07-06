using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicacgDownloadRenamer.Models
{
    [SugarTable("DB_COMIC_DETAIL_OBJECT")]
    public class CacheComicDetail
    {
        public int ID { get; set; }

        public string AUTHOR { get; set; }

        public string CATEGORIES { get; set; }

        public string CHINESE_TEAM { get; set; }

        public string COMIC_ID { get; set; }

        public int COMMENTS_COUNT { get; set; }

        public DateTime CREATED_AT { get; set; }

        public string CREATOR_AVATAR_FILE_SERVER { get; set; }

        public string CREATOR_AVATAR_ORIGINAL_NAME { get; set; }

        public string CREATOR_AVATAR_PATH { get; set; }

        public string CREATOR_GENDER { get; set; }

        public string CREATOR_ID { get; set; }

        public string CREATOR_NAME { get; set; }

        public string DESCRIPTION { get; set; }

        public int DOWNLOAD_STATUS { get; set; }

        public int EPISODE_COUNT { get; set; }

        public int FINISHED { get; set; }

        public int IS_FAVOURITE { get; set; }

        public int IS_LIKED { get; set; }

        public long LAST_VIEW_TIMESTAMP { get; set; }

        public int LIKES_COUNT { get; set; }

        public int PAGES_COUNT { get; set; }

        public string TAGS { get; set; }

        public string THUMB_FILE_SERVER { get; set; }

        public string THUMB_ORIGINAL_NAME { get; set; }

        public string THUMB_PATH { get; set; }

        public string TITLE { get; set; }

        public DateTime UPDATED_AT { get; set; }

        public int VIEWS_COUNT { get; set; }

        public static async Task<CacheComicDetail?> FindByComicID(SqlSugarClient db, string id)
        {
            return await db.Queryable<CacheComicDetail>().FirstAsync(x => x.COMIC_ID == id);
        }
    }
}
