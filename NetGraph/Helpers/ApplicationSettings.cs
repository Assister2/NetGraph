using CyConex.Graph;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;

namespace CyConex
{
    public class ApplicationSettings
    {
        public bool AutoSelectLastNode;
        public bool AutoCenterGraph;
        public bool AutoSaveOnChanges;
        public bool AutoSaveOnTimer;
        public bool OpenLastDocumentOnStart; 
        public bool RestoreWindowStateOnStart;
        public bool ShowImportPreview;
        public string LastFile;
        public List<JObject> ResentFiles = new List<JObject>();
        public bool UseLastNodeColorAsDefault = false;
        public bool UseLastNodeShapeAsDefault = false;
        public bool UseLastNodeSizeAsDefault = false;
        public bool AskNodeTitleWhenAdding = true;
        public string DefaultNodeTitle = "Control";
        public bool MainWindowMaximized = false;
        public System.Drawing.Rectangle MainWindowPosition;
        public bool SaveCalculationLog = true;
        public bool ShowCalculationLog = false;
        public bool ShowNodeFocusCue = true;
        public bool ShowEdgeDistribution = false;
        public decimal DefaultNodeSize = 100;
        public ManualTargetingMode ManualTargetingMode;
        public bool AllowSelfConnectedNodes = false;
        public bool AutoCalc = false;
        //Do not show
        public bool ShowDeleteTreeDialog = true;
        public bool ShowDeleteNodeDialog = true;
        public bool ShowClearAllEdgesDialog = true;
        public bool ShowClearAllNodesAndLinks = true;
        public bool ShowClearAllLinksFromNode = true;
        public bool ShowClearAllLinksToNode = true;
        public bool ShowClearAllEdges = true;
        public bool ShowRelayoutDialog = true;
        public bool ShowMaxFontSizeReached = true;
        public bool ShowMinFontSizeReached = true;
        public bool ShowDeleteEdgeDialog = true;

        public bool ShowRiskPanel = false;
        public bool ShowNodeValuesPanel = false;
        public bool ShowCompliancePanel = false;
        public bool ShowRiskListPanel = false;
        public bool ShowNodesPanel = false;
        public bool ShowNodeRepository = false;
        public bool ShowNodeInformation = false;
        public bool ShowRiskHeatMap = false;
        public bool ShowNodeDataPanel= false;


        //View
        public bool ShowGraphTab = true;
        public bool ShowNodesTab = true;
        public bool ShowLinkedNodes = true;
        public bool ShowLabelsOnGraph = true;
        public bool ShowTextOnGraph = true;
        public bool ShowGrid = true;
        public bool ShowLabel = true;
        public bool SwitchTitle = true;
        //Colorize
        public bool ColorizeNodes = false;

        public bool NodeSizeIsedgeStrengthValue = false;
        public decimal DefaultNodeedgeStrengthValue = 30;
        public decimal DefautNodeBasedValue = 50;
        public decimal DefaultedgeStrengthValue = 2;
        public string EdgeRelationdata = "";
        public JArray NodeinherentStrengthData = new JArray();
        public JArray EdgeStrengthList = new JArray();
        public JArray EdgeDisplayList = new JArray();
        public JArray NodeimplementedStrengthData = new JArray();
        public JArray KeyWordsData = new JArray();
        public TenantItem SelectedTenantItem = new TenantItem();
        public EnterpriseItem SelectedEnterpriseItem = new EnterpriseItem();
        
        public string InfoBoxDefaultDescriptionText = "Testing";

        public JObject NodeAttrAsset = new JObject();
        public JObject NodeAttrAttack = new JObject();
        public JObject NodeAttrActor = new JObject();
        public JObject NodeAttrVulnerability = new JObject();
        public JObject EdgeAttr = new JObject();

        public static string TenantName = "bluesquaresoftware";
        public static string AADAppClientId = "0e4738a1-e093-4fb1-9b40-78e3251f0b26";
        public static string PolicySignUpSignIn = "B2C_1_EnterpriseLogin";
        public static string ApiScopes = "https://bluesquaresoftware.onmicrosoft.com/f6b4f665-8f16-4578-b852-b413d3345a5a/NetGraph.Read";
        public static string AzureFunctionUserUrl = "https://userenterprise.azurewebsites.net/api/v1/users";
        public static string MvcUserConsentUrl = "https://userenterprisetenant.azurewebsites.net/UserConsent";
        public static string MvcEnterpriseInsertUrl = "https://userenterprisetenant.azurewebsites.net/Enterprise/InsertEnterprise";
        public static string MvcTenantInsertUrl = "https://userenterprisetenant.azurewebsites.net/Tenant/InsertTenant";
        public static string MvcEnterpriseChangeUrl = "https://userenterprisetenant.azurewebsites.net/Enterprise/EditEnterprise";
        public static string MvcTenantChangeUrl = "https://userenterprisetenant.azurewebsites.net/Tenant/EditTenant";
        public static string ApiRootURL = "https://apim-netgraph-dev-001.azure-api.net";
        public static string ApiSubscriptionKey = "5fe49599e3bf43688b7cd4fd339987c3";
        public static string AzureTenantID = "a8ad6ad6-67ce-4e12-809b-c675c9f07d56";
        public static string ApiTokenURL = "https://login.microsoftonline.com/a8ad6ad6-67ce-4e12-809b-c675c9f07d56/oauth2/v2.0/token";
        public static string OAuthTokenClientID = "1e0804d2-9d8b-47ca-a50b-5031b573c08a";
        public static string OAuthTokenClientSecret = "Ikq8Q~VJ1CdqKCAYJg4RweGM~QliJRCTXbXxKabp";
        public static string OAuthTokenScope = "https://bluesquaresoftware.onmicrosoft.com/80428c2d-ce2f-4ae3-9959-5cf6daffe82e/.default";
        public string[] graphFileExtension = {"graph"};
        public JArray DockingLayouts = new JArray();

        public static string ZoopleHTMLEditorHiddenButtons = "tsbShowCode;tsbPrintPreview;tsbPrint;tsbCut;tsbCopy;tsbPaste;tsbundo;tsbRedo;tsbBold;tsbItalic;tsbUnderline;tsbStrikeout;tsbSuperscript;tsbSubscript;tsbRemoveFormatting;tsbAlignLeft;tsbAlignCentre;tsbAlignRight;tsbAlignJustify;tsbOrderedList;tsbUnorderedList;tsbOutdent;tsbIndent;tsbInsertLink;tsbRemoveLink;InsertImageToolStripButton;TableOptionsToolStripMenuItem;FormatSelectionCombo;tsbElementProperties;tsbInsertSymbol;";
        private static string _applicationFolder
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }

        /// <summary>
        /// Load application settings
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static ApplicationSettings Load(string fileName = "netgraph.ini")
        {
            ApplicationSettings retval = new ApplicationSettings();
            if (String.IsNullOrEmpty(Path.GetDirectoryName(fileName)))
            {
                fileName = Path.Combine(_applicationFolder, fileName);
            }
            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            }
            if (File.Exists(fileName))
            {
                var settings = new JsonSerializerSettings { Error = (se, ev) => ev.ErrorContext.Handled = true };
                retval = JsonConvert.DeserializeObject<ApplicationSettings>(File.ReadAllText(fileName), settings);
            }
            else
            {
                File.WriteAllText(fileName, JsonConvert.SerializeObject(retval), Encoding.UTF8);
            }
            return retval;
        }

        /// <summary>
        /// Save settings
        /// </summary>
        /// <param name="fileName"></param>
        public bool Save(string fileName = "netgraph.ini")
        {
            bool retval = false;
            try
            {
                if (String.IsNullOrEmpty(Path.GetDirectoryName(fileName)))
                {
                    fileName = Path.Combine(_applicationFolder, fileName);
                }
                if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                }
                File.WriteAllText(fileName, JsonConvert.SerializeObject(this, Formatting.Indented), Encoding.UTF8);
                retval = true;
            }
            catch
            {
                return false;
                //Do nothing 
            }
            return retval;
        }
    }
}
