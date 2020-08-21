using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TestAutomation.Framework.Constants;
using TestAutomation.Framework.Helpers.Assertions;
using TestAutomation.Framework.Helpers.AutoIT;
using TestAutomation.Framework.Helpers.Files;
using TestAutomation.Framework.Helpers.PDF;
using TestAutomation.Framework.Helpers.UI_Helpers;

namespace TestAutomation.PDFTests.StepDefinitions
{
    [Binding]
    public sealed class PDFDownloadSteps: UIFramework
    {
        private readonly AutoITHelper autoITHelper = new AutoITHelper();
        readonly FileExtentions fileExtentions = new FileExtentions();
        public PDFDownloadSteps(IWebDriver driver)
        {
            UIController.Instance.Driver = driver;
        }

        [Given(@"I navigate to pdf url")] 
        public void GivenINavigateToPdfUrl()
        {
            UIActions.NavigateToUrl();

            // Delete existing report
            string filePattern = "test-automation-brochure.*?.pdf";
            string downloadPath = Path.GetDirectoryName(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) + ConfigurationManager.AppSettings["Files"];
            fileExtentions.DeleteFilesWithPattern(downloadPath, filePattern);
        }

        [Then(@"I can download pdf")]
        [When(@"I download pdf")]
        public void ThenICanDownloadPdf()
        {
            System.Threading.Thread.Sleep(5000);
            PDFHelper.SaveWebPDF();
            autoITHelper.FileSaveAs();

            string filePattern = "test-automation-brochure.*?.pdf";
            string downloadPath = Path.GetDirectoryName(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) + ConfigurationManager.AppSettings["Files"];

            fileExtentions.VerifyFileExists(downloadPath, filePattern);
        }

        [Then(@"I can read text from pdf")]
        public void ThenICanReadTextFromPdf()
        {
            string pdfName = ScenarioContext.Current.Get<string>("AutoITClipboard");
            string pdfText = PDFHelper.ExtractTextFromPdf(pdfName + ".pdf");
        }

        [Then(@"I can verify pdf contains required text")]
        public void ThenICanVerifyPdfContainsRequiredText()
        {
            string pdfName = ScenarioContext.Current.Get<string>("AutoITClipboard");
            bool pdfContainsText = PDFHelper.Contains(pdfName + ".pdf", "Test Automation");
        }

        [Then(@"I can verify pdf page count")]
        public void ThenICanVerifyPdfPageCount()
        {
            string pdfName = ScenarioContext.Current.Get<string>("AutoITClipboard");
            int pdfPageCount = PDFHelper.PageCount(pdfName + ".pdf");
            AssertHelpers.AssertEquals(pdfPageCount.ToString(), "6");
        }

    }
}
