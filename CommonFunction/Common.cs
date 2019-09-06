//-----------------------------------------------------------------------
//(C) Copyright, 2007, Unikaihatsu Software Pvt. Ltd. All Rights Reserved
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace FRS
{
    #region Common
    /// <summary>
    /// Defines the global variables, flags, tables name, messages for overall project
    /// </summary>
    ///<!--Date :2007/10/01 By Hasmukh Mandavia-->

  
    class Common
    {
        
        public const long MAXSIZE = 1000000;                //INI Max. File size
        public const string LOG_FILE_NAME = "FRSlog.txt";   //Error log file name
        public const string BACKUP_FILE_NAME = "FRSlog.bak";//Backup file name
        public const string INI_FILE_NAME = "FRSLog.ini";      //INI File Name
        public const string PROJECT_CODE = "FRS";           //Project Code
        public const int SUCCESS_CODE = 0;                  //Success Code
        public const int FAIL_CODE = -1;                    //Failure Code

        public const string LOGIN_SCR_CD = "LGN";           //Login screen code
        public const string CHANGE_PASS_SCR_CD = "LCP";     //Change Password screen code
        public const string EDTUSR_RGT_SCR_CD = "EUR";      //Edit user right screen code
        public const string SET_SCR_CD = "SET";      //Edit user right screen code
        public const string MAIN_MENU_CD = "MAN";


        //Added By: Vishal Upadhyay,2017/12/21.
        //******************Start******************//

        public const string strcmpmsg = "Please select company name first";
        public const string strdepartmentmsg = "Please select department name first";
        public const string strdesignationmsg = "Please select designation name first";
        public const string strmontherror = "Can not select month in the future";
        public const string strmonth = "Please select month first";
        public const string stryear = "Please select year first";
        public const string strdateformat_duration = "{0:yyyy/MM/dd HH:mm:ss}";
        public const string stralreadyexported = "Can not export data as attendance calculation is already done for selected month and year";
        public const string strtimeerror = "SignIn time can not be greter than SignOut time";
        public const string updatesuccess = "Record updated successfully";
        public const string updatefailure = "Updation failed";
        public const string selectemp = "Please select employee name first";
        public const string signinerror = "Please enter correct signin datetime";
        public const string signouterror = "Please enter correct signout datetime";
        public const string strnodata = "Data are not available for this date";
        public const string strcombo_and_Head = "Value of ComboBox and Headname should not be Empty ";
        //Added Code by Rahul Shukla on 01-11-2018
        public const string strHeadValue = "Head Value cannot be Empty";
        //Ended Code Here by Rahul Shukla on 01-11-2018
        public const string strcombo= "Please Select Value from Sequence Control ";
        public const string strHead = "Value of Headname should not be Empty ";
        public const string strinsertcontrol = "You can insert only 12 control";
        public const string strData_Saved = " Details Saved successfully";
        public const string strData_not_Saved = "SeqNo. is duplicate";

        //*******************End*******************//

        //------Added By Nisha on 2018/02/23---------

        public const string strEngAlrt = "Alert Message";
        public const string strJapAlrt = "警報メッセージ";

        public const string strEngSuces = "Successful Message";
        public const string strJapSuces = "成功したメッセージ";

        public const string strEngCnfrm = "Confirmation Message";
        public const string strJapCnfrm = "確認メッセージ";

        public const string strEngFun = "This functionality is not available";
        public const string strJapFun = "この機能は利用できません";

        public const string strEngVdoPth = "Please Select video path";
        public const string strJapVdoPth = "ビデオパスを選択してください";

        public const string strEngSlctImg = "Please Select images";
        public const string strJapSlctImg = "イメージを選択してください";

        public const string strEngDlt = "Record deleted successfully";

        public const string strEngDltRw = "Are you sure to delete row?";
        public const string strJapDltRw = "行を削除してもよろしいですか？";
    

        public const string strEngDltclm = "Unable to delete column";
        public const string strJapDltclm = "列を削除できません";

        public const string strEngCsv = "CSV file saved";
        public const string strJapCsv = "CSVファイルを保存しました";

        public const string strEngNodata = "File has no data";
        public const string strJapNodata = "ファイルにデータがありません";

        public const string strEngCsSv = "CSV template file is saved";
        public const string strJapCsSv = "CSVテンプレートファイルが保存されます";

        public const string strEngFc = "User not enrolled with this face";
        public const string strJapFc = "この顔に登録されていないユーザー";

        public const string strEngIntgr = "Please enter integer only";
        public const string strJapIntgr = "整数のみを入力してください";

        public const string strEngDtlUpdt = "Details updated successfully";
        public const string strEngNtUpdt = "Details are not updated successfully";

        public const string strEngScr = "Score Value should not exceed 1.0";
        public const string strJapScr = "スコア値は1.0を超えてはなりません";

        public const string strEngUndrscr = "Head Name should not contain space for separation please use '_' ";

        public const string strJapGls = "メガネを着用してください。";
        public const string strEngGls = "Please put/wear a glasses on face.";

        public const string strJapReg = "ユーザーが正常に登録されました";
        public const string strEngReg = "User Registered Successfully";

        public const string strEngSeqNo = "Seq. is duplicate please select other seq. no.";

        //------Added By vishal on 2018/02/28---------
        public const string strEngCmpVid = "compelete video show";
        public const string strJapCmpVid = "ビデオ終了";
        //------ended By vishal on 2018/02/28---------
        

        //------Ended By Nisha on 2018/02/23---------

        //------Added By vishal on 2018/02/26---------
        //Head Name validations:
        public const string headnamespace = "Head Name should not be contain signle character(use '_' for separation)";
        public const string headnamedigit = "Head Name should not be digit/number";
        public const string headnamedempty="Head Name should not be empty";
        //------Ended By vishal on 2018/02/26---------

        //********************* Database Tables Name *********************
        public const string TBL_SYSETTINGS = "SYSSETTINGS";
        public const string TBL_USERINFO = "USER_INFO";
        public const string TBL_VIDEO_INFO = "video_info";
        public const string TBL_LOGININFO = "LOGININFO";
        //********************* Database Tables Name *********************

        //Common error messgae when error occures
        public const string MSG_SYS_ERR = "System Error Occured, View Error Log";

        //Common Caption for message box
        //public const string MSG_CAPTION = "GatePass Generation System";

        //************************** Common Messages ****************************
        public const string MSG_SM0001 = "Either Username or Password is invalid";
        public const string MSG_SM0002 = "Invalid Password. Please retype old password";
        public const string MSG_SM0003 = "Invalid Password. Passwords you entered do not match";
        public const string MSG_SM0004 = "Password changed Successfully";
        public const string MSG_SM0005 = "Please enter password";
        public const string MSG_SM0006 = "Are you sure you want to delete";
        public const string MSG_SM0007 = "Username should be between 4-10 characters";
        public const string MSG_SM0008 = "Password should be between 4-10 characters";
        public const string MSG_SM0009 = "Registration Completed Successfully";
        public const string MSG_SM0010 = "Please enter both Gate No & Gate Name";
        public const string MSG_SM0011 = "Please select at least one checkbox";
        public const string MSG_SM0012 = "User name already exists";
        public const string MSG_SM0013 = "Are you sure you want to Logout?";
        public const string MSG_SM0014 = "Search Is Complete.There Are No Results To Display";
        public const string MSG_SM0015 = "Gate No already present.Please enter another Gate No";
        public const string MSG_SM0016 = "Time Already Alloted.Please Select different time";
        public const string MSG_SM0017 = "AccessTo Must Be Greater Than AccessFrom DateTime";
        public const string MSG_SM0033 = "Please select at least one System Access Rights";

        //************************** Common Messages ****************************

        public static int RGT_DEFAULT = 0;      //Flag for default rights
        public static int RGT_REG = 0;          //Flag for registration rights
        public static int RGT_ACT = 0;          //Flag for activation rights
        public static int RGT_SET = 0;          //Flag for settings rights
        public static int Count = 1;

        //********************* Database Tables Name *********************
      //  public const string TBL_SYSETTINGS = "SYSSETTINGS";
      //  public const string TBL_USERINFO = "USER_INFO";
        public const string TBL_formvalues = "formvalues";
       // public const string TBL_VIDEO_INFO = "video_info";
 
        //********************* Database Tables Name *********************



        //Falg to handle MainMenu Form Activted event
        public static Boolean bolValidate = false;
        public static Boolean bolLoggedIn = false;
        public const string SET_REG = "Registration";
        public const string SET_SUGG = "Suggestion";
        public const string SET_RCG = "Recognition";     
        public const string SET_APP = "Application";
        public const string SET_ATT = "Attendance";
        public const string SET_CURR = "Current";
        public const string SET_CAM = "Camera";//Pragnesha
        public const string SET_RESOLUTION = "Resolution";
       
        //variable declare for japanese setting menu// done by vishal
        public const string JAP_SET_REG = "登録";
        public const string JAP_SET_SUGG = "提案";
        public const string JAP_SET_RCG = "認識";
        public const string JAP_SET_APP = "応用";
        public const string JAP_SET_ATT = "出欠席";
        public const string JAP_SET_CURR = "現在";
        public const string JAP_SET_CAM = "カメラ";//Pragnesha
        public const string JAP_SET_RESOLUTION = "解決";
       
        //--------------------------------------------------//
        public const string SET_ATTR_IMG_NO = "No. of images";
        //This Code Written By Rahul shukla on 29-11-2018
        public const string SET_ATTR_Display = "Display Data";
        //Code Ended here by Rahul Shukla on 29-11-2018
        public const string SET_ATTR_FAIL_COUNT = "MaxFailCount";
        public const string SET_ATTR_THRESHOLD = "Threshold";
        public const string SET_ATTR_LANGUAGE = "Language";
        public const string SET_ATTR_MATCH_FACE = "Match Faces";
        public const string SET_ATTR_MUNCIPAL_ID = "TerminalId";
        public const string SET_ATTR_CUR_TERMINAL = "Current Terminal";    
        public const string SET_SKIP_FRAME = "Skip Frame";
        public const string SET_ATTR_CAM_NO = "No.of Camera";
        public const string SET_ATTR_PAGESIZE = "PageSize";
        public const string SET_ATTR_DELETE= "LocalDelete";
        //Added By Ashwini on 2018-12-25 To add Terminal ID form PDF report generation
        public const string SET_ATTR_REPORT_TERMINALID = "Generate Report From";
        //Ended By Ashwini on 2018-12-25
        
       // public const string SET_ATTR_IPCAM = "IP Camera";
        public const string SET_ATTR_RESOLUTION = "Resolution";
        //--------------------------------------------------//
        public const string JAP_SET_ATTR_IMG_NO = "イメージ 番号";
        public const string JAP_SET_ATTR_FAIL_COUNT = "最大失敗回数";
        public const string JAP_SET_ATTR_THRESHOLD = "閾値";
        public const string JAP_SET_ATTR_LANGUAGE = "言語";
        public const string JAP_SET_ATTR_MATCH_FACE = "マッチフェイス";
        public const string JAP_SET_ATTR_MUNCIPAL_ID = "ターミナルID";
        public const string JAP_SET_ATTR_CUR_TERMINAL = "現在のターミナル";
        public const string JAP_SET_SKIP_FRAME = "スキップフレーム";
        public const string JAP_SET_ATTR_MODE = "デフォルトモード";
        public const string JAP_SET_ATTR_SCORE = "スコア";
        public const string JAP_SET_ATTR_VIDEO_CONTINOUSMODE = "継続モード";
        public const string JAP_SET_ATTR_AUDIO_MODE = "オーディオモード";
        public const string JAP_SET_ATTR_CAM_NO = "カメラ数";
        public const string JAP_SET_ATTR_PAGESIZE = "ページサイズ";
        public const string JAP_SET_ATTR_DELETE = "地元削除";
        public const string JAP_SET_ATTR_IPCAM = "IPカメラ";
        public const string JAP_SET_ATTR_RESOLUTION = "解決";
        //--------------------------------------------------//
        public const string JAP_SET_ATTR_MODE_STILL = "スチルカメラ";
        public const string JAP_SET_ATTR_MODE_VIDEO = "ビデオカメラ";      
        public const string JAP_SET_ATTR_DISP_YEAR = "表示年";    
        public const string JAP_SET_ATTR_COL_SEPRATOR = "カラムセパレータ";
        //***************************** Nirav
        public const string REGADD_SCR_CD = "RGA";
        public const string REGEDIT_SCR_CD = "RGE";
        public const string REGDETAIL_SCR_CD = "RGD";
        public const string REGMODIFY_SCR_CD = "RGM";
        public static bool REGADD_CAPTURE = false;
        //***************************** Nirav


        //***************************** Sandeep
        public const string LblImage_Text_Image = "Image";
        public const string LblImage_Text_PersonID = "ID";
        public const string LblImage_Text_PersonName = "Name";
        public const string SCH_OPT_SCR_CD = "SCH_OPT";
        public const string ACT_GATEDIT_SCR_CD = "ACT_GATEDIT";
        public const string ACT_GATACC_SCR_CD = "ACT_GATACC";

        //***************************** Sandeep

        //***************************** Jimit
        public const string RGC_CAPTURE_SCR_CD = "RGC";
        public const string REC_CAPTURE_SCR_CD = "REC";
        public const string COMM_IMG_SCR_CD = "CIP";
        public static bool SET_LANG_FLAG_JAP = false;
        public static bool SET_LANG_FLAG_ENG = false;
        public const string JAP_CULTURE = "ja-JP";
        //***************************** Jimit

        public static int MAINMENU_TOP = 0;
        public static int MAINMENU_LEFT = 0;
        public static int MAINMENU_HEIGHT = 0;
        public static int MAINMENU_WIDTH = 0;

        public static string SPINSERTDAILYTRACK = "spInsertDailyTrack";
        public static string SPUPDATESIGNINOUT = "spUpdateSignInOut_Manual";
        public static string SPINSERTMONTHLYATT = "spInsertMonthlyAttendance";
        public static string SPUPDATEMONTHLYATT = "spUpdateMonthlyAttendance";
        public static string ENCRPTKEY = "myKey123";
        public static string EXPORTEDIMAGESPATH = CImgProc.strFilePath.Substring(6) + "\\ExportImport\\Images";
        public static string IMPORTEDIMAGESPATH = "//Images";
        public static string GPRSIMPORTDIR = "\\RemoteImport";
        public static string AFIDIMPORTDIR = "//AFIDs";
        public static string IMPORTEDBACKUPPATH = CImgProc.strFilePath.Substring(6) + "\\ExportImport\\ImportedBackup";
        public static string IMAGEFILEEXTENSION = ".jpeg";
        public static string EXPORTFILENAME = "ExportedData";
        public static string EXPORTFILENAMEGPRS = "ExportedDataGPRS";
        public static string EXPORTFILEEXTENSION = ".sql";

        public static string INVALIDID = "invalid";


        //Start vishal upadhyay 02-08-2018
        public static bool detVideo;
        public static bool detImage;
        public static bool detIpCam;

        public static bool regWeb;
        public static bool regImage;
        public static bool regCSV;
        public static bool regIpCam;

        public static int ipCamera;
        
        
        //End vishal upadhyay 02-08-2018
      



        //Extra Variable Added by: Bharat Prajapati
        //Purpose: To get the Selected Month and Year for the Attendance search in Common Logic
        public static int ATTENDANCE_MONTH = 0;
        public static int ATTENDANCE_YEAR = 0;
        public static string WELCOME_FILE = CImgProc.strFilePath.Substring(6) + "/Sound Files/welcome.wav";
        public static string ERROR_FILE = CImgProc.strFilePath.Substring(6) + "/Sound Files/error.wav";

        public static int REG_TRIAL_COUNT = 5;
       // public static string USERIMGFILEPATH = CImgProc.strFilePath.Substring(6) + "\\ExportImport\\Images\\";

        //vishal 08-06-18.For shoud not be static it will come from app.config file.
        //public static string USERIMGFILEPATH = CImgProc.strFilePath.Substring(6) + "\\ExportImport\\EnrollImages\\";
        public static string USERIMGFILEPATH = ConfigurationManager.AppSettings["ImagePath"].ToString();
        //End of logic vishal 08-06-18
      
        //GPGRS
        public static string PDFFILE = "\\GatePass.pdf";

        //GPGRS SDK 1.1 - Jimit
        public static string DATAFILEPATH = "\\data\\AFIDEngineData";
        public static string AFIDFILEPATH = CImgProc.strFilePath.Substring(6) + "\\ExportImport\\AFID\\";
        public static string AFIDEXTENSION = ".afid";
        public static string AFIDSEPARATOR = "-";
        public static string RUNDATE = "";
        public struct AFIDData
        {
            public byte[] AFIDArr;
            public string AFIDName;
        }

        public struct AFIDMatch
        {
            public Common.AFIDData afid;
            public float Score;
        }
        //////////Added by Avni//////
    
        /// <summary>
        /// afidFileName
        /// </summary>
        private static List<string> afidFileName = new List<string>();

        public static List<string> AfidFileName
        {
            get { return afidFileName; }
            set { afidFileName = value; }
        }


    }
    #endregion
}
