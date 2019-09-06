using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Emgu.CV;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Threading;
//using Ayonix.FaceID;
using System.Collections;
using FRS.Class;
using System.Threading.Tasks;
using iTextSharp.text;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using System.Net.NetworkInformation;
using System.Timers;


namespace FRS
{
    public partial class FFMPEGMultipleDisplayVer3 : Form
    {//
        DataSet ds = new DataSet();
        DataSet dsIP = new DataSet();
        int camera_count = 0;
        public static int NumberOfCamera = Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfCamera"]);
        private static BackgroundWorker[] worker1 = new BackgroundWorker[NumberOfCamera];
        int i = 1;
       public static bool[] BlProcess = new bool[NumberOfCamera];
       private int counter1, counter2;
        System.Windows.Forms.Timer t1 = new System.Windows.Forms.Timer();
        private string[] path1 = new string[NumberOfCamera];
        private string[] ip1= new string[NumberOfCamera];
        private bool cam9 = true;
        private int camstop9 = 0;
        int xlocation = 0;
        int ylocation = 0;
        int hiddenhight = 0;
        int hiddenwidth = 0;
        int actualval = 0;
        string terminalid = ConfigurationManager.AppSettings["TerminalId"].ToString();
        int no_of_camera = NumberOfCamera;
        List<VerifyNew.MatchResult> result_list = null;
        private bool captureInProgress;
        CDatabaseWin objCdatabase = new CDatabaseWin();
        string IPCamDataPath = ConfigurationManager.AppSettings["IPCamDataPath"].ToString();
        string AFIDsFolder = ConfigurationManager.AppSettings["AFIDs"].ToString();
        int personcount = 0, page_no, Unmatchcount, totalreg_face = 0, totalunreg_face = 0, MatchCount, matchpage_no, unmatchpage_no, totalunreg_facehistory, totalunreg_face1;
        int[] index = new int[NumberOfCamera];
        public static List<string> afidsNames = new List<string>();         //Used to store afid Name which we pass in Verification
        static IEnumerable<string> afidsFileNames = null;                   //Used to store afid name
        public static List<byte[]> afids = new List<byte[]>();  //Added By Ashwini
       // public static IEnumerable<byte[]> afids = null;                    //Used to store afid in bytes which is require for Verification
        List<string> matchafidsNames = new List<string>(); // define array for match afids
        List<FRS.Class.CShowRegisterDetails> lstShowregisterDetail = new List<FRS.Class.CShowRegisterDetails>();
        DBFile objDBFile = new DBFile(ConfigurationManager.ConnectionStrings["VideoGrabber"].ToString());
        string ImageFolder = ConfigurationManager.AppSettings["ImagePath"].ToString();
        string _noPhoto = ConfigurationManager.AppSettings["ImagePath"].ToString() + "\\photo_not_available.png";
        VerifyNew.VerifyFace verifynew = new VerifyNew.VerifyFace();
        int _URID = 1;//Unmatch No
        int _URID_DB = 1;//Unmatch No for db
        CInsertVideoDetails _objInsertVideoDetails = new CInsertVideoDetails();
        string FFMPEGdll = ConfigurationManager.AppSettings["FFMPEGdll"];
        public static string[] strPathCam = new string[NumberOfCamera];

        decimal location_id;
        decimal location;
        int totalUnRegFaceDateChange = 0;
        int totalRegFaceDateChange = 0;
        int personCountDateChange = 0;
        int personcount1 = 0;
        int totalreg_face1 = 0;
        int totalreg_facehistory = 0;
        string strHistoryDate = "";
        bool datechangeflag = false, _detectface;
        int DBreg_face = 0;
        int DBunreg_face = 0;
        public static int cameracounter, count, campath,CameraNum;
        private static int skipFrame;// = 50;  
        internal class matchdata
        {
            public string imagePath { get; set; }
            public string bmpPath { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string score { get; set; }
            public string locatoin { get; set; }
            public string iplocatoin { get; set; }
            public int ictr { get; set; } // location count
            public string age { get; set; }
            public string videoFileName { get; set; }

            public int cameraNo { get; set; }
        };

        internal class unmatchdata
        {
            public string imgPath { get; set; }
            public string id { get; set; }
            public string locatoin { get; set; }
            public string imagePath { get; set; }
            public int ictr { get; set; } // location count
            public string gender { get; set; }
            public int age { get; set; } // location count
            public int cameraNo { get; set; }
        };
        List<matchdata> matchar = new List<matchdata>();
        List<unmatchdata> unmatchar = new List<unmatchdata>();
        string strperson, path;
        string face_id;
        PictureBox picbxVideo = null;
        ImageBox[] imageboxs;
        public static PictureBox[] picbxvideos;
        Label[] labelcam;
        public static int intpicboxno;
        List<PictureBox> picbxVideos = new List<PictureBox>();
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        MySqlDataAdapter adap = null;
        string strquery = null, dtlocation, IPStreamPath;
        string strconstr = null;
        public static int[] strCameraNo = new int[NumberOfCamera];
        public static int[] ProcessID = new int[NumberOfCamera];
        public static int[] picbx = new int[NumberOfCamera];
        public static string _dicName;
        public static int firstcamera;
        public static int no_ofCam;
        private string historyDate;
        private static Image<Bgr, Byte> imageCV = new Image<Bgr, byte>(global::FRS.Properties.Resources.greenback);
        int displayframe = 10;
        Mat greenbackimg = imageCV.Mat;
        static System.Timers.Timer timer;
        System.Timers.Timer timerforping;
        public static  string[] strPassword = new string[NumberOfCamera];
        public static string[] strID = new string[NumberOfCamera];
        public static string[] strIPaddress = new string[NumberOfCamera];
        public static string[] strPort = new string[NumberOfCamera];

       

        //Created by Adarsh on 01-03-2019 
        public FFMPEGMultipleDisplayVer3()
        {

            //Added by Ashwini on 15-04-2019 To identify whether new AFID is created.
            Task.Factory.StartNew(() =>
            {
                FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
                fileSystemWatcher.Filter = "*.*";
                string pathAFID = ConfigurationManager.AppSettings["AFIDs"].ToString();
                fileSystemWatcher.Path = pathAFID;
                fileSystemWatcher.Created += FileSystemWatcher_Created;
                fileSystemWatcher.NotifyFilter = NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName;
                fileSystemWatcher.EnableRaisingEvents = true;

            });
            //Ended by Ashwini on 15-04-2019

           
            //Added by Ashwini on 2018-12-26 To generate PDF at 12:01 AM

            DateTime nowTime = DateTime.Now;
            historyDate = nowTime.ToString("yyyy/MM/dd");

            DateTime scheduledTime = DateTime.Today.AddDays(1);
            double tickTime = (double)(scheduledTime - DateTime.Now).TotalMilliseconds;
            timer = new System.Timers.Timer(tickTime);

            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
            ////Ended By Ashwini on 2018-12-26
            InitializeComponent();
            //Added By Ashwini on 2019-02-22 To assign skipFrame value from database
           
            skipFrame = Convert.ToInt32(objDBFile.GetSkipValue());
            //Ended by Ashwini on 2019-02-22
        
            this.WindowState = FormWindowState.Maximized;

            //This Code is Written By Rahul Shukla on 03-12-2018
            //This Code is Written for Displaying the Particular tab of IP Camera(Live)Detection as per Setting Option given by user.
            CDatabaseWin objDatabase = new CDatabaseWin();
            DataTable dt = new DataTable();
            dt = objDatabase.Display("Value", "syssettings", " where Key1 = 'Application' and Key2 = 'Display Data'");
            string DisplayData = dt.Rows[0]["Value"].ToString();

            if (DisplayData == "Unmatched")
            {
                tabControlShowDetail.TabPages.Remove(tabRegistered);
                //This Code is Added By Rahul Shukla on 06-12-2018
                label5.Visible = false;
                lblMatchedFace.Visible = false;
                lblRegisterCount1.Visible = false;
                //This Code Ended Here by Rahul Shukla on 06-12-2018

            }

            if (DisplayData == "Matched")
            {
                tabControlShowDetail.TabPages.Remove(tabUnregisterDisplay);
                //This Code is Added By Rahul Shukla on 06-12-2018
                lblUnmatchedFace.Visible = false;
                lblUnregisteredCount.Visible = false;
                lblMatchedFace.Location = new Point(336, 614);
                lblRegisterCount.Location = new Point(449, 614);
                //This Code Ended Here by Rahul Shukla on 06-12-2018
            }
            else
            {
                lblUnmatchedFace.Visible = true;//This Code is Added By Rahul Shukla on 06-12-2018
            }

            //Code Ended here by Rahul Shukla on 03-12-2018
            try
            {

            //    firstcamera = firstcamerano;
                string strconstrIP = ConfigurationManager.ConnectionStrings["VideoGrabber"].ToString();
                MySqlConnection conn = new MySqlConnection(strconstrIP);
                conn.Open();

                //  vishal upadhyay.2018-10-08.To fetch record of Every day.

                //start vishal upadhyay .2018-10-08.Retriving older values from database
                // Modified  by Vaibhav on 2018-10-30 to improvice the logic to bring  max(faceid) selection + camera settings + skip frame  in single fetch

                MySqlDataAdapter cmdGetIP = new MySqlDataAdapter("Select * from ipcamera_details where run_datetime = '" + DateTime.Now.ToString("yyyy-MM-dd") + "';SELECT MAX(face_id) as face_id FROM ipcamera_details where run_datetime = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND `type`=2;Select * from ipcamconfig Order By camerano asc;Select Value from syssettings WHERE Key1='Recognition' AND Key2='Skip Frame'", conn);
                //End of logic.Vishal upadhyay 2018-10-08
                totalreg_face1 = 0;
                totalunreg_face1 = 0;
                personcount1 = 0;

                cmdGetIP.Fill(dsIP, "ipcamera_details");
                conn.Close();
                if (dsIP.Tables[0].Rows.Count != 0)
                {
                    int Total_Matched_FacesFromdb = Convert.ToInt32(dsIP.Tables[0].Rows[dsIP.Tables[0].Rows.Count - 1]["Total_Matched_Faces"]);
                    int Total_Unmatched_FacesFromdb = Convert.ToInt32(dsIP.Tables[0].Rows[dsIP.Tables[0].Rows.Count - 1]["Total_Unmatched_Faces"]);
                    int Total_Matched_Person = Convert.ToInt32(dsIP.Tables[0].Rows[dsIP.Tables[0].Rows.Count - 1]["Total_Matched_Person"]);
                    totalreg_face1 = Total_Matched_FacesFromdb;
                    totalreg_facehistory = Total_Matched_FacesFromdb;
                    totalunreg_face1 = Total_Unmatched_FacesFromdb;
                    totalunreg_facehistory = Total_Unmatched_FacesFromdb;
                    personcount1 = Total_Matched_Person;
                }
                else
                {
                    totalreg_face1 = 0;
                    totalunreg_face1 = 0;
                    personcount1 = 0;
                }

                face_id = dsIP.Tables[1].Rows[0]["face_id"].ToString();
                for (count = 0; count < NumberOfCamera; count++)
                {
                    campath = count + 1;
                    strPathCam[count] = ConfigurationManager.AppSettings["PathCam" + campath.ToString()];
                    if (!Directory.Exists("E:"))
                    {

                        if (!Directory.Exists("C:\\" + strPathCam[count]))
                        {
                            Log("Create dir. : C:\\" + strPathCam[count]);
                            Directory.CreateDirectory("C:\\" + strPathCam[count]);
                            strPathCam[count] = "C:\\" + strPathCam[count];
                        }
                        else
                        {
                            strPathCam[count] = "C:\\" + strPathCam[count];
                        }
                    }
                    else
                    {

                        if (!Directory.Exists("E:\\" + strPathCam[count]))
                        {
                            Log("Create dir. : E:\\" + strPathCam[count]);
                            Directory.CreateDirectory("E:\\" + strPathCam[count]);
                            strPathCam[count] = "E:\\" + strPathCam[count];
                        }
                        else
                        {
                            strPathCam[count] = "E:\\" + strPathCam[count];
                        }
                    }
                }
               
                //Ended by Ashwini on 2019-03-07


                #region Design of IP Camera according to License.
                //timer1.Interval = 500;
                int panel_height = 820;
                int panel_width = 682;//656
                picbxvideos = new PictureBox[no_of_camera];
                int current_height = 0;
                int current_width = 0;
                if (no_of_camera == 1)
                {
                    int picbxheight = panel_height;
                    int picbxwidth = panel_width;
                    picbxvideos[camera_count] = new PictureBox();
                    picbxvideos[camera_count].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    picbxvideos[camera_count].Location = new System.Drawing.Point(current_height, current_width);
                    picbxvideos[camera_count].Name = "picbxVideo" + (camera_count + 1);
                    picbxvideos[camera_count].Size = new System.Drawing.Size(picbxheight, picbxwidth);
                    picbxvideos[camera_count].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                   // picbxvideos[camera_count].FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
                    picbxvideos[camera_count].TabIndex = 0;
                    picbxvideos[camera_count].TabStop = false;
                    current_width += picbxwidth;
                    splitContainer2.Panel1.Controls.Add(picbxvideos[camera_count]);
                    camera_count++;
                }
                if (no_of_camera == 2)
                {
                    int no_of_row = 2;
                    int picbxheight = panel_height;
                    int picbxwidth = panel_width / no_of_row;
                    picbxvideos[camera_count] = new PictureBox();
                    picbxvideos[camera_count].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    picbxvideos[camera_count].Location = new System.Drawing.Point(current_height, current_width);
                    picbxvideos[camera_count].Name = "picbxVideo" + (camera_count + 1);
                    picbxvideos[camera_count].Size = new System.Drawing.Size(picbxheight, picbxwidth);
                    picbxvideos[camera_count].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    //picbxvideos[camera_count].FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
                    picbxvideos[camera_count].TabIndex = 0;
                    picbxvideos[camera_count].TabStop = false;
                    current_width += picbxwidth;
                    // current_height += picbxheight;
                    splitContainer2.Panel1.Controls.Add(picbxvideos[camera_count]);
                    camera_count++;
                    picbxvideos[camera_count] = new PictureBox();
                    picbxvideos[camera_count].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    picbxvideos[camera_count].Location = new System.Drawing.Point(current_height, current_width);
                    picbxvideos[camera_count].Name = "picbxVideo" + (camera_count + 1);
                    picbxvideos[camera_count].Size = new System.Drawing.Size(picbxheight, picbxwidth);
                    picbxvideos[camera_count].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                  //  picbxvideos[camera_count].FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
                    picbxvideos[camera_count].TabIndex = 0;
                    picbxvideos[camera_count].TabStop = false;
                    current_width += picbxwidth;
                    // current_height += picbxheight;
                    splitContainer2.Panel1.Controls.Add(picbxvideos[camera_count]);
                    camera_count++;
                }
                // Edited by vaibhav on 2018-10--30  to improve the design and get a smooth flow 
                if (no_of_camera == 3)
                {
                    int no_of_row = 2;
                    int picbxheight = panel_height / no_of_row;
                    int picbxwidth = panel_width / no_of_row;
                    picbxvideos[camera_count] = new PictureBox();
                    picbxvideos[camera_count].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    picbxvideos[camera_count].Location = new System.Drawing.Point(current_height, current_width);
                    picbxvideos[camera_count].Name = "picbxVideo" + (camera_count + 1);
                    picbxvideos[camera_count].Size = new System.Drawing.Size(picbxheight, picbxwidth);
                    picbxvideos[camera_count].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                   // picbxvideos[camera_count].FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
                    picbxvideos[camera_count].TabIndex = 0;
                    picbxvideos[camera_count].TabStop = false;
                    current_width += picbxwidth;
                    // current_height += picbxheight;
                    splitContainer2.Panel1.Controls.Add(picbxvideos[camera_count]);
                    camera_count++;
                    picbxvideos[camera_count] = new PictureBox();
                    picbxvideos[camera_count].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    picbxvideos[camera_count].Location = new System.Drawing.Point(current_height, current_width);
                    picbxvideos[camera_count].Name = "picbxVideo" + (camera_count + 1);
                    picbxvideos[camera_count].Size = new System.Drawing.Size(2 * picbxheight, picbxwidth);
                    picbxvideos[camera_count].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                   // picbxvideos[camera_count].FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
                    picbxvideos[camera_count].TabIndex = 0;
                    picbxvideos[camera_count].TabStop = false;
                    current_width -= picbxwidth;
                    current_height += picbxheight;
                    splitContainer2.Panel1.Controls.Add(picbxvideos[camera_count]);
                    camera_count++;
                    picbxvideos[camera_count] = new PictureBox();
                    picbxvideos[camera_count].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    picbxvideos[camera_count].Location = new System.Drawing.Point(current_height, current_width);
                    picbxvideos[camera_count].Name = "picbxVideo" + (camera_count + 1);
                    picbxvideos[camera_count].Size = new System.Drawing.Size(picbxheight, picbxwidth);
                    picbxvideos[camera_count].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                  //  picbxvideos[camera_count].FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
                    picbxvideos[camera_count].TabIndex = 0;
                    picbxvideos[camera_count].TabStop = false;
                    current_width += picbxwidth;
                    //  current_height += picbxheight;
                    splitContainer2.Panel1.Controls.Add(picbxvideos[camera_count]);
                    camera_count++;
                }



                // Added by Vaibhav on 27-09-2018
                // IP camera logic with EMGU Imagebox
                if (no_of_camera == 6)
                {
                    int no_of_row = 2;
                    int no_of_columns = 3;
                    int picbxheight = panel_height / no_of_row;
                    int picbxwidth = panel_width / no_of_columns;
                    for (int i = 1; i <= no_of_row; i++)
                    {
                        int current_width4 = 0;
                        for (int j = 1; j <= no_of_columns; j++)
                        {
                            picbxvideos[camera_count] = new PictureBox();
                            picbxvideos[camera_count].Location = new System.Drawing.Point(current_height, current_width4);
                            picbxvideos[camera_count].Size = new System.Drawing.Size(picbxheight, picbxwidth);
                            picbxvideos[camera_count].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                            picbxvideos[camera_count].Name = "picbxVideo" + (camera_count + 1);
                         //   picbxvideos[camera_count].FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
                            picbxvideos[camera_count].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                            //  picbxvideos[camera_count].HorizontalScrollBar.Enabled = false;
                            // picbxvideos[camera_count].VerticalScrollBar.Enabled = false;
                            picbxvideos[camera_count].TabIndex = 0;
                            picbxvideos[camera_count].TabStop = false;
                            current_width4 += picbxwidth;
                            splitContainer2.Panel1.Controls.Add(picbxvideos[camera_count]);
                            camera_count++;


                        }
                        current_height += picbxheight;
                    }
                }
                if (no_of_camera == 4 || no_of_camera == 16 || no_of_camera == 25 || no_of_camera == 36 || no_of_camera == 49)
                {

                    int cameracounterdisplay = 1;
                    int no_of_row = (Int16)Math.Sqrt(no_of_camera);
                    int picbxheight = panel_height / no_of_row;
                    int picbxwidth = panel_width / no_of_row;
                    for (int i = 1; i <= no_of_row; i++)
                    {
                        int current_width4 = 0;
                        for (int j = 1; j <= no_of_row; j++)
                        {

                            picbxvideos[camera_count] = new PictureBox();
                            picbxvideos[camera_count].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                            picbxvideos[camera_count].Location = new System.Drawing.Point(current_height, current_width4);
                            picbxvideos[camera_count].Name = "picbxVideo" + (cameracounterdisplay);
                            picbxvideos[camera_count].Size = new System.Drawing.Size(picbxheight, picbxwidth);
                           // picbxvideos[camera_count].FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
                            picbxvideos[camera_count].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                            picbxvideos[camera_count].TabIndex = 0;
                            picbxvideos[camera_count].TabStop = false;
                            current_width4 += picbxwidth;
                            splitContainer2.Panel1.Controls.Add(picbxvideos[camera_count]);
                            cameracounterdisplay++;
                            camera_count++;
                        }
                        current_height += picbxheight;
                    }
                }
                if (no_of_camera == 9)
                {
                    int no_of_row = (Int16)Math.Sqrt(no_of_camera);
                    int picbxheight = panel_height / no_of_row;
                    int picbxwidth = panel_width / no_of_row;
                    for (int i = 1; i <= no_of_row; i++)
                    {
                        int current_width4 = 0;
                        for (int j = 1; j <= no_of_row; j++)
                        {

                            picbxvideos[camera_count] = new PictureBox();
                            picbxvideos[camera_count].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                            picbxvideos[camera_count].Location = new System.Drawing.Point(current_height, current_width4);
                            picbxvideos[camera_count].Name = "picbxVideo" + (camera_count + 1);
                            picbxvideos[camera_count].Size = new System.Drawing.Size(picbxheight, picbxwidth);
                            picbxvideos[camera_count].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                           // picbxvideos[camera_count].FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
                            picbxvideos[camera_count].TabIndex = 0;
                            picbxvideos[camera_count].TabStop = false;
                            current_width4 += picbxwidth;
                            splitContainer2.Panel1.Controls.Add(picbxvideos[camera_count]);

                            camera_count++;
                        }
                        current_height += picbxheight;
                    }
                }
                #endregion


                #region Mouse click event for IP Camera
                //   Edited  by Vaibhav on 2018-10-30 to cut short  the logic 

                for (int k = 0; k < camera_count; k++)
                {

                    picbxvideos[k].MouseClick += picbxVideo_Click;

                }

                #endregion
                afidsFileNames = Common.AfidFileName;        //Get the selected Afid FileName from Common Class 

                if (afidsFileNames.Count() != 0) { }
                else
                    afidsFileNames = FindAfids(AFIDsFolder);
                afids = null;
                Log("Read afids from afid folder");
                afids = ReadAfids(afidsFileNames).ToList();
                checkProcess();
            }

            catch (Exception ex)
            {

                string filePath = ConfigurationManager.AppSettings["ErrorLog"];

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }

            } 

        }
 
        public void checkProcess()
        {
            IPStreamPath = IPCamDataPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
            //Added By Ashwini on 01-05-2019 to log the IPStreamPath
            Log("Check process IPstreamPath:" + IPStreamPath);
            //Ended by Ashwini on 01-05-2019
            //Added By Ashwini for testing the path created
            bool directoryfound=true;
            try
            {
                directoryfound = !Directory.Exists(IPStreamPath);
               // if (!Directory.Exists(IPStreamPath))
                if(directoryfound)
                {
                    Log("Desktop Create dir. :" + IPStreamPath);
                    Directory.CreateDirectory(IPStreamPath);
                }
            }
            catch (Exception ex)
            { 
            string filePath = ConfigurationManager.AppSettings["ErrorLog"];

                        using (StreamWriter writer = new StreamWriter(filePath, true))
                        {
                            writer.WriteLine("Deskto checkprocess1 :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                               "" + Environment.NewLine + "Date :" + DateTime.Now.ToString()+IPStreamPath+directoryfound);
                            writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                        }
                        directoryfound = true;
                    
            }
            string[] strfile = Directory.GetDirectories(IPStreamPath);
            int filecount = (strfile.Length) + 1;

            IPStreamPath = IPStreamPath + "\\" + "Stream1";
             //Added By Ashwini for testing the path created
            bool directoryfound1=true;
            try
            {
                directoryfound1 = !Directory.Exists(IPStreamPath);
               // if (!Directory.Exists(IPStreamPath))
                if(directoryfound1)
                {
                    Log("Desktop Create dir. :" + IPStreamPath);
                    Directory.CreateDirectory(IPStreamPath);
                }
            }
            catch (Exception ex)
            {
                string filePath = ConfigurationManager.AppSettings["ErrorLog"];

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("Deskto checkprocess1Stream1 :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString()+IPStreamPath+directoryfound1);
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }
                directoryfound1 = false;

            }
            
            if (DateTime.Now.ToString("yyyy/MM/dd") != historyDate)
            {
                totalunreg_face1 = 0;
                //    totalreg_face = 0;
                totalreg_face1 = 0;
                //    personcount1 = 0;

                totalreg_facehistory = 0;
                totalunreg_facehistory = 0;

                datechangeflag = true;
               totalRegFaceDateChange = 0;
               totalUnRegFaceDateChange = 0;
                personCountDateChange = 0;
                DBreg_face = 0;
                DBunreg_face = 0;
               
                //Added By Ashwini on 2019-07-03 To check matched unmatched count after 12AM
                string strconstrIP = ConfigurationManager.ConnectionStrings["VideoGrabber"].ToString();
                MySqlConnection con = new MySqlConnection(strconstrIP);
                con.Open();

                //  vishal upadhyay.2018-10-08.To fetch record of Every day.

                //start vishal upadhyay .2018-10-08.Retriving older values from database
                // Modified  by Vaibhav on 2018-10-30 to improvice the logic to bring  max(faceid) selection + camera settings + skip frame  in single fetch

                MySqlDataAdapter cmdGetIP = new MySqlDataAdapter("Select * from ipcamera_details where run_datetime = '" + DateTime.Now.ToString("yyyy-MM-dd") + "';SELECT MAX(face_id) as face_id FROM ipcamera_details where run_datetime = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND `type`=2;Select * from ipcamconfig Order By camerano asc;Select Value from syssettings WHERE Key1='Recognition' AND Key2='Skip Frame'", con);
                //End of logic.Vishal upadhyay 2018-10-08
                personcount1 = 0;
                cmdGetIP.Fill(dsIP, "ipcamera_details");
                con.Close();
                if (dsIP.Tables[0].Rows.Count != 0)
                {
                    int Total_Matched_FacesFromdb = Convert.ToInt32(dsIP.Tables[0].Rows[dsIP.Tables[0].Rows.Count - 1]["Total_Matched_Faces"]);
                    int Total_Unmatched_FacesFromdb = Convert.ToInt32(dsIP.Tables[0].Rows[dsIP.Tables[0].Rows.Count - 1]["Total_Unmatched_Faces"]);
                    int Total_Matched_Person = Convert.ToInt32(dsIP.Tables[0].Rows[dsIP.Tables[0].Rows.Count - 1]["Total_Matched_Person"]);
                    totalreg_face1 = Total_Matched_FacesFromdb;
                    totalreg_facehistory = Total_Matched_FacesFromdb;
                    totalunreg_face1 = Total_Unmatched_FacesFromdb;
                    totalunreg_facehistory = Total_Unmatched_FacesFromdb;
                    personcount1 = Total_Matched_Person;
                }
                else
                {
                    totalreg_face1 = 0;
                    totalunreg_face1 = 0;
                    personcount1 = 0;
                }
                //Ended By Ashwini on 2019-03-07
            }
            strconstr = ConfigurationManager.ConnectionStrings["VideoGrabber"].ToString();
            conn = new MySqlConnection(strconstr);
            conn.Open();
            MySqlDataAdapter cmdGet = new MySqlDataAdapter("Select * from ipcamconfig where Terminalid ='" + terminalid + "' Order By CameraNo ASC", conn);
            //DataSet ds = new DataSet();
            ds.Reset();
            cmdGet.Fill(ds, "ipcamconfig");
           
            #region 1 IP Camera
            for (cameracounter = 0; cameracounter < ds.Tables[0].Rows.Count; cameracounter++)
            {
                CameraNum = cameracounter;
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[cameracounter]["CameraNo"] != null)
                {

                    strCameraNo[cameracounter] = Convert.ToInt32(ds.Tables[0].Rows[cameracounter]["CameraNo"]);
                    strIPaddress[cameracounter] = Convert.ToString(ds.Tables[0].Rows[cameracounter]["IPaddress"]);
                    strPort[cameracounter] = Convert.ToString(ds.Tables[0].Rows[cameracounter]["CameraPort"]);
                    strID[cameracounter] = Convert.ToString(ds.Tables[0].Rows[cameracounter]["UserID"]);
                    strPassword[cameracounter] = Convert.ToString(ds.Tables[0].Rows[cameracounter]["CameraPassword"]);
                    CaptureFromIPCamera(strPassword[cameracounter], strID[cameracounter], strIPaddress[cameracounter], strPort[cameracounter], strPathCam[cameracounter]);
                }
            }
            #endregion

         
            //IPStreamPath = IPCamDataPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
            //if (!Directory.Exists(IPStreamPath))
            //{
            //    Log("Create dir. :" + IPStreamPath);
            //    Directory.CreateDirectory(IPStreamPath);
            //}
            //string[] strfile = Directory.GetDirectories(IPStreamPath);
            //int filecount = (strfile.Length) + 1;

            //IPStreamPath = IPStreamPath + "\\" + "Stream1";
            //if (!Directory.Exists(IPStreamPath))
            //{
            //    Log("Create dir. :" + IPStreamPath);
            //    Directory.CreateDirectory(IPStreamPath);
            //}

            timerforping = new System.Timers.Timer(60000);

            timerforping.Elapsed += new ElapsedEventHandler(timer_ElapsedforPing);
            timerforping.Start();
        }
        public void CaptureFromIPCamera(string password, string userid, string ipaddress, string port, string pathcam)
        {
            if (ipaddress !="")
            {
                string file1 = FFMPEGdll;
                string arguments1 = @"-i rtsp://" + userid + ":" + password + "@" + ipaddress + ":" + port + "/ -vsync cfr -r " + skipFrame + " -f image2 " + pathcam + "img%1d.jpeg";
                Ping myPing = new Ping();
                PingReply reply = myPing.Send(ipaddress, 240000);
                bool pingable = reply.Status == IPStatus.Success;
                if (pingable)
                {
                    var processStartInfo1 = new ProcessStartInfo(file1, arguments1);
                    processStartInfo1.CreateNoWindow = true;
                    processStartInfo1.RedirectStandardError = true;
                    processStartInfo1.RedirectStandardOutput = true;
                    processStartInfo1.UseShellExecute = false;

                    try
                    {
                        worker1[CameraNum] = new BackgroundWorker();

                        worker1[CameraNum].WorkerReportsProgress = true;

                        Process[] processlist = Process.GetProcesses();

                        foreach (Process theprocess in processlist)
                        {
                            if (theprocess.Id == ProcessID[CameraNum] && ProcessID[CameraNum] != 0)
                            {
                                theprocess.Kill();
                            }
                        }
                        var process1 = new Process();


                        process1.StartInfo = processStartInfo1;
                        process1.Start();
                        BlProcess[CameraNum] = true;
                        //pictureBox1.ImageLocation = "e:/test/image-*.png";
                        worker1[CameraNum].RunWorkerAsync(process1);
                        ProcessID[CameraNum] = process1.Id;
                       
                    }
                    //Added by Ashwin on 08-03-2019 To check whether exception occurs while starting the process in background worker
                    catch (Exception ex)
                    {
                        string filePath = ConfigurationManager.AppSettings["ErrorLog"];

                        using (StreamWriter writer = new StreamWriter(filePath, true))
                        {
                            writer.WriteLine("Message Process1 :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                               "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                            writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                        }
                    }
                    //Ended by Ashwini on 08-03-2019

                }
                else
                {
                    picbxvideos[CameraNum].Image = global::FRS.Properties.Resources.StreamNotAvailable;
                    Task.Factory.StartNew(() =>
                    {
                        System.Threading.Thread.Sleep(24000);
                        CaptureFromIPCamera(strPassword[CameraNum], strID[CameraNum], strIPaddress[CameraNum], strPort[CameraNum], strPathCam[CameraNum]);
                    });

                }

            }
        }

        private void InitializeTimer1()
        {
            counter1 = 0;
            t1.Interval = 10;
            t1.Enabled = true;

            t1.Tick += new EventHandler(timer1_Tick);
        }

        public void showFrame()
        {

            Thread.Sleep(6000);
            InitializeTimer1();
        }    
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            ////Added by Ashwini on 2019/06/04 To retrive mode of detection i.e: Single face detection or multiface detection
            //CDatabaseWin objDatabase = new CDatabaseWin();
            //DataTable dt = new DataTable();
            //dt = objDatabase.Display("Value", "syssettings", " where Key1 = 'Application' and Key2 = 'Detection Switch'");
            //strDetectionSwitch = dt.Rows[0]["Value"].ToString();
            ////Ended by Ashwini on 2019/06/04

            try
            {
                for (int countercam = 0; countercam < NumberOfCamera; countercam++)
                {
                    if (BlProcess[countercam])
                    {
                        try
                        {
                            Process currentProcesses = Process.GetProcessById(ProcessID[countercam]);
                        }
                        catch (ArgumentException)
                        {
                            CameraNum = countercam;
                            CaptureFromIPCamera(strPassword[countercam], strID[countercam], strIPaddress[countercam], strPort[countercam], strPathCam[countercam]);
                        }
                        DirectoryInfo directory2 = new DirectoryInfo(strPathCam[countercam]);
                        FileInfo myFile2 = (from f in directory2.GetFiles()
                                            orderby f.LastWriteTime descending
                                            select f).First();
                        string strfile2 = myFile2.ToString();

                        Bitmap bm2 = new Bitmap(strPathCam[countercam] + strfile2);
                        picbxvideos[countercam].SizeMode = PictureBoxSizeMode.StretchImage;
                        picbxvideos[countercam].Image = bm2;

                        if (index[countercam] % skipFrame == 0)//skip every 49 frames because in live stream 25-30 frames per second (fps).
                        {
                            bool facedetect2 = verifynew.CheckFaceIPCamera(bm2);
                            if (facedetect2)
                            {
                                string imagePath = "";
                                VerifyFaceDynamic(imagePath, strCameraNo[countercam]);
                            }
                        }
                        else
                        {
                            System.GC.Collect();
                            System.GC.WaitForPendingFinalizers();
                        }
                        index[countercam]++;
                        //Code added to delete frames from local
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            try
                            {

                                string[] filePaths = Directory.GetFiles(strPathCam[countercam]);
                                foreach (string filePath in filePaths)
                                {
                                    var name = new FileInfo(filePath).Name;
                                    name = name.ToLower();
                                    if (name != strfile2)
                                    {
                                        File.Delete(filePath);
                                    }
                                    //GC.Collect();
                                    //GC.WaitForPendingFinalizers();
                                }
                            }
                            catch { }
                        });

                    }
                    else
                    {
                        picbxvideos[countercam].Image = global::FRS.Properties.Resources.StreamNotAvailable;
                    }

                }
     
                }

            catch (Exception ex)
            {
                timer1_Tick(new object(), new EventArgs());
            }
        }

        #region picbxVideo_Click(Zoom image)
        //Added by Pragnesha [Function: Popup image]
        private void picbxVideo_Click(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                string strpicboxno = ((System.Windows.Forms.Control)(sender)).Name;
                string[] words = strpicboxno.Split('o');
                intpicboxno = Int32.Parse(words[1]);
                FormCollection fc = Application.OpenForms;
                foreach (Form frm in fc)//Checks wheather form FrmMultiCamConfiguration is opened or not from Form Collection 
                {

                    if (frm.Text == "IPCamera Configuration") //If Form FrmMultiCamConfiguration is open then closes the form and breaks out of the loop
                    {
                        frm.Close();
                        break;
                    }
                }
               // picbx = intpicboxno;
                //Ended For Ganesh Wadgaonkar
                //for (int multicam = 1; multicam < NumberOfCamera; multicam++)
                //{
                //    if (intpicboxno == multicam)
                //    {
                //        intpicboxno = strCameraNo[intpicboxno-1];
                //    }
                //}
                FrmMultiCamConfiguration objFrmMultiCamConfig = new FrmMultiCamConfiguration(intpicboxno.ToString());
                objFrmMultiCamConfig.Show();


                //END.Vishal upadhyay 2018-09-18.
                return;
            }
           
        }
        #endregion

        #region Log
        private void Log(string Message)
        {
            LogFile Log = new LogFile(ConfigurationManager.AppSettings["LogFile"].ToString());
            Log.GetLogged(Message);
            Log = null;
        }
        #endregion
        #region ReadAfids
        IEnumerable<byte[]> ReadAfids(IEnumerable<string> afidsFileNames)
        {
            IList<byte[]> afids = new List<byte[]>();
            Log("Read all afids");
            foreach (string afidFileName in afidsFileNames)
            {
                using (FileStream fs = new FileStream(afidFileName, FileMode.Open, FileAccess.Read))
                {
                    byte[] afid = new byte[fs.Length];

                    fs.Read(afid, 0, (int)fs.Length);
                    afids.Add(afid);
                }
            }
            return afids;
        }
        #endregion
        #region FindAfids
        IEnumerable<string> FindAfids(string path)
        {
            if (Directory.Exists(path))
                return Directory.EnumerateFiles(path, "*.afid");
            else if (File.Exists(path))
                return new List<string>() { path };
            else
                throw new ArgumentException("Specified path is not correct", path);
        }
        #endregion
        #region RegisterPanelDynamicDesign
        public void RegisterPanelDynamicDesign(FRS.Class.CShowRegisterDetails objClassShowRegisterDetails)
        {
            TableLayoutPanel objtbl = null;
            PictureBox picbxSrcimg, picbxTrgtimg = null;
            LinkLabel lnklblDetails, lnklblName, lnklblLocation = null;
            Label lblSourceFace, lblTargetFace, lbltxtID, lblID, lblrecognizedAs, lbltxtScore, lblScore, lbltxtlocation, lbllocation, lbltxtCamera, lblCamera = null;
            try
            {
                objtbl = new TableLayoutPanel();
                picbxSrcimg = new PictureBox();
                picbxTrgtimg = new PictureBox();
                lnklblDetails = new LinkLabel();
                lnklblName = new LinkLabel();
                lnklblLocation = new LinkLabel();
                lblSourceFace = new Label();
                lblTargetFace = new Label();
                lbltxtID = new Label();
                lblID = new Label();
                lblrecognizedAs = new Label();
                lbltxtScore = new Label();
                lblScore = new Label();
                lbltxtlocation = new Label();
                lbllocation = new Label();
                lbltxtCamera = new Label();
                lblCamera = new Label();
                objtbl.BackColor = System.Drawing.Color.Black;
                lblSourceFace.Text = "Source Face";
                lblSourceFace.Font = new System.Drawing.Font(lblSourceFace.Font, FontStyle.Bold);
                lblSourceFace.TextAlign = ContentAlignment.MiddleCenter;
                lblTargetFace.Text = "Target Face";
                lblTargetFace.Font = new System.Drawing.Font(lblTargetFace.Font, FontStyle.Bold);
                lblTargetFace.TextAlign = ContentAlignment.MiddleCenter;
                lbltxtID.Text = "ID-";
                lblrecognizedAs.Text = "Identified As-";
                lbltxtScore.Text = "Score-";
                lbltxtCamera.Text = "Camera No-";
                lbltxtlocation.Text = "Location-";

                picbxSrcimg.Size = new System.Drawing.Size(100, 100);
                picbxTrgtimg.Size = new System.Drawing.Size(100, 100);
                picbxSrcimg.SizeMode = PictureBoxSizeMode.StretchImage;
                picbxTrgtimg.SizeMode = PictureBoxSizeMode.StretchImage;
                picbxSrcimg.Image = objClassShowRegisterDetails.SrcImg;
                picbxTrgtimg.Image = objClassShowRegisterDetails.TrgtImg;
                lblID.Text = objClassShowRegisterDetails.UserID;
                lnklblName.Text = objClassShowRegisterDetails.Name + "\n" + objClassShowRegisterDetails.UserID;
                lnklblName.Click += new System.EventHandler(lnklblName_Click);
                //Added by Ashwini on 2019-02-13 To Display time along with date in location
                string[] strlocation = objClassShowRegisterDetails.Location.Split(' ');
                lbllocation.Text = strlocation[0] + "\n" + strlocation[1];//Display time along with Date
                //Ended By Ashwini on 2019-02-13
                lblScore.Text = objClassShowRegisterDetails.Score.ToString();
                lblCamera.Text = terminalid + ":" + objClassShowRegisterDetails.cameraNo;
                objtbl.Controls.Clear();
                //Clear out the existing row and column styles
                objtbl.ColumnStyles.Clear();
                objtbl.RowStyles.Clear();
                objtbl.RowCount = 6;
                objtbl.ColumnCount = 4;
                objtbl.Font = new System.Drawing.Font("Calibri", 8F);// Added by Ashwini on 2018-10-04
                objtbl.Width = 419;
                objtbl.Height = 128;
                objtbl.BackColor = Color.Black;
                objtbl.CellPaint += tableLayoutPanel1_CellPaint;
                objtbl.Controls.Add(lblSourceFace, 0, 0);
                objtbl.Controls.Add(picbxSrcimg, 0, 1);
                objtbl.Controls.Add(lblTargetFace, 1, 0);
                objtbl.Controls.Add(picbxTrgtimg, 1, 1);
                objtbl.Controls.Add(lbltxtID, 2, 1);
                objtbl.Controls.Add(lblID, 3, 1);
                objtbl.Controls.Add(lblrecognizedAs, 2, 2);
                objtbl.Controls.Add(lnklblName, 3, 2);
                objtbl.Controls.Add(lbltxtScore, 2, 3);
                objtbl.Controls.Add(lblScore, 3, 3);
                objtbl.Controls.Add(lbltxtCamera, 4, 3);
                objtbl.Controls.Add(lblCamera, 4, 4);
                objtbl.Controls.Add(lbltxtlocation, 5, 4);
                objtbl.Controls.Add(lbllocation, 8, 5);
                objtbl.Size = new System.Drawing.Size(480, 140);
                objtbl.SetRowSpan(picbxSrcimg, 5);
                objtbl.SetRowSpan(picbxTrgtimg, 5);

                flpRegisterDisplay.Controls.Add(objtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objtbl = null;
                picbxSrcimg = null;
                picbxTrgtimg = null;
                lnklblDetails = null;
                lnklblName = null;
                lnklblLocation = null;
                lblSourceFace = null;
                lblTargetFace = null;
                lbltxtID = null;
                lblID = null;
                lblrecognizedAs = null;
                lbltxtScore = null;
                lblScore = null;
                lbllocation = null;
                lbltxtlocation = null;
                lblCamera = null;
                lbltxtCamera = null;
            }
        }
        #endregion
        #region FillRegisteredFaceDetailArray
        public void FillRegisteredFaceDetailArray(int indx)
        {
            try
            {
                //Added by Adarsh on 5/5/2017
                FRS.Class.CShowRegisterDetails objClassShowRegisterDetails = new FRS.Class.CShowRegisterDetails();
                objClassShowRegisterDetails.SrcImg = (Bitmap)System.Drawing.Image.FromFile(matchar[indx].imagePath);
                objClassShowRegisterDetails.TrgtImg = (Bitmap)System.Drawing.Image.FromFile(matchar[indx].bmpPath);
                objClassShowRegisterDetails.UserID = matchar[indx].id;
                objClassShowRegisterDetails.Name = matchar[indx].name;
                objClassShowRegisterDetails.Score = matchar[indx].score;
                objClassShowRegisterDetails.cameraNo = matchar[indx].cameraNo.ToString();
                objClassShowRegisterDetails.Location = matchar[indx].iplocatoin;
                lstShowregisterDetail.Add(objClassShowRegisterDetails);
                RegisterPanelDynamicDesign(objClassShowRegisterDetails);

                //Added By Ashwini Kamble on 2018-11-01 To Enable the Trackbar in match panel on Face detection
                trackBar1.Enabled = true;
                //Ended By Ashwini Kamble on 2018-11-01
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region UnRegisterPanelDynamicDesign
        public void UnRegisterPanelDynamicDesignarray(int indx)
        //Bitmap img, string ID, string intLocation, string imagepath, int location_count)
        {
            TableLayoutPanel objtbl = null;
            PictureBox picbximg = null;
            LinkLabel lnklblRegister;
            Label lbltxtID, lblID, lbllocation, lnklblLocation, lbltxtAge, lblAge, lbltxtGender, lblGender, lbltxtCamerano, lblCamerano = null;

            try
            {
                //Added By Ashwini Kamble on 2018-11-01 To Enable the Trackbar in unmatch panel on Face detection
                Trckbarmatch.Enabled = true;
                //Ended By Ashwini Kamble on 2018-11-01

                objtbl = new TableLayoutPanel();
                picbximg = new PictureBox();
                picbximg = new PictureBox();
                lnklblRegister = new LinkLabel();
                lnklblLocation = new Label();
                lbltxtID = new Label();
                lblID = new Label();
                lbllocation = new Label();
                lbltxtAge = new Label();
                lblAge = new Label();
                lbltxtGender = new Label();
                lblGender = new Label();
                lbltxtCamerano = new Label();
                lblCamerano = new Label();
                objtbl.BackColor = System.Drawing.Color.Black;
                
                objtbl.Name = "tbl_" + Path.GetFileNameWithoutExtension(unmatchar[indx].imagePath);

                lbltxtID.Text = "ID-";
                lbllocation.Text = "Location-";
                lbltxtCamerano.Text = "Camera No-";
                lblCamerano.Text = terminalid + ":" + unmatchar[indx].cameraNo.ToString();
                lnklblRegister.Text = "Register" + " $" + unmatchar[indx].imagePath;
                lnklblRegister.Click += new System.EventHandler(lnklblRegister_Click);
                picbximg.Size = new System.Drawing.Size(115, 115);
                picbximg.Image = (Bitmap)System.Drawing.Image.FromFile(unmatchar[indx].imgPath);
                picbximg.SizeMode = PictureBoxSizeMode.StretchImage;
                /////////////////////////////////////////////////////////////vishal///////////////////
                EnrollNew.EnrollFace en = new EnrollNew.EnrollFace();


                double gender;
                lblID.Text = unmatchar[indx].id;
                //lnklblLocation.Text = unmatchar[indx].locatoin;
                //Added by Ashwini on 2019-02-13 To display date along with time in location
                string[] strlocation1 = unmatchar[indx].locatoin.Split(' ');
                lnklblLocation.Text = strlocation1[0] + "\n" + strlocation1[1];//Display time along with Date
                //Ended by Ashwini on 2019-02-13
                lnklblLocation.Left = lbllocation.Left + lbllocation.Width;
                lnklblLocation.TextAlign = ContentAlignment.TopLeft;
                lnklblLocation.Name = unmatchar[indx].ictr.ToString();

                // lnklblLocation.Click += new System.EventHandler(lnklblLocation_Click);
                objtbl.Controls.Clear();
                //Clear out the existing row and column styles
                objtbl.ColumnStyles.Clear();
                objtbl.RowStyles.Clear();
                objtbl.RowCount = 5;
                objtbl.ColumnCount = 4;
                //Added By Ashwini to adjust dimension of table to display unmatch details on 2019-02-13
                objtbl.Font = new System.Drawing.Font("Calibri", 8F);// Added by Ashwini on 2018-10-04
                objtbl.Width = 419;
                objtbl.Height = 128;
                objtbl.BackColor = Color.Black;
                //Ended By Ashwini on 2019-02-13
                objtbl.CellPaint += tableLayoutPanel1_CellPaint;
                objtbl.Controls.Add(picbximg, 0, 0);
                objtbl.Controls.Add(lbltxtID, 1, 0);
                objtbl.Controls.Add(lblID, 2, 0);
                objtbl.Controls.Add(lbllocation, 1, 1);
                objtbl.Controls.Add(lnklblLocation, 2, 1);
                objtbl.Controls.Add(lbltxtCamerano, 1, 2);
                objtbl.Controls.Add(lblCamerano, 2, 2);
                objtbl.Controls.Add(lnklblRegister, 1, 3);
                //tableLayoutPanel1_CellPaint(new object(), new TableLayoutCellPaintEventArgs());
                objtbl.SetRowSpan(picbximg, 5);
                objtbl.Size = new System.Drawing.Size(480, 140);//120
                flpUnregisterDisplay.Controls.Add(objtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objtbl = null;
                picbximg = null;
                picbximg = null;
                // lnklblDetails = null;
                lnklblRegister = null;
                lnklblLocation = null;
                lbltxtID = null;
                lblID = null;
                lbllocation = null;
                lblAge = null;
                lblGender = null;
                lbltxtAge = null;
                lbltxtGender = null;
            }
        }
        #endregion

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {

            if (e.Row == e.Column)
                using (SolidBrush brush = new SolidBrush(Color.Black))
                    e.Graphics.FillRectangle(brush, e.CellBounds);
            else
                using (SolidBrush brush = new SolidBrush(Color.Black))
                    e.Graphics.FillRectangle(brush, e.CellBounds);
        }




        #region GetImage
        private Bitmap GetImage(string _id)
        {
            Bitmap image = null;
            if (!Directory.Exists(ImageFolder + "\\" + _id))
            {
                image = new Bitmap(_noPhoto);
                return image;
            }
            string[] returnFiles = Directory.GetFiles(ImageFolder + "\\" + _id);
            if (returnFiles.Length >= 1)
            {
                string filePath = returnFiles[0].ToString();
                //Bitmap img = new Bitmap(newImage, new Size(93, 112));

                image = new Bitmap(filePath);

            }
            // image.Size = new Size(50, 100);
            return image;
        }
        #endregion
        public void VerifyFaceDynamic(string filename, int IPCamID)
        {
            //Added By Vaibhav On 2018-11-01 To handle Exception
            try
            {
                //Ended By Vaibhav On 2018-11-01



                string strconstrIP = ConfigurationManager.ConnectionStrings["VideoGrabber"].ToString();
                MySqlConnection conn = new MySqlConnection(strconstrIP);
                conn.Open();
                //End of logic.Vishal upadhyay 2018-10-08

                //-------- Edited by Vaibhav on 2018-11-29 , to get max faceid  from ipcamderatils
                DataSet dsfaceid = new DataSet();
                MySqlDataAdapter cmdGetIP = new MySqlDataAdapter("SET SESSION TRANSACTION ISOLATION LEVEL READ UNCOMMITTED ;SELECT MAX(face_id) as face_id FROM ipcamera_details where run_datetime = '" + DateTime.Now.ToString("yyyy-MM-dd") + "';SET SESSION TRANSACTION ISOLATION LEVEL REPEATABLE READ ;", conn);


                cmdGetIP.Fill(dsfaceid, "faceid");
                conn.Close();
                if (dsfaceid.Tables[0].Rows.Count > 0)
                {
                    face_id = dsfaceid.Tables[0].Rows[0]["face_id"].ToString();
                }

                //  ----------
                string folder_name = "Stream1";
                string myString = null;
                int numSkip = Convert.ToInt32(objDBFile.GetSkipValue());
                string _id, _score, _name, _age;
                float _Threshold = (float)Convert.ToDecimal(objDBFile.GetThreshold());
                //int _URID = 1;

                afidsNames.Clear();
                foreach (string afidFileName in afidsFileNames)
                    afidsNames.Add(Path.GetFileNameWithoutExtension(afidFileName));
               
                string currrentpath = "";
                List<VerifyNew.MatchResult> result = null;
                //Pragnesha 03-04-18 [Path for save detected faces from frame]
                string FacePath = null;
                string[] strfile1 = Directory.GetDirectories(IPStreamPath);
                //  int filecount1 = (strfile1.Length) + 1;
                FacePath = IPStreamPath + "\\" + "Faces";// +filecount1;

                //Added By Ashwini on 22-04-2019 To Create log of IPStreamPath Created
                Log("Desktop IPStreamPath dir. :" + IPStreamPath);
                //Ended By Ashwini on 22-04-2019
                //Added by Ashwini to test ipstrampath
                bool directoryfound = true ;
                try
                {
                    directoryfound = !Directory.Exists(FacePath);
                    //if (!Directory.Exists(FacePath))
                   if(directoryfound)
                    {
                        Log(" Desktop Create dir. :" + FacePath);
                        Directory.CreateDirectory(FacePath);
                    }
                }
                catch (Exception ex)
                {
                    string filePath = ConfigurationManager.AppSettings["ErrorLog"];

                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        writer.WriteLine("Desktop Facepath :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                           "" + Environment.NewLine + "Date :" + DateTime.Now.ToString()+FacePath+directoryfound);
                        writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                    }
                    directoryfound = false;
                }
                Bitmap _faceCheck;
                if (VerifyNew.VerifyFace.cutImage.Count >= 1)
                {
                    _faceCheck = VerifyNew.VerifyFace.cutImage[VerifyNew.VerifyFace.cutImage.Count - 1];
                   // _detectface = verifynew.CheckFaceIPCamera(_faceCheck);
                }
                //Added by Ashwini on 18-03-2019
                string strconstrIP1 = ConfigurationManager.ConnectionStrings["VideoGrabber"].ToString();
                MySqlConnection conn1 = new MySqlConnection(strconstrIP1);
                MySqlCommand cmd1;
                //Ended by Ashwini on 18-03-2019

                    //Pragnesha 2-4-2018 [Extract facefrom every frame to match] 
                    foreach (var cImg in VerifyNew.VerifyFace.cutImage)
                    {
                         bool faceredetect = verifynew.ReCheckFaceIPCamera(cImg);
                         if (faceredetect)
                         {
                             Bitmap _face = cImg;
                             string IPFaceChk = FacePath + "\\" + "frame" + DateTime.Now.ToString("HHmmssfff") + ".jpeg";
                             _face.Save(IPFaceChk, System.Drawing.Imaging.ImageFormat.Jpeg);

                             //This Code is Written By Rahul Shukla on 03-12-2018
                             //This Code is Written for Displaying the Particular tab of IP Camera(Live)Detection as per Setting Option given by user.
                             CDatabaseWin objDatabase = new CDatabaseWin();
                             DataTable dt = new DataTable();
                             dt = objDatabase.Display("Value", "syssettings", " where Key1 = 'Application' and Key2 = 'Display Data'");
                             string DisplayData = dt.Rows[0]["Value"].ToString();
                             //Code Ended here by Rahul Shukla on 03-12-2018
                             bool blusr = false;
                             //bool _detect = verifynew.CheckFaceIPCamera(_face);
                             //if (_detect)
                             //{
                             result = verifynew.VerifyImageNew(afids, afidsNames, _Threshold, IPFaceChk, ref currrentpath, blusr);//FaceChk path of detected face
                             //This Code Added By Rahul Shukla on 05-12-2018
                             if (result == null || DisplayData == "Matched" || DisplayData == "Show Both")
                             {
                                 //Code Ended here by Rahul Shukla on 05-12-2018
                                 if (result != null && result.ToString().Trim() != string.Empty)
                                 {
                                     for (int count = 0; count < result.Count; count++)
                                     {
                                         _id = result[count].AFID.ToString();

                                         bool findinmatchafid = false;
                                         for (int j = 0; j <= matchafidsNames.Count - 1; j++)
                                         {
                                             if (matchafidsNames[j] == _id)
                                             {
                                                 findinmatchafid = true;
                                                 break;
                                             }
                                         }
                                         if (findinmatchafid)
                                         {
                                             //do nothing
                                         }
                                         else
                                         {
                                             personcount++;
                                             matchafidsNames.Add(_id);
                                         }

                                         if (_id != strperson)
                                         {
                                             strperson = _id;
                                             // personcount++;
                                         }
                                         lblPersonCount.Text = personcount.ToString();
                                         lblPersonCount1.Text = personcount.ToString();

                                         //Added by Ashwini on 15-03-2019 To set person count to 0 on date change
                                         if (datechangeflag == true)
                                         {
                                             personcount1 = 0;
                                             datechangeflag = false;
                                         }
                                         //Ended by Ashwini on 15-03-2019


                                         //Vishal upadhyay.26-06-18.We need a specific result for ID to check it further.
                                         //MySqlDataAdapter cmdGetHistory = new MySqlDataAdapter("Select * from ipcamera_details where type = 1", conn);
                                         MySqlDataAdapter cmdGetHistory = new MySqlDataAdapter("Select * from ipcamera_details where type = 1 AND face_id LIKE '" + _id + "' AND run_datetime='" + DateTime.Now.ToString("yyyy-MM-dd") + "'", conn);
                                         DataSet dsHistory = new DataSet();
                                         cmdGetHistory.Fill(dsHistory, "ipcamera_details");
                                         if (dsHistory.Tables[0].Rows.Count != 0)
                                         {
                                             //no need to matched ID .Now we have already mached it in query.If mached succesfull then count is greater then 0.
                                             // MatchFaceID = (dsHistory.Tables[0].Rows[dsHistory.Tables[0].Rows.Count - 1]["face_id"]).ToString();
                                         }
                                         else
                                         {//if ID is not present in face_id coloum then only increment
                                             personcount1++;
                                             personCountDateChange++;
                                         }
                                         //End of logic. Vishal upadhyay.26-06-18.

                                         if (_id != "")
                                         {
                                             _score = string.Format("{0:0.00}", Convert.ToDouble(result[count].Score.ToString()));
                                             _name = objDBFile.GetUserName(_id);
                                             _age = objDBFile.GetUserAge(_id);
                                             Bitmap srcimg1 = GetImage(_id);
                                             dtlocation = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                             Log("Fill Registerd user face detail for the user" + _id);
                                             //FillRegisteredFaceDetail(srcimg1, _bmpImg, _id, _name, _score);   //Show registered face details on tab
                                             string[] returnFiles = Directory.GetFiles(ImageFolder + "\\" + _id);
                                             // matchar.Add(new matchdata() { imagePath = returnFiles[0].ToString(), bmpPath = filename, id = _id, name = _name, score = _score, iplocatoin = dtlocation, age = _age });
                                             matchar.Add(new matchdata() { imagePath = returnFiles[0].ToString(), bmpPath = IPFaceChk, id = _id, name = _name, score = _score, iplocatoin = dtlocation, age = _age, cameraNo = IPCamID });
                                             MatchCount++;
                                             if (MatchCount <= 4)
                                             {
                                                 FillRegisteredFaceDetailArray(MatchCount - 1);
                                                 matchpage_no = 0;

                                             }
                                             else
                                             {
                                                 //added by vishal on 01-06-18 for Automatic Refresh the Register Panel for IP Camera.
                                                 // flpRegisterDisplay.Controls.Clear();

                                                 for (MatchCount = matchpage_no * 4; MatchCount < matchpage_no * 4 + 4; MatchCount++)
                                                 {
                                                     if (MatchCount < matchar.Count)
                                                     {
                                                         FillRegisteredFaceDetailArray(MatchCount);
                                                         //Added By Ashwini on 2019-01-03 to alter the display order of matched face token
                                                         int mpageno = trackBar1.Maximum;
                                                         //trackBar1.Maximum++;
                                                         matchpage_no = (mpageno == 1 ? 0 : mpageno - 1);
                                                         trackBar1.Value = matchpage_no + 1;
                                                         try
                                                         {
                                                             flpRegisterDisplay.Controls.Clear();
                                                             //get the first array value from current value ((current value -1) * 6) //means if current value is 3 than first array is 13 (value 12)
                                                             for (MatchCount = matchpage_no * 4; MatchCount < matchpage_no * 4 + 4; MatchCount++)
                                                             {
                                                                 if (MatchCount < matchar.Count)
                                                                 {
                                                                     FillRegisteredFaceDetailArray(MatchCount);
                                                                 }
                                                             }
                                                             Checkmatch_enable(matchpage_no);

                                                         }
                                                         catch (Exception ex)
                                                         {
                                                             MessageBox.Show(ex.Message);
                                                         }
                                                         //Ended by Ashwini on 2019-01-03
                                                     }
                                                 }
                                                 ////////////end of logic//////////
                                                 Checkmatch_enable(matchpage_no);
                                             }


                                             totalreg_face++;
                                             DBreg_face++;
                                             lblRegisterCount.Text = totalreg_face.ToString();
                                             lblRegisterCount1.Text = totalreg_face.ToString();
                                             //totalreg_facehistory = totalreg_face1 + totalreg_face;
                                             //Added By Ashwini on 2019-01-21 To display proper matched count on date change also
                                             //********************
                                             //Added by Ashwini on 11-03-2019 To retrive matched count from database
                                             //string match;
                                             //string unmatch;
                                             //objCdatabase.GetConnection();
                                             //objCdatabase.OpenConnection();
                                             //objCdatabase.CreateCommand();
                                             //match = objCdatabase.GetSinglValue("max(Total_Matched_Faces)", "ipcamera_details", "where run_datetime='" + DateTime.Now.ToString("yyyy-MM-dd") + "'").ToString();
                                             //if (match == "")
                                             //{
                                             //    match = "0";
                                             //}
                                             //totalreg_face1 = Convert.ToInt32(match);
                                             //totalreg_facehistory = totalreg_face1 + 1;

                                             //unmatch = objCdatabase.GetSinglValue("max(Total_Unmatched_Faces)", "ipcamera_details", "where run_datetime='" + DateTime.Now.ToString("yyyy-MM-dd") + "'").ToString();
                                             //if (unmatch == "")
                                             //{
                                             //    unmatch = "0";
                                             //}
                                             //totalunreg_face1 = Convert.ToInt32(unmatch);
                                             //totalunreg_facehistory = totalunreg_face1;


                                             string[] SplitIPFaceChk = IPFaceChk.Split('\\');
                                             //Vishal 08-06-18.Spliting is static i.e.6.we need last index
                                             // string IPframeName = SplitIPFaceChk[6].Split('.')[0];//Adarsh

                                             string IPframeName = SplitIPFaceChk[SplitIPFaceChk.Length - 1].Split('.')[0];
                                             //End of logic 08-06-18


                                             location = location_id;
                                             //Commented by Ashwini on 15-03-2019 
                                             //if (datechangeflag)
                                             //{
                                             //    totalRegFaceDateChange++;
                                             //    //Added By Ashwini on 2019-02-13 To assign matched face count on date change
                                             //    DBreg_face = totalRegFaceDateChange;

                                             //    _objInsertVideoDetails.InsertIPCameraDetails(IPCamID, _id, DateTime.Now.ToString("HH:mm:ss"), IPframeName, _score, "1", numSkip.ToString(), "", "", "frame" + location, "", totalRegFaceDateChange.ToString(), totalUnRegFaceDateChange.ToString(), personCountDateChange.ToString(), folder_name, "abc", _Threshold.ToString(), numSkip.ToString(), terminalid);

                                             //}
                                             //else
                                             //{
                                             //    _objInsertVideoDetails.InsertIPCameraDetails(IPCamID, _id, DateTime.Now.ToString("HH:mm:ss"), IPframeName, _score, "1", numSkip.ToString(), "", "", "frame" + location, "", totalreg_facehistory.ToString(), totalunreg_facehistory.ToString(), personcount1.ToString(), folder_name, "abc", _Threshold.ToString(), numSkip.ToString(), terminalid);
                                             //}

                                             //Added by Ashwini on 14-03-2019 To insert matched data in ipcamera_details through stored procedure
                                             conn1.Open();
                                             cmd1 = new MySqlCommand("insertipcameradetailsMatched", conn1);
                                             cmd1.CommandType = CommandType.StoredProcedure;
                                             cmd1.Parameters.AddWithValue("ipcamera_id", IPCamID);
                                             cmd1.Parameters.AddWithValue("face_id", _id.ToString());
                                             cmd1.Parameters.AddWithValue("location", DateTime.Now.ToString("HH:mm:ss"));
                                             cmd1.Parameters.AddWithValue("frame_id", IPframeName.ToString());
                                             cmd1.Parameters.AddWithValue("score", _score);
                                             cmd1.Parameters.AddWithValue("_type", 1);
                                             cmd1.Parameters.AddWithValue("suggested_img_faceid", null);
                                             cmd1.Parameters.AddWithValue("score_suggestion", null);
                                             cmd1.Parameters.AddWithValue("del_flag", "0");
                                             string s1 = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));
                                             cmd1.Parameters.AddWithValue("run_datetime1", s1);
                                             cmd1.Parameters.AddWithValue("update_flag", "0");
                                             cmd1.Parameters.AddWithValue("location_id", "frame" + location);
                                             cmd1.Parameters.AddWithValue("Camera_No", null);
                                             // cmd.Parameters.AddWithValue("@Total_Matched_Faces", MatchCount);
                                             // cmd.Parameters.AddWithValue("@Total_Unmatched_Faces", UnmatchCount);
                                             cmd1.Parameters.AddWithValue("Total_Matched_Person", personcount1);
                                             cmd1.Parameters.AddWithValue("IP_FolderName", folder_name.ToString());
                                             cmd1.Parameters.AddWithValue("IPCamPath", "abc");
                                             cmd1.Parameters.AddWithValue("Threshold", _Threshold);
                                             cmd1.Parameters.AddWithValue("Skip_Frame", numSkip);
                                             cmd1.Parameters.AddWithValue("terminalid", terminalid.ToString());
                                             string s = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));
                                             cmd1.Parameters.AddWithValue("CurrentDate", s);
                                             int results = cmd1.ExecuteNonQuery();
                                             if (results > 0)
                                             {

                                             }
                                             cmd1.Dispose();
                                             conn1.Close();
                                             //Ended by Ashwini on 15-03-2019

                                             location_id = location_id + numSkip;

                                         }
                                     }
                                 }
                             }
                             //This Code Added By Rahul Shukla on 05-12-2018
                             if ((DisplayData == "Unmatched" && result == null) || (DisplayData == "Show Both" && result == null))
                             {
                                 //Code Ended Here by Rahul Shukla ron 05-12-2018
                                 //int facecount = v.DetectFace(filename);
                                 //if (facecount > 0)
                                 //{
                                 int facecount = verifynew.DetectFaceIPCamera(_face);
                                 if (facecount > 0)
                                 {
                                     //START Vishal upadhyay,Ashwini.2018-10-08.To retrive the maximum face id from ipcam_details so that we can add next value in it.
                                     //START Vishal upadhyay 2018-10-09.New variable is created for the database insertion.
                                     if (conn.State == ConnectionState.Closed)
                                     {
                                         conn.Open();
                                     }
                                     DataSet dsfaceid1 = new DataSet();
                                     MySqlDataAdapter cmdGetIP1 = new MySqlDataAdapter("SET SESSION TRANSACTION ISOLATION LEVEL READ UNCOMMITTED ;SELECT MAX(CAST(face_id AS SIGNED)) AS face_id FROM ipcamera_details WHERE  TYPE=2 ;SET SESSION TRANSACTION ISOLATION LEVEL REPEATABLE READ ;", conn);


                                     cmdGetIP1.Fill(dsfaceid1, "faceid");
                                     conn.Close();
                                     if (dsfaceid1.Tables[0].Rows.Count > 0)
                                     {
                                         face_id = dsfaceid1.Tables[0].Rows[0]["face_id"].ToString();
                                     }
                                     if (face_id.Equals("") || face_id == null)
                                     {
                                         _URID_DB = 1;
                                     }
                                     else
                                     {
                                         _URID_DB = Convert.ToInt32(face_id);
                                         _URID_DB = _URID_DB + 1;
                                     }

                                     string _URfaceId = "";
                                     string _URfaceIdDB = "";
                                     // Added by vaibhav singh on 05-12-2018 , to control the unwanted 0 in database 
                                     if (_URID_DB > 9)
                                     {
                                         _URfaceIdDB = _URID_DB.ToString();
                                     }
                                     else
                                     {
                                         _URfaceIdDB = "0" + _URID_DB;
                                     }
                                     //------End of logic by Vaibhav ----  //

                                     if (_URID > 09)
                                     {
                                         _URfaceId = _URID.ToString();
                                         //_URfaceIdDB = _URID_DB.ToString();
                                     }
                                     else
                                     {
                                         _URfaceId = "0" + _URID;
                                         // _URfaceIdDB = "0" + _URID_DB;
                                     }
                                     //START Vishal upadhyay 2018-10-09.New variable is created for the database insertion.

                                     //END Vishal upadhyay,Ashwini.2018-10-08.To retrive the maximum face id from ipcam_details so that we can add next value in it.

                                     Log("Fill Unregisterd user face detail");

                                     _URID++;
                                     dtlocation = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                     //unmatchar.Add(new unmatchdata() { imgPath = filename, id = _URfaceId, iplocatoin = dtlocation, imagePath = filename });

                                     unmatchar.Add(new unmatchdata() { imgPath = IPFaceChk, id = _URfaceId, locatoin = dtlocation, imagePath = IPFaceChk, cameraNo = IPCamID });
                                     Unmatchcount++;
                                     if (Unmatchcount <= 4)
                                     {
                                         UnRegisterPanelDynamicDesignarray(Unmatchcount - 1);
                                         unmatchpage_no = 0;
                                     }
                                     else
                                     {
                                         //added by vishal on 01-06-18 for Automatic Refresh the UnRegister Panel for IP Camera.

                                         //flpUnregisterDisplay.Controls.Clear();

                                         for (Unmatchcount = unmatchpage_no * 4; Unmatchcount < unmatchpage_no * 4 + 4; Unmatchcount++)
                                         {
                                             if (Unmatchcount < unmatchar.Count)
                                             {
                                                 UnRegisterPanelDynamicDesignarray(Unmatchcount);

                                                 //Added by Ashwini on 2019-01-03 To alter the display order of unmatched face token
                                                 int unmpageno = Trckbarmatch.Maximum;
                                                 //Trckbarmatch.Maximum++;

                                                 //get unmatch page no
                                                 unmatchpage_no = (unmpageno == 0 ? 1 : unmpageno - 1);
                                                 Trckbarmatch.Value = unmatchpage_no + 1;
                                                 try
                                                 {
                                                     flpUnregisterDisplay.Controls.Clear();
                                                     //get the first array value from current value ((current value -1) * 6) //means if current value is 3 than first array is 13 (value 12)
                                                     for (Unmatchcount = unmatchpage_no * 4; Unmatchcount < unmatchpage_no * 4 + 4; Unmatchcount++)
                                                     {
                                                         if (Unmatchcount < unmatchar.Count)
                                                         {
                                                             UnRegisterPanelDynamicDesignarray(Unmatchcount);
                                                         }
                                                     }
                                                     Checkunmatch_enable(unmatchpage_no);
                                                 }
                                                 catch (Exception ex)
                                                 {
                                                     MessageBox.Show(ex.Message);
                                                 }
                                                 //Ended by Ashwini on 2019-01-03


                                             }
                                         }

                                         //////////End of logic////////////////////////
                                         Checkunmatch_enable(unmatchpage_no);

                                     }

                                     totalunreg_face++;
                                     DBunreg_face++;
                                     lblUnregisteredCount.Text = totalunreg_face.ToString();
                                     lblUnregisterCount1.Text = totalunreg_face.ToString();

                                     //Added by Ashwini on 15-03-2019 To set personcount to 0 on datechange
                                     if (datechangeflag == true)
                                     {
                                         personcount1 = 0;
                                         datechangeflag = false;
                                     }
                                     //Ended by Ashwini on 15-03-2019

                                     //totalunreg_facehistory = totalunreg_face1 + totalunreg_face;
                                     //Added By Ashwini on 2019-02-13 To display proper unmatched count on date change also
                                     //***************
                                     //Added by Ashwini on 11-03-2019 To retrive unmatched count from database
                                     //string unmatch;
                                     //string match;
                                     //objCdatabase.GetConnection();
                                     //objCdatabase.OpenConnection();
                                     //objCdatabase.CreateCommand();
                                     //unmatch = objCdatabase.GetSinglValue("max(Total_Unmatched_Faces)", "ipcamera_details", "where run_datetime='" + DateTime.Now.ToString("yyyy-MM-dd") + "'").ToString();
                                     //if (unmatch == "")
                                     //{
                                     //    unmatch = "0";
                                     //}
                                     //totalunreg_face1 = Convert.ToInt32(unmatch);
                                     //totalunreg_facehistory = totalunreg_face1 + 1;

                                     //match = objCdatabase.GetSinglValue("max(Total_Matched_Faces)", "ipcamera_details", "where run_datetime='" + DateTime.Now.ToString("yyyy-MM-dd") + "'").ToString();
                                     //if (match == "")
                                     //{
                                     //    match = "0";
                                     //}
                                     //totalreg_face1 = Convert.ToInt32(match);
                                     //totalreg_facehistory = totalreg_face1;
                                     //Ended by Ashwini on 11-03-2019
                                     //*****************
                                     //totalunreg_facehistory = totalunreg_face1 + DBunreg_face;
                                     //Ended By Ashwini on 2019-02-13
                                     string[] SplitIPFaceChk = IPFaceChk.Split('\\');

                                     //Vishal 08-06-18.Spliting is static i.e.6.we need last index
                                     // string IPframeName = SplitIPFaceChk[6].Split('.')[0];//Adarsh

                                     string IPframeName = SplitIPFaceChk[SplitIPFaceChk.Length - 1].Split('.')[0];
                                     //End of logic 08-06-18


                                     location = location_id;
                                     //Commented by Ashwini on 14-03-2019 
                                     //if (datechangeflag)
                                     //{
                                     //    totalUnRegFaceDateChange++;
                                     //    //Added By Ashwini on 2019-02-13 To assign unmatched face count on date change
                                     //    DBunreg_face = totalUnRegFaceDateChange;
                                     //    //personCountDateChange = 0;

                                     //    //Ended By Ashwini on 2018-02-13

                                     //    //START Vishal upadhyay.New variable is created for the database insertion.
                                     //    //Change is _URfaceId replaced by _URfaceIdDB.
                                     //    _objInsertVideoDetails.InsertIPCameraDetails(IPCamID, _URfaceIdDB, DateTime.Now.ToString("HH:mm:ss"), IPframeName, "", "2", numSkip.ToString(), "", "", "frame" + location, "", totalRegFaceDateChange.ToString(), totalUnRegFaceDateChange.ToString(), personCountDateChange.ToString(), folder_name, "abc", _Threshold.ToString(), numSkip.ToString(), terminalid);
                                     //    //END Vishal upadhyay.New variable is created for the database insertion.
                                     //}
                                     //else
                                     //{

                                     //    //START Vishal upadhyay.New variable is created for the database insertion.
                                     //    //Change is _URfaceId replaced by _URfaceIdDB.
                                     //    // Edited by vaibhav singh on 05-12-2018 , to add terminalid
                                     //    _objInsertVideoDetails.InsertIPCameraDetails(IPCamID, _URfaceIdDB, DateTime.Now.ToString("HH:mm:ss"), IPframeName, "", "2", numSkip.ToString(), "", "", "frame" + location, "", totalreg_facehistory.ToString(), totalunreg_facehistory.ToString(), personcount1.ToString(), folder_name, "abc", _Threshold.ToString(), numSkip.ToString(), terminalid);
                                     //    //END Vishal upadhyay.New variable is created for the database insertion.

                                     //}

                                     //Added by Ashwini on 14-03-2019 To insert unmatched face details in ipcamera_details through stored procedure
                                     conn1.Open();

                                     cmd1 = new MySqlCommand("insertipcameradetailsUnmatched", conn1);
                                     cmd1.CommandType = CommandType.StoredProcedure;
                                     cmd1.Parameters.AddWithValue("ipcamera_id", IPCamID);
                                     cmd1.Parameters.AddWithValue("face_id", _URfaceIdDB.ToString());
                                     cmd1.Parameters.AddWithValue("location", DateTime.Now.ToString("HH:mm:ss"));
                                     cmd1.Parameters.AddWithValue("frame_id", IPframeName.ToString());
                                     cmd1.Parameters.AddWithValue("score", null);
                                     cmd1.Parameters.AddWithValue("_type", 2);
                                     cmd1.Parameters.AddWithValue("suggested_img_faceid", null);
                                     cmd1.Parameters.AddWithValue("score_suggestion", null);
                                     cmd1.Parameters.AddWithValue("del_flag", "0");
                                     string s1 = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));
                                     cmd1.Parameters.AddWithValue("run_datetime1", s1);
                                     cmd1.Parameters.AddWithValue("update_flag", "0");
                                     cmd1.Parameters.AddWithValue("location_id", "frame" + location);
                                     cmd1.Parameters.AddWithValue("Camera_No", null);
                                     // cmd.Parameters.AddWithValue("@Total_Matched_Faces", MatchCount);
                                     // cmd.Parameters.AddWithValue("@Total_Unmatched_Faces", UnmatchCount);
                                     cmd1.Parameters.AddWithValue("Total_Matched_Person", personcount1);
                                     cmd1.Parameters.AddWithValue("IP_FolderName", folder_name.ToString());
                                     cmd1.Parameters.AddWithValue("IPCamPath", "abc");
                                     cmd1.Parameters.AddWithValue("Threshold", _Threshold);
                                     cmd1.Parameters.AddWithValue("Skip_Frame", numSkip);
                                     cmd1.Parameters.AddWithValue("terminalid", terminalid.ToString());
                                     string s = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));
                                     cmd1.Parameters.AddWithValue("CurrentDate", s);
                                     int results = cmd1.ExecuteNonQuery();
                                     if (results > 0)
                                     {

                                     }
                                     cmd1.Dispose();
                                     conn1.Close();

                                     //Ended by Ashwini on 15-03-2019
                                     location_id = location_id + numSkip;

                                 }
                             }
                             //}
                             //set the unmatch track bar minimum and maximum value
                             Trckbarmatch.Minimum = 1;
                             if (unmatchar.Count % 4 != 0)
                             {
                                 Trckbarmatch.Maximum = Convert.ToInt32(unmatchar.Count / 4) + 1;
                             }
                             else
                             {
                                 Trckbarmatch.Maximum = Convert.ToInt32(unmatchar.Count / 4);
                             }
                             Trckbarmatch.LargeChange = (Trckbarmatch.Maximum <= 10 ? 1 : Convert.ToInt32(Trckbarmatch.Maximum / 10));
                             //----------------------------------------------------------------
                             //set the match track bar minimum and maximum value
                             trackBar1.Minimum = 1;
                             if (matchar.Count % 4 != 0)
                             {
                                 trackBar1.Maximum = Convert.ToInt32(matchar.Count / 4) + 1;

                             }
                             else
                             {
                                 trackBar1.Maximum = Convert.ToInt32(matchar.Count / 4);
                             }
                             trackBar1.LargeChange = (trackBar1.Maximum <= 10 ? 1 : Convert.ToInt32(trackBar1.Maximum / 10));
                         }
                    }
                    VerifyNew.VerifyFace.RecutImage.Clear();
                    VerifyNew.VerifyFace.cutImage.Clear();

                    //Added By Vaibhav On 2018-11-01 To handle Exception
           
            }
            catch (Exception ex)
            {
                string filePath = ConfigurationManager.AppSettings["ErrorLog"];

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }

            }
            //Ended By Vaibhav On 2018-11-01
        }

        #region Name Click
        private void lnklblName_Click(object sender, EventArgs e)
        {
            LinkLabel lnk = sender as LinkLabel;
            FrmRegistrationDetails objFrmdetail = new FrmRegistrationDetails(lnk.Text, "Details");
            //FrmRegistrationDetails objFrmdetail = new FrmRegistrationDetails();
            objFrmdetail.ShowDialog();
        }
        #endregion
        public static bool ExecuteWithTimeLimit(TimeSpan timeSpan, Action codeBlock)
        {
            try
            {
                Task task = Task.Factory.StartNew(() => codeBlock());
                task.Wait(timeSpan);
                return task.IsCompleted;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerExceptions[0];
            }
        }
     
        private static Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }
  
        #region btnMatchPrev_Click
        private void btnMatchPrev_Click(object sender, EventArgs e)
        {
            try
            {
                matchpage_no--;
                flpRegisterDisplay.Controls.Clear();

                for (MatchCount = matchpage_no * 4; MatchCount < matchpage_no * 4 + 4; MatchCount++)
                {
                    if (MatchCount < matchar.Count)
                    {
                        FillRegisteredFaceDetailArray(MatchCount);
                    }
                }
                Checkmatch_enable(matchpage_no);
                if (trackBar1.Value == trackBar1.Minimum)
                {
                    btnMatchPrev.ForeColor = Color.OrangeRed;
                    btnMatchNext.ForeColor = Color.OrangeRed;
                    btnMatchPrev.Enabled = false;
                }
                trackBar1.Value = matchpage_no + 1;
                // trackBar1.Value = matchpage_no == 0 ? trackBar1.Minimum : matchpage_no;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region btnMatchNext_Click
        private void btnMatchNext_Click(object sender, EventArgs e)
        {
            try
            {

                matchpage_no++;
                flpRegisterDisplay.Controls.Clear();

                for (MatchCount = matchpage_no * 4; MatchCount < matchpage_no * 4 + 4; MatchCount++)
                {
                    if (MatchCount < matchar.Count)
                    {
                        FillRegisteredFaceDetailArray(MatchCount);
                    }
                }
                Checkmatch_enable(matchpage_no);
                if (trackBar1.Value == trackBar1.Maximum)
                {
                    btnMatchPrev.ForeColor = Color.OrangeRed;
                    btnMatchNext.ForeColor = Color.OrangeRed;
                    btnMatchNext.Enabled = false;
                }
                trackBar1.Value = matchpage_no + 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region Checkmatch_enable
        private void Checkmatch_enable(int page)
        {
            if (page == -1)
            {
                btnMatchPrev.ForeColor = Color.OrangeRed;
                btnMatchNext.ForeColor = Color.OrangeRed;
                btnMatchPrev.Enabled = false;
                btnMatchNext.Enabled = false;
            }
            else if (page == 0)
            {
                btnMatchPrev.ForeColor = Color.OrangeRed;
                btnMatchNext.ForeColor = Color.OrangeRed;
                btnMatchPrev.Enabled = false;
                btnMatchNext.Enabled = true;
            }
            //else if (page == (matchar.Count / 10) && (matchar.Count % 10 != 0))
            else if (page == (matchar.Count / 4) && (matchar.Count % 4 != 0))
            {
                btnMatchPrev.ForeColor = Color.OrangeRed;
                btnMatchNext.ForeColor = Color.OrangeRed;
                btnMatchNext.Enabled = false;
                btnMatchPrev.Enabled = true;
            }
            //else if (page + 1 == images.Count / 10 && (images.Count % 10 == 0))
            else if (page + 1 == matchar.Count / 4 && (matchar.Count % 4 == 0))
            {
                btnMatchPrev.ForeColor = Color.OrangeRed;
                btnMatchNext.ForeColor = Color.OrangeRed;
                btnMatchNext.Enabled = false;
                btnMatchPrev.Enabled = true;
            }
            else
            {
                btnMatchPrev.ForeColor = Color.OrangeRed;
                btnMatchNext.ForeColor = Color.OrangeRed;
                btnMatchPrev.Enabled = true;
                btnMatchNext.Enabled = true;
            }
        }
        #endregion

        #region show best image
        private void rbtnShowBest_CheckedChanged(object sender, EventArgs e)
        {
            flpRegisterDisplay.Controls.Clear();
            SortedDictionary<int, string> dicCompare;
            Dictionary<string, string> dicIDAndScore;
            List<string> lstCompare, lstBestMatch;
            try
            {
                dicCompare = new SortedDictionary<int, string>();
                int i = 0;
                foreach (FRS.Class.CShowRegisterDetails objClassShowRegister in lstShowregisterDetail)
                {
                    dicCompare.Add(i++, objClassShowRegister.UserID + "," + objClassShowRegister.Score);
                }
                var SortedList = dicCompare.OrderByDescending(d => d.Value).ToList();
                lstCompare = new List<string>();
                dicIDAndScore = new Dictionary<string, string>();
                foreach (var maxValue in SortedList)
                {
                    string[] arrIDScore = maxValue.Value.Split(',');
                    if (!lstCompare.Contains(arrIDScore[0]))
                    {
                        lstCompare.Add(arrIDScore[0]);
                        dicIDAndScore.Add(arrIDScore[0], arrIDScore[1]);
                    }
                }
                lstBestMatch = new List<string>();
                foreach (FRS.Class.CShowRegisterDetails objClassShowRegister in lstShowregisterDetail)
                {
                    if (dicIDAndScore[objClassShowRegister.UserID].Contains(objClassShowRegister.Score))
                    {
                        if (!lstBestMatch.Contains(objClassShowRegister.UserID))
                        {
                            lstBestMatch.Add(objClassShowRegister.UserID);
                            RegisterPanelDynamicDesign(objClassShowRegister);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                lstCompare = null;
                lstBestMatch = null;
                dicCompare = null;
                dicIDAndScore = null;
            }
        }
        #endregion

        #region reset button
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                btnMatchPrev.ForeColor = Color.OrangeRed;
                btnMatchNext.ForeColor = Color.OrangeRed;
                btnMatchPrev.Enabled = false;
                btnMatchNext.Enabled = false;
                rbtnShowBest.Checked = false;
                rbtnShowFirst.Checked = false;
                lstShowregisterDetail.Clear();
                objCdatabase.GetConnection();
                objCdatabase.OpenConnection();
                //DataTable dtRegisterFaceDetail = objCdatabase.Display("max(Video_ID)", "video_info");    //Get max video id
                flpRegisterDisplay.Controls.Clear();
                //GetRegisteredFaceDetail(dtRegisterFaceDetail.Rows[0][0].ToString(), "Details");   //This function get the details of newly registered face, show the details on tab and also delete image from unregister tab 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region show first image
        private void rbtnShowFirst_CheckedChanged(object sender, EventArgs e)
        {
            flpRegisterDisplay.Controls.Clear();
            List<string> lstCompare;
            try
            {
                lstCompare = new List<string>();
                foreach (FRS.Class.CShowRegisterDetails objClassShowRegister in lstShowregisterDetail)
                {
                    if (!lstCompare.Contains(objClassShowRegister.UserID))
                    {
                        lstCompare.Add(objClassShowRegister.UserID);
                        RegisterPanelDynamicDesign(objClassShowRegister);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                lstCompare = null;
            }
        }
        #endregion

        #region show all images
        private void rbtnShowAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                flpRegisterDisplay.Controls.Clear();
                foreach (FRS.Class.CShowRegisterDetails objClassShowRegister in lstShowregisterDetail)
                {
                    RegisterPanelDynamicDesign(objClassShowRegister);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Enable Disable prev and next button
        private void Checkunmatch_enable(int page)
        {
            if (page == -1)
            {

                btnunMatchprev.Enabled = false;
                btnunMatchNext.Enabled = false;
            }
            else if (page == 0)
            {
                btnunMatchprev.Enabled = false;
                btnunMatchNext.Enabled = true;
            }
            //else if (page == (matchar.Count / 10) && (matchar.Count % 10 != 0))
            else if (page == (unmatchar.Count / 4) && (unmatchar.Count % 4 != 0))
            {
                btnunMatchNext.Enabled = false;
                btnunMatchprev.Enabled = true;
            }
            //else if (page + 1 == images.Count / 10 && (images.Count % 10 == 0))
            else if (page + 1 == unmatchar.Count / 4 && (unmatchar.Count % 4 == 0))
            {
                btnunMatchNext.Enabled = false;
                btnunMatchprev.Enabled = true;
            }
            else
            {
                btnunMatchprev.Enabled = true;
                btnunMatchNext.Enabled = true;
            }
        }
        #endregion

        #region button UnMatched Next click
        private void btnunMatchNext_Click(object sender, EventArgs e)
        {
            try
            {
                //trackBar1.Right.ToString();
                unmatchpage_no++;
                flpUnregisterDisplay.Controls.Clear();

                for (Unmatchcount = unmatchpage_no * 4; Unmatchcount < unmatchpage_no * 4 + 4; Unmatchcount++)
                {
                    if (Unmatchcount < unmatchar.Count)
                    {
                        UnRegisterPanelDynamicDesignarray(Unmatchcount);
                    }
                }
                Checkunmatch_enable(unmatchpage_no);
                //Added By Rahul Shukla on 08-10-2018
                if (Trckbarmatch.Value == Trckbarmatch.Maximum)
                {
                    btnunMatchNext.Enabled = false;
                }
                Trckbarmatch.Value = unmatchpage_no + 1;

                //Logic End here
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region button UnMatched Prev click
        private void btnunMatchprev_Click(object sender, EventArgs e)
        {
            try
            {
                unmatchpage_no--;
                flpUnregisterDisplay.Controls.Clear();

                for (Unmatchcount = unmatchpage_no * 4; Unmatchcount < unmatchpage_no * 4 + 4; Unmatchcount++)
                {
                    if (Unmatchcount < unmatchar.Count)
                    {
                        UnRegisterPanelDynamicDesignarray(Unmatchcount);
                    }
                }
                Checkunmatch_enable(unmatchpage_no);
                //Added By Rahul Shukla on 08-10-2018
                if (Trckbarmatch.Value == Trckbarmatch.Minimum)
                {
                    btnunMatchprev.Enabled = false;

                }
                Trckbarmatch.Value = unmatchpage_no + 1;
                //Logic Ended Here Rahul Shukla
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Register Click
        private void lnklblRegister_Click(object sender, EventArgs e)
        {
            try
            {
                LinkLabel lnk = sender as LinkLabel;
                afidsFileNames = Common.AfidFileName;
                string[] image_path = lnk.Text.Split('$');
                string match = objDBFile.GetMatchFace();
                if (match == "Selected" && afidsFileNames.Count() == 0 && FrmSettingSelected.strdone == "Done")
                {
                }
                else if (match == "Selected" && afidsFileNames.Count() != 0) { }
                else
                    afidsFileNames = FindAfids(AFIDsFolder);
                afids = null;
                afids = ReadAfids(afidsFileNames).ToList();
                afidsNames.Clear();
                foreach (string afidFileName in afidsFileNames)
                    afidsNames.Add(Path.GetFileNameWithoutExtension(afidFileName));

                //result_list = v.VerifyTest(afids, afidsNames, ref path, image_path[1]);
                float score = 0.3f;

                bool usr = false;
                result_list = verifynew.VerifyImageNew(afids, afidsNames, score, image_path[1], ref path, usr);

                bool blusr = false;
                result_list = verifynew.VerifyImageNew(afids, afidsNames, score, image_path[1], ref path, blusr);

                FrmSuggestions objFrmSug = new FrmSuggestions(result_list, image_path[1]);
                //   FrmSuggestions objFrmSug = new FrmSuggestions();
                objFrmSug.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region TrackBar scroll for unmatched
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //get the current value of track bar
            int unmpageno = trackBar1.Value;
            //get unmatch page no
            // unmatchpage_no = (mpageno == 1 ? 1 : mpageno - 1);
            matchpage_no = (unmpageno == 1 ? 0 : unmpageno - 1);
            try
            {
                flpRegisterDisplay.Controls.Clear();
                //get the first array value from current value ((current value -1) * 6) //means if current value is 3 than first array is 13 (value 12)
                for (MatchCount = matchpage_no * 4; MatchCount < matchpage_no * 4 + 4; MatchCount++)
                {
                    if (MatchCount < matchar.Count)
                    {
                        FillRegisteredFaceDetailArray(MatchCount);
                    }
                }
                Checkmatch_enable(matchpage_no);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region TrackBar Scrool For matched
        private void Trckbarmatch_Scroll(object sender, EventArgs e)
        {
            //get the current value of track bar
            int mpageno = Trckbarmatch.Value;
            //get unmatch page no
            unmatchpage_no = (mpageno == 0 ? 1 : mpageno - 1);
            try
            {
                flpUnregisterDisplay.Controls.Clear();
                //get the first array value from current value ((current value -1) * 6) //means if current value is 3 than first array is 13 (value 12)
                for (Unmatchcount = unmatchpage_no * 4; Unmatchcount < unmatchpage_no * 4 + 4; Unmatchcount++)
                {
                    if (Unmatchcount < unmatchar.Count)
                    {
                        UnRegisterPanelDynamicDesignarray(Unmatchcount);
                    }
                }
                Checkunmatch_enable(unmatchpage_no);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void CreatPdf_Click(object sender, EventArgs e)
        {
            DateFilterIP objdateFilterIP = new DateFilterIP();
            objdateFilterIP.ShowDialog();
        }

        private void lblclose_Click(object sender, EventArgs e)
        {
            picbxvideos[actualval].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            picbxvideos[actualval].Location = new System.Drawing.Point(xlocation, ylocation);

            picbxvideos[actualval].Size = new System.Drawing.Size(hiddenwidth, hiddenhight);
           // picbxvideos[actualval].FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            picbxvideos[actualval].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            picbxvideos[actualval].TabIndex = 0;
            picbxvideos[actualval].TabStop = false;

            for (int i = 0; i < no_of_camera; i++)
            {
                if (i != actualval)
                {
                    picbxvideos[i].Show();
                }

            }

            lblclose.Visible = false;
        }

        private void DisplayDetectionIpCam_Load(object sender, EventArgs e)
        {
            
        }    

        private void DisplayDetectionIpCamBrowser_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                
               //Close the ffmpeg process
                    foreach (System.Diagnostics.Process myProc in System.Diagnostics.
                        Process.GetProcesses())
                        if (myProc.ProcessName == "ffmpeg")
                            myProc.Kill();       
                    Environment.Exit(Environment.ExitCode);
               
            }
            catch (AccessViolationException ex)
            {

            }
        }
        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Added by Ashwini on 18-04-2019 To restart the application at 12AM
            MainScreen.objFormFFMPEGMultipleDisplay.checkProcess();
            //Ended by Ashwini on 18-04-2019

            MainScreen.objFormFFMPEGMultipleDisplay.totalunreg_face1 = 0;
            //    totalreg_face = 0;
            MainScreen.objFormFFMPEGMultipleDisplay.totalreg_face1 = 0;
            //    personcount1 = 0;

            MainScreen.objFormFFMPEGMultipleDisplay.totalreg_facehistory = 0;
            MainScreen.objFormFFMPEGMultipleDisplay.totalunreg_facehistory = 0;

            MainScreen.objFormFFMPEGMultipleDisplay.datechangeflag = true;
            MainScreen.objFormFFMPEGMultipleDisplay.totalRegFaceDateChange = 0;
            MainScreen.objFormFFMPEGMultipleDisplay.totalUnRegFaceDateChange = 0;
            MainScreen.objFormFFMPEGMultipleDisplay.personCountDateChange = 0;
           
            //Added By Ashwini on 2018-12-28 To generate PDF at 12AM
            DateFilterIP objDateFilterIP = new DateFilterIP();
            objDateFilterIP.TextBoxFromValue = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            objDateFilterIP.TextBoxToValue = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            timer.Stop();
            //MainScreen.objFormDynamicCamera.checkIP();
            objDateFilterIP.BtnSearch_Click(sender, e);
            //Added by Ashwini to set timer after 12AM for next day
            DateTime nowTime = DateTime.Now;



            DateTime scheduledTime = DateTime.Today.AddDays(1);
            double tickTime = (double)(scheduledTime - DateTime.Now).TotalMilliseconds;
            timer = new System.Timers.Timer(tickTime);

            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
            //Ended By Ashwini on 2018-12-28
   }

        private void FFMPEGDisplay_Load(object sender, EventArgs e)
        {
            
        }
        public void timer_ElapsedforPing(object sender, ElapsedEventArgs e)
        {
            for (int IPping = 0; IPping < NumberOfCamera; IPping++)
            {
                if (strIPaddress[IPping] != "" && strIPaddress[IPping] != null)
                {
                    Ping myPing1 = new Ping();
                    PingReply reply1 = myPing1.Send(strIPaddress[IPping], 5000);
                    bool pingable1 = reply1.Status == IPStatus.Success;
                    if (!pingable1)
                    {

                        BlProcess[IPping] = false;
                        CameraNum = IPping;
                        CaptureFromIPCamera(strPassword[IPping], strID[IPping], strIPaddress[IPping], strPort[IPping], strPathCam[IPping]);
                    }
                }
            }
        }

        internal void CaptureFromIPCamera(char p, char p_2, char p_3, char p_4, char p_5)
        {
            throw new NotImplementedException();
        }

        //Added by Ashwini on 15-04-2019 To add newly created AFID in AFID list
        private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {


            IList<byte[]> afids1 = new List<byte[]>();
            System.Threading.Thread.Sleep(1000);
            using (FileStream fs1 = new FileStream(e.FullPath, FileMode.Open, FileAccess.Read))
            {
                byte[] afid1 = new byte[fs1.Length];

                fs1.Read(afid1, 0, (int)fs1.Length);
                afids1.Add(afid1);

                FFMPEGMultipleDisplayVer3.afids.Add(afid1);

            }
        }
        //Ended by Ashwini on 15-04-2019
    }
}
