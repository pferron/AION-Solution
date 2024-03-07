using AION.BL.Adapters;
using AION.BL.Models;
using DemoInterface.Helpers;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Windows.Forms;
using DemoInterface.Adapters;
using MapLoaderInterface.Adapters;
using Meck.Azure;

namespace DemoInterface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            rtbDemoInfo.Visible = true;
        }




        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnStartMMFTest_Click(object sender, EventArgs e)
        {
            //  DemoEstimator mDemo = new DemoEstimator( );

            //  rtbDemoInfo.Text = mDemo.ProjectEstimationDefaultsCreationTest();
        }

        private void btnRunEpmSamples_Click(object sender, EventArgs e)
        {
            //  DemoEstimator mDemo = new DemoEstimator();

            //   rtbDemoInfo.Text = mDemo.RunJsonSamples();
        }

        private void btnLoadAccelaRecordToAION_Click(object sender, EventArgs e)
        {

            DemoFileUtility mDemofileUtility = new DemoFileUtility();

            mDemofileUtility.SetupAccelaEngineTests();

            EstimationAccelaAdapter mEstimationAdapter = new EstimationAccelaAdapter();

            ProjectParms mProjectParms = new ProjectParms()
            {
                PerformAutoEstimation = false,
                ProjectId = txtRecID.Text,
                IsReschedule = false,
                StatusMessage = string.Empty,
                LoggedInUserEmail = "testuser@someplace.com"
            };

            rtbDemoInfo.Clear();

            var result = mEstimationAdapter.GetProjectDetailsLoad(mProjectParms);

            rtbDemoInfo.Text = JsonConvert.SerializeObject(result);

        }

        private void txtRecID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadTest_Click(object sender, EventArgs e)
        {
            //dgvAccelaToAionMap.Visible = false;
            rtbDemoInfo.Visible = true;
            OpenFileDialog mOpenFileDialog = new OpenFileDialog();

            // mOpenFileDialog.Filter = "csv|(*.csv);All Files(*.*)";
            mOpenFileDialog.ShowReadOnly = true;
            mOpenFileDialog.ShowDialog();

            txtCSVAccelaProjectModel.Text = mOpenFileDialog.FileName;

            txtCSVAccelaProjectModel.Refresh();

            if (String.IsNullOrWhiteSpace(txtCSVAccelaProjectModel.Text))
            {
                MessageBox.Show("Select the CSV file to be loaded");
                return;
            }
            else
            {
                {

                    AccelaEngineAdapter mAccelaAdapter = new AccelaEngineAdapter();


                    var result = mAccelaAdapter.TEstAIONRecordBeforeLoad(mOpenFileDialog.FileName);

                    txtCSVAccelaProjectModel.Text = result.ToString(); 

                }
            }
        }

        private void btnParseProfessionals_CLick(object sender, EventArgs e)
        {
            SampleParsing mParserSample = new SampleParsing();

          var result =   mParserSample.GetProfessionDestailobject(txtRecID.Text);
          
          txtCSVAccelaProjectModel.BringToFront();

          foreach (var prof in result)
          {
          
              txtCSVAccelaProjectModel.Text = " ------------------  \n\r" +  prof.To_String() + "---------------------- \n\r"; 



          } 
        }

    }

}

