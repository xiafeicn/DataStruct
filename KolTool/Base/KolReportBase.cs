using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using Aspose.Words;
using GBI.Metrix.Model.Entity;
using GBI.Metrix.Service;
using GBI.Metrix.Service.Misc;
using GBI.Metrix.Utility;
using KolTool.Base;
using KolTool.Model;
using word;

namespace KolTool
{
    /// <summary>
    /// Contains common functionality for the upload and download classes
    /// This class should really be marked abstract but VS doesn't like that because it can't draw it as a component then :(
    /// </summary>
    public class KolReportBase : BackgroundWorker
    {
        public int MaxRetries = 50;						// max number of corrupted chunks or failed transfers to allow before giving up
        protected int NumRetries = 0;
        public int ChunkSizeSampleInterval = 15;		// interval to update the chunk size, used in conjunction with AutoSetChunkSize. 
        public bool IncludeHashVerification = true;		// checks the local file hash against the uploaded file hash to verify that the files are identical
        public int PreferredTransferDuration = 800;		// milliseconds, the timespan the class will attempt to achieve for each chunk, to give responsive feedback on the progress bar.
        protected DateTime StartTime;
        protected Thread HashThread;
        public string Guid;						// used to track events when multiple file transfers are happening
        public ManualResetEvent manualReset = new ManualResetEvent(true);
        public Guid guid_Parent;
        public KolFile KolFile;

        /// <summary>
        /// 请求报表的url
        /// </summary>
        public static string sourceUrl = "192.168.0.96:8091";
        public static string domain = "192.168.0.96";

        protected string template;
        protected string templateEnd;
        protected string templateKOL;
        protected string templateDescription;


        public static List<MiscEntity> departmentsDic = MiscEnum.hospital_department.GetMiscList();


        public static List<MiscEntity> professionalTitleDic = MiscEnum.hcp_professional_title.GetMiscList();
        public static List<MiscEntity> adminTitleDic = MiscEnum.hcp_admin_title.GetMiscList();
        public static List<MiscEntity> academicTitleDic = MiscEnum.hcp_academic_title.GetMiscList();
        public static List<MiscEntity> associationTitleDic = MiscEnum.hcp_association_title.GetMiscList();


        public event EventHandler ChunkSizeChanged;
        private CookieContainer cookies = new CookieContainer();

        public KolReportBase()
        {
            base.WorkerReportsProgress = true;
            base.WorkerSupportsCancellation = true;
        }
        protected override void Dispose(bool disposing)
        {
            if (this.HashThread != null && this.HashThread.IsAlive)
                this.HashThread.Abort();

            base.Dispose(disposing);
        }

        protected override void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
        {
            if (this.HashThread != null && this.HashThread.IsAlive)
                this.HashThread.Abort();
            base.OnRunWorkerCompleted(e);
        }

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            // make sure we can connect to the web service.  if this step is not done here, it will retry 50 times because of the retry code...

            base.OnDoWork(e);
        }

        /// <summary>
        /// Returns a description of a number of bytes, in appropriate units.
        /// e.g. 
        ///		passing in 1024 will return a string "1 Kb"
        ///		passing in 1230000 will return "1.23 Mb"
        /// Megabytes and Gigabytes are formatted to 2 decimal places.
        /// Kilobytes are rounded to whole numbers.
        /// If the rounding results in 0 Kb, "1 Kb" is returned, because Windows behaves like this also.
        /// </summary>
        public static string CalcFileSize(long numBytes)
        {
            string fileSize = "";

            if (numBytes > 1073741824)
                fileSize = String.Format("{0:0.00} Gb", (double)numBytes / 1073741824);
            else if (numBytes > 1048576)
                fileSize = String.Format("{0:0.00} Mb", (double)numBytes / 1048576);
            else
                fileSize = String.Format("{0:0} Kb", (double)numBytes / 1024);

            if (fileSize == "0 Kb")
                fileSize = "1 Kb";	// min.							
            return fileSize;
        }

        #region Help Methods



        #endregion

        /// <summary>
        /// This method is intended to be run on a background thread. 
        /// </summary>
        public static string CalcFileHash(string FilePath)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hash;
            using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096))
                hash = md5.ComputeHash(fs);
            return BitConverter.ToString(hash);
        }


        protected void InsertDocument(Aspose.Words.Node insertAfterNode, Document srcDoc)
        {
            // Make sure that the node is either a paragraph or dataSet.
            if ((!insertAfterNode.NodeType.Equals(NodeType.Paragraph)) &
              (!insertAfterNode.NodeType.Equals(NodeType.Table)))
                throw new ArgumentException("The destination node should be either a paragraph or dataSet.");
            // We will be inserting into the parent of the destination paragraph.
            CompositeNode dstStory = insertAfterNode.ParentNode;
            // This object will be translating styles and lists during the import.
            NodeImporter importer = new NodeImporter(srcDoc, insertAfterNode.Document, ImportFormatMode.KeepDifferentStyles);
            // Loop through all sections in the source document.
            foreach (Section srcSection in srcDoc.Sections)
            {
                // Loop through all block level nodes (paragraphs and tables) in the body of the section.
                foreach (Aspose.Words.Node srcNode in srcSection.Body)
                {
                    // Let's skip the node if it is a last empty paragraph in a section.
                    if (srcNode.NodeType.Equals(NodeType.Paragraph))
                    {
                        Paragraph para = (Paragraph)srcNode;
                        if (para.IsEndOfSection && !para.HasChildNodes)
                            continue;
                    }
                    // This creates a clone of the node, suitable for insertion into the destination document.
                    Aspose.Words.Node newNode = importer.ImportNode(srcNode, true);
                    // Insert new node after the reference node.
                    dstStory.InsertAfter(newNode, insertAfterNode);
                    insertAfterNode = newNode;
                }
            }
        }


        protected void GeneralKol(Bookmark bookmark, DocumentBuilder builder, ResearcherEntity entity, string language)
        {
            var currentLog = new List<string>();
            var orgList = HttpService.GetByIds<BaseEntity<InstitutionEntity>>(entity.link_organization, DataSetEnum.organization);

            Document insertDoc = new Document(templateKOL);//KOL模板
            DocumentBuilder insertBuilder = new DocumentBuilder(insertDoc);
            insertDoc.Range.Replace("$NAME$", GeneratePriorityLanguage(language, entity.name_en, entity.name_cn), false, false);
            if (orgList != null && orgList.data != null && orgList.data.Any())
            {
                var org = orgList.data.FirstOrDefault(t => t.id == entity.link_organization[0]);
                insertDoc.Range.Replace("$DEPART$", GeneratePriorityLanguage(language, org.name_en, org.name_cn), false, false);
                insertDoc.Range.Replace("$HOSPITAL_LEVEL$", org.level.NullToString(), false, false);
                insertDoc.Range.Replace("$FUDAN_SCORE$", org.rank.NullToString(), false, false);

                var otherDeps = orgList.data.Except(new InstitutionEntity[] { org });
                if (otherDeps.Any())
                {
                    var otherDepNames = string.Join(",", otherDeps.Select(t => GeneratePriorityLanguage(language, t.name_en, t.name_cn)));
                    insertDoc.Range.Replace("$OTHER_DEPARTMENT$", otherDepNames, false, false);
                }
                else
                {
                    insertDoc.Range.Replace("$OTHER_DEPARTMENT$", "", false, false);
                }
                //$OTHER_DEPARTMENT$
            }
            insertDoc.Range.Replace("$DEPARTMENT_ID$", GetDepartment(entity.department_ids, departmentsDic, language), false, false);
            insertDoc.Range.Replace("$SEX$", GetGenderByStr(entity.gender, language), false, false);

            if (entity.resume != null && entity.resume.resume != null)
                insertDoc.Range.Replace("$RESUME$", entity.resume.resume, false, false);
            else
                insertDoc.Range.Replace("$RESUME$", string.Empty, false, false);
            if (entity.specialty_list != null && entity.specialty_list.Any())
            {
                insertDoc.Range.Replace("$SPECIAL_LIST$", entity.specialty_list[0].raw_specialty_name ?? string.Empty, false, false);
            }

            var professional = GetHcpTitle(professionalTitleDic, null, null, entity.professional_title, null, null,
                language);
            insertDoc.Range.Replace("$PROFESSIONAL_TITLE$", professional, false, false);

            var admin = GetHcpTitle(null, null, adminTitleDic, null, null, entity.admin_title, language);
            insertDoc.Range.Replace("$ADMIN_TITLE$", admin, false, false);

            var academic = GetHcpTitle(null, academicTitleDic, null, null, entity.academic_title, null,
    language);
            insertDoc.Range.Replace("$ACADEMIC_TITLE$", academic, false, false);

            if (orgList != null)
            {
                var society = GetSocietyName(entity.institution_list, orgList.data, associationTitleDic, language);
                insertDoc.Range.Replace("$SOCIETY$", string.Join("   ", society), false, false);
            }
            //entity.institution_list.GetSocietyName(orgList, AssociationTitleDic, pamarater.language),
            insertBuilder.MoveToDocumentEnd();

            CaptureHelper captureHelper = new CaptureHelper()
            {
                url = string.Format("http://{0}/kol/index?hcpid={1}", sourceUrl, entity.id),
            };
            List<string> file = captureHelper.CaptureByNreco(language);
            insertBuilder.InsertOleObject(templateKOL, false, false, Image.FromFile(file[0]));
            insertBuilder.MoveToDocumentEnd();

            insertBuilder.InsertOleObject(templateKOL, false, false, Image.FromFile(file[1]));
            insertBuilder.MoveToDocumentEnd();

            insertBuilder.InsertOleObject(templateKOL, false, false, Image.FromFile(file[2]));

            insertBuilder.MoveToDocumentEnd();

            insertBuilder.InsertOleObject(templateKOL, false, false, Image.FromFile(file[3]));

            insertBuilder.MoveToDocumentEnd();

            insertBuilder.InsertOleObject(templateKOL, false, false, Image.FromFile(file[4]));
            insertBuilder.MoveToDocumentEnd();

            insertBuilder.InsertOleObject(templateKOL, false, false, Image.FromFile(file[5]));

            insertBuilder.MoveToDocumentEnd();

            insertBuilder.InsertOleObject(templateKOL, false, false, Image.FromFile(file[6]));


            //builder.MoveToBookmark(bookmark.Name, false, true);
            InsertDocument(bookmark.BookmarkStart.ParentNode, insertDoc);
        }

        public static string GetDepartment(string[] ids, List<MiscEntity> deptDic, string language)
        {
            var result = string.Empty;
            if (ids == null || !ids.Any()) return result;
            for (int i = 0; i < ids.Length; i++)
            {
                if (!ids[i].Equals("0"))
                {
                    result +=
       language == "en"
           ? deptDic.Where(d => d.dataid == ids[i]).FirstOrDefault().name_en + ","
           : deptDic.Where(d => d.dataid == ids[i]).FirstOrDefault().name_cn + "，";

                }
            }
            return result.Trim().TrimEnd(',').TrimEnd('，');
        }

        public static string GetGenderByStr(string gender, string language)
        {
            if (string.IsNullOrEmpty(gender)) return "";
            else if (gender.Equals("男"))
            {
                return language.Equals(Consts.Language.EN) ? "male" : "男";
            }
            else
            {
                return language.Equals(Consts.Language.EN) ? "female" : "女";
            }
        }

        public List<string> GetSocietyName(List<HcpOrg> listhHcpOrgs, List<InstitutionEntity> listOrg,
         List<MiscEntity> associationDic, string language)
        {
            List<string> result = new List<string>();
            if (listhHcpOrgs == null || !listhHcpOrgs.Any()) return result;

            List<string> orgIds = new List<string>();
            listhHcpOrgs.ForEach(t =>
            {
                orgIds.Add(t.institution_id);
            });
            listhHcpOrgs.ForEach(t =>
            {
                if (t.association_title != null && t.association_title.Any())
                {
                    string title = string.Empty;
                    foreach (var asso in t.association_title)
                    {
                        var entity = associationDic.FirstOrDefault(p => p.dataid.Equals(asso));
                        title += entity.GetMiscName(language) + ",";
                    }
                    title = title.TrimEnd(',');
                    var orgEntity = listOrg.Where(s => s.id.Equals(t.institution_id)).FirstOrDefault();
                    if (orgEntity != null)
                        result.Add(GeneratePriorityLanguage(language,
                                      orgEntity.name_en,
                                      orgEntity.name_cn) + "  " +
                                  title);
                }
            });
            return result;
        }

        public string GetHcpTitle(List<MiscEntity> professionalTitle, List<MiscEntity> academicTitle,
           List<MiscEntity> adminTitle, string[] professional_title, string[] academic_title, string[] admin_title,
           string language, string separate = ", ")
        {
            var Title = "";
            if (professional_title != null)
            {
                for (var j = 0; j < professional_title.Length; j++)
                {
                    Title += language == "en"
                        ? professionalTitle.Where(d => d.dataid == professional_title[j]).FirstOrDefault().name_en +
                          separate
                        : professionalTitle.Where(d => d.dataid == professional_title[j]).FirstOrDefault().name_cn +
                          separate;
                }
            }
            if (academic_title != null)
            {
                for (var j = 0; j < academic_title.Length; j++)
                {
                    Title += language == "en"
                        ? academicTitle.Where(d => d.dataid == academic_title[j]).FirstOrDefault().name_en + separate
                        : academicTitle.Where(d => d.dataid == academic_title[j]).FirstOrDefault().name_cn + separate;
                }
            }
            if (admin_title != null)
            {
                for (var j = 0; j < admin_title.Length; j++)
                {
                    Title += language == "en"
                        ? adminTitle.Where(d => d.dataid == admin_title[j]).FirstOrDefault().name_en + separate
                        : adminTitle.Where(d => d.dataid == admin_title[j]).FirstOrDefault().name_cn + separate;
                }
            }
            Title = Title.Trim().TrimEnd(',');
            if (Title.IndexOf("Doc.Degree") > 0)
            {
                Title = Title.Replace("Doc.Degree", "Doc. Degree");
            }
            return Title;
        }

        public string GeneratePriorityLanguage(string currentLanguage, string en, string cn)
        {
            if (!string.IsNullOrEmpty(en))
            {
                en = en.Replace(" , ", ", ");
            }
            if (currentLanguage != null && currentLanguage.Equals("en"))
            {
                return string.IsNullOrWhiteSpace(en) ? cn : en;
            }
            return string.IsNullOrWhiteSpace(cn) ? en : cn;
        }

    }
}
