using Newtonsoft.Json.Linq;
using PicacgDownloadRenamer.Models;
using SqlSugar;

namespace PicacgDownloadRenamer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private string DBPath { get; set; } = "";

        private bool DryRun => DryRunSelector.Checked;

        private string? OutputPath { get; set; } = "";

        private List<string> SelectedFolders { get; set; } = [];

        private async Task AddActionHistory(string message, bool needAlarm = false)
        {
            await InvokeAsync(() => ActionDisplay.Items.Add(message));
            if (needAlarm)
            {
                ShowError(message);
            }
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog()
            {
                Multiselect = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SelectedFolders = [];
                SelectedFolders.AddRange(dialog.SelectedPaths);
                PathPreviews.Text = string.Join("; ", SelectedFolders);
                OutputPath = Path.GetDirectoryName(SelectedFolders.FirstOrDefault() ?? "");
                OutputPathPreview.Text = OutputPath;
            }
        }

        private void BrowserOutputButton_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog()
            {
                Description = "选择输出目录"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                OutputPath = dialog.SelectedPath;
                if (!Directory.Exists(OutputPath))
                {
                    Directory.CreateDirectory(OutputPath);
                }
                OutputPathPreview.Text = OutputPath;
            }
        }

        private void DBSelectButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "SQLite Database|*.db;*.sqlite|All Files|*.*",
                Multiselect = false
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DBPath = dialog.FileName;
                DBPathPreview.Text = DBPath;
                File.WriteAllText("config.txt", DBPath);
            }
        }

        private async Task HandleV1Cache()
        {
            if (File.Exists(DBPath) is false)
            {
                ShowError("数据库文件不存在，请选择一个有效的数据库文件。");
                return;
            }
            if (SelectedFolders.Count == 0)
            {
                ShowError("请至少选择一个缓存文件夹。");
                return;
            }
            ActionDisplay.Items.Clear();
            using SqlSugarClient db = new(new ConnectionConfig
            {
                ConnectionString = $"DataSource={DBPath}",
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true
            });
            var eps = await db.Queryable<DownloadEpisode>().ToListAsync();
            bool dryRun = DryRun;
            bool copyMode = CopyModeSelector.Checked;
            bool keepEPName = KeepEPNameSelector.Checked;
            int current = 0;
            int total = 0;
            bool hasError = false;
            await Task.Run(async () =>
            {
                foreach (var path in SelectedFolders)
                {
                    // 获取缓存文件夹名作为EPISODE_ID
                    var cacheEPId = Path.GetFileName(path);
                    // 在数据库中查找对应的章节记录
                    var ep = eps.FirstOrDefault(x => x.EPISODE_ID.Equals(cacheEPId, StringComparison.OrdinalIgnoreCase));
                    if (ep == null)
                    {
                        // 未找到则报错并返回
                        ShowError($"在数据库中未找到对应的缓存记录：{path}");
                        return;
                    }
                    // 查找漫画详情
                    var comic = await CacheComicDetail.FindByComicID(db, ep.COMIC_ID);
                    if (comic != null)
                    {
                        string comicName = comic.TITLE;
                        string epName = ep.TITLE;
                        string epId = ep.EPISODE_ID;
                        await AddActionHistory($"{epId} => {comicName} {epName}");
                        // 是否保留章节名
                        epName = keepEPName ? epName : ep.EPISODE_ORDER.ToString();
                        // 查询章节下所有图片页
                        var pages = await DownloadPage.FindPagesByEpisodeId(db, ep.EPISODE_ID);
                        total += pages.Count;
                        await AddActionHistory($"Page Count={pages.Count}");
                        foreach (var page in pages)
                        {
                            string fileName = page.MEDIA_ORIGINAL_NAME;
                            // 替换非法文件名字符
                            Path.GetInvalidFileNameChars().ToList().ForEach(c =>
                            {
                                comicName = comicName.Replace(c.ToString(), "_");
                                epName = epName.Replace(c.ToString(), "_");
                            });
                            Path.GetInvalidPathChars().ToList().ForEach(c =>
                            {
                                fileName = fileName.Replace(c.ToString(), "_");
                            });
                            // 构造原始文件路径和新路径
                            string originalPath = Path.Combine(path, page.MEDIA_PATH);
                            string outputPath = string.IsNullOrEmpty(OutputPath) ? Path.GetDirectoryName(path)! : OutputPath;
                            string newPath = Path.Combine(outputPath, comicName, epName, fileName);
                            if (!File.Exists(originalPath))
                            {
                                // 文件不存在则报错
                                await AddActionHistory($"[Error] {originalPath} 文件不存在，跳过。", true);
                                hasError = true;
                            }
                            else
                            {
                                if (dryRun)
                                {
                                    // 仅预览操作
                                    await AddActionHistory($"[DryRun] {originalPath} => {newPath}");
                                }
                                else
                                {
                                    if (!Directory.Exists(Path.GetDirectoryName(newPath)))
                                    {
                                        Directory.CreateDirectory(Path.GetDirectoryName(newPath));
                                    }
                                    if (copyMode)
                                    {
                                        File.Copy(originalPath, newPath, true);
                                    }
                                    else
                                    {
                                        File.Move(originalPath, newPath);
                                    }
                                    await AddActionHistory($"{originalPath} => {newPath}");
                                }
                            }
                            // 更新进度
                            await UpdateProgress(++current, total, hasError);
                        }
                    }
                    else
                    {
                        // 未找到漫画详情
                        await AddActionHistory($"{ep.COMIC_ID} => 未知ID", true);
                    }
                }
            });
        }

        private async Task HandleV2Cache()
        {
            if (SelectedFolders.Count == 0)
            {
                ShowError("请至少选择一个缓存文件夹。");
                return;
            }
            ActionDisplay.Items.Clear();
            bool dryRun = DryRun;
            bool keepEPName = KeepEPNameSelector.Checked;
            bool copyMode = CopyModeSelector.Checked;
            int current = 0;
            int total = SelectedFolders.Count;
            bool hasError = false;
            await Task.Run(async () =>
            {
                foreach (var dir in SelectedFolders)
                {
                    bool dirResult = await ProcessV2ComicDirectory(dir, dryRun, copyMode, keepEPName);
                    if (!dirResult)
                    {
                        hasError = true;
                    }

                    await UpdateProgress(++current, total, hasError);
                }
            });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            PicacgVersionSelector.SelectedIndex = 0;
            if (File.Exists("config.txt"))
            {
                var lines = File.ReadAllLines("config.txt");
                if (lines.Length > 0)
                {
                    DBPath = lines[0];
                    DBPathPreview.Text = DBPath;
                }
            }
        }

        private async Task<bool> ProcessV2ComicDirectory(string dir, bool dryRun, bool copyMode, bool keepEPName)
        {
            string metaDataPath = Path.Combine(dir, $"comic_{Path.GetFileName(dir)}.json");
            if (!Directory.Exists(dir))
            {
                await AddActionHistory($"※[Error] {dir} 文件不存在，跳过。", true);
                return false;
            }
            if (!File.Exists(metaDataPath))
            {
                await AddActionHistory($"※[Error] {dir} 目录不存在元数据，跳过。", true);
                return false;
            }
            try
            {
                JObject json = JObject.Parse(File.ReadAllText(metaDataPath));
                string comicName = json["title"]!.ToString();
                comicName = ReplaceInvalidFileNameChars(comicName);
                bool hasError = false;
                foreach (var ep in Directory.GetDirectories(dir))
                {
                    bool epResult = await ProcessV2EpisodeDirectory(ep, comicName, dir, dryRun, copyMode, keepEPName);
                    if (!epResult)
                    {
                        hasError = true;
                    }
                }
                return !hasError;
            }
            catch (Exception e)
            {
                await AddActionHistory($"[Error] 处理 {dir} 时发生错误：{e.Message}", true);
                return false;
            }
        }

        private async Task<bool> ProcessV2EpisodeDirectory(string ep, string comicName, string dir, bool dryRun, bool copyMode, bool keepEPName)
        {
            string epMetaDataPath = Path.Combine(ep, "chapter_status.json");
            if (!File.Exists(epMetaDataPath))
            {
                await AddActionHistory($"※[Error] {ep} 目录不存在章节元数据，跳过。", true);
                return false;
            }
            JObject epJson = JObject.Parse(File.ReadAllText(epMetaDataPath));
            string epName = keepEPName ? epJson["chapter"]!["title"]!.ToString() : Path.GetFileName(ep);
            epName = ReplaceInvalidFileNameChars(epName);
            await AddActionHistory($"{Path.GetFileName(dir)} - {Path.GetFileName(ep)} => {comicName} {epName}");
            string originalPath = ep;
            string outputPath = string.IsNullOrEmpty(OutputPath) ? Path.GetDirectoryName(dir)! : OutputPath;
            string newPath = Path.Combine(outputPath, comicName, epName);
            if (dryRun)
            {
                await AddActionHistory($"[DryRun] {originalPath} => {newPath}");
                return true;
            }
            if (copyMode)
            {
                Directory.CreateDirectory(newPath);
                foreach (var page in Directory.GetFiles(ep).Where(x => Path.GetExtension(x) != ".json"))
                {
                    string fileName = Path.GetFileName(page);
                    string newFilePath = Path.Combine(newPath, fileName);
                    File.Copy(page, newFilePath, true);
                    await AddActionHistory($"[Copy] {page} => {newFilePath}");
                }
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(newPath)!);
                Directory.Move(originalPath, newPath);
                await AddActionHistory($"[Move] {originalPath} => {newPath}");
            }
            return true;
        }

        private string ReplaceInvalidFileNameChars(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c.ToString(), "_");
            }
            return name;
        }

        private bool ShowConfirm(string message)
        {
            return MessageBox.Show(message, "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private async void StartButton_Click(object sender, EventArgs e)
        {
            if (!DryRun && !ShowConfirm("当前为非DryRun，会操作实际文件，是否确认？"))
            {
                return;
            }
            OutputPath = OutputPathPreview.Text;
            if (!Directory.Exists(OutputPath))
            {
                Directory.CreateDirectory(OutputPath);
            }
            switch (PicacgVersionSelector.SelectedIndex)
            {
                case 0:
                    await HandleV1Cache();
                    break;

                default:
                    await HandleV2Cache();
                    break;
            }
        }

        private async Task UpdateProgress(int current, int max, bool hasError)
        {
            await InvokeAsync(() =>
            {
                bool isComplete = current != max;
                DBSelectButton.Enabled = !isComplete;
                BrowserButton.Enabled = !isComplete;
                DryRunSelector.Enabled = !isComplete;
                StartButton.Enabled = !isComplete;
                CopyModeSelector.Enabled = !isComplete;
                PicacgVersionSelector.Enabled = !isComplete;
                KeepEPNameSelector.Enabled = !isComplete;

                float progress = Math.Min(100, (float)current / max * 100);
                MoveProgressBar.Value = (int)progress;
                ProgressDisplay.Text = $"{(hasError ? "[!]" : "")}{current}/{max} ({progress:0.00}%)";
            });
        }
    }
}