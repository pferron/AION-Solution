using AIONData;
using ReadAIONDb.Dtos;
using ReadAIONDb.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;


namespace ReadAIONDb
{
    class Program
    {
        private static string ADUsername = ConfigurationManager.AppSettings["ADUsername"];
        private static string ADPassword = ConfigurationManager.AppSettings["ADPassword"];
        private static string ADDomain = ConfigurationManager.AppSettings["Domain"];

        private static List<string> source_load = new List<string>();
        private static List<KeyValuePair<string, string>> _accelaList = new List<KeyValuePair<string, string>>();
        private static List<ReadVW_Review_Result_TradeDto> tempVW_Review_Result = null;
        private static List<ReadPROJECT_CYCLEDto> tblProjectCycle = null;
        private static int AION_PROJECT_ID = 0;
        private static void InitLoad()
        {
            #region Epm Source 

            //source_load.Add("428426");
            //source_load.Add("436252");
            //source_load.Add("436515");
            //source_load.Add("436530");
            //source_load.Add("436557");
            //source_load.Add("436614");
            //source_load.Add("436621");
            //source_load.Add("436652");
            //source_load.Add("436675");
            //433973
            //source_load.Add("433973");
            //source_load.Add("RV-433973-001");
            //source_load.Add("RV-429692-001");
            //source_load.Add("417890");
            //source_load.Add("422283");
            //source_load.Add("423721");
            //source_load.Add("423722");
            //source_load.Add("435207");
            //source_load.Add("429274");
            //source_load.Add("435161");
            //source_load.Add("436480");
            //source_load.Add("436490");
            //source_load.Add("436514");
            //source_load.Add("436540");
            //source_load.Add("436606");
            //source_load.Add("436615");
            //source_load.Add("429692");
            source_load.Add("431711");
            //source_load.Add("433231");
            //source_load.Add("436500");
            //source_load.Add("436517");
            //source_load.Add("436555");
            //source_load.Add("436586");
            //source_load.Add("418056");
            //source_load.Add("427539");
            //source_load.Add("434530");
            //source_load.Add("434950");
            //source_load.Add("435609");

            //source_load.Add("433973");
            #endregion region
            #region Accela information
            using (new Impersonator(ADUsername, ADDomain, ADPassword))
            {
                using (var context = new AIONDbContext())
                {
                    foreach (var item in source_load)
                    {
                        var isNumeric = int.TryParse(item, out _);
                        if (isNumeric)
                        {
                            var rec_id = context.Database.SqlQuery<string>($@"SELECT column2 from [dbo].[Acceladata] where column1 = {item}").FirstOrDefault();
                            _accelaList.Add(new KeyValuePair<string, string>(item, rec_id));
                        }
                        else
                        {
                            var rec_id = context.Database.SqlQuery<string>($@"SELECT column2 from [dbo].[Acceladata] where column1 = '{item}'").FirstOrDefault();
                            _accelaList.Add(new KeyValuePair<string, string>(item, rec_id));
                        }
                    }
                }
            }
            #endregion
        }
        static void Main(string[] args)
        {
            InitLoad();
            StartProcessingProjects();
        }

        private static void StartProcessingProjects()
        {
            string tempprojectnumber = string.Empty;
            foreach (var item in source_load)
            {
                string ProjectNumber = item;
                using (new Impersonator(ADUsername, ADDomain, ADPassword))
                {
                    using (var context = new AIONDbContext())
                    {
                        //var isNumeric = int.TryParse(ProjectNumber, out _);
                        //if (isNumeric)
                        //{
                        //    //tempprojectnumber = int.Parse(ProjectNumber);
                        //    //if (tempprojectnumber == 436517)
                        //    //{
                        TransferProjectFromEPMToAION(ProjectNumber);
                        Console.WriteLine("Project transfered:" + tempprojectnumber.ToString());
                        //    //}
                        //}
                        //else
                        //{
                        //tempprojectnumber = int.Parse(ProjectNumber.Replace("RV-", "").Replace("-001", ""));
                        //if (tempprojectnumber > 0)
                        //{
                        //TransferProjectFromEPMToAION(tempprojectnumber, ProjectNumber);
                        //Console.WriteLine("Project transfered:" + tempprojectnumber.ToString());
                        //}
                        //    }
                    }
                }
                Console.WriteLine("Project: " + tempprojectnumber.ToString() + " processed...");
            }
            Console.WriteLine("Project transfered:");
        }

        private static void TransferProjectFromEPMToAION(string project_number)
        {
            var bigQuery = $@"select * from
				(
				SELECT
						    nme_project as PROJECT_NM,
						 -1 as EXTERNAL_SYSTEM_REF_ID,
						(CASE
				 			WHEN id_project_state = 1003 then 6
				 			WHEN id_project_state = 1004 then 7
				 			WHEN id_project_state = 1015 then 8
				 			WHEN id_project_state = 1016 then 10
				 			WHEN id_project_state = 1005 then 11
				 			WHEN id_project_state = 1006 then 13
				 			WHEN id_project_state = 1025 then 14
				 			WHEN id_project_state = 1026 then 16
				 			WHEN id_project_state = 1025 then 17
				 			WHEN id_project_state = 1013 then 22
				  			WHEN id_project_state = 1027 then 22
				  			WHEN id_project_state = 1011 then 24
				  			WHEN id_project_state = 1012 then 26
						  else -1 end) as PROJECT_STATUS_REF_ID ,
						  (CASE
				  			WHEN txt_review_type_code  = 'RT' then 2
				  			WHEN txt_review_type_code  = 'OS' then 2
				  			WHEN txt_review_type_code  = 'MP' then 2
				  			WHEN txt_review_type_code  = 'PS' then 2
				  			WHEN txt_review_type_code  = 'PL' then 2
				  			WHEN txt_review_type_code  = 'ER' then 1
						  else -1 end) as PROJECT_TYP_REF_ID,
						 (CASE WHEN id_user IS NULL THEN NULL ELSE CONCAT(999 , u.id_user) END) AS ASSIGNED_ESTIMATOR_ID,
						 (CASE WHEN txt_review_type_code  = 'OS' then 2 WHEN txt_review_type_code  = 'ER' then 1 ELSE -1 END) AS PROJECT_MODE_REF_ID,
						(CASE WHEN txt_review_type_code  = 'RT' THEN 1 ELSE 0 END) AS RTAP_IND,
						txt_address as PROJECT_ADDR_TXT,
						permit_fee as TOTAL_FEE_AMT,
						(CASE WHEN id_project_manager IS NULL THEN NULL ELSE CONCAT(999, id_project_manager)END) AS PROJECT_MANAGER_ID,
						p.[project_number] as SRC_SYSTEM_VAL_TXT,
						 review_start_date as TAG_CREATED_BY_TS,
						review_end_date as TAG_UPDATED_BY_TS,
						(CASE WHEN txt_review_type_code  = 'PL' then 1 else 0 end) AS PRELIMINARY_IND,
						0 as GATE_ACCEPTED_IND,
						0 as FIFO_IND,
						plans_ready_on as PLANS_READY_ON_DT,
						team_score_indicator as TEAM_GRADE_TXT,
						[gate_close_date] as GATE_DT,				
						[txt_cde_summary_code] as BUILD_CODE_VERSION_DESC,				
						CASE p.app_form_xml.value('(//rblPropertyType)[1]', 'varchar(max)')
							WHEN 'FA' THEN '1-2 Family -=> 3.5 Stories'
							WHEN 'TH' THEN 'TH, LFS -=> 3.5 Stories'
							WHEN 'CN' THEN 'Condo or Apts'
							WHEN 'CO' THEN 'Commercial'
						   ELSE ''
						 END AS REVIEW_TYP_REF_DESC,
						CASE app_form_xml.value('(//rblBuildingCode)[1]', 'varchar(max)')
							WHEN 'BC-2012' THEN '2012 NC Building Code'
							WHEN 'BC-2015' THEN '2015 NC Existing Building Code'
							WHEN 'BC-2018' THEN '2018 NC Building Code'
							WHEN 'EBC-2018' THEN '2018 NC Existing Building Code'
						  ELSE ''
						 END AS [CODE_SUMMARY_DESC],
						 CASE(select txt_constr_type_desc from dbo.ctb_constr_types_defs where txt_constr_types_code = app_form_xml.value('(//rblConstrType)[1]', 'varchar(max)'))
							WHEN 'I-A'   THEN '1A * NONCOMBUSTIBLE/PROTECTED'
							WHEN 'I-B'   THEN '1B * NONCOMBUSTIBLE/UNPROTECTED'
							WHEN 'II-A'  THEN '2A * NONCOMBUSTIBLE/PROTECTED'
							WHEN 'II-B'  THEN '2B * NONCOMBUSTIBLE/UNPROTECTED'
							WHEN 'III-A' THEN '3A * NONCOMBUSTIBLE WALLS/PROTECTED'
							WHEN 'III-B' THEN '3B * NONCOMBUSTIBLE WALLS/UNPROTECTED'
							WHEN 'IV'    THEN '4 * HEAVY TIMBER'
							WHEN 'V-A'   THEN '5A * WOOD FRAME/PROTECTED'
							WHEN 'V-B'   THEN '5B * WOOD FRAME/UNPROTECTED'
						   ELSE ''
						 END AS [CONSTR_TYP_DESC] ,
						 LTRIM(RTRIM((select txt_worktype_desc from dbo.ctb_worktypes_defs where txt_worktype_code = app_form_xml.value('(//rblWorkType)[1]', 'varchar(max)')))) [WORK_TYP_DESC], 
						 app_form_xml.value('(//txtProposedWork)[1]', 'varchar(max)') [OVERALL_WORK_SCOPE_DESC],
						 app_form_xml.value('(//txtElecWrkScope)[1]', 'varchar(max)') [ELCTR_WORK_SCOPE_DESC],
 			 			 app_form_xml.value('(//txtMechWrkScope)[1]', 'varchar(max)') [MECH_WORK_SCOPE_DESC],
						 app_form_xml.value('(//txtPlumbWrkScope)[1]', 'varchar(max)') [PLUMB_WORK_SCOPE_DESC],
						 app_form_xml.value('(//txtCivilWrkScope)[1]', 'varchar(max)') [CIVIL_WORK_SCOPE_DESC],
						 p.project_number as [PERMIT_NUM],
						 app_form_xml.value('(//txtTotalSheets)[1]', 'varchar(max)') SHEETS_CNT_DESC,
						 CONCAT(
						   (--frmStructural
							   CASE WHEN LEN(p.app_form_xml.value('(//frmStructural/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN
								 CASE WHEN ISNULL(app_form_xml.value('(//frmStructural/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
									   AND ISNULL(app_form_xml.value('(//frmStructural/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
								  THEN App_form_xml.value('(//frmStructural/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
								  ELSE  app_form_xml.value('(//frmStructural/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmStructural/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
								END
							END),
							(--frmFireProtection
								CASE WHEN LEN(p.app_form_xml.value('(//frmFireProtection/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN
									CASE WHEN ISNULL(app_form_xml.value('(//frmFireProtection/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
				   						 AND ISNULL(app_form_xml.value('(//frmFireProtection/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
									THEN App_form_xml.value('(//frmFireProtection/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
									ELSE  app_form_xml.value('(//frmFireProtection/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmFireProtection/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
								END
							END),
							(--frmCivil
								CASE WHEN LEN(p.app_form_xml.value('(//frmCivil/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN
									CASE WHEN ISNULL(app_form_xml.value('(//frmCivil/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
										AND ISNULL(app_form_xml.value('(//frmCivil/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
										THEN App_form_xml.value('(//frmCivil/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
										ELSE  app_form_xml.value('(//frmCivil/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmCivil/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
									END
							END),
							(--frmPlumbing
							CASE WHEN LEN(p.app_form_xml.value('(//frmPlumbing/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN
									CASE WHEN ISNULL(app_form_xml.value('(//frmPlumbing/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
										AND ISNULL(app_form_xml.value('(//frmPlumbing/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
										THEN App_form_xml.value('(//frmPlumbing/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
										ELSE  app_form_xml.value('(//frmPlumbing/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmPlumbing/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
									END
							END),
							(--frmMechanical
								CASE WHEN LEN(p.app_form_xml.value('(//frmMechanical/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN
									 CASE WHEN ISNULL(app_form_xml.value('(//frmMechanical/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
										   AND ISNULL(app_form_xml.value('(//frmMechanical/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
										  THEN App_form_xml.value('(//frmMechanical/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
										  ELSE  app_form_xml.value('(//frmMechanical/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmMechanical/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
									 END
							END),
							(--frmElectrical
							CASE WHEN LEN(p.app_form_xml.value('(//frmElectrical/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN
								 CASE WHEN ISNULL(app_form_xml.value('(//frmElectrical/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
									   AND ISNULL(app_form_xml.value('(//frmElectrical/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
									  THEN App_form_xml.value('(//frmElectrical/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
									  ELSE  app_form_xml.value('(//frmElectrical/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmElectrical/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
								 END
							END),
						 (--frmArchitect
							CASE WHEN LEN(p.app_form_xml.value('(//frmArchitect/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN
								  CASE WHEN ISNULL(app_form_xml.value('(//frmArchitect/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
									   AND ISNULL(app_form_xml.value('(//frmArchitect/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
									  THEN App_form_xml.value('(//frmArchitect/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
									  ELSE  app_form_xml.value('(//frmArchitect/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmArchitect/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
								 END
							END)
						) AS [DESIGNER_DESC],
						CONCAT(
							(--frmStructural
								CASE WHEN LEN(p.app_form_xml.value('(//frmStructural/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN
			      					  CASE WHEN ISNULL(app_form_xml.value('(//frmStructural/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
										AND ISNULL(app_form_xml.value('(//frmStructural/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
								  THEN App_form_xml.value('(//frmStructural/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
								  ELSE  app_form_xml.value('(//frmStructural/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmStructural/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
								END
							END),
							(--frmFireProtection
								CASE WHEN LEN(p.app_form_xml.value('(//frmFireProtection/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN
									CASE WHEN ISNULL(app_form_xml.value('(//frmFireProtection/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
				   						 AND ISNULL(app_form_xml.value('(//frmFireProtection/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
									THEN App_form_xml.value('(//frmFireProtection/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
									ELSE  app_form_xml.value('(//frmFireProtection/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmFireProtection/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
								END
							END),
							(--frmCivil
								CASE WHEN LEN(p.app_form_xml.value('(//frmCivil/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN
									CASE WHEN ISNULL(app_form_xml.value('(//frmCivil/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
										AND ISNULL(app_form_xml.value('(//frmCivil/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
										THEN App_form_xml.value('(//frmCivil/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
										ELSE  app_form_xml.value('(//frmCivil/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmCivil/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
									END
							END),
							(--frmPlumbing
							CASE WHEN LEN(p.app_form_xml.value('(//frmPlumbing/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN
									CASE WHEN ISNULL(app_form_xml.value('(//frmPlumbing/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
										AND ISNULL(app_form_xml.value('(//frmPlumbing/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
										THEN App_form_xml.value('(//frmPlumbing/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
										ELSE  app_form_xml.value('(//frmPlumbing/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmPlumbing/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
									END
							END),
							(--frmMechanical
								CASE WHEN LEN(p.app_form_xml.value('(//frmMechanical/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN
									 CASE WHEN ISNULL(app_form_xml.value('(//frmMechanical/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
										   AND ISNULL(app_form_xml.value('(//frmMechanical/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
										  THEN App_form_xml.value('(//frmMechanical/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
										  ELSE  app_form_xml.value('(//frmMechanical/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmMechanical/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
									 END
							END),
							(--frmElectrical
							CASE WHEN LEN(p.app_form_xml.value('(//frmElectrical/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN
								 CASE WHEN ISNULL(app_form_xml.value('(//frmElectrical/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
									   AND ISNULL(app_form_xml.value('(//frmElectrical/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
									  THEN App_form_xml.value('(//frmElectrical/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
									  ELSE  app_form_xml.value('(//frmElectrical/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmElectrical/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
								 END
							END),
						 (--frmArchitect
							CASE WHEN LEN(p.app_form_xml.value('(//frmArchitect/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN
								  CASE WHEN ISNULL(app_form_xml.value('(//frmArchitect/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
									   AND ISNULL(app_form_xml.value('(//frmArchitect/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
									  THEN App_form_xml.value('(//frmArchitect/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
									  ELSE  app_form_xml.value('(//frmArchitect/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmArchitect/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
								 END
							END)
						) AS [SEAL_HOLDERS_DESC],
						CONCAT(
							'FireNFPAType13R:',
							 app_form_xml.value('(//chkNFPAType13R)[1]', 'varchar(max)')
							,';','FireFirePump:',
							 app_form_xml.value('(//rblFirePump)[1]', 'varchar(max)')
							,';','FireElevator:',
							 app_form_xml.value('(//rblElevator)[1]', 'varchar(max)')
							,';','FireDrawingsIncluded:',
							 app_form_xml.value('(//rblIncludesAlarmAndSprinklerDwg)[1]', 'varchar(max)')
							,';','FireNFPAType13:',
							 app_form_xml.value('(//chkNFPAType13)[1]', 'varchar(max)')
							,';','FireStandpipeClassIII:',
							 app_form_xml.value('(//chkStandpipeClassIII)[1]', 'varchar(max)')
							,';','FireStandpipeClassI:',
							 app_form_xml.value('(//chkStandpipeClassI)[1]', 'varchar(max)')
							,';','FireStandpipeClassWet:',
							 app_form_xml.value('(//chkStandpipeClassWet)[1]', 'varchar(max)')
							,';','FireStandpipe:',
							 app_form_xml.value('(//rblStandpipe)[1]', 'varchar(max)')
							,';','FirePumpNewOrExisting:',
							 app_form_xml.value('(//rblFirePumpType)[1]', 'varchar(max)')
							,';','FireStandpipeClassDry:',
							 app_form_xml.value('(//chkStandpipeClassDry)[1]', 'varchar(max)')
							,';','FireSmokeDetector:',
							 app_form_xml.value('(//rblSmokeDetection)[1]', 'varchar(max)')
							,';','FireStandpipeClassII:',
							 app_form_xml.value('(//chkStandpipeClassII)[1]', 'varchar(max)')
							,';','FireBuildingSprinkled:',
							 app_form_xml.value('(//rblBuildingSprinklered)[1]', 'varchar(max)')
							,';','id:CE_COM-FIRE.cDETAILS;'
							,'FireFireAlarm:',
							 app_form_xml.value('(//rblFireAlarm)[1]', 'varchar(max)')
							,';','FireNFPAType13D:',
							 app_form_xml.value('(//chkNFPAType13D)[1]', 'varchar(max)')
							,';','FireNFPANewOrExisting:',
							 app_form_xml.value('(//rblSprinklerType)[1]', 'varchar(max)')
							,';','FireNFPANewOrExisting:',
 							 app_form_xml.value('(//rblSprinklerType)[1]', 'varchar(max)')
						) AS [FIRE_DETAIL_DESC],
						CASE app_form_xml.value('(//frmPrimaryOccupancy/frmFactoryOrIndustryDtl/rblFactoryOrIndustryType)[1]', 'varchar(max)')
								WHEN 'F1' THEN 'F1 * Factory/Industrial - Moderate Hazard'
								WHEN 'F2' THEN 'F2 * Factory/Industrial - Low Hazard'
							ELSE
							CASE app_form_xml.value('(//frmPrimaryOccupancy/frmStorageDtl/rblStorageType)[1]', 'varchar(max)')
								WHEN 'S1' THEN 'S1 * Storage - Moderate Hazard'
	 							WHEN 'S2' THEN 'S2 * Storage - Low Hazard'
							ELSE
							CASE app_form_xml.value('(//frmPrimaryOccupancy/frmAssemblyDtl/rblAssemblyType)[1]', 'varchar(max)')
								WHEN 'A1' THEN 'A1 * Assembly - Theater w/o Stage'
								WHEN 'A2' THEN 'A2 * Assembly - Restaurants, Bars, Banquet Halls'
								WHEN 'A3' THEN 'A3C * Assembly ? Common Assemblies'
								WHEN 'A4' THEN 'A4 * Assembly ? Indoor Arena, Skating Rink, Tennis Court'
								WHEN 'A5' THEN 'A5 * Assembly ? Outdoor Stadium, Bleacher, Grandstand'
							ELSE
							CASE app_form_xml.value('(//frmPrimaryOccupancy/frmHighHazardDtl/rblHighHazardType)[1]', 'varchar(max)')
								WHEN 'H1' THEN 'H1 * High Hazard ? Explosives'
								WHEN 'H2' THEN 'H2 * High Hazard ? Deflagration'
								WHEN 'H3' THEN 'H3 * High Hazard ? Readily Combustible'
								WHEN 'H4' THEN 'H4 * High Hazard ? Health Hazard'
								WHEN 'H5' THEN 'H5 * High Hazard - HPM'
							ELSE
							CASE app_form_xml.value('(//frmPrimaryOccupancy/frmResedentialDtl/rblResedentialType)[1]', 'varchar(max)')
		    					WHEN 'R1' THEN 'R1 * Residential ? Hotels'
								WHEN 'R2' THEN 'R2 * Residential - Multiple Family'
								WHEN 'R3' THEN 'R3 * Residential - Single Family'
								WHEN 'R4' THEN 'R4 * Residential - Care/Assisted Living Facilities, Condition 1 (Ambulatory)'
							ELSE
							CASE app_form_xml.value('(//frmPrimaryOccupancy/frmInstitutionalDtl/rblInstitutionalType)[1]', 'varchar(max)')
								WHEN 'I1' THEN 'I1 * Institutional - Supervised Environment, Condition 1 (Ambulatory)'
								WHEN 'I2' THEN 'I2H * Institutional - Incapacitated ? Hospital, Full Nursing & Medical Treatment'
								WHEN 'I3' THEN 'I3 * Institutional - Restrained'
								WHEN 'I4' THEN 'I4 * Institutional - Day Care'
								WHEN 'I-3 Use Condition' THEN 'I3 * Institutional - Restrained'
							ELSE
							CASE
								WHEN app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'BS' THEN 'B * Business'
								WHEN app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'ED' THEN 'E * Education'
								WHEN app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'MR' THEN 'M * Mercantile'
								WHEN app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'ED' THEN 'E * Education'
								WHEN app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'UM' THEN 'U * Utility'
							ELSE ''
								END
								END
								END
								END
								END
								END
							END AS [OCCUPANCY_DESC],
							CASE app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)')
								WHEN 'AS' THEN 'Assembly'
								WHEN 'BS' THEN 'Business'
								WHEN 'ED' THEN 'Educational'
								WHEN 'MR' THEN 'Mercantile'
								WHEN 'FI' THEN 'Factory/Industrial'
								WHEN 'HH' THEN 'High Hazard'
								WHEN 'IN' THEN 'Institutional'
								WHEN 'RE' THEN 'Residential'
								WHEN 'ST' THEN 'Storage'
								WHEN 'UM' THEN 'Utility/Miscellaneous'
							ELSE ''
							END AS [PRI_OCCUPANCY_DESC], 
							CASE app_form_xml.value('(//frmSecondaryOccupancy/rblOccupancyType)[1]', 'varchar(max)')
								WHEN 'AS' THEN 'Assembly'
								WHEN 'BS' THEN 'Business'
								WHEN 'ED' THEN 'Educational'
								WHEN 'MR' THEN 'Mercantile'
								WHEN 'FI' THEN 'Factory/Industrial'
								WHEN 'HH' THEN 'High Hazard'
								WHEN 'IN' THEN 'Institutional'
								WHEN 'RE' THEN 'Residential'
								WHEN 'ST' THEN 'Storage'
								WHEN 'UM' THEN 'Utility/Miscellaneous'
							ELSE ''
							END AS [SECONDARY_OCCUPANCY_DESC], 
							CAST(CAST(app_form_xml.value('(//txtSqFtArea)[1]', 'varchar(max)') AS money)AS varchar) [SQUARE_FOOTAGE_DESC],
							app_form_xml.value('(//txtZoning)[1]', 'varchar(max)') [ZONING_OF_SITE_DESC],
							CASE
								WHEN app_form_xml.value('(//chkChangeOfUse)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END AS [CHG_OF_USE_DESC],
								app_form_xml.value('(//txtPreviousBiz)[1]', 'varchar(max)') [PREVIOUS_BUSINESS_TYP_DESC],
								app_form_xml.value('(//txtPropsedBiz)[1]', 'varchar(max)') [PROPOSED_BUSINESS_TYP_DESC],
							CASE app_form_xml.value('(//rblHasCondProjNum)[1]', 'varchar(max)')
								WHEN 'N' THEN 'No'
								WHEN 'Y' THEN 'Yes'
							ELSE ''
							END AS [CONDITIONAL_PERMIT_APPROVAL_DESC],
							CONCAT(
								'CityAddingImperviousArea:',
								 app_form_xml.value('(//chkAddingImperviousArea)[1]', 'varchar(max)')
								,';','CityChangingDrivewayEtc:',
								app_form_xml.value('(//chkChangingDrivewayEtc)[1]', 'varchar(max)')
								 ,';','CityAddingTreePlanting:'
								,app_form_xml.value('(//chkAddingTreePlanting)[1]', 'varchar(max)')
								,';','CityAddingTreeProtection:',
								app_form_xml.value('(//chkAddingTreeProtection)[1]', 'varchar(max)')
								,';','CityGradingMoreThanOneAcre:',
								app_form_xml.value('(//chkGradingMoreThanOneAcre)[1]', 'varchar(max)')
								,';','CityAddingImperviousArea:',
								app_form_xml.value('(//chkAddingImperviousArea)[1]', 'varchar(max)')
								,';','CityChangingDrivewayEtc:',
								app_form_xml.value('(//chkChangingDrivewayEtc)[1]', 'varchar(max)')
								,';','CityAddingTreePlanting:',
								app_form_xml.value('(//chkAddingTreePlanting)[1]', 'varchar(max)')
								,';','CityAddingTreeProtection:',
								app_form_xml.value('(//chkAddingTreeProtection)[1]', 'varchar(max)')
								,';','CityGradingMoreThanOneAcre:',
								app_form_xml.value('(//chkGradingMoreThanOneAcre)[1]', 'varchar(max)')
								,';','CityBuildingOver1000SqFt:',
								app_form_xml.value('(//chkBuildingOver1000SqFt)[1]', 'varchar(max)')
								,'CityBuildingOver5PercentSqFt:',
								app_form_xml.value('(//chkBuildingOver5PercentSqFt)[1]', 'varchar(max)')
								,';','CityAdding11OrMoreParkingSpace:',
								app_form_xml.value('(//chkAdding11OrMoreParkingSpace)[1]', 'varchar(max)')
								,'CityChangingFacadeOver10Percent:',
								app_form_xml.value('(//chkChangingFacadeOver10Percent)[1]', 'varchar(max)')
								,';','CityZonedUrban:',
							CASE
								WHEN app_form_xml.value('(//chkZonedUrban)[1]', 'varchar(max)') = 'ZU' THEN 'CHECKED'
								WHEN app_form_xml.value('(//chkZonedUrban)[1]', 'varchar(max)') = 'Y' THEN 'CHECKED'
								ELSE 'UNCHECKED'
							 END
							,';','CityPlannedMultiFamily:',
							CASE
							   WHEN app_form_xml.value('(//chkPlannedMultiFamily)[1]', 'varchar(max)') = 'PM' THEN 'CHECKED'
							   WHEN app_form_xml.value('(//chkPlannedMultiFamily)[1]', 'varchar(max)') = 'Y' THEN 'CHECKED'
							   ELSE 'UNCHECKED'
							END
							,';','CityAdjoinsPublicStreet:',
							app_form_xml.value('(//chkAdjoinsPublicStreet)[1]', 'varchar(max)')
							,';','CityNewPublicOrPrivateStreet:',
							app_form_xml.value('(//chkNewPublicOrPrivateStreet)[1]', 'varchar(max)')
							,';') AS [CITY_OF_CHARLOTTE_DESC],
						CONCAT(
							'NewSepticTank:',
							CASE
								WHEN app_form_xml.value('(//chkNewSepticTank)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
								ELSE 'UNCHECKED'
								END ,';',
								'NewPublicWater:',
							CASE
								WHEN app_form_xml.value('(//chkNewPublicWater)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
								ELSE 'UNCHECKED'
								END
							,';','ExistingPublicWater:',
							CASE
								WHEN app_form_xml.value('(//chkExistingPublicWater)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
								ELSE 'UNCHECKED'
							END
							,';','ExistingPublicSewer:',
							CASE
								WHEN app_form_xml.value('(//chkExistingPublicSewer)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
								ELSE 'UNCHECKED'
								END
							,';','NewPublicSewer:',
							CASE
								WHEN app_form_xml.value('(//chkNewPublicSewer)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
								ELSE 'UNCHECKED'
							END
							,';','ExistingWell:',
							CASE
								WHEN app_form_xml.value('(//chkExistingWell)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
								ELSE 'UNCHECKED'
							END
							,';','NewWell',
							CASE
								WHEN app_form_xml.value('(//chkNewWell)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
								ELSE 'UNCHECKED'
								END
							,';'
							,'id:CE_COM-WATER.cSEWER.cDETAILS;'
							,'ExistingSepticTank:',
							CASE
								WHEN app_form_xml.value('(//chkExistingSepticTank)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
								ELSE 'UNCHECKED'
								END
							,';') AS [WATER_SEWER_DETAIL_DESC],
							 LTRIM(RTRIM((select txt_backflow_desc from dbo.ctb_backflow_defs where txt_backflow_code = app_form_xml.value('(//rblUndergroundPiping)[1]', 'varchar(max)')))) [PROPOSED_OUTDOOR_UNDERGROUND_PIPING_DESC],
							LTRIM(RTRIM((select txt_backflow_desc from dbo.ctb_backflow_defs where txt_backflow_code = app_form_xml.value('(//rblSprinklerPiping)[1]', 'varchar(max)')))) [PROPOSED_FIRE_SPRINKLER_PIPING_DESC],
							app_form_xml.value('(//rblCMUDPreventer)[1]', 'varchar(max)') [INSTALL_CMUD_BACKFLOW_PREVENTER_DESC], 
							app_form_xml.value('(//rblExtendsPublicWaterOrSanitarySewerSystem)[1]', 'varchar(max)') [EXTENDING_PUBLIC_WATER_SEWER_DESC],
							app_form_xml.value('(//rblGradeModificationWithinCharlotteEasement)[1]', 'varchar(max)') [GRADE_MOD_WATER_SEWER_EASEMENT_DESC],
						app_form_xml.value('(//rblEncroachmentInCharlotteEasement)[1]', 'varchar(max)') [PROPOSED_ENCROACHMENT_WATER_SEWER_EASEMENT_DESC],
						CONCAT(
							'HDMeatMarket:',
							CASE
								WHEN app_form_xml.value('(//chkMeatMarket)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDAdultDayCare:',
							CASE
								WHEN app_form_xml.value('(//chkAdultDayCare)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDLodging:',
							CASE
								WHEN app_form_xml.value('(//chkLodging)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDSeafood:',
							CASE
								WHEN app_form_xml.value('(//chkSeafood)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDSeafoodUtensilType:',
							CASE
								WHEN
									app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN 'Both'
								WHEN
									app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') = ''
								THEN  'Reusable'
								WHEN
								app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') = ''
								AND
								app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN  'Disposable'
							END
							,';','HDSeafoodCapacity:',
							app_form_xml.value('(//txtSeafoodCapacity)[1]', 'varchar(max)')
							,';','HDWaterRecreation_Pool:',
							CASE
								WHEN app_form_xml.value('(//chkWaterRecreation)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDBarServiceUtensilType:',
							CASE
								WHEN
								app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN 'Both'
								WHEN
								app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') = ''
								THEN  'Reusable'
								WHEN
								app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') = ''
								AND
								app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN  'Disposable'
							END
							,';','HDOtherl:',
							CASE
								WHEN app_form_xml.value('(//chkOther)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END,
							';','HDBarServiceCapacity:',
								app_form_xml.value('(//txtBarSvcCapacity)[1]', 'varchar(max)')
							,';','Restaurant:',
							CASE
								WHEN app_form_xml.value('(//chkRestaurant)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END,
							';','RestaurantUtensilType:',
							CASE
								WHEN
								app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN 'Both'
								WHEN
								app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') = ''
								THEN  'Reusable'
								WHEN
								app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') = ''
								AND
								app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN  'Disposable'
								END
							,';','HDAdultDayCareCapacity:',
							app_form_xml.value('(//txrNumAdults)[1]', 'varchar(max)')
							,';','HDChildDayCare:',
							CASE
								WHEN app_form_xml.value('(//chkChildDayCare)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
								END
							,';','HDChildDayCareCapacity:',
							app_form_xml.value('(//txtNumChildren)[1]', 'varchar(max)')
							,';','HDOtherDescription:',
							app_form_xml.value('(//txtOtherDesc)[1]', 'varchar(max)')
							,';','RestaurantCapacity:',
							app_form_xml.value('(//txtRestaurantCapacity)[1]', 'varchar(max)')
							,';'
							,'id:CE_COM-HEALTH.cDEPARTMENT.cDETAILS;',
							'BarService:',
							CASE
								WHEN app_form_xml.value('(//chkBarSvc)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
								END
							,';') AS [DAY_CARE_DESC],
							CONCAT(
							'HDMeatMarket:',
							CASE
								WHEN app_form_xml.value('(//chkMeatMarket)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDAdultDayCare:',
							CASE
								WHEN app_form_xml.value('(//chkAdultDayCare)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDLodging:',
							CASE
								WHEN app_form_xml.value('(//chkLodging)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDSeafood:',
							CASE
								WHEN app_form_xml.value('(//chkSeafood)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDSeafoodUtensilType:',
							CASE
								WHEN
									app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN 'Both'
								WHEN
									app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') = ''
								THEN  'Reusable'
								WHEN
								app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') = ''
								AND
								app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN  'Disposable'
							END
							,';','HDSeafoodCapacity:',
							app_form_xml.value('(//txtSeafoodCapacity)[1]', 'varchar(max)')
							,';','HDWaterRecreation_Pool:',
							CASE
								WHEN app_form_xml.value('(//chkWaterRecreation)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDBarServiceUtensilType:',
							CASE
								WHEN
								app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN 'Both'
								WHEN
								app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') = ''
								THEN  'Reusable'
								WHEN
								app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') = ''
								AND
								app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN  'Disposable'
							END
							,';','HDOtherl:',
							CASE
								WHEN app_form_xml.value('(//chkOther)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END,
							';','HDBarServiceCapacity:',
								app_form_xml.value('(//txtBarSvcCapacity)[1]', 'varchar(max)')
							,';','Restaurant:',
							CASE
								WHEN app_form_xml.value('(//chkRestaurant)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END,
							';','RestaurantUtensilType:',
							CASE
								WHEN
								app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN 'Both'
								WHEN
								app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') = ''
								THEN  'Reusable'
								WHEN
								app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') = ''
								AND
								app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN  'Disposable'
								END
							,';','HDAdultDayCareCapacity:',
							app_form_xml.value('(//txrNumAdults)[1]', 'varchar(max)')
							,';','HDChildDayCare:',
							CASE
								WHEN app_form_xml.value('(//chkChildDayCare)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
								END
							,';','HDChildDayCareCapacity:',
							app_form_xml.value('(//txtNumChildren)[1]', 'varchar(max)')
							,';','HDOtherDescription:',
							app_form_xml.value('(//txtOtherDesc)[1]', 'varchar(max)')
							,';','RestaurantCapacity:',
							app_form_xml.value('(//txtRestaurantCapacity)[1]', 'varchar(max)')
							,';'
							,'id:CE_COM-HEALTH.cDEPARTMENT.cDETAILS;',
							'BarService:',
							CASE
								WHEN app_form_xml.value('(//chkBarSvc)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
								END
							,';') AS [HEALTH_DEPT_DETAIL_DESC],
							p.app_form_xml.value('(//txtCostEstimationTotal)[1]', 'varchar(max)') AS TOTAL_JOB_COST_AMT,
							p.txt_SOI,
							id_project_coordinator							
								FROM
									dbo.tb_project_current_status p
								LEFT JOIN dbo.tb_users u on p.txt_performed_by = u.txt_username
								WHERE 	txt_review_type_code <> 'WL'
								AND  p.project_number='{project_number}'
							) as TempTable";
            var epmUsersQuery = $@"
				select distinct
					CAST((CONCAT(999, id_user)) AS int) as USER_ID,
					nme_first as FIRST_NM,
					nme_last AS LAST_NM,
					-1 as EXTERNAL_SYSTEM_REF_ID,
					txt_username+'EPM'+CAST(id_user AS varchar) as SRC_SYSTEM_VAL_TXT,
					1 AS WKR_ID_CREATED_TXT,
					-- GETDATE() AS CREATED_DTTM,
					 1 AS [WKR_ID_UPDATED_TXT],
					-- GETDATE() AS UPDATED_DTTM,
					1 as ACTIVE_IND,
					NULL USER_INTERFACE_SETTING_TXT,
					NULL IS_EXPRESS_SCHEDULED_IND,
					txt_email AS USER_NM,
					NULL as LAN_ID_TXT,
					txt_phone PHONE_NUM,
 					txt_email EMAIL_ADDR_TXT,
 					(case 
 						when city_access_flg  = 'Y' then 1
 						when city_access_flg  = 'N' then 0
 						else null end) as CITY_IND
				from [dbo].[tb_users]";
            //project manager id on epm 
            List<ReadEPMProjectDTO> epmTable = null;
            List<ReadEPMUsersDTO> userTable = null;

            try
            {
                using (new Impersonator(ADUsername, ADDomain, ADPassword))
                {
                    using (var context = new EPMData.EPMDbConnect())
                    {
                        epmTable = context.Database.SqlQuery<ReadEPMProjectDTO>(bigQuery).ToList();
                        userTable = context.Database.SqlQuery<ReadEPMUsersDTO>(epmUsersQuery).ToList();
                        if (epmTable != null)
                        {
                            foreach (var item in epmTable)
                            {
                                if (item.id_project_coordinator.HasValue)
                                {
                                    string tempuserId = "999" + item.id_project_coordinator.ToString();
                                    item.id_project_coordinator = decimal.Parse(tempuserId);

                                    if (!CheckAionUser(int.Parse(tempuserId)))
                                    {
                                        SeedUser(tempuserId);
                                    }
                                }
                                else
                                {
                                    item.id_project_coordinator = -1;
                                }
                            }
                        }
                    }
                }
                if (epmTable.Count > 0)
                {
                    AIONDbContext ent = new AIONDbContext();
                    InsertAIONTables(epmTable, userTable, ent.PROJECTs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        private static void TransferProjectFromEPMToAION(string project_number, string src_system_val)
        {
            var bigQuery = $@"select * from
				(
				SELECT
				   distinct(CASE
							 WHEN ISNUMERIC(p.project_number)=0 THEN
							(CASE
									WHEN LEN(p.[project_number])=13 THEN SUBSTRING(p.project_number, 4, len(p.project_number)-7)
									WHEN LEN(p.[project_number])=14 THEN SUBSTRING(p.project_number, 5, len(p.project_number)-8)
									WHEN LEN(p.[project_number])=15 THEN SUBSTRING(p.project_number, 6, len(p.project_number)-9)
									WHEN LEN(p.[project_number])=20 THEN SUBSTRING(p.project_number, 7, len(p.project_number)-14)
							 END) ELSE p.project_number
							 END) AS PROJECT_ID ,
							nme_project as PROJECT_NM,
						 -1 as EXTERNAL_SYSTEM_REF_ID,
						(CASE
				 			WHEN id_project_state = 1003 then 6
				 			WHEN id_project_state = 1004 then 7
				 			WHEN id_project_state = 1015 then 8
				 			WHEN id_project_state = 1016 then 10
				 			WHEN id_project_state = 1005 then 11
				 			WHEN id_project_state = 1006 then 13
				 			WHEN id_project_state = 1025 then 14
				 			WHEN id_project_state = 1026 then 16
				 			WHEN id_project_state = 1025 then 17
				 			WHEN id_project_state = 1013 then 22
				  			WHEN id_project_state = 1027 then 22
				  			WHEN id_project_state = 1011 then 24
				  			WHEN id_project_state = 1012 then 26
						  else -1 end) as PROJECT_STATUS_REF_ID ,
						  (CASE
				  			WHEN txt_review_type_code  = 'RT' then 2
				  			WHEN txt_review_type_code  = 'OS' then 2
				  			WHEN txt_review_type_code  = 'MP' then 2
				  			WHEN txt_review_type_code  = 'PS' then 2
				  			WHEN txt_review_type_code  = 'PL' then 2
				  			WHEN txt_review_type_code  = 'ER' then 1
						  else -1 end) as PROJECT_TYP_REF_ID,
						 (CASE WHEN id_user IS NULL THEN NULL ELSE CONCAT(999 , u.id_user) END) AS ASSIGNED_ESTIMATOR_ID,
						 (CASE WHEN txt_review_type_code  = 'OS' then 2 WHEN txt_review_type_code  = 'ER' then 1 ELSE -1 END) AS PROJECT_MODE_REF_ID,
						(CASE WHEN txt_review_type_code  = 'RT' THEN 1 ELSE 0 END) AS RTAP_IND,
						txt_address as PROJECT_ADDR_TXT,
						permit_fee as TOTAL_FEE_AMT,
						(CASE WHEN id_project_manager IS NULL THEN NULL ELSE CONCAT(999, id_project_manager)END) AS PROJECT_MANAGER_ID,
						p.[project_number] as SRC_SYSTEM_VAL_TXT,
						 review_start_date as TAG_CREATED_BY_TS,
						review_end_date as TAG_UPDATED_BY_TS,
						(CASE WHEN txt_review_type_code  = 'PL' then 1 else 0 end) AS PRELIMINARY_IND,
						0 as GATE_ACCEPTED_IND,
						0 as FIFO_IND,
						plans_ready_on as PLANS_READY_ON_DT,
						team_score_indicator as TEAM_GRADE_TXT,
						[gate_close_date] as GATE_DT,				
						[txt_cde_summary_code] as BUILD_CODE_VERSION_DESC,				
						CASE p.app_form_xml.value('(//rblPropertyType)[1]', 'varchar(max)')
							WHEN 'FA' THEN '1-2 Family -=> 3.5 Stories'
							WHEN 'TH' THEN 'TH, LFS -=> 3.5 Stories'
							WHEN 'CN' THEN 'Condo or Apts'
							WHEN 'CO' THEN 'Commercial'
						   ELSE ''
						 END AS REVIEW_TYP_REF_DESC,
						CASE app_form_xml.value('(//rblBuildingCode)[1]', 'varchar(max)')
							WHEN 'BC-2012' THEN '2012 NC Building Code'
							WHEN 'BC-2015' THEN '2015 NC Existing Building Code'
							WHEN 'BC-2018' THEN '2018 NC Building Code'
							WHEN 'EBC-2018' THEN '2018 NC Existing Building Code'
						  ELSE ''
						 END AS [CODE_SUMMARY_DESC],
						 CASE(select txt_constr_type_desc from dbo.ctb_constr_types_defs where txt_constr_types_code = app_form_xml.value('(//rblConstrType)[1]', 'varchar(max)'))
							WHEN 'I-A'   THEN '1A * NONCOMBUSTIBLE/PROTECTED'
							WHEN 'I-B'   THEN '1B * NONCOMBUSTIBLE/UNPROTECTED'
							WHEN 'II-A'  THEN '2A * NONCOMBUSTIBLE/PROTECTED'
							WHEN 'II-B'  THEN '2B * NONCOMBUSTIBLE/UNPROTECTED'
							WHEN 'III-A' THEN '3A * NONCOMBUSTIBLE WALLS/PROTECTED'
							WHEN 'III-B' THEN '3B * NONCOMBUSTIBLE WALLS/UNPROTECTED'
							WHEN 'IV'    THEN '4 * HEAVY TIMBER'
							WHEN 'V-A'   THEN '5A * WOOD FRAME/PROTECTED'
							WHEN 'V-B'   THEN '5B * WOOD FRAME/UNPROTECTED'
						   ELSE ''
						 END AS [CONSTR_TYP_DESC] ,
						 LTRIM(RTRIM((select txt_worktype_desc from dbo.ctb_worktypes_defs where txt_worktype_code = app_form_xml.value('(//rblWorkType)[1]', 'varchar(max)')))) [WORK_TYP_DESC], 
						 app_form_xml.value('(//txtProposedWork)[1]', 'varchar(max)') [OVERALL_WORK_SCOPE_DESC],
						 app_form_xml.value('(//txtElecWrkScope)[1]', 'varchar(max)') [ELCTR_WORK_SCOPE_DESC],
 			 			 app_form_xml.value('(//txtMechWrkScope)[1]', 'varchar(max)') [MECH_WORK_SCOPE_DESC],
						 app_form_xml.value('(//txtPlumbWrkScope)[1]', 'varchar(max)') [PLUMB_WORK_SCOPE_DESC],
						 app_form_xml.value('(//txtCivilWrkScope)[1]', 'varchar(max)') [CIVIL_WORK_SCOPE_DESC],
						 p.project_number as [PERMIT_NUM],
						 app_form_xml.value('(//txtTotalSheets)[1]', 'varchar(max)') SHEETS_CNT_DESC,
						 CONCAT(
						   (--frmStructural
							   CASE WHEN LEN(p.app_form_xml.value('(//frmStructural/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN
								 CASE WHEN ISNULL(app_form_xml.value('(//frmStructural/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
									   AND ISNULL(app_form_xml.value('(//frmStructural/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
								  THEN App_form_xml.value('(//frmStructural/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
								  ELSE  app_form_xml.value('(//frmStructural/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmStructural/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
								END
							END),
							(--frmFireProtection
								CASE WHEN LEN(p.app_form_xml.value('(//frmFireProtection/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN
									CASE WHEN ISNULL(app_form_xml.value('(//frmFireProtection/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
				   						 AND ISNULL(app_form_xml.value('(//frmFireProtection/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
									THEN App_form_xml.value('(//frmFireProtection/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
									ELSE  app_form_xml.value('(//frmFireProtection/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmFireProtection/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
								END
							END),
							(--frmCivil
								CASE WHEN LEN(p.app_form_xml.value('(//frmCivil/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN
									CASE WHEN ISNULL(app_form_xml.value('(//frmCivil/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
										AND ISNULL(app_form_xml.value('(//frmCivil/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
										THEN App_form_xml.value('(//frmCivil/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
										ELSE  app_form_xml.value('(//frmCivil/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmCivil/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
									END
							END),
							(--frmPlumbing
							CASE WHEN LEN(p.app_form_xml.value('(//frmPlumbing/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN
									CASE WHEN ISNULL(app_form_xml.value('(//frmPlumbing/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
										AND ISNULL(app_form_xml.value('(//frmPlumbing/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
										THEN App_form_xml.value('(//frmPlumbing/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
										ELSE  app_form_xml.value('(//frmPlumbing/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmPlumbing/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
									END
							END),
							(--frmMechanical
								CASE WHEN LEN(p.app_form_xml.value('(//frmMechanical/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN
									 CASE WHEN ISNULL(app_form_xml.value('(//frmMechanical/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
										   AND ISNULL(app_form_xml.value('(//frmMechanical/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
										  THEN App_form_xml.value('(//frmMechanical/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
										  ELSE  app_form_xml.value('(//frmMechanical/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmMechanical/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
									 END
							END),
							(--frmElectrical
							CASE WHEN LEN(p.app_form_xml.value('(//frmElectrical/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN
								 CASE WHEN ISNULL(app_form_xml.value('(//frmElectrical/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
									   AND ISNULL(app_form_xml.value('(//frmElectrical/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
									  THEN App_form_xml.value('(//frmElectrical/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
									  ELSE  app_form_xml.value('(//frmElectrical/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmElectrical/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
								 END
							END),
						 (--frmArchitect
							CASE WHEN LEN(p.app_form_xml.value('(//frmArchitect/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN
								  CASE WHEN ISNULL(app_form_xml.value('(//frmArchitect/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
									   AND ISNULL(app_form_xml.value('(//frmArchitect/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
									  THEN App_form_xml.value('(//frmArchitect/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
									  ELSE  app_form_xml.value('(//frmArchitect/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmArchitect/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
								 END
							END)
						) AS [DESIGNER_DESC],
						CONCAT(
							(--frmStructural
								CASE WHEN LEN(p.app_form_xml.value('(//frmStructural/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN
			      					  CASE WHEN ISNULL(app_form_xml.value('(//frmStructural/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
										AND ISNULL(app_form_xml.value('(//frmStructural/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
								  THEN App_form_xml.value('(//frmStructural/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
								  ELSE  app_form_xml.value('(//frmStructural/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmStructural/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
								END
							END),
							(--frmFireProtection
								CASE WHEN LEN(p.app_form_xml.value('(//frmFireProtection/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN
									CASE WHEN ISNULL(app_form_xml.value('(//frmFireProtection/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
				   						 AND ISNULL(app_form_xml.value('(//frmFireProtection/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
									THEN App_form_xml.value('(//frmFireProtection/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
									ELSE  app_form_xml.value('(//frmFireProtection/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmFireProtection/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
								END
							END),
							(--frmCivil
								CASE WHEN LEN(p.app_form_xml.value('(//frmCivil/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN
									CASE WHEN ISNULL(app_form_xml.value('(//frmCivil/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
										AND ISNULL(app_form_xml.value('(//frmCivil/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
										THEN App_form_xml.value('(//frmCivil/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
										ELSE  app_form_xml.value('(//frmCivil/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmCivil/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
									END
							END),
							(--frmPlumbing
							CASE WHEN LEN(p.app_form_xml.value('(//frmPlumbing/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN
									CASE WHEN ISNULL(app_form_xml.value('(//frmPlumbing/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
										AND ISNULL(app_form_xml.value('(//frmPlumbing/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
										THEN App_form_xml.value('(//frmPlumbing/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
										ELSE  app_form_xml.value('(//frmPlumbing/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmPlumbing/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
									END
							END),
							(--frmMechanical
								CASE WHEN LEN(p.app_form_xml.value('(//frmMechanical/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN
									 CASE WHEN ISNULL(app_form_xml.value('(//frmMechanical/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
										   AND ISNULL(app_form_xml.value('(//frmMechanical/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
										  THEN App_form_xml.value('(//frmMechanical/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
										  ELSE  app_form_xml.value('(//frmMechanical/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmMechanical/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
									 END
							END),
							(--frmElectrical
							CASE WHEN LEN(p.app_form_xml.value('(//frmElectrical/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN
								 CASE WHEN ISNULL(app_form_xml.value('(//frmElectrical/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
									   AND ISNULL(app_form_xml.value('(//frmElectrical/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
									  THEN App_form_xml.value('(//frmElectrical/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
									  ELSE  app_form_xml.value('(//frmElectrical/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmElectrical/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
								 END
							END),
						 (--frmArchitect
							CASE WHEN LEN(p.app_form_xml.value('(//frmArchitect/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN
								  CASE WHEN ISNULL(app_form_xml.value('(//frmArchitect/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = ''
									   AND ISNULL(app_form_xml.value('(//frmArchitect/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
									  THEN App_form_xml.value('(//frmArchitect/frmDtl/txtLicenseNum)[1]', 'varchar(max)')
									  ELSE  app_form_xml.value('(//frmArchitect/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmArchitect/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
								 END
							END)
						) AS [SEAL_HOLDERS_DESC],
						CONCAT(
							'FireNFPAType13R:',
							 app_form_xml.value('(//chkNFPAType13R)[1]', 'varchar(max)')
							,';','FireFirePump:',
							 app_form_xml.value('(//rblFirePump)[1]', 'varchar(max)')
							,';','FireElevator:',
							 app_form_xml.value('(//rblElevator)[1]', 'varchar(max)')
							,';','FireDrawingsIncluded:',
							 app_form_xml.value('(//rblIncludesAlarmAndSprinklerDwg)[1]', 'varchar(max)')
							,';','FireNFPAType13:',
							 app_form_xml.value('(//chkNFPAType13)[1]', 'varchar(max)')
							,';','FireStandpipeClassIII:',
							 app_form_xml.value('(//chkStandpipeClassIII)[1]', 'varchar(max)')
							,';','FireStandpipeClassI:',
							 app_form_xml.value('(//chkStandpipeClassI)[1]', 'varchar(max)')
							,';','FireStandpipeClassWet:',
							 app_form_xml.value('(//chkStandpipeClassWet)[1]', 'varchar(max)')
							,';','FireStandpipe:',
							 app_form_xml.value('(//rblStandpipe)[1]', 'varchar(max)')
							,';','FirePumpNewOrExisting:',
							 app_form_xml.value('(//rblFirePumpType)[1]', 'varchar(max)')
							,';','FireStandpipeClassDry:',
							 app_form_xml.value('(//chkStandpipeClassDry)[1]', 'varchar(max)')
							,';','FireSmokeDetector:',
							 app_form_xml.value('(//rblSmokeDetection)[1]', 'varchar(max)')
							,';','FireStandpipeClassII:',
							 app_form_xml.value('(//chkStandpipeClassII)[1]', 'varchar(max)')
							,';','FireBuildingSprinkled:',
							 app_form_xml.value('(//rblBuildingSprinklered)[1]', 'varchar(max)')
							,';','id:CE_COM-FIRE.cDETAILS;'
							,'FireFireAlarm:',
							 app_form_xml.value('(//rblFireAlarm)[1]', 'varchar(max)')
							,';','FireNFPAType13D:',
							 app_form_xml.value('(//chkNFPAType13D)[1]', 'varchar(max)')
							,';','FireNFPANewOrExisting:',
							 app_form_xml.value('(//rblSprinklerType)[1]', 'varchar(max)')
							,';','FireNFPANewOrExisting:',
 							 app_form_xml.value('(//rblSprinklerType)[1]', 'varchar(max)')
						) AS [FIRE_DETAIL_DESC],
						CASE app_form_xml.value('(//frmPrimaryOccupancy/frmFactoryOrIndustryDtl/rblFactoryOrIndustryType)[1]', 'varchar(max)')
								WHEN 'F1' THEN 'F1 * Factory/Industrial - Moderate Hazard'
								WHEN 'F2' THEN 'F2 * Factory/Industrial - Low Hazard'
							ELSE
							CASE app_form_xml.value('(//frmPrimaryOccupancy/frmStorageDtl/rblStorageType)[1]', 'varchar(max)')
								WHEN 'S1' THEN 'S1 * Storage - Moderate Hazard'
	 							WHEN 'S2' THEN 'S2 * Storage - Low Hazard'
							ELSE
							CASE app_form_xml.value('(//frmPrimaryOccupancy/frmAssemblyDtl/rblAssemblyType)[1]', 'varchar(max)')
								WHEN 'A1' THEN 'A1 * Assembly - Theater w/o Stage'
								WHEN 'A2' THEN 'A2 * Assembly - Restaurants, Bars, Banquet Halls'
								WHEN 'A3' THEN 'A3C * Assembly ? Common Assemblies'
								WHEN 'A4' THEN 'A4 * Assembly ? Indoor Arena, Skating Rink, Tennis Court'
								WHEN 'A5' THEN 'A5 * Assembly ? Outdoor Stadium, Bleacher, Grandstand'
							ELSE
							CASE app_form_xml.value('(//frmPrimaryOccupancy/frmHighHazardDtl/rblHighHazardType)[1]', 'varchar(max)')
								WHEN 'H1' THEN 'H1 * High Hazard ? Explosives'
								WHEN 'H2' THEN 'H2 * High Hazard ? Deflagration'
								WHEN 'H3' THEN 'H3 * High Hazard ? Readily Combustible'
								WHEN 'H4' THEN 'H4 * High Hazard ? Health Hazard'
								WHEN 'H5' THEN 'H5 * High Hazard - HPM'
							ELSE
							CASE app_form_xml.value('(//frmPrimaryOccupancy/frmResedentialDtl/rblResedentialType)[1]', 'varchar(max)')
		    					WHEN 'R1' THEN 'R1 * Residential ? Hotels'
								WHEN 'R2' THEN 'R2 * Residential - Multiple Family'
								WHEN 'R3' THEN 'R3 * Residential - Single Family'
								WHEN 'R4' THEN 'R4 * Residential - Care/Assisted Living Facilities, Condition 1 (Ambulatory)'
							ELSE
							CASE app_form_xml.value('(//frmPrimaryOccupancy/frmInstitutionalDtl/rblInstitutionalType)[1]', 'varchar(max)')
								WHEN 'I1' THEN 'I1 * Institutional - Supervised Environment, Condition 1 (Ambulatory)'
								WHEN 'I2' THEN 'I2H * Institutional - Incapacitated ? Hospital, Full Nursing & Medical Treatment'
								WHEN 'I3' THEN 'I3 * Institutional - Restrained'
								WHEN 'I4' THEN 'I4 * Institutional - Day Care'
								WHEN 'I-3 Use Condition' THEN 'I3 * Institutional - Restrained'
							ELSE
							CASE
								WHEN app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'BS' THEN 'B * Business'
								WHEN app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'ED' THEN 'E * Education'
								WHEN app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'MR' THEN 'M * Mercantile'
								WHEN app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'ED' THEN 'E * Education'
								WHEN app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'UM' THEN 'U * Utility'
							ELSE ''
								END
								END
								END
								END
								END
								END
							END AS [OCCUPANCY_DESC],
							CASE app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)')
								WHEN 'AS' THEN 'Assembly'
								WHEN 'BS' THEN 'Business'
								WHEN 'ED' THEN 'Educational'
								WHEN 'MR' THEN 'Mercantile'
								WHEN 'FI' THEN 'Factory/Industrial'
								WHEN 'HH' THEN 'High Hazard'
								WHEN 'IN' THEN 'Institutional'
								WHEN 'RE' THEN 'Residential'
								WHEN 'ST' THEN 'Storage'
								WHEN 'UM' THEN 'Utility/Miscellaneous'
							ELSE ''
							END AS [PRI_OCCUPANCY_DESC], 
							CASE app_form_xml.value('(//frmSecondaryOccupancy/rblOccupancyType)[1]', 'varchar(max)')
								WHEN 'AS' THEN 'Assembly'
								WHEN 'BS' THEN 'Business'
								WHEN 'ED' THEN 'Educational'
								WHEN 'MR' THEN 'Mercantile'
								WHEN 'FI' THEN 'Factory/Industrial'
								WHEN 'HH' THEN 'High Hazard'
								WHEN 'IN' THEN 'Institutional'
								WHEN 'RE' THEN 'Residential'
								WHEN 'ST' THEN 'Storage'
								WHEN 'UM' THEN 'Utility/Miscellaneous'
							ELSE ''
							END AS [SECONDARY_OCCUPANCY_DESC], 
							CAST(CAST(app_form_xml.value('(//txtSqFtArea)[1]', 'varchar(max)') AS money)AS varchar) [SQUARE_FOOTAGE_DESC],
							app_form_xml.value('(//txtZoning)[1]', 'varchar(max)') [ZONING_OF_SITE_DESC],
							CASE
								WHEN app_form_xml.value('(//chkChangeOfUse)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END AS [CHG_OF_USE_DESC],
								app_form_xml.value('(//txtPreviousBiz)[1]', 'varchar(max)') [PREVIOUS_BUSINESS_TYP_DESC],
								app_form_xml.value('(//txtPropsedBiz)[1]', 'varchar(max)') [PROPOSED_BUSINESS_TYP_DESC],
							CASE app_form_xml.value('(//rblHasCondProjNum)[1]', 'varchar(max)')
								WHEN 'N' THEN 'No'
								WHEN 'Y' THEN 'Yes'
							ELSE ''
							END AS [CONDITIONAL_PERMIT_APPROVAL_DESC],
							CONCAT(
								'CityAddingImperviousArea:',
								 app_form_xml.value('(//chkAddingImperviousArea)[1]', 'varchar(max)')
								,';','CityChangingDrivewayEtc:',
								app_form_xml.value('(//chkChangingDrivewayEtc)[1]', 'varchar(max)')
								 ,';','CityAddingTreePlanting:'
								,app_form_xml.value('(//chkAddingTreePlanting)[1]', 'varchar(max)')
								,';','CityAddingTreeProtection:',
								app_form_xml.value('(//chkAddingTreeProtection)[1]', 'varchar(max)')
								,';','CityGradingMoreThanOneAcre:',
								app_form_xml.value('(//chkGradingMoreThanOneAcre)[1]', 'varchar(max)')
								,';','CityAddingImperviousArea:',
								app_form_xml.value('(//chkAddingImperviousArea)[1]', 'varchar(max)')
								,';','CityChangingDrivewayEtc:',
								app_form_xml.value('(//chkChangingDrivewayEtc)[1]', 'varchar(max)')
								,';','CityAddingTreePlanting:',
								app_form_xml.value('(//chkAddingTreePlanting)[1]', 'varchar(max)')
								,';','CityAddingTreeProtection:',
								app_form_xml.value('(//chkAddingTreeProtection)[1]', 'varchar(max)')
								,';','CityGradingMoreThanOneAcre:',
								app_form_xml.value('(//chkGradingMoreThanOneAcre)[1]', 'varchar(max)')
								,';','CityBuildingOver1000SqFt:',
								app_form_xml.value('(//chkBuildingOver1000SqFt)[1]', 'varchar(max)')
								,'CityBuildingOver5PercentSqFt:',
								app_form_xml.value('(//chkBuildingOver5PercentSqFt)[1]', 'varchar(max)')
								,';','CityAdding11OrMoreParkingSpace:',
								app_form_xml.value('(//chkAdding11OrMoreParkingSpace)[1]', 'varchar(max)')
								,'CityChangingFacadeOver10Percent:',
								app_form_xml.value('(//chkChangingFacadeOver10Percent)[1]', 'varchar(max)')
								,';','CityZonedUrban:',
							CASE
								WHEN app_form_xml.value('(//chkZonedUrban)[1]', 'varchar(max)') = 'ZU' THEN 'CHECKED'
								WHEN app_form_xml.value('(//chkZonedUrban)[1]', 'varchar(max)') = 'Y' THEN 'CHECKED'
								ELSE 'UNCHECKED'
							 END
							,';','CityPlannedMultiFamily:',
							CASE
							   WHEN app_form_xml.value('(//chkPlannedMultiFamily)[1]', 'varchar(max)') = 'PM' THEN 'CHECKED'
							   WHEN app_form_xml.value('(//chkPlannedMultiFamily)[1]', 'varchar(max)') = 'Y' THEN 'CHECKED'
							   ELSE 'UNCHECKED'
							END
							,';','CityAdjoinsPublicStreet:',
							app_form_xml.value('(//chkAdjoinsPublicStreet)[1]', 'varchar(max)')
							,';','CityNewPublicOrPrivateStreet:',
							app_form_xml.value('(//chkNewPublicOrPrivateStreet)[1]', 'varchar(max)')
							,';') AS [CITY_OF_CHARLOTTE_DESC],
						CONCAT(
							'NewSepticTank:',
							CASE
								WHEN app_form_xml.value('(//chkNewSepticTank)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
								ELSE 'UNCHECKED'
								END ,';',
								'NewPublicWater:',
							CASE
								WHEN app_form_xml.value('(//chkNewPublicWater)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
								ELSE 'UNCHECKED'
								END
							,';','ExistingPublicWater:',
							CASE
								WHEN app_form_xml.value('(//chkExistingPublicWater)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
								ELSE 'UNCHECKED'
							END
							,';','ExistingPublicSewer:',
							CASE
								WHEN app_form_xml.value('(//chkExistingPublicSewer)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
								ELSE 'UNCHECKED'
								END
							,';','NewPublicSewer:',
							CASE
								WHEN app_form_xml.value('(//chkNewPublicSewer)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
								ELSE 'UNCHECKED'
							END
							,';','ExistingWell:',
							CASE
								WHEN app_form_xml.value('(//chkExistingWell)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
								ELSE 'UNCHECKED'
							END
							,';','NewWell',
							CASE
								WHEN app_form_xml.value('(//chkNewWell)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
								ELSE 'UNCHECKED'
								END
							,';'
							,'id:CE_COM-WATER.cSEWER.cDETAILS;'
							,'ExistingSepticTank:',
							CASE
								WHEN app_form_xml.value('(//chkExistingSepticTank)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
								ELSE 'UNCHECKED'
								END
							,';') AS [WATER_SEWER_DETAIL_DESC],
							 LTRIM(RTRIM((select txt_backflow_desc from dbo.ctb_backflow_defs where txt_backflow_code = app_form_xml.value('(//rblUndergroundPiping)[1]', 'varchar(max)')))) [PROPOSED_OUTDOOR_UNDERGROUND_PIPING_DESC],
							LTRIM(RTRIM((select txt_backflow_desc from dbo.ctb_backflow_defs where txt_backflow_code = app_form_xml.value('(//rblSprinklerPiping)[1]', 'varchar(max)')))) [PROPOSED_FIRE_SPRINKLER_PIPING_DESC],
							app_form_xml.value('(//rblCMUDPreventer)[1]', 'varchar(max)') [INSTALL_CMUD_BACKFLOW_PREVENTER_DESC], 
							app_form_xml.value('(//rblExtendsPublicWaterOrSanitarySewerSystem)[1]', 'varchar(max)') [EXTENDING_PUBLIC_WATER_SEWER_DESC],
							app_form_xml.value('(//rblGradeModificationWithinCharlotteEasement)[1]', 'varchar(max)') [GRADE_MOD_WATER_SEWER_EASEMENT_DESC],
						app_form_xml.value('(//rblEncroachmentInCharlotteEasement)[1]', 'varchar(max)') [PROPOSED_ENCROACHMENT_WATER_SEWER_EASEMENT_DESC],
						CONCAT(
							'HDMeatMarket:',
							CASE
								WHEN app_form_xml.value('(//chkMeatMarket)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDAdultDayCare:',
							CASE
								WHEN app_form_xml.value('(//chkAdultDayCare)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDLodging:',
							CASE
								WHEN app_form_xml.value('(//chkLodging)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDSeafood:',
							CASE
								WHEN app_form_xml.value('(//chkSeafood)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDSeafoodUtensilType:',
							CASE
								WHEN
									app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN 'Both'
								WHEN
									app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') = ''
								THEN  'Reusable'
								WHEN
								app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') = ''
								AND
								app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN  'Disposable'
							END
							,';','HDSeafoodCapacity:',
							app_form_xml.value('(//txtSeafoodCapacity)[1]', 'varchar(max)')
							,';','HDWaterRecreation_Pool:',
							CASE
								WHEN app_form_xml.value('(//chkWaterRecreation)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDBarServiceUtensilType:',
							CASE
								WHEN
								app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN 'Both'
								WHEN
								app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') = ''
								THEN  'Reusable'
								WHEN
								app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') = ''
								AND
								app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN  'Disposable'
							END
							,';','HDOtherl:',
							CASE
								WHEN app_form_xml.value('(//chkOther)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END,
							';','HDBarServiceCapacity:',
								app_form_xml.value('(//txtBarSvcCapacity)[1]', 'varchar(max)')
							,';','Restaurant:',
							CASE
								WHEN app_form_xml.value('(//chkRestaurant)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END,
							';','RestaurantUtensilType:',
							CASE
								WHEN
								app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN 'Both'
								WHEN
								app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') = ''
								THEN  'Reusable'
								WHEN
								app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') = ''
								AND
								app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN  'Disposable'
								END
							,';','HDAdultDayCareCapacity:',
							app_form_xml.value('(//txrNumAdults)[1]', 'varchar(max)')
							,';','HDChildDayCare:',
							CASE
								WHEN app_form_xml.value('(//chkChildDayCare)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
								END
							,';','HDChildDayCareCapacity:',
							app_form_xml.value('(//txtNumChildren)[1]', 'varchar(max)')
							,';','HDOtherDescription:',
							app_form_xml.value('(//txtOtherDesc)[1]', 'varchar(max)')
							,';','RestaurantCapacity:',
							app_form_xml.value('(//txtRestaurantCapacity)[1]', 'varchar(max)')
							,';'
							,'id:CE_COM-HEALTH.cDEPARTMENT.cDETAILS;',
							'BarService:',
							CASE
								WHEN app_form_xml.value('(//chkBarSvc)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
								END
							,';') AS [DAY_CARE_DESC],
							CONCAT(
							'HDMeatMarket:',
							CASE
								WHEN app_form_xml.value('(//chkMeatMarket)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDAdultDayCare:',
							CASE
								WHEN app_form_xml.value('(//chkAdultDayCare)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDLodging:',
							CASE
								WHEN app_form_xml.value('(//chkLodging)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDSeafood:',
							CASE
								WHEN app_form_xml.value('(//chkSeafood)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDSeafoodUtensilType:',
							CASE
								WHEN
									app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN 'Both'
								WHEN
									app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') = ''
								THEN  'Reusable'
								WHEN
								app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') = ''
								AND
								app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN  'Disposable'
							END
							,';','HDSeafoodCapacity:',
							app_form_xml.value('(//txtSeafoodCapacity)[1]', 'varchar(max)')
							,';','HDWaterRecreation_Pool:',
							CASE
								WHEN app_form_xml.value('(//chkWaterRecreation)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END
							,';','HDBarServiceUtensilType:',
							CASE
								WHEN
								app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN 'Both'
								WHEN
								app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') = ''
								THEN  'Reusable'
								WHEN
								app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') = ''
								AND
								app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN  'Disposable'
							END
							,';','HDOtherl:',
							CASE
								WHEN app_form_xml.value('(//chkOther)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END,
							';','HDBarServiceCapacity:',
								app_form_xml.value('(//txtBarSvcCapacity)[1]', 'varchar(max)')
							,';','Restaurant:',
							CASE
								WHEN app_form_xml.value('(//chkRestaurant)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
							END,
							';','RestaurantUtensilType:',
							CASE
								WHEN
								app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN 'Both'
								WHEN
								app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') != ''
								AND
								app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') = ''
								THEN  'Reusable'
								WHEN
								app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') = ''
								AND
								app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') != ''
								THEN  'Disposable'
								END
							,';','HDAdultDayCareCapacity:',
							app_form_xml.value('(//txrNumAdults)[1]', 'varchar(max)')
							,';','HDChildDayCare:',
							CASE
								WHEN app_form_xml.value('(//chkChildDayCare)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
								END
							,';','HDChildDayCareCapacity:',
							app_form_xml.value('(//txtNumChildren)[1]', 'varchar(max)')
							,';','HDOtherDescription:',
							app_form_xml.value('(//txtOtherDesc)[1]', 'varchar(max)')
							,';','RestaurantCapacity:',
							app_form_xml.value('(//txtRestaurantCapacity)[1]', 'varchar(max)')
							,';'
							,'id:CE_COM-HEALTH.cDEPARTMENT.cDETAILS;',
							'BarService:',
							CASE
								WHEN app_form_xml.value('(//chkBarSvc)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
								ELSE 'CHECKED'
								END
							,';') AS [HEALTH_DEPT_DETAIL_DESC],
							p.app_form_xml.value('(//txtCostEstimationTotal)[1]', 'varchar(max)') AS TOTAL_JOB_COST_AMT,
							p.txt_SOI,
							id_project_coordinator							
								FROM
									dbo.tb_project_current_status p
								LEFT JOIN dbo.tb_users u on p.txt_performed_by = u.txt_username
								WHERE 	txt_review_type_code <> 'WL'
							) as TempTable
							where PROJECT_ID='{project_number}'
								and RTAP_IND=1";
            var epmUsersQuery = $@"
				select distinct
					CAST((CONCAT(999, id_user)) AS int) as USER_ID,
					nme_first as FIRST_NM,
					nme_last AS LAST_NM,
					-1 as EXTERNAL_SYSTEM_REF_ID,
					txt_username+'EPM'+CAST(id_user AS varchar) as SRC_SYSTEM_VAL_TXT,
					1 AS WKR_ID_CREATED_TXT,
					-- GETDATE() AS CREATED_DTTM,
					 1 AS [WKR_ID_UPDATED_TXT],
					-- GETDATE() AS UPDATED_DTTM,
					1 as ACTIVE_IND,
					NULL USER_INTERFACE_SETTING_TXT,
					NULL IS_EXPRESS_SCHEDULED_IND,
					txt_email AS USER_NM,
					NULL as LAN_ID_TXT,
					txt_phone PHONE_NUM,
 					txt_email EMAIL_ADDR_TXT,
 					(case 
 						when city_access_flg  = 'Y' then 1
 						when city_access_flg  = 'N' then 0
 						else null end) as CITY_IND
				from [dbo].[tb_users]";
            //project manager id on epm 
            List<ReadEPMProjectDTO> epmTable = null;
            List<ReadEPMUsersDTO> userTable = null;

            try
            {
                using (new Impersonator(ADUsername, ADDomain, ADPassword))
                {
                    using (var context = new EPMData.EPMDbConnect())
                    {
                        epmTable = context.Database.SqlQuery<ReadEPMProjectDTO>(bigQuery).ToList();
                        userTable = context.Database.SqlQuery<ReadEPMUsersDTO>(epmUsersQuery).ToList();
                        if (epmTable != null)
                        {
                            foreach (var item in epmTable)
                            {
                                item.SRC_SYSTEM_VAL_TXT = src_system_val;

                                if (item.id_project_coordinator.HasValue)
                                {
                                    string tempuserId = "999" + item.id_project_coordinator.ToString();
                                    item.id_project_coordinator = decimal.Parse(tempuserId);

                                    if (!CheckAionUser(int.Parse(tempuserId)))
                                    {
                                        SeedUser(tempuserId);
                                    }
                                }
                                else
                                {
                                    item.id_project_coordinator = -1;
                                }
                            }
                        }
                    }
                }
                if (epmTable.Count > 0)
                {
                    AIONDbContext ent = new AIONDbContext();

                    InsertAIONTables(epmTable, userTable, ent.PROJECTs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public static void InsertAIONTables(List<ReadEPMProjectDTO> EPMTable, List<ReadEPMUsersDTO> EpmUser, DbSet<PROJECT> AIONTable)
        {
            //int counter = 0;
            //string creationDate = "2022-09-12 05:30:00.000";
            int returnedUserId = 0;
            string projectnumber = string.Empty;
            var epmUsersQuery = $@"
				select distinct
					CAST((CONCAT(999, id_user)) AS int) as USER_ID,
					nme_first as FIRST_NM,
					nme_last AS LAST_NM,
					-1 as EXTERNAL_SYSTEM_REF_ID,
					txt_username+'EPM'+CAST(id_user AS varchar) as SRC_SYSTEM_VAL_TXT,
					1 AS WKR_ID_CREATED_TXT,
					-- GETDATE() AS CREATED_DTTM,
					 1 AS [WKR_ID_UPDATED_TXT],
					-- GETDATE() AS UPDATED_DTTM,
					1 as ACTIVE_IND,
					NULL USER_INTERFACE_SETTING_TXT,
					NULL IS_EXPRESS_SCHEDULED_IND,
					txt_email AS USER_NM,
					NULL as LAN_ID_TXT,
					txt_phone PHONE_NUM,
 					txt_email EMAIL_ADDR_TXT,
 					(case 
 						when city_access_flg  = 'Y' then 1
 						when city_access_flg  = 'N' then 0
 						else null end) as CITY_IND
				from [dbo].[tb_users]";

            var sqlAIONQueryInsert = string.Empty;

            foreach (var item in EPMTable)
            {
                using (new Impersonator(ADUsername, ADDomain, ADPassword))
                {
                    using (var context = new AIONDbContext())
                    {
                        try
                        {
                            var data = new PROJECT();
                            foreach (var aionitem in EPMTable)
                            {
                                int user_id = int.Parse(aionitem.PROJECT_MANAGER_ID);
                                var returnedAIONUser = context.Database.SqlQuery<int?>($@"SELECT USER_ID from [AION].[USER] where USER_ID = {user_id}").ToList();

                                if (returnedAIONUser != null)
                                {
                                    if (returnedAIONUser.Count() == 0)
                                    {
                                        //If the user does not exists on AION, then added
                                        if (user_id.ToString().Contains("999"))
                                        {
                                            var selectUserid = user_id.ToString().Replace("999", "");
                                            int? userid = int.Parse(selectUserid);
                                            var epmUserData = EpmUser.Where(x => x.USER_ID == userid);
                                            var returnedEPMUser = context.Database.SqlQuery<ReadEPMUsersDTO>(
                                                $@"
													select distinct
														CAST((CONCAT(999, id_user)) AS int) as USER_ID,
														nme_first as FIRST_NM,
														nme_last AS LAST_NM,
														-1 as EXTERNAL_SYSTEM_REF_ID,
														txt_username+'EPM'+CAST(id_user AS varchar) as SRC_SYSTEM_VAL_TXT,
														1 AS WKR_ID_CREATED_TXT,
														-- GETDATE() AS CREATED_DTTM,
															1 AS [WKR_ID_UPDATED_TXT],
														-- GETDATE() AS UPDATED_DTTM,
														1 as ACTIVE_IND,
														NULL USER_INTERFACE_SETTING_TXT,
														NULL IS_EXPRESS_SCHEDULED_IND,
														txt_email AS USER_NM,
														NULL as LAN_ID_TXT,
														txt_phone PHONE_NUM,
 														txt_email EMAIL_ADDR_TXT,
 														(case 
 															when city_access_flg  = 'Y' then 1
 															when city_access_flg  = 'N' then 0
 															else null end) as CITY_IND
													from [dbo].[tb_users] where id_user = {selectUserid}").ToList();
                                            if (returnedEPMUser.Count == 1)
                                            {
                                                var epmUser = returnedEPMUser[0];
                                                try
                                                {
                                                    epmUser.USER_INTERFACE_SETTING_TXT = string.IsNullOrEmpty(epmUser.USER_INTERFACE_SETTING_TXT) ? null : epmUser.USER_INTERFACE_SETTING_TXT;
                                                    epmUser.IS_EXPRESS_SCHEDULED_IND = string.IsNullOrEmpty(epmUser.IS_EXPRESS_SCHEDULED_IND) ? null : epmUser.IS_EXPRESS_SCHEDULED_IND;
                                                    epmUser.USER_NM = string.IsNullOrEmpty(epmUser.USER_NM) ? null : epmUser.USER_NM;
                                                    epmUser.LAN_ID_TXT = string.IsNullOrEmpty(epmUser.LAN_ID_TXT) ? null : epmUser.LAN_ID_TXT;
                                                    epmUser.CITY_IND = !epmUser.CITY_IND.HasValue ? null : epmUser.CITY_IND;

                                                    int aionid = int.Parse(aionitem.PROJECT_MANAGER_ID);

                                                    var sqlQueryInsert = $@"
														SET IDENTITY_INSERT [AION].[USER] ON;
														INSERT INTO [AION].[USER]
																   (
																	 USER_ID
																	,FIRST_NM
																   ,LAST_NM
																   ,EXTERNAL_SYSTEM_REF_ID
																   ,SRC_SYSTEM_VAL_TXT
																   ,WKR_ID_CREATED_TXT
																   ,CREATED_DTTM
																   ,ACTIVE_IND
																   ,USER_INTERFACE_SETTING_TXT
																   ,IS_EXPRESS_SCHEDULED_IND
																   ,USER_NM
																   ,LAN_ID_TXT
																   ,PHONE_NUM
																   ,EMAIL_ADDR_TXT
																   ,CITY_IND)
															 VALUES
																   (
																	'{aionid}'
																   ,'{epmUser.FIRST_NM}'
																   ,'{epmUser.LAST_NM}'
																   ,'{epmUser.EXTERNAL_SYSTEM_REF_ID}'
																   ,'{epmUser.SRC_SYSTEM_VAL_TXT}'
																   ,'{epmUser.WKR_ID_CREATED_TXT}'
																   ,'{DateTime.Now}'
																   ,'{epmUser.ACTIVE_IND}'
																   ,'{epmUser.USER_INTERFACE_SETTING_TXT}'
																   ,'{epmUser.IS_EXPRESS_SCHEDULED_IND}'
																   ,'{epmUser.USER_NM}'
																   ,'{epmUser.LAN_ID_TXT}'
																   ,'{epmUser.PHONE_NUM}'
																   ,'{epmUser.EMAIL_ADDR_TXT}'
																   ,'{epmUser.CITY_IND}')";
                                                    var noOfRowInserted1 = context.Database.ExecuteSqlCommand(sqlQueryInsert);
                                                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [AION].[USER] OFF;");
                                                }
                                                catch
                                                {
                                                    throw;
                                                }
                                                var returnedInsertedUserId = context.Database.SqlQuery<int?>($@"
													SELECT USER_ID 
														from 
													[AION].[USER] 
													WHERE  FIRST_NM = '{epmUser.FIRST_NM}'
													AND LAST_NM = '{epmUser.LAST_NM}'
													AND EMAIL_ADDR_TXT = '{epmUser.EMAIL_ADDR_TXT}'
													AND SRC_SYSTEM_VAL_TXT = '{epmUser.SRC_SYSTEM_VAL_TXT}'").ToList();
                                                returnedUserId = returnedInsertedUserId[0].Value;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        returnedUserId = returnedAIONUser[0].Value;
                                    }
                                }

                                projectnumber = aionitem.SRC_SYSTEM_VAL_TXT;

                                var currentAccelaRecId = _accelaList.Where(a => a.Key.Contains(projectnumber)).FirstOrDefault().Value;
                                //aionitem.MECH_WORK_SCOPE_DESC.Replace("'", "''");
                                //single quote fixed
                                var temp_MECH_WORK_SCOPE_DESC = string.IsNullOrEmpty(aionitem.MECH_WORK_SCOPE_DESC) ? " " : aionitem.MECH_WORK_SCOPE_DESC.Replace("'", "''");
                                var temp_PLUMB_WORK_SCOPE_DESC = string.IsNullOrEmpty(aionitem.PLUMB_WORK_SCOPE_DESC) ? " " : aionitem.PLUMB_WORK_SCOPE_DESC.Replace("'", "''");
                                var temp_OVERALL_WORK_SCOPE_DESC = string.IsNullOrEmpty(aionitem.OVERALL_WORK_SCOPE_DESC) ? " " : aionitem.OVERALL_WORK_SCOPE_DESC.Replace("'", "''");
                                var temp_ELCTR_WORK_SCOPE_DESC = string.IsNullOrEmpty(aionitem.ELCTR_WORK_SCOPE_DESC) ? " " : aionitem.ELCTR_WORK_SCOPE_DESC.Replace("'", "''");
                                var tempSEAL_HOLDERS_DESC = string.IsNullOrEmpty(aionitem.SEAL_HOLDERS_DESC) ? " " : aionitem.SEAL_HOLDERS_DESC.Replace("'", "''");

                                aionitem.MECH_WORK_SCOPE_DESC = temp_MECH_WORK_SCOPE_DESC;
                                aionitem.PLUMB_WORK_SCOPE_DESC = temp_PLUMB_WORK_SCOPE_DESC;
                                aionitem.OVERALL_WORK_SCOPE_DESC = temp_OVERALL_WORK_SCOPE_DESC;
                                aionitem.ELCTR_WORK_SCOPE_DESC = temp_ELCTR_WORK_SCOPE_DESC;
                                aionitem.SEAL_HOLDERS_DESC = tempSEAL_HOLDERS_DESC;

                                aionitem.TAG_CREATED_BY_TS = ProjectCreationDate(projectnumber);

                                sqlAIONQueryInsert = $@"
									---SET IDENTITY_INSERT [AION].[PROJECT] ON;
									INSERT INTO [AION].[PROJECT]
									(										
										PROJECT_NM,
										EXTERNAL_SYSTEM_REF_ID,
										PROJECT_STATUS_REF_ID,
										PROJECT_TYP_REF_ID,
										ASSIGNED_ESTIMATOR_ID,
										PROJECT_MODE_REF_ID,
										RTAP_IND,
										PROJECT_ADDR_TXT,
										TOTAL_FEE_AMT,
										PROJECT_MANAGER_ID,
										SRC_SYSTEM_VAL_TXT,
										TAG_CREATED_BY_TS,
										PRELIMINARY_IND,
										GATE_ACCEPTED_IND,
										FIFO_IND,
										PLANS_READY_ON_DT,
										TEAM_GRADE_TXT,
										BUILD_CODE_VERSION_DESC,
										REVIEW_TYP_REF_DESC,
										CODE_SUMMARY_DESC,
										CONSTR_TYP_DESC,
										WORK_TYP_DESC,
										OVERALL_WORK_SCOPE_DESC,
										ELCTR_WORK_SCOPE_DESC,
										MECH_WORK_SCOPE_DESC,
										PLUMB_WORK_SCOPE_DESC,
										CIVIL_WORK_SCOPE_DESC,
										PERMIT_NUM,
										SHEETS_CNT_DESC,
										DESIGNER_DESC,
										SEAL_HOLDERS_DESC,
										FIRE_DETAIL_DESC,
										OCCUPANCY_DESC,
										PRI_OCCUPANCY_DESC,
										SECONDARY_OCCUPANCY_DESC,
										SQUARE_FOOTAGE_DESC,
										ZONING_OF_SITE_DESC,
										CHG_OF_USE_DESC,
										PREVIOUS_BUSINESS_TYP_DESC,
										PROPOSED_BUSINESS_TYP_DESC,
										CONDITIONAL_PERMIT_APPROVAL_DESC,
										CITY_OF_CHARLOTTE_DESC,
										WATER_SEWER_DETAIL_DESC,
										PROPOSED_OUTDOOR_UNDERGROUND_PIPING_DESC,
										PROPOSED_FIRE_SPRINKLER_PIPING_DESC,
										INSTALL_CMUD_BACKFLOW_PREVENTER_DESC,
										EXTENDING_PUBLIC_WATER_SEWER_DESC,
										GRADE_MOD_WATER_SEWER_EASEMENT_DESC,
										PROPOSED_ENCROACHMENT_WATER_SEWER_EASEMENT_DESC,
										DAY_CARE_DESC,
										HEALTH_DEPT_DETAIL_DESC,
										CYCLE_NBR,
										TOTAL_JOB_COST_AMT,
										REC_ID_TXT,
										CREATED_DTTM,
										ASSIGNED_FACILITATOR_ID 
									)
									VALUES
									(										
										'{aionitem.PROJECT_NM}'
										,'{aionitem.EXTERNAL_SYSTEM_REF_ID}'
										,'{aionitem.PROJECT_STATUS_REF_ID}'
										,'{aionitem.PROJECT_TYP_REF_ID}'
										,'{-1}'
										,'{aionitem.PROJECT_MODE_REF_ID}'
										,'{Convert.ToBoolean(aionitem.RTAP_IND)}'
										,'{aionitem.PROJECT_ADDR_TXT}'
										,'{Convert.ToDecimal(aionitem.TOTAL_FEE_AMT)}'
										,'{returnedUserId}'
										,'{aionitem.SRC_SYSTEM_VAL_TXT}'
										,'{DateTime.Now}'
										,'{Convert.ToBoolean(aionitem.PRELIMINARY_IND)}'
										,'{Convert.ToBoolean(aionitem.GATE_ACCEPTED_IND)}'
										,'{Convert.ToBoolean(aionitem.FIFO_IND)}'
										,'{aionitem.PLANS_READY_ON_DT}'
										,'{aionitem.TEAM_GRADE_TXT}'
										,'{aionitem.BUILD_CODE_VERSION_DESC}'
										,'{aionitem.REVIEW_TYP_REF_DESC}'
										,'{aionitem.CODE_SUMMARY_DESC}'
										,'{aionitem.CONSTR_TYP_DESC}'
										,'{aionitem.WORK_TYP_DESC}'
										,'{aionitem.OVERALL_WORK_SCOPE_DESC}'
										,'{aionitem.ELCTR_WORK_SCOPE_DESC}'
										,'{aionitem.MECH_WORK_SCOPE_DESC}'
										,'{aionitem.PLUMB_WORK_SCOPE_DESC}'
										,'{aionitem.CIVIL_WORK_SCOPE_DESC}'
										,'{aionitem.PERMIT_NUM}'
										,'{aionitem.SHEETS_CNT_DESC}'
										,'{aionitem.DESIGNER_DESC}'
										,'{aionitem.SEAL_HOLDERS_DESC}'
										,'{aionitem.FIRE_DETAIL_DESC}'
										,'{aionitem.OCCUPANCY_DESC}'
										,'{aionitem.PRI_OCCUPANCY_DESC}'
										,'{aionitem.SECONDARY_OCCUPANCY_DESC}'
										,'{aionitem.SQUARE_FOOTAGE_DESC}'
										,'{aionitem.ZONING_OF_SITE_DESC}'
										,'{aionitem.CHG_OF_USE_DESC}'
										,'{aionitem.PREVIOUS_BUSINESS_TYP_DESC}'
										,'{aionitem.PROPOSED_BUSINESS_TYP_DESC}'
										,'{aionitem.CONDITIONAL_PERMIT_APPROVAL_DESC}'
										,'{aionitem.CITY_OF_CHARLOTTE_DESC}'
										,'{aionitem.WATER_SEWER_DETAIL_DESC}'
										,'{aionitem.PROPOSED_OUTDOOR_UNDERGROUND_PIPING_DESC}'
										,'{aionitem.PROPOSED_FIRE_SPRINKLER_PIPING_DESC}'
										,'{aionitem.INSTALL_CMUD_BACKFLOW_PREVENTER_DESC}'
										,'{aionitem.EXTENDING_PUBLIC_WATER_SEWER_DESC}'
										,'{aionitem.GRADE_MOD_WATER_SEWER_EASEMENT_DESC}'
										,'{aionitem.PROPOSED_ENCROACHMENT_WATER_SEWER_EASEMENT_DESC}'
										,'{aionitem.DAY_CARE_DESC}'
										,'{aionitem.HEALTH_DEPT_DETAIL_DESC}'
										,'1'
										,'{Convert.ToDecimal(aionitem.TOTAL_JOB_COST_AMT)}'
										,'{currentAccelaRecId}'
										,'{aionitem.TAG_CREATED_BY_TS}'
										,'{aionitem.id_project_coordinator}'
										);
										-- SELECT CAST(SCOPE_IDENTITY() AS INT)";
                                // context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [AION].[PROJECT] ON;");
                                var noOfRowInserted = context.Database.ExecuteSqlCommand(sqlAIONQueryInsert);
                                AION_PROJECT_ID = context.Database
                                        .SqlQuery<int>(sqlAIONQueryInsert)
                                        .Single();
                                //context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [AION].[PROJECT] OFF;");
                                Console.WriteLine("[AION].[PROJECT] records inserted");
                            }

                            //Work start
                            SeedRelatedData(returnedUserId, projectnumber, item, EpmUser);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            throw;
                        }
                        //}					
                    }
                }
            }
        }

        private static DateTime? ProjectCreationDate(string projectnumber)
        {
            using (new Impersonator(ADUsername, ADDomain, ADPassword))
            {
                using (var context = new EPMData.EPMDbConnect())
                {

                    var tempquery = $@"SELECT       
											[application_received_on]
									FROM [tst_EPM].[dbo].[tb_project_current_status] where project_number = '{projectnumber}'";

                    DateTime? createdate = context.Database.SqlQuery<DateTime?>(tempquery)
                                    .Single();

                    if (createdate == null) return DateTime.Now;
                    return createdate;
                }
            }
        }

        private static void SeedRelatedData(int returnedUserId, string projectnumber, ReadEPMProjectDTO item, List<ReadEPMUsersDTO> EpmUser)
        {
            //TODO: Add shceduling notes done
            //TODO: Match epm task and aion to seed project audit
            //TODO:
            //AION_PROJECT_ID = 523880;

            LoadInitdata(projectnumber);

            SeedPROJECT_CYCLE(projectnumber, returnedUserId, EpmUser);

            SeedPROJECT_CYCLE_DETAILS(projectnumber, returnedUserId, EpmUser);

            //Console.WriteLine("[AION].[PROJECT_CYCLE] records inserted");

            SeedProjectBusiness(projectnumber, returnedUserId, item.txt_SOI);
            //Console.WriteLine("[AION].[PROJECT_BUSINESS] records inserted");

            SeedPROJECT_AUDIT(projectnumber, returnedUserId);
            //Console.WriteLine("[AION].[PROJECT_AUDIT] records inserted");
            SeedPROJECT_NOTES(projectnumber, returnedUserId, item.txt_SOI);
            //Console.WriteLine("[AION].[NOTES] records inserted");

            SeedPROJECT_EMAIL_NOTIF(projectnumber, returnedUserId);
            //Console.WriteLine("[AION].[EMAIL_NOTIFICATIONS] records inserted");
            SeedPRELIMINARY_MEETINGS(projectnumber, returnedUserId, item);
            //Console.WriteLine("[AION].[PRELIMINARY_MEETINGS] records inserted");
        }

        private static void LoadInitdata(string projectnumber)
        {
            //PLAN_REVIEW_SCHEDULE_DETAIL
            using (new Impersonator(ADUsername, ADDomain, ADPassword))
            {
                //read from aion
                using (var context = new AIONDbContext())
                {
                    tblProjectCycle = context.Database.SqlQuery<ReadPROJECT_CYCLEDto>($@"SELECT * FROM [AION].[PROJECT_CYCLE]  WHERE PROJECT_ID='{AION_PROJECT_ID}'").ToList();
                }
            }

            using (new Impersonator(ADUsername, ADDomain, ADPassword))
            {
                //read from aion
                using (var context = new EPMData.EPMDbConnect())
                {
                    var sqlReadvw_review_result_trade = $@"
					SELECT 	
						CASE
							WHEN  v.txt_trade_code = 'BL' THEN 1
							WHEN  v.txt_trade_code = 'EL' THEN 2
							WHEN  v.txt_trade_code = 'MC' THEN 3
							WHEN  v.txt_trade_code = 'PL' THEN 4
							WHEN  v.txt_trade_code = 'ZN' THEN 12
							WHEN  v.txt_trade_code = 'FR' THEN 20
							WHEN  v.txt_trade_code = 'BF' THEN 25
							WHEN  v.txt_trade_code = 'HL' THEN 22
							WHEN  v.txt_trade_code = 'PA' THEN 23
							WHEN  v.txt_trade_code = 'DC' THEN 21
							WHEN  v.txt_trade_code = 'LD' THEN 24
							WHEN  v.txt_trade_code = 'ZN' THEN 26
							WHEN  v.txt_trade_code = 'FR' THEN 27
							ELSE -1 END AS BUSINESS_REF_ID,
							v.rvw_start_date,	
							v.rvw_end_date,	
							v.review_assigned_to,
							v.assessment_cycle,
							CASE
							WHEN  v.pool_trade_flg = 'N' THEN 0	
							ELSE 1 END AS pool_trade_flg,	
							v.created_by,
							v.created_on,
							v.updated_on,
							v.estimated_time_hours,
							v.re_review_time_hours
						FROM dbo.vw_review_result_trade v
						WHERE txt_result IS NOT NULL
						AND project_number='{projectnumber}'";
                    tempVW_Review_Result = context.Database.SqlQuery<ReadVW_Review_Result_TradeDto>(sqlReadvw_review_result_trade).ToList();
                }
            }
        }

        private static void SeedPROJECT_CYCLE_DETAILS(string projectnumber, int returnedUserId, List<ReadEPMUsersDTO> epmUser)
        {
            //projectnumber = "431711";

            using (new Impersonator(ADUsername, ADDomain, ADPassword))
            {
                using (var context = new AIONDbContext())
                {
                    foreach (var item in tblProjectCycle)
                    {
                        var temp_rereview = tempVW_Review_Result.Where(x => x.assessment_cycle == item.CYCLE_NBR && x.re_review_time_hours > 0);
                        if (temp_rereview.Any())
                        {
                            var next_cycle = item.CYCLE_NBR + 1;
                            var cycle = tblProjectCycle.FirstOrDefault(x => x.CYCLE_NBR == next_cycle);
                            if (cycle != null)
                            {
                                foreach (var rereview in temp_rereview)
                                {
                                    var sqlPROJECT_CYCLE_DETAILQueryInsert = $@"
										INSERT INTO [AION].[PROJECT_CYCLE_DETAIL]
										   ([PROJECT_CYCLE_ID]
										   ,[BUSINESS_REF_ID]
										   ,[REREVIEW_HOURS_NBR]
										   ,[WKR_ID_CREATED_TXT]
										   ,[CREATED_DTTM]
										   ,[WKR_ID_UPDATED_TXT]
										   ,[UPDATED_DTTM])
									 VALUES
										   ('{cycle.PROJECT_CYCLE_ID}'
										   ,'{rereview.BUSINESS_REF_ID}'
										   ,'{rereview.re_review_time_hours}'
										   ,'1'
										   ,'{rereview.created_on}'
										   ,'1'
										   ,'{rereview.updated_on}')";
                                    var returned = context.Database.ExecuteSqlCommand(sqlPROJECT_CYCLE_DETAILQueryInsert);
                                }
                            }
                        }
                    }

                    var filteredVW_Review_Result = tempVW_Review_Result
                        .GroupBy(x => x.assessment_cycle)
                        .Select(p => p.First()).ToList();

                    int PLAN_REVIEW_SCHEDULE_ID = 0;
                    int PROJECT_SCHEDULE_ID = 0;
                    foreach (var item in filteredVW_Review_Result)
                    {
                        var project_cycle_id = tblProjectCycle.FirstOrDefault(x => x.CYCLE_NBR == item.assessment_cycle).PROJECT_CYCLE_ID;
                        var sqlPLAN_REVIEW_SCHEDULEQueryInsert = $@"
								INSERT INTO [AION].[PLAN_REVIEW_SCHEDULE]
									([PROJECT_CYCLE_ID]
									,[PROJECT_SCHEDULE_TYP_DESC]
									,[IS_RESCHEDULE_IND]
									,[APPT_RESPONSE_STATUS_REF_ID]									
									,[WKR_ID_CREATED_TXT]
									,[CREATED_DTTM]									
									)
								VALUES
									(
									'{project_cycle_id}'
									,'PR'
									,0
									,7								
									,1
									,'{DateTime.UtcNow}'									
									);
									SELECT CAST(SCOPE_IDENTITY() AS INT)";
                        PLAN_REVIEW_SCHEDULE_ID = context.Database
                                         .SqlQuery<int>(sqlPLAN_REVIEW_SCHEDULEQueryInsert)
                                         .Single();

                        var details = tempVW_Review_Result.Where(x => x.assessment_cycle == item.assessment_cycle);

                        foreach (var viewitem in details)
                        {
                            if (viewitem.UserReview_assigned_to == 0)
                            {
                                viewitem.UserReview_assigned_to = -1;
                            }
                            PLAN_REVIEW_SCHEDULE_ID = 316;
                            var sqlPLAN_REVIEW_SCHEDULE_DETAILQueryInsert = $@"
								INSERT INTO [AION].[PLAN_REVIEW_SCHEDULE_DETAIL] (
											[PLAN_REVIEW_SCHEDULE_ID]
											,[BUSINESS_REF_ID]
											,[START_DT]
											,[END_DT]
											,[POOL_REQUEST_IND]
											,[SAME_BUILD_CONTR_IND]
											,[MANUAL_ASSIGNMENT_IND]
											,[ASSIGNED_HOURS_NBR]
											,[ASSIGNED_PLAN_REVIEWER_ID]
											,[WKR_ID_CREATED_TXT]
											,[CREATED_DTTM]	)
										VALUES
											(
											 '{PLAN_REVIEW_SCHEDULE_ID}'
											,'{viewitem.BUSINESS_REF_ID}'
											,'{viewitem.rvw_start_date}'
											,'{viewitem.rvw_end_date}'
											,'{Convert.ToBoolean(viewitem.pool_trade_flg)}'
											,0
											,0
											,'{viewitem.estimated_time_hours}'			
											,'{viewitem.UserReview_assigned_to}'
											,'1'
											,'{DateTime.UtcNow}'
											);
											SELECT CAST(SCOPE_IDENTITY() AS INT)";
                            // NO SAVING
                            var noOfRowInserted = context.Database.ExecuteSqlCommand(sqlPLAN_REVIEW_SCHEDULE_DETAILQueryInsert);
                        }

                        var filteredPROJECT_SCHEDULEVW_Review_Result = tempVW_Review_Result
                                    .GroupBy(x => x.assessment_cycle)
                                    .Select(grp => new
                                    {
                                        grp.Key,
                                        Min = grp.Min(x => x.rvw_start_date)
                                    }).ToList();

                        var StartReviewDate = filteredPROJECT_SCHEDULEVW_Review_Result
                            .FirstOrDefault(x => x.Key == item.assessment_cycle).Min;


                        var sqlPROJECT_SCHEDULEQueryInsert = $@"
								INSERT INTO [AION].[PROJECT_SCHEDULE]
										   ([PROJECT_SCHEDULE_TYP_DESC]
										   ,[APPT_ID]
										   ,[WKR_ID_CREATED_TXT]
										   ,[CREATED_DTTM]
										   ,[WKR_ID_UPDATED_TXT]
										   ,[UPDATED_DTTM]
										   ,[RECURRING_APPT_DT])
									 VALUES
										   (
											'PR'
										   ,'{PLAN_REVIEW_SCHEDULE_ID}'
										   ,'1'
										   ,'{DateTime.UtcNow}'
										   ,'1'
										   ,'{DateTime.UtcNow}'
										   ,'{StartReviewDate}'	
											);
											SELECT CAST(SCOPE_IDENTITY() AS INT)";

                        PROJECT_SCHEDULE_ID = context.Database
                                      .SqlQuery<int>(sqlPROJECT_SCHEDULEQueryInsert)
                                      .Single();

                        var finaldatadetail = tempVW_Review_Result.Where(x => x.assessment_cycle == item.assessment_cycle);

                        foreach (var epmview in finaldatadetail)
                        {
                            //todo: needed user id
                            var sqlUSER_SCHEDULEQueryInsert = $@"
								INSERT INTO [AION].[USER_SCHEDULE]
										   ([START_DTTM]
										   ,[END_DTTM]
										   ,[PROJECT_SCHEDULE_ID]
										   ,[USER_ID]
										   ,[WKR_ID_CREATED_TXT]
										   ,[CREATED_DTTM]
										   ,[WKR_ID_UPDATED_TXT]
										   ,[UPDATED_DTTM]
										   ,[BUSINESS_REF_ID])
									 VALUES
										   ('{epmview.rvw_start_date}'
										   ,'{epmview.rvw_end_date}'
										   ,'{PROJECT_SCHEDULE_ID}'
										   ,'1'
										   ,'1'
										   ,'{DateTime.UtcNow}'
										   ,'1'
										   ,'{DateTime.UtcNow}'
										   ,'{epmview.BUSINESS_REF_ID}')";

                            var returned = context.Database.ExecuteSqlCommand(sqlUSER_SCHEDULEQueryInsert);
                        }
                    }
                    //insert into PLAN_REVIEW_SCHEDULE_DETAIL
                }
            }
        }

        private static void SeedPROJECT_SCHEDULE(int projectnumber, int returnedUserId)
        {
            #region Read
            List<PROJECT_SCHEDULEDto> epmTable = null;
            var sqlReadEPMQuery = $@"
					 select 
								ph.id_tb_project_history,			
								txt_performed_by = case 
									when ph.id_task = 502 then (select u1.nme_first + ' ' + u1.nme_last from tb_users u1 where u1.txt_username = mh.updated_by)
									when ph.id_task = 201 then COALESCE((select u2.nme_first + ' ' + u2.nme_last from tb_users u2 where u2.txt_username = eh.txt_performed_by), eh.txt_performed_by)
									when ph.txt_performed_by = 'MODELER' then 'Posse'
									when ph.txt_performed_by = 'SCHEDULER' then 'Posse'
									else ISNULL((select top 1 u3.nme_first + ' ' + u3.nme_last from tb_users u3 where u3.txt_username = ph.txt_performed_by), ph.txt_performed_by)
								end,
								case 
									when ph.id_task = 502 then mh.updated_on
									when ph.id_task = 201 then eh.performed_on
									else ph.performed_on
								end perf_on,
								state.nme_status,
								task.nme_task,
								task_desc = 
								case 
									when ph.id_task = 201 and eh.txt_trade_code IS NOT NULL then 
										td.txt_trade_desc + ' Estimation ' + 
											case eh.txt_est_state_code
												when 'NA' THEN 'Not Required'
												when 'PE' THEN 'Pending'
												when 'ES' THEN 'Completed'
											end
									when ph.id_task = 201 and eh.txt_trade_code IS NULL THEN 'All Estimations Completed'
									when ph.id_task = 502 THEN mh.txt_status_text
									when ph.id_task = 405 THEN 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Created'
									when ph.id_task = 403 or ph.id_task = 5037 THEN 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Disapproved'
									when ph.id_task = 404 or ph.id_task = 5038 THEN 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Approved'
									when ph.id_task = 406 THEN 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Approved'
                
									when ph.id_task = 603 THEN /* Waive Fees */     COALESCE(ftd.nme_fee_type, 'Fee') + ' Waived'
									when ph.id_task = 604 THEN /* Collect Fees */   COALESCE(ftd.nme_fee_type, 'Fee') + ' Collected'
									when ph.id_task = 605 THEN /* Pay Fees */       COALESCE(ftd.nme_fee_type, 'Fee') + ' Paid'
                
									when ph.id_task = 1002 THEN 
											case when la.txt_link_action_type_code = 'L' THEN 'Project Linked'
													 when la.txt_link_action_type_code = 'D' THEN 'Project Link Removed'
											end
									when ph.id_task = 2040 then ptd.txt_trade_desc + ' Trade Result: ' + prh.result
									when ph.id_task = 2050 then 'Minutes Review Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ': Disapproved'
									when ph.id_task = 2055 then 'Minutes Review Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ': Approved'                
									when ph.id_task = 3000 THEN /* Add Fees */      COALESCE(ftd.nme_fee_type, 'Fee') + ' Added'
									when ph.id_task = 3014 THEN /* Update Fees */   COALESCE(ftd.nme_fee_type, 'Fee') + ' Updated'                
									when ph.id_task = 3016 then /* Start Plan Review */ 
										txt_history_text + ': Cycle ' + cast(ph.assessment_cycle as varchar)                
									when ph.id_task = 3007 then /* Enter Trade Review Result */ 
										rrt_td.txt_trade_desc + ' ' + txt_history_text                
									when ph.id_task = 3008 then /* Enter Agency Review Result */ 
										rrca_ad.nme_agency + ' ' + txt_history_text                
									when ph.id_task = 5004 or ph.id_task = 5005 or ph.id_task = 5027 then /* Save, Accept or Reject Intake */ 
										 case when ia.id_agency_def = 4 then 'County '
											  else 'Town '
										 end + txt_history_text                
									when ph.id_task = 5006 THEN /* Create Cycle */ 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Created'
									when ph.id_task = 5008 THEN /* Reopen Cycle */ 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Reopened'
									when ph.id_task = 5010 THEN /* Add Fees */      COALESCE(ftd.nme_fee_type, 'Fee') + ' Added'
									when ph.id_task = 5011 THEN /* Waive Fees */    COALESCE(ftd.nme_fee_type, 'Fee') + ' Waived'
									when ph.id_task = 5012 THEN /* Collect Fees */  COALESCE(ftd.nme_fee_type, 'Fee') + ' Collected'                
									when ph.id_task = 5021 then /* WLR Enter Agency Review Result */ 
										rra_ad.nme_agency + ' ' + txt_history_text                
									when ph.id_task = 5025 THEN /* Update Fees */   COALESCE(ftd.nme_fee_type, 'Fee') + ' Updated'                
									else txt_history_text                
								end
								from 
									tb_project_history ph 
									left outer join ctb_state_defs state on ph.id_project_state = state.id_status 
									left outer join ctb_task_defs task on ph.id_task = task.id_task 
									left outer join tb_est_history eh on eh.id_project_history = ph.id_tb_project_history 
									left outer join ctb_trade_defs td on td.txt_trade_code = eh.txt_trade_code 
									left outer join tb_meeting_history mh on mh.id_project_history = ph.id_tb_project_history 
									left outer join tb_link_actions la on ph.id_tb_project_history = la.id_project_history 
									left outer join tb_prelim_results_history prh on ph.id_tb_project_history = prh.id_project_history and ph.id_task = 2040 
									left outer join ctb_trade_defs ptd on ptd.txt_trade_code = prh.txt_trade_code
									left outer join tb_rvw_res_trade_history rrt on ph.id_task = 3007 /* Enter Trade Review Result */ and ph.id_tb_project_history = rrt.id_tb_project_history
									left outer join ctb_trade_defs rrt_td on rrt.txt_trade_code = rrt_td.txt_trade_code            
									left outer join tb_rvw_res_ce_agency_history rrca on ph.id_task = 3008 /* Enter Agency Review Result */ and ph.id_tb_project_history = rrca.id_tb_project_history
									left outer join ctb_agency_defs rrca_ad on rrca.id_agency = rrca_ad.id_agency_def                                        
									left outer join tb_intake_action ia on ph.id_tb_project_history = ia.id_tb_project_history                
									left outer join tb_rvw_res_agency_history rra on ph.id_task = 5021 /* WLR Enter Agency Review Result */ and ph.id_tb_project_history = rra.id_tb_project_history
									left outer join ctb_agency_defs rra_ad on rra.id_agency_def = rra_ad.id_agency_def            
									left outer join tb_project_fee_history pfh on ph.id_task in (603, 604, 605, 3000, 3014, 5010, 5011, 5012, 5025) /* Fee Related Tasks */ and ph.id_tb_project_history = pfh.id_tb_project_history
									left outer join ctb_feetype_defs ftd on pfh.id_fee_types = ftd.id_fee_types
								where
									ph.project_number = '{projectnumber}';";

            using (new Impersonator(ADUsername, ADDomain, ADPassword))
            {
                using (var context = new EPMData.EPMDbConnect())
                {
                    epmTable = context.Database.SqlQuery<PROJECT_SCHEDULEDto>(sqlReadEPMQuery).ToList();
                }
            }
            #endregion
            #region Write
            try
            {
                using (new Impersonator(ADUsername, ADDomain, ADPassword))
                {
                    using (var context = new AIONDbContext())
                    {
                        foreach (var item in epmTable)
                        {
                            var sqlAIONQueryInsert = $@"								 
							INSERT INTO [AION].[PROJECT_SCHEDULE]
								  (
							        [PROJECT_SCHEDULE_TYP_DESC]
								   ,[APPT_ID]
								   ,[WKR_ID_CREATED_TXT]
								   ,[CREATED_DTTM]
								   ,[WKR_ID_UPDATED_TXT]
								   ,[UPDATED_DTTM]
								   ,[RECURRING_APPT_DT])
							 VALUES
								  (
									'{item.PROJECT_SCHEDULE_TYP_DESC}'
								   ,'{item.APPT_ID}'
								   ,<WKR_ID_CREATED_TXT, varchar(10),>
								   ,<CREATED_DTTM, datetime,>
								   ,<WKR_ID_UPDATED_TXT, varchar(10),>
								   ,<UPDATED_DTTM, datetime,>
								   ,<RECURRING_APPT_DT, datetime,>);";
                            var noOfRowInserted = context.Database.ExecuteSqlCommand(sqlAIONQueryInsert);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            #endregion
        }

        private static void SeedProjectBusiness(string projectnumber, int userid, string txt_SOI)
        {
            List<PBRELATIONSHIPDto> epmTable = null;

            // create temp table with all 11 rows with default values using txt_SOI - default to county then change to charlotte if needed
            // business refs you need fire, zoning, bemp (4 rows), backflow, ehs (4 rows)
            // look at correct row in the temp table and update that row and put correct values for business ref id and other values

            //insert all 11 in project_business_relationship table			

            var sqlReadEPMQuery = $@"
					 select distinct  
						[txt_est_hrs] as ESTIMATION_HOURS_NBR,
						(case 
							when [txt_trade_code]  = 'BL' then 1
							when [txt_trade_code]  = 'EL' then 2
							when [txt_trade_code]  = 'MC' then 3
							when [txt_trade_code]  = 'PL' then 4
							when [txt_trade_code]  = 'ZN' then (case when txt_SOI = 'Charlotte' then 12 else 26 end)
							when [txt_trade_code]  = 'FR' then (case when txt_SOI = 'Charlotte' then 20 else 27 end)
							when [txt_trade_code]  = 'BF' then 25
							when [txt_trade_code]  = 'HL' then 22
							when [txt_trade_code]  = 'PA' then 23
							when [txt_trade_code]  = 'LD' then 24
							when [txt_trade_code]  = 'DC' then 21
							else -1 end) as BUSINESS_REF_ID,
							a.[project_number] as PROJECT_ID,
						(case when b.[id_user]  is null 
								then null else
								CAST((CONCAT(999, b.[id_user])) AS int) end) as ASSIGNED_PLAN_REVIEWER_ID,  --b.[id_user]
						(case 
						when [txt_est_state_code] = 'NA' then 1
						else 0 end) as ESTIMATION_NOT_APPLICABLE_IND,
						'C' as PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC, 
						(case 
							when [id_project_state] = 1003 then 6
							when [id_project_state]  = 1004 then 7
							when [id_project_state] = 1015 then 8
							when [id_project_state]  = 1016 then 10
							when [id_project_state] = 1005 then 11
							when [id_project_state]  = 1006 then 13
							when [id_project_state] = 1025 then 14
							when [id_project_state]  = 1026 then 16
							when [id_project_state] = 1025 then 17
							when [id_project_state]  = 1013 then 22
							when [id_project_state]  = 1027 then 22
							when [id_project_state] = 1011 then 24
							when [id_project_state]  = 1012 then 26
							else -1 end) as PROJECT_STATUS_REF_ID,
							0 as IS_DEPT_REQUESTED_IND
						FROM [dbo].[tb_est_current_status] a
						 LEFT JOIN [dbo].[tb_users] b on [txt_performed_by] = b.txt_username
						 LEFT JOIN dbo.tb_project_current_status p  on a.[project_number]= p.[project_number]
						 where p.project_number='{projectnumber}'";

            using (new Impersonator(ADUsername, ADDomain, ADPassword))
            {
                using (var context = new EPMData.EPMDbConnect())
                {
                    epmTable = context.Database.SqlQuery<PBRELATIONSHIPDto>(sqlReadEPMQuery).ToList();

                    if (epmTable.Count < 11)
                    {
                        var businesreftbl = from a in epmTable
                                            select new List<int> { a.BUSINESS_REF_ID };

                        if (txt_SOI != "Charlotte")
                        {
                            List<int> tempfulllist = new List<int>();
                            var originalBRefid = new List<int> { 1, 2, 3, 4, 26, 27, 25, 22, 23, 24, 21 };
                            foreach (var item1 in businesreftbl)
                            {
                                tempfulllist.Add(item1[0]);
                            }
                            var missedReferences = originalBRefid.Union(tempfulllist).Except(originalBRefid.Intersect(tempfulllist));
                            //epmTable.Clear();

                            //TODO: add missedReferences  to epmTable
                            foreach (var item in missedReferences)
                            {
                                int ddd = item;
                                epmTable.Add(new PBRELATIONSHIPDto
                                {
                                    ESTIMATION_HOURS_NBR = 0,
                                    BUSINESS_REF_ID = item,
                                    PROJECT_ID = projectnumber.ToString(),
                                    ESTIMATION_NOT_APPLICABLE_IND = 1,
                                    PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC = "C",
                                    PROJECT_STATUS_REF_ID = 6,
                                    IS_DEPT_REQUESTED_IND = 0,
                                    ASSIGNED_PLAN_REVIEWER_ID = userid
                                });
                            }
                        }
                        else
                        {
                            List<int> tempfulllist = new List<int>();
                            var txtSOIBRefid = new List<int> { 1, 2, 3, 4, 12, 20, 25, 22, 23, 24, 21 };
                            foreach (var item1 in businesreftbl)
                            {
                                tempfulllist.Add(item1[0]);
                            }
                            var missedReferences = txtSOIBRefid.Union(tempfulllist).Except(txtSOIBRefid.Intersect(tempfulllist));
                            //epmTable.Clear();
                            //TODO: add missedReferences  to epmTable
                            foreach (var item in missedReferences)
                            {
                                int ddd = item;
                                epmTable.Add(new PBRELATIONSHIPDto
                                {
                                    ESTIMATION_HOURS_NBR = 0,
                                    BUSINESS_REF_ID = item,
                                    PROJECT_ID = projectnumber.ToString(),
                                    ESTIMATION_NOT_APPLICABLE_IND = 1,
                                    PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC = "C",
                                    PROJECT_STATUS_REF_ID = 6,
                                    IS_DEPT_REQUESTED_IND = 0,
                                    ASSIGNED_PLAN_REVIEWER_ID = userid
                                });
                            }
                        }
                    }
                }
            }
            try
            {
                using (new Impersonator(ADUsername, ADDomain, ADPassword))
                {
                    using (var context = new AIONDbContext())
                    {
                        foreach (var item in epmTable)
                        {
                            if (!item.ESTIMATION_HOURS_NBR.HasValue)
                            {
                                //item.ASSIGNED_PLAN_REVIEWER_ID = -1;								
                                item.ASSIGNED_PLAN_REVIEWER_ID = -1;
                            }
                            else
                            {
                                item.ASSIGNED_PLAN_REVIEWER_ID = userid;
                            }

                            var sqlAIONQueryInsert = $@"								 
							INSERT INTO [AION].[PROJECT_BUSINESS_RELATIONSHIP]
							(
								 [ESTIMATION_HOURS_NBR]
								,[BUSINESS_REF_ID]
								,[PROJECT_ID]							
								,[ESTIMATION_NOT_APPLICABLE_IND]
								,[PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC]
								,[PROJECT_STATUS_REF_ID]
								,[IS_DEPT_REQUESTED_IND]
								,ASSIGNED_PLAN_REVIEWER_ID
							)
							VALUES
							(
							  '{Convert.ToDecimal(item.ESTIMATION_HOURS_NBR)}',
							  '{item.BUSINESS_REF_ID}',
							  '{AION_PROJECT_ID}',
							  '{item.ESTIMATION_NOT_APPLICABLE_IND}',
							  '{item.PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC}',
							  '{item.PROJECT_STATUS_REF_ID}',
							  '{item.IS_DEPT_REQUESTED_IND}',
							  '{item.ASSIGNED_PLAN_REVIEWER_ID}'
							  )";
                            var noOfRowInserted = context.Database.ExecuteSqlCommand(sqlAIONQueryInsert);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        private static void SeedPROJECT_NOTES(string projectnumber, int returnedUserId, string txtSOI)
        {
            int user_id = int.Parse(returnedUserId.ToString().Replace("999", ""));
            List<ReadNotesDto> epmTable = null;
            //List<EstimationNotesDto> selectedEstimationNotes = null;

            //only retrieve scheduling and estimation notes
            var SQLEstimationNotesQuery = $@"					 
						  SELECT  
						  1 as NOTES_TYP_REF_ID,
						  [txt_int_notes] as NOTES_COMMENT,
						  case 
							when txt_trade_code = 'BL' then 1
							when txt_trade_code = 'EL' then 2
							when txt_trade_code = 'MC' then 3
							when txt_trade_code = 'PL' then 4
							when txt_trade_code = 'ZN' then 5
							when txt_trade_code = 'FR' then 13
						--  when [txt_trade_code]  = 'ZN' then (case when txt_SOI = 'Charlotte' then 12 else 26 end)
						--  when [txt_trade_code]  = 'FR' then (case when txt_SOI = 'Charlotte' then 20 else 27 end)
							when txt_trade_code = 'BF' then 25
							when txt_trade_code = 'HL' then 22
							when txt_trade_code = 'PA' then 23
							when txt_trade_code = 'LD' then 24
							when txt_trade_code = 'DC' then 21
							else -1 end as BUSINESS_REF_ID,
						  dateadd(hour,4,[performed_on]) as CREATED_DTTM,
						  (case when u.[id_user]  is null then null else CAST((CONCAT(999, u.[id_user])) AS int) end)  as WKR_ID_CREATED_TXT
						  FROM [dbo].[tb_est_current_status] e
						  LEFT JOIN dbo.tb_users u on e.txt_performed_by = u.txt_username
						  where e.project_number='{projectnumber}'
						  and txt_int_notes<>''
						 -----
						 UNION
						 SELECT  --gate notes
						  2 as NOTES_TYP_REF_ID,
						  [txt_gate_notes] as NOTES_COMMENT,
						  case 
							when txt_trade_code = 'BL' then 1
							when txt_trade_code = 'EL' then 2
							when txt_trade_code = 'MC' then 3
							when txt_trade_code = 'PL' then 4
							when txt_trade_code = 'ZN' then 5
							when txt_trade_code = 'FR' then 13
							when txt_trade_code = 'BF' then 25
							when txt_trade_code = 'HL' then 22
							when txt_trade_code = 'PA' then 23
							when txt_trade_code = 'LD' then 24
							when txt_trade_code = 'DC' then 21
							else -1 end as BUSINESS_REF_ID,
						   dateadd(hour,4,[performed_on]) as CREATED_DTTM,
						  (case when u.[id_user]  is null then null else CAST((CONCAT(999, u.[id_user])) AS int) end)  as WKR_ID_CREATED_TXT
						  FROM [dbo].[tb_est_current_status] e
						  LEFT JOIN dbo.tb_users u on e.txt_performed_by = u.txt_username
						  where e.project_number='{projectnumber}'
						  and txt_gate_notes<>''
					UNION
						 ------------
						 --cust/gate notes
						SELECT  
						  3 as NOTES_TYP_REF_ID, 
						  [txt_cust_notes] as NOTES_COMMENT,
						  case 
							when txt_trade_code = 'BL' then 1
							when txt_trade_code = 'EL' then 2
							when txt_trade_code = 'MC' then 3
							when txt_trade_code = 'PL' then 4
							when txt_trade_code = 'PL' then 4
							when txt_trade_code = 'ZN' then 5
							when txt_trade_code = 'FR' then 13
							when txt_trade_code = 'BF' then 25
							when txt_trade_code = 'HL' then 22
							when txt_trade_code = 'PA' then 23
							when txt_trade_code = 'LD' then 24
							when txt_trade_code = 'DC' then 21
						else -1 end as BUSINESS_REF_ID,						  
						  dateadd(hour,4,[performed_on]) as CREATED_DTTM,
						  (case when u.[id_user]  is null then null else CAST((CONCAT(999, u.[id_user])) AS int) end) as WKR_ID_CREATED_TXT
						  FROM [dbo].[tb_est_current_status] e
						  LEFT JOIN dbo.tb_users u on e.txt_performed_by = u.txt_username
						  --LEFT JOIN [azure_sustst].[tst_sustainable].[AION].[project] pp  on e.[project_number]= pp.[SRC_SYSTEM_VAL_TXT]
						  where e.project_number='{projectnumber}'
						  and txt_cust_notes <>'' 
					UNION
					SELECT 
							10 as NOTES_TYP_REF_ID, 
							ac.txt_comment NOTES_COMMENT,
							-1  as BUSINESS_REF_ID,		
							DateAdd(hour,4,ac.inserted_on) as CREATED_DTTM,
							(case when u.[id_user]  is null then null else CAST((CONCAT(999, u.[id_user])) AS int) end) as WKR_ID_CREATED_TXT
						FROM 
					[tst_EPM].[dbo].[tb_action_comments] ac
							INNER JOIN [tst_EPM].[dbo].[tb_users] u ON ac.txt_inserted_by = u.txt_username
							INNER JOIN [tst_EPM].[dbo].[tb_project_history] ph ON ac.id_project_history=ph.id_tb_project_history
							where ph.project_number='{projectnumber}'
							and ph.id_task ='202'
							and txt_comment<>''";

            #region Estimation Notes
            using (new Impersonator(ADUsername, ADDomain, ADPassword))
            {
                using (var context = new EPMData.EPMDbConnect())
                {
                    epmTable = context.Database.SqlQuery<ReadNotesDto>(SQLEstimationNotesQuery).ToList();
                }
                if (epmTable != null)
                {
                    foreach (var item in epmTable)
                    {
                        if (!CheckAionUser(item.WKR_ID_CREATED_TXT.Value))
                        {
                            SeedUser(item.WKR_ID_CREATED_TXT.ToString());
                        }
                    }
                }
            }
            #endregion

            try
            {
                using (new Impersonator(ADUsername, ADDomain, ADPassword))
                {
                    using (var context = new AIONDbContext())
                    {
                        foreach (var item in epmTable)
                        {
                            var sqlAIONQueryInsert = $@"
								INSERT INTO [AION].[NOTES]
								   ( 
									 [NOTES_TYP_REF_ID],
									 [PROJECT_ID],
									 [NOTES_COMMENT], 
									 [BUSINESS_REF_ID],
									 [CREATED_DTTM],									
									 [WKR_ID_CREATED_TXT])
                                    VALUES
								   (
									  {item.NOTES_TYP_REF_ID},
									 '{AION_PROJECT_ID}',
									 '{item.NOTES_COMMENT}',
									 '{item.BUSINESS_REF_ID}',
									 '{item.CREATED_DTTM}',
									 '{item.WKR_ID_CREATED_TXT}'
									)";
                            var noOfRowInserted = context.Database.ExecuteSqlCommand(sqlAIONQueryInsert);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        private static void SeedPROJECT_CYCLE(string projectnumber, int returnedUserId, List<ReadEPMUsersDTO> EpmUser)
        {
            int user_id = int.Parse(returnedUserId.ToString().Replace("999", ""));
            List<PROJECT_CYCLEDto> epmTable = null;
            //List<ReadPLAN_REVIEW_SCHEDULE_DETAILDto> tempPlanReviewSchedDetail = null;

            //string tempprojectid = "RV-429692-001";
            //projectnumber = "431711";

            //AION_PROJECT_ID = 523880;

            int CURRENT_CYCLE_IND = 0;
            var sqlReadEPMQuery = $@"
					  SELECT distinct 
							pe.assessment_cycle as CYCLE_NBR,
							max(pe.plans_ready_on) AS PLANS_READY_ON_DT,
							max(pe.gate_close_date) AS GATE_DT,
							  0 AS CURRENT_CYCLE_IND,
							  0 AS FUTURE_CYCLE_IND,
							  0 AS IS_COMPLETE_IND
						FROM [tst_EPM].[dbo].[tb_project_history] pe	 
						where pe.[project_number] = '{projectnumber}'
						group by assessment_cycle";

            using (new Impersonator(ADUsername, ADDomain, ADPassword))
            {
                using (var context = new EPMData.EPMDbConnect())
                {
                    epmTable = context.Database.SqlQuery<PROJECT_CYCLEDto>(sqlReadEPMQuery).ToList();
                    if (tempVW_Review_Result != null)
                    {
                        foreach (var item in tempVW_Review_Result)
                        {
                            //created_by
                            if (item.created_by == "SYSTEM")
                            {
                                item.UserCreated_by = 1;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(item.created_by))
                                {
                                    if (!CheckAionUser(item.created_by))
                                    {
                                        SeedUser(item.created_by);
                                    }
                                    else
                                    {
                                        item.UserCreated_by = GetAionUserId(item.created_by);
                                    }
                                }
                            }
                            if (item.review_assigned_to == "SYSTEM")
                            {
                                item.UserReview_assigned_to = 1;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(item.review_assigned_to))
                                {
                                    if (!CheckAionUser(item.review_assigned_to))
                                    {
                                        SeedUser(item.review_assigned_to);
                                    }
                                    else
                                    {
                                        item.UserReview_assigned_to = GetAionUserId(item.review_assigned_to);
                                    }
                                }
                            }
                        }
                    }
                    CURRENT_CYCLE_IND = epmTable.Count;
                }
            }

            try
            {
                using (new Impersonator(ADUsername, ADDomain, ADPassword))
                {
                    using (var context = new AIONDbContext())
                    {
                        foreach (var item in epmTable)
                        {
                            if (Convert.ToInt32(item.CYCLE_NBR) == CURRENT_CYCLE_IND)
                            {
                                item.CURRENT_CYCLE_IND = 1;
                            }
                            else
                            {
                                item.IS_COMPLETE_IND = 1;
                                item.IS_APRV_IND = 1;
                            }
                            if (!item.PLANS_READY_ON_DT.HasValue)
                            {
                                Nullable<DateTime> date = null;
                                item.PLANS_READY_ON_DT = date;
                            }
                            var sqlAIONQueryInsert = $@"
								INSERT INTO [AION].[PROJECT_CYCLE]
								   ( 
									   [PROJECT_ID]
									  ,[CYCLE_NBR]
									  ,[PLANS_READY_ON_DT]
									  ,[GATE_DT]
									  ,[CURRENT_CYCLE_IND]
									  ,[FUTURE_CYCLE_IND]
									  ,[IS_COMPLETE_IND]
									  ,[IS_APRV_IND]
									)
                                    VALUES
								   (
									 '{AION_PROJECT_ID}',
									 '{item.CYCLE_NBR}',
									 '{item.PLANS_READY_ON_DT}',
									 '{item.GATE_DT}',
									 '{item.CURRENT_CYCLE_IND}',
									 '{item.FUTURE_CYCLE_IND}',
									 '{item.IS_COMPLETE_IND}',
									 '{item.IS_APRV_IND}'
									)";
                            // NO SAVING
                            var noOfRowInserted = context.Database.ExecuteSqlCommand(sqlAIONQueryInsert);

                            //GET THE MAXIMUM CYCLE NUMBER AND SET UP INTO THE PROJECT TABLE 						 
                            var CYCLE_NBRUpdated = context.Database.ExecuteSqlCommand($@"UPDATE [AION].[PROJECT] SET CYCLE_NBR = '{CURRENT_CYCLE_IND}'  WHERE PROJECT_ID = '{AION_PROJECT_ID}'");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private static void SeedPROJECT_AUDIT(string projectnumber, int returnedUserId)
        {
            int user_id = int.Parse(returnedUserId.ToString().Replace("999", ""));
            //List<PROJECT_AUDITtESTDto> epmTable = null;
            List<EstimationHistoryDto> estHistory = null;
            List<ReadPROJECT_CYCLEDto> project_cycles = null;
            var sqlReadEPMQuery = $@"
				select  distinct 
						pp.PROJECT_ID as PROJECT_ID,
						(case 
							  when  p.[id_task]= 106 then 1
							  when  p.[id_task]= 204 AND [id_user]  is null then 1
							  when  p.[id_task]= 225 then 1
							  when  p.[id_task]= 232 AND [id_user]  is null then 1
							  when  p.[id_task]= 4000 AND [id_user]  is null then 1
							  when  p.[id_task]= 5000 AND [id_user]  is null then 1
							   when  p.[id_task]= 5001 AND [id_user]  is null then 1
							  when [id_user]  is null then NULL else CAST((CONCAT(999, [id_user])) AS int) 
						 end) as AUDIT_USER_NM,
						--,p.performed_on as AUDIT_DT,
						--p.[id_task],
						DateAdd(hour,4,p.performed_on) as AUDIT_DT,
						(case 
							when p.[id_task] =	100	then 1
							when p.[id_task] =	101	then 46
							when p.[id_task] =	105	then 51
							when p.[id_task] =	106	then 34
							when p.[id_task] =	107	then 51
							when p.[id_task] =	108	then 51
							when p.[id_task] =	111	then 51
							when p.[id_task] =	112	then 47							
							when p.[id_task] =	201	then 43
							when p.[id_task] =	202	then 49
							when p.[id_task] =	203	then 48
							when p.[id_task] =	204	then 55
							when p.[id_task] =	205	then 50
							when p.[id_task] =	210	then 2
							when p.[id_task] =	211	then 14
							when p.[id_task] =	212	then 23
							when p.[id_task] =	213	then 30
							when p.[id_task] =	214	then 40
							when p.[id_task] =	215	then 17
							when p.[id_task] =	216	then 5
							when p.[id_task] =	217	then 37
							--This is broken out into 4 groups in AION. Need to find out which one to assign from Sara
							--when p.[id_task] =	217	then 8
							--when p.[id_task] =	217	then 11
							--when p.[id_task] =	217	then 20
							when p.[id_task] =	225	then 43
							when p.[id_task] =	230	then 53
							when p.[id_task] =	231	then 53
							when p.[id_task] =	231	then 53
							when p.[id_task] =	232	then 53
							when p.[id_task] =	4000	then 34
							when p.[id_task] =	5000	then 34
							when p.[id_task] =	5001	then 34
							when p.[id_task] =	5002	then 26
							when p.[id_task] =	5003	then 47
							when p.[id_task] =	5034	then 47
							when p.[id_task] =	6000	then 34
							else -1 end
							) as AUDIT_ACTION_REF_ID
						FROM [dbo].[tb_project_history] p
							LEFT JOIN dbo.tb_users u on p.txt_performed_by = u.txt_username
							LEFT JOIN [azure_sustst].[tst_sustainable].[AION].[project] pp  on p.[project_number]= pp.[SRC_SYSTEM_VAL_TXT]
							WHERE p.[id_task] in 
							(
								100,101,105,106,107,108,111,112,
					 			201,202,203,204,205,208,
								210,211,212,213,214,215,216,217,225,230,231,232,4000,
								5000,5001,5002,5003,5034,5035
							)
							AND p.project_number='{projectnumber}'";
            var sqlReadAllHistory = $@"
					select 
						ph.DML_Datetime,
						txt_performed_by = case 
							when ph.id_task = 502 then (select CAST((CONCAT(999, u1.id_user)) AS varchar) from tb_users u1 where u1.txt_username = mh.updated_by)
							when ph.id_task = 201 then COALESCE((select CAST((CONCAT(999, u2.id_user)) AS varchar) from tb_users u2 where u2.txt_username = eh.txt_performed_by), eh.txt_performed_by)
							when ph.txt_performed_by = 'MODELER' then '1'
							when ph.txt_performed_by = 'SCHEDULER' then '1'
							else ISNULL((select top 1  CAST((CONCAT(999, u3.id_user)) AS varchar) from tb_users u3 where u3.txt_username = ph.txt_performed_by), ph.txt_performed_by)
						end,                 
						case                                
							when ph.id_task = 502 then DateAdd(hour,4,mh.updated_on)
							when ph.id_task = 201 then DateAdd(hour,4,eh.performed_on)
							else DateAdd(hour,4,ph.performed_on) 
						end perf_on,
						state.nme_status,
						task.nme_task,
						task_desc = 
						case 
							when ph.id_task = 201 and eh.txt_trade_code IS NOT NULL then 
								td.txt_trade_desc + ' Estimation ' + 
									case eh.txt_est_state_code
										when 'NA' THEN 'Not Required'
										when 'PE' THEN 'Pending'
										when 'ES' THEN 'Completed'
									end
							when ph.id_task = 201 and eh.txt_trade_code IS NULL THEN 'All Estimations Completed'
							when ph.id_task = 502 THEN mh.txt_status_text
							when ph.id_task = 405 THEN 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Created'
							when ph.id_task = 403 or ph.id_task = 5037 THEN 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Disapproved'
							when ph.id_task = 404 or ph.id_task = 5038 THEN 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Approved'
							when ph.id_task = 406 THEN 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Approved'                    
							when ph.id_task = 603 THEN /* Waive Fees */     COALESCE(ftd.nme_fee_type, 'Fee') + ' Waived'
							when ph.id_task = 604 THEN /* Collect Fees */   COALESCE(ftd.nme_fee_type, 'Fee') + ' Collected'
							when ph.id_task = 605 THEN /* Pay Fees */       COALESCE(ftd.nme_fee_type, 'Fee') + ' Paid'                    
							when ph.id_task = 1002 THEN 
									case when la.txt_link_action_type_code = 'L' THEN 'Project Linked'
											 when la.txt_link_action_type_code = 'D' THEN 'Project Link Removed'
									end
							when ph.id_task = 2040 then ptd.txt_trade_desc + ' Trade Result: ' + prh.result
							when ph.id_task = 2050 then 'Minutes Review Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ': Disapproved'
							when ph.id_task = 2055 then 'Minutes Review Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ': Approved'
                    
							when ph.id_task = 3000 THEN /* Add Fees */      COALESCE(ftd.nme_fee_type, 'Fee') + ' Added'
							when ph.id_task = 3014 THEN /* Update Fees */   COALESCE(ftd.nme_fee_type, 'Fee') + ' Updated'                    
							when ph.id_task = 3016 then /* Start Plan Review */ 
								txt_history_text + ': Cycle ' + cast(ph.assessment_cycle as varchar)                    
							when ph.id_task = 3007 then /* Enter Trade Review Result */ 
								rrt_td.txt_trade_desc + ' ' + txt_history_text                    
							when ph.id_task = 3008 then /* Enter Agency Review Result */ 
								rrca_ad.nme_agency + ' ' + txt_history_text                    
							when ph.id_task = 5004 or ph.id_task = 5005 or ph.id_task = 5027 then /* Save, Accept or Reject Intake */ 
								 case when ia.id_agency_def = 4 then 'County '
									  else 'Town '
								 end + txt_history_text                    
							when ph.id_task = 5006 THEN /* Create Cycle */ 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Created'
							when ph.id_task = 5008 THEN /* Reopen Cycle */ 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Reopened'
							when ph.id_task = 5010 THEN /* Add Fees */      COALESCE(ftd.nme_fee_type, 'Fee') + ' Added'
							when ph.id_task = 5011 THEN /* Waive Fees */    COALESCE(ftd.nme_fee_type, 'Fee') + ' Waived'
							when ph.id_task = 5012 THEN /* Collect Fees */  COALESCE(ftd.nme_fee_type, 'Fee') + ' Collected'                    
							when ph.id_task = 5021 then /* WLR Enter Agency Review Result */ 
								rra_ad.nme_agency + ' ' + txt_history_text                    
							when ph.id_task = 5025 THEN /* Update Fees */   COALESCE(ftd.nme_fee_type, 'Fee') + ' Updated'
							else txt_history_text                
						end,
						ph.id_task,
						ph.id_tb_project_history,
						ph.assessment_cycle,
						ph.app_form_file,
						ph.id_file_store
					from 
						tb_project_history ph 
						left outer join ctb_state_defs state on ph.id_project_state = state.id_status 
						left outer join ctb_task_defs task on ph.id_task = task.id_task 
						left outer join tb_est_history eh on eh.id_project_history = ph.id_tb_project_history 
						left outer join ctb_trade_defs td on td.txt_trade_code = eh.txt_trade_code 
						left outer join tb_meeting_history mh on mh.id_project_history = ph.id_tb_project_history 
						left outer join tb_link_actions la on ph.id_tb_project_history = la.id_project_history 
						left outer join tb_prelim_results_history prh on ph.id_tb_project_history = prh.id_project_history and ph.id_task = 2040 
						left outer join ctb_trade_defs ptd on ptd.txt_trade_code = prh.txt_trade_code                
						left outer join tb_rvw_res_trade_history rrt on ph.id_task = 3007 /* Enter Trade Review Result */ and ph.id_tb_project_history = rrt.id_tb_project_history
						left outer join ctb_trade_defs rrt_td on rrt.txt_trade_code = rrt_td.txt_trade_code            
						left outer join tb_rvw_res_ce_agency_history rrca on ph.id_task = 3008 /* Enter Agency Review Result */ and ph.id_tb_project_history = rrca.id_tb_project_history
						left outer join ctb_agency_defs rrca_ad on rrca.id_agency = rrca_ad.id_agency_def                                        
						left outer join tb_intake_action ia on ph.id_tb_project_history = ia.id_tb_project_history                
						left outer join tb_rvw_res_agency_history rra on ph.id_task = 5021 /* WLR Enter Agency Review Result */ and ph.id_tb_project_history = rra.id_tb_project_history
						left outer join ctb_agency_defs rra_ad on rra.id_agency_def = rra_ad.id_agency_def            
						left outer join tb_project_fee_history pfh on ph.id_task in (603, 604, 605, 3000, 3014, 5010, 5011, 5012, 5025) /* Fee Related Tasks */ and ph.id_tb_project_history = pfh.id_tb_project_history
						left outer join ctb_feetype_defs ftd on pfh.id_fee_types = ftd.id_fee_types            
					where
						ph.project_number ='{projectnumber}'
						and ph.id_task in (
						'100','101','105','106','107','201','230','231','232','112',
						'4000','5001','5002','5003','6000','3014','3025','3000'
						)
					order by        
						ph.id_tb_project_history asc";
            using (new Impersonator(ADUsername, ADDomain, ADPassword))
            {
                //read from aion
                using (var context = new AIONDbContext())
                {
                    project_cycles = context.Database.SqlQuery<ReadPROJECT_CYCLEDto>($@"SELECT * FROM [AION].[PROJECT_CYCLE]  WHERE PROJECT_ID='{AION_PROJECT_ID}'").ToList();
                }
                //read from epm
                using (var context = new EPMData.EPMDbConnect())
                {
                    estHistory = context.Database.SqlQuery<EstimationHistoryDto>(sqlReadAllHistory).ToList();
                }

                //if (item.CYCLE_NBR > 0)
                //{
                //    var cycleid = epmTable.FirstOrDefault(x => x.CYCLE_NBR == item.CYCLE_NBR).PROJECT_CYCLE_ID;
                //    item.CYCLE_NBR = cycleid;
                //}

                //Assing AUDIT_ACTION_REF
                foreach (var item in estHistory)
                {
                    if (item.assessment_cycle > 0)
                    {
                        //assing the right project cycle id
                        int cyclenb = (int)item.assessment_cycle;
                        int selectedprojectcycleid = project_cycles.FirstOrDefault(x => x.CYCLE_NBR == cyclenb).PROJECT_CYCLE_ID;
                        item.assessment_cycle = selectedprojectcycleid;
                    }

                    if (item.txt_performed_by == "SYSTEM")
                    {
                        item.txt_performed_by = "1";
                    }
                    if (item.task_desc.Contains("Application Submitted"))
                    {   //id task 100
                        item.AUDIT_ACTION_REF_ID = 1;
                    }
                    if (item.task_desc.Contains("Plans Ready Date Entered"))
                    {   //id task 101
                        item.AUDIT_ACTION_REF_ID = 46;
                    }
                    if (item.task_desc.Contains("Project Coordinator Assigned"))
                    {   //id task 106
                        item.AUDIT_ACTION_REF_ID = 34;
                    }
                    if (item.task_desc.Contains("Appointments Created (Auto)"))
                    {
                        //230
                        item.AUDIT_ACTION_REF_ID = 45;
                    }
                    if (item.task_desc.Contains("Appointments Created (Manual)"))
                    {
                        //231
                        item.AUDIT_ACTION_REF_ID = 45;
                    }
                    if (item.task_desc.Contains("Appointments Cancelled"))
                    {   //id task 232
                        item.AUDIT_ACTION_REF_ID = 44;
                    }
                    if (item.task_desc.Contains("Project Coordinator"))
                    {   //id task 4000
                        item.AUDIT_ACTION_REF_ID = 34;
                    }
                    if (item.task_desc.Contains("Project Coordinator Assigned"))
                    {   //id task 5001
                        item.AUDIT_ACTION_REF_ID = 34;
                    }
                    if (item.task_desc.Contains("Project Cancelled (Posse)"))
                    {   //id task 5002
                        item.AUDIT_ACTION_REF_ID = 51;
                    }
                    if (item.task_desc.Contains("Project Note Entered"))
                    {   //id task 5003
                        item.AUDIT_ACTION_REF_ID = 47;
                    }
                    if (item.task_desc.Contains("Project Coordinator"))
                    {   //id task 6000
                        item.AUDIT_ACTION_REF_ID = 34;
                    }
                    if (item.task_desc.Contains("Project Abandoned") || item.task_desc.Contains("Project Canceled"))
                    {   //id task 5034,5035
                        item.txt_performed_by = "1";
                    }
                    if (item.task_desc.Contains("CLTWTR Backflow Prevention Estimation Completed"))
                    {
                        item.AUDIT_ACTION_REF_ID = 5;
                    }
                    if (item.task_desc.Contains("CLTWTR Backflow Prevention Estimation Not Required"))
                    {
                        item.AUDIT_ACTION_REF_ID = 6;
                    }
                    if (item.task_desc.Contains("Food Service Estimation Not Required"))
                    {
                        item.AUDIT_ACTION_REF_ID = 21;
                    }
                    if (item.task_desc.Contains("Public Pool Estimation Not Required"))
                    {
                        item.AUDIT_ACTION_REF_ID = 38;
                    }
                    if (item.task_desc.Contains("Zoning Estimation Completed"))
                    {
                        item.AUDIT_ACTION_REF_ID = 40;
                    }
                    if (item.task_desc.Contains("Fire Estimation Completed"))
                    {
                        item.AUDIT_ACTION_REF_ID = 17;
                    }
                    if (item.task_desc.Contains("Building Estimation Completed"))
                    {
                        item.AUDIT_ACTION_REF_ID = 2;
                    }
                    if (item.task_desc.Contains("Electrical Estimation Completed"))
                    {
                        item.AUDIT_ACTION_REF_ID = 14;
                    }
                    if (item.task_desc.Contains("Mechanical Estimation Completed"))
                    {
                        item.AUDIT_ACTION_REF_ID = 23;
                    }
                    if (item.task_desc.Contains("Plumbing Estimation Completed"))
                    {
                        item.AUDIT_ACTION_REF_ID = 30;
                    }
                    if (item.task_desc.Contains("Fee Updated"))
                    {
                        item.AUDIT_ACTION_REF_ID = 29;
                    }
                    if (item.task_desc.Contains("Fee Updated"))
                    {
                        item.AUDIT_ACTION_REF_ID = 29;
                    }
                    if (item.task_desc.Contains("EHS Facility / Lodging Estimation Not Required"))
                    {
                        item.AUDIT_ACTION_REF_ID = 12;
                    }
                    if (item.task_desc.Contains("Plan Review Fee Added"))
                    {
                        item.AUDIT_ACTION_REF_ID = 28;
                    }
                    if (item.task_desc.Contains("Potential Agencies for Permitting"))
                    {
                        item.AUDIT_ACTION_REF_ID = 33;
                    }
                    if (item.task_desc.Contains("Commercial Day Care Estimation Not Required"))
                    {
                        item.AUDIT_ACTION_REF_ID = 9;
                    }
                    if (item.task_desc.Contains("Building Estimation Pending"))
                    {
                        item.AUDIT_ACTION_REF_ID = 4;
                    }
                    if (item.task_desc.Contains("CLTWTR Backflow Prevention Estimation Pending"))
                    {
                        item.AUDIT_ACTION_REF_ID = 7;
                    }
                    if (item.task_desc.Contains("Commercial Day Care Estimation Pending"))
                    {
                        item.AUDIT_ACTION_REF_ID = 10;
                    }
                    if (item.task_desc.Contains("Electrical Estimation Pending"))
                    {
                        item.AUDIT_ACTION_REF_ID = 16;
                    }
                    if (item.task_desc.Contains("Mechanical Estimation Pending"))
                    {
                        item.AUDIT_ACTION_REF_ID = 25;
                    }
                    if (item.task_desc.Contains("Plumbing Estimation Pending"))
                    {
                        item.AUDIT_ACTION_REF_ID = 32;
                    }
                    if (item.task_desc.Contains("Building Estimation Pending"))
                    {
                        item.AUDIT_ACTION_REF_ID = 4;
                    }
                    if (item.task_desc.Contains("Public Pool Estimation Completed"))
                    {
                        item.AUDIT_ACTION_REF_ID = 37;
                    }
                    if (item.task_desc.Contains("Electrical Estimation Not Required"))
                    {
                        item.AUDIT_ACTION_REF_ID = 15;
                    }
                    if (item.task_desc.Contains("Mechanical Estimation Not Required"))
                    {
                        item.AUDIT_ACTION_REF_ID = 24;
                    }
                    if (item.task_desc.Contains("Plumbing Estimation Not Required"))
                    {
                        item.AUDIT_ACTION_REF_ID = 31;
                    }
                    if (item.task_desc.Contains("Zoning Estimation Pending"))
                    {
                        item.AUDIT_ACTION_REF_ID = 42;
                    }
                    if (item.task_desc.Contains("Food Service Estimation Pending"))
                    {
                        item.AUDIT_ACTION_REF_ID = 22;
                    }
                    if (item.task_desc.Contains("Public Pool Estimation Pending"))
                    {
                        item.AUDIT_ACTION_REF_ID = 39;
                    }
                    if (item.task_desc.Contains("EHS Facility / Lodging Estimation Pending"))
                    {
                        item.AUDIT_ACTION_REF_ID = 13;
                    }
                    if (item.task_desc.Contains("Zoning Estimation Not Required"))
                    {
                        item.AUDIT_ACTION_REF_ID = 41;
                    }
                    if (item.task_desc.Contains("Food Service Estimation Completed"))
                    {
                        item.AUDIT_ACTION_REF_ID = 20;
                    }
                    if (item.task_desc.Contains("EHS Facility / Lodging Estimation Completed"))
                    {
                        item.AUDIT_ACTION_REF_ID = 11;
                    }
                    if (item.task_desc.Contains("Commercial Day Care Estimation Not Required"))
                    {
                        item.AUDIT_ACTION_REF_ID = 9;
                    }
                    if (item.task_desc.Contains("Fire Estimation Not Required"))
                    {
                        item.AUDIT_ACTION_REF_ID = 18;
                    }
                    if (item.task_desc.Contains("Building Estimation Not Required"))
                    {
                        item.AUDIT_ACTION_REF_ID = 3;
                    }
                    if (item.task_desc.Contains("Project Note Entered"))
                    {
                        item.AUDIT_ACTION_REF_ID = 47;
                    }

                    //Project Note Entered
                }

                //           foreach (var item in estHistory)
                //           {
                //int? projectid = projectnumber;
                //int? userid = int.Parse(item.txt_performed_by);
                //epmTable.Add(new PROJECT_AUDITtESTDto
                //               {
                //                   PROJECT_ID = projectid,
                //                   AUDIT_DT = item.perf_on.Value,
                //                   AUDIT_USER_NM = userid,
                //                   AUDIT_ACTION_REF_ID = item.AUDIT_ACTION_REF_ID
                //               });
                //           }

                //seed missed user on aion user table
                if (estHistory != null)
                {
                    foreach (var item in estHistory)
                    {
                        if (!string.IsNullOrEmpty(item.txt_performed_by))
                        {
                            if (!CheckAionUser(int.Parse(item.txt_performed_by)))
                            {
                                SeedUser(item.txt_performed_by);
                            }
                        }
                    }
                }
            }
            try
            {
                using (new Impersonator(ADUsername, ADDomain, ADPassword))
                {
                    using (var context = new AIONDbContext())
                    {
                        foreach (var item in estHistory)
                        {
                            var sqlAIONQueryInsert = $@"
								--SET IDENTITY_INSERT [AION].[PROJECT_AUDIT] ON;
								INSERT INTO [AION].[PROJECT_AUDIT]
								   ( 
									  [PROJECT_ID]
									 ,[AUDIT_USER_NM]
									 ,[AUDIT_DT]
									 ,[WKR_ID_CREATED_TXT]
									 ,[WKR_ID_UPDATED_TXT]
									 ,[AUDIT_ACTION_REF_ID]
							         ,[PROJECT_CYCLE_ID]
									)
                                    VALUES
								   (
									 '{AION_PROJECT_ID}',
									 '{item.txt_performed_by}',
									 '{item.perf_on}',
								     '1',
									 '1',
									 '{item.AUDIT_ACTION_REF_ID}',
							         '{item.assessment_cycle}'
									)";
                            var noOfRowInserted = context.Database.ExecuteSqlCommand(sqlAIONQueryInsert);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private static void SeedPROJECT_EMAIL_NOTIF(string projectnumber, int returnedUserId)
        {
            int user_id = int.Parse(returnedUserId.ToString().Replace("999", ""));
            List<PROJECT_EMAIL_NOTIFDto> epmTable = null;

            var sqlReadEPMQuery = $@"
					  SELECT  eq.id_email_queue as id_email_queue, 
							er.txt_email, 
							pe.assessment_cycle,
						    et.id_email_template,
							et.nme_template, 
						    et.subject_template,
							et.body_template,
						    sent_on, 
							trade_or_agency = pe.txt_trade_or_agency
					FROM tb_project_email pe
					inner join tb_email_queue eq on pe.id_email_queue = eq.id_email_queue
					inner join tb_email_recipient er on eq.id_email_queue = er.id_email_queue and er.txt_email <> ''

					inner join ctb_email_template et on pe.id_email_template = et.id_email_template
					where pe.project_number ='{projectnumber}' and (er.inactive_reason is null OR er.inactive_reason not in ('Unsubscribed', 'Opted out'))
					order by eq.id_email_queue";

            using (new Impersonator(ADUsername, ADDomain, ADPassword))
            {
                using (var context = new EPMData.EPMDbConnect())
                {
                    epmTable = context.Database.SqlQuery<PROJECT_EMAIL_NOTIFDto>(sqlReadEPMQuery).ToList();
                }
            }
            try
            {
                using (new Impersonator(ADUsername, ADDomain, ADPassword))
                {
                    using (var context = new AIONDbContext())
                    {
                        int counter = 1;
                        //Plan Review Aborted email  exclude
                        foreach (var item in epmTable)
                        {
                            EmailNotifType EmailNotif;
                            var emailNotifType = string.Empty;
                            //TODO: Need to identified each EmailNotifyType and match it between epm and aion
                            counter++;


                            if (item.id_email_template.Value == 4
                               || item.id_email_template.Value == 5
                               || item.id_email_template.Value == 6
                               || item.id_email_template.Value == 7
                               || item.id_email_template.Value == 8
                               || item.id_email_template.Value == 9
                               || item.id_email_template.Value == 10
                               || item.id_email_template.Value == 11
                               || item.id_email_template.Value == 12
                               || item.id_email_template.Value == 13
                               || item.id_email_template.Value == 14
                               || item.id_email_template.Value == 15
                               || item.id_email_template.Value == 17
                               || item.id_email_template.Value == 18
                               || item.id_email_template.Value == 19
                               || item.id_email_template.Value == 20
                               || item.id_email_template.Value == 21
                               || item.id_email_template.Value == 22
                               || item.id_email_template.Value == 23
                               || item.id_email_template.Value == 24
                               || item.id_email_template.Value == 25
                               || item.id_email_template.Value == 26
                               || item.id_email_template.Value == 27
                               || item.id_email_template.Value == 28
                               || item.id_email_template.Value == 29
                               || item.id_email_template.Value == 30
                               || item.id_email_template.Value == 31
                               || item.id_email_template.Value == 32
                               )
                            {
                                continue;
                            }


                            if (item.id_email_template == 1)
                            {
                                EmailNotif = EmailNotifType.Pending_Estimation;
                                emailNotifType = EmailNotif.ToString();
                            }
                            if (item.id_email_template == 2)
                            {
                                EmailNotif = EmailNotifType.Preliminary_Tentative_Scheduled;
                                emailNotifType = EmailNotif.ToString();
                            }
                            if (item.id_email_template == 3)
                            {
                                EmailNotif = EmailNotifType.Plan_Review_Tentative_Scheduled;
                                emailNotifType = EmailNotif.ToString();
                            }
                            if (item.id_email_template == 16)
                            {
                                EmailNotif = EmailNotifType.Meeting_Tentative_Scheduled;
                                emailNotifType = EmailNotif.ToString();
                            }

                            var sqlAIONQueryInsert = $@"
								INSERT INTO [AION].[PROJECT_EMAIL_NOTIFICATION]
								   ([PROJECT_ID]
								   ,[EMAIL_TYP_DESC]
								   ,[EMAIL_SUBJECT_TXT]
								   ,[EMAIL_BODY_TXT]
								   ,[EMAIL_SENT_DT]
								   ,[SENDER_USER_ID]
								   ,[WKR_ID_CREATED_TXT]
								   ,[CREATED_DTTM]
								   ,[WKR_ID_UPDATED_TXT]
								   ,[UPDATED_DTTM])
							 VALUES
								   (
									 '{AION_PROJECT_ID}'
									,'{emailNotifType}'
									,'{item.subject_template}'
									,'{item.body_template}'
									,'{item.sent_on}'
									,'1'
									,'1'
									,'{DateTime.UtcNow}'
									,'1'
									,'{DateTime.UtcNow}'
									);
								SELECT CAST(SCOPE_IDENTITY() AS INT)";
                            //this query return the inserted id
                            int PROJECT_EMAIL_NOTIFICATION_ID = context.Database
                                    .SqlQuery<int>(sqlAIONQueryInsert)
                                    .Single();

                            var notifEmailQuery = $@"
                            	INSERT INTO [AION].[NOTIFICATION_EMAIL_LIST]
                            	   ([PROJECT_EMAIL_NOTIFICATION_ID]												   
                            	   ,[EMAIL_ADDR_TXT]
                            	   ,[WKR_ID_CREATED_TXT]
                            	   ,[CREATED_DTTM]
                            	   ,[WKR_ID_UPDATED_TXT]
                            	   ,[UPDATED_DTTM])
                             VALUES
                            	   (
                            		'{PROJECT_EMAIL_NOTIFICATION_ID}'												   
                            	   ,'{item.txt_email}'
                            	   ,'1'
                            	   ,'{DateTime.UtcNow}'
                            	   ,'1'
                            	   ,'{DateTime.UtcNow}'
                            );";
                            var result = context.Database.SqlQuery<NOTIFICATION_EMAIL_LISTDto>(notifEmailQuery).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        private static void SeedPRELIMINARY_MEETINGS(string projectnumber, int returnedUserId, ReadEPMProjectDTO data)
        {
            int user_id = int.Parse(returnedUserId.ToString().Replace("999", ""));
            List<TB_PROJECT_CURRENT_STATUSDto> epmTable = null;
            vw_appointmentsDto appoinmnetInfo = null;

            var sqlReadEPMQuery = $@"
					 SELECT TOP (1) 	
							[t0].[performed_on],	
							[t0].[assessment_cycle],	
							[t0].[plans_ready_on],	
							[t0].[plans_review_fee], 
							[t0].[id_tb_project_history], 
							[t0].[application_received_on],
							[t0].[review_cancel_by],
							[t0].[schedule_cycle],
							[t0].[interactive_cycle], 
							[t0].[intake_cycle]
						FROM [dbo].[tb_project_current_status] AS [t0]
						WHERE [t0].[project_number] = '{projectnumber}'";
            var sqlAIONPrelimMeetingAppQueryInsert = string.Empty;


            using (new Impersonator(ADUsername, ADDomain, ADPassword))
            {
                using (var context = new EPMData.EPMDbConnect())
                {
                    epmTable = context.Database.SqlQuery<TB_PROJECT_CURRENT_STATUSDto>(sqlReadEPMQuery).ToList();

                }
                using (var context = new EPMData.EPMDbConnect())
                {
                    appoinmnetInfo = context.Database
                                            .SqlQuery<vw_appointmentsDto>(
                                            $@"SELECT
								t0.txt_trade_code,
								t0.id_appointment_type,
								t0.start_date,
								t0.txt_conf_room,
								t0.appt_start, 	
								t0.appt_end 
						   FROM [dbo].[vw_appointments] AS t0
					       WHERE t0.project_number ='{projectnumber}'").FirstOrDefault();
                }
                //Virtual - Teams Meeting
                //CHECK IF THE ROOM EXIST IF NOT CREATE IT AND RETURN ROOM ID 

            }
            try
            {
                string project_typ_ref_txt = string.Empty;
                if (data.PROJECT_TYP_REF_ID == 1)
                {
                    project_typ_ref_txt = "EXP";
                }
                else if (data.PROJECT_TYP_REF_ID == 2)
                {
                    project_typ_ref_txt = "PMA";
                }
                using (new Impersonator(ADUsername, ADDomain, ADPassword))
                {
                    using (var context = new AIONDbContext())
                    {
                        foreach (var item in epmTable)
                        {

                            //NEED TO DEFINED THE MEETING ROOM
                            // MEETING_ROOM_REF_ID = 12   'Lue - Room 229B'
                            if (appoinmnetInfo == null || appoinmnetInfo.appt_start == null)
                            {
                                break;
                            }
                            sqlAIONPrelimMeetingAppQueryInsert = $@"
								INSERT INTO [AION].[PRELIMINARY_MEETING_APPOINTMENT]
								   ([FROM_DT]
								   ,[TO_DT]
								   ,[MEETING_ROOM_REF_ID]
								   ,[APPT_RESPONSE_STATUS_REF_ID]
								   ,[WKR_ID_CREATED_TXT]
								   ,[CREATED_DTTM]
								   ,[WKR_ID_UPDATED_TXT]
								   ,[UPDATED_DTTM]
								   ,[PROJECT_ID]
								   ,[APPENDIX_AGENDA_DUE_DT]
								   ,[PROPOSED_1_DT]
								   ,[PROPOSED_2_DT]
								   ,[PROPOSED_3_DT]
								   ,[APPT_CANCELLATION_REF_ID]
								   ,[CANCEL_AFTER_DT]
								   ,[RESCHEDULE_IND])
							 VALUES
								   (
									'{appoinmnetInfo.appt_start}'
								   ,'{appoinmnetInfo.appt_end}'
								   ,'12'
								   ,'1'
								   ,'1'
								   ,'{DateTime.UtcNow}'
								   ,'1'
								   ,'{DateTime.UtcNow}'
								   ,'{AION_PROJECT_ID}'
								   ,'{DateTime.UtcNow}'
								   ,NULL
								   ,NULL
								   ,NULL
								   ,NULL
								   ,'{item.review_cancel_by}'
								   ,'0');
									SELECT CAST(SCOPE_IDENTITY() AS INT)";

                            int APPT_ID = context.Database
                                            .SqlQuery<int>(sqlAIONPrelimMeetingAppQueryInsert)
                                            .Single();
                            //app_id=202

                            //int APPT_ID=202;
                            int project_shcedule_id_inserted = context.Database
                                           .SqlQuery<int>($@"INSERT INTO [AION].[PROJECT_SCHEDULE]
											   ([PROJECT_SCHEDULE_TYP_DESC]
											   ,[APPT_ID]
											   ,[WKR_ID_CREATED_TXT]
											   ,[CREATED_DTTM]
											   ,[WKR_ID_UPDATED_TXT]
											   ,[UPDATED_DTTM] )
										 VALUES
											   ('{project_typ_ref_txt}'
											   ,'{APPT_ID}'
											   ,'1'
											   ,'{DateTime.UtcNow}'
											   ,'1'
											   ,'{DateTime.UtcNow}'
											);
											 SELECT CAST(SCOPE_IDENTITY() AS INT)")
                                           .Single();

                            int lastinsertedId = project_shcedule_id_inserted;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        private static string FomatedPermitNum(string pERMIT_NUM)
        {
            if (pERMIT_NUM == null)
            {
                return null;
            }
            var permit_num = pERMIT_NUM.Split('-');
            string ValueToReturn;
            switch (pERMIT_NUM.Length)
            {
                case 13:
                    ValueToReturn = permit_num[1];
                    break;
                case 14:
                    // code block
                    ValueToReturn = permit_num[1];
                    break;
                case 15:
                    // code block
                    ValueToReturn = permit_num[1];
                    break;
                case 18:
                    // code block
                    ValueToReturn = permit_num[3];
                    break;
                case 20:
                    // code block
                    ValueToReturn = permit_num[2];
                    break;
                default:
                    // code block
                    return pERMIT_NUM;
            }
            return ValueToReturn;
        }

        private static void SeedUser(string user_id)
        {
            var epmuser = GetEpmUserInfo(user_id);
            if (epmuser != null)
            {
                AddNewAionUser(epmuser);
            }
        }

        private static void AddNewAionUser(List<ReadEPMUsersDTO> epmuser)
        {

            using (new Impersonator(ADUsername, ADDomain, ADPassword))
            {
                using (var context = new AIONDbContext())
                {
                    if (epmuser != null && epmuser.Count > 0)
                    {
                        foreach (var user in epmuser)
                        {
                            var sqlQueryInsert = $@"
							SET IDENTITY_INSERT [AION].[USER] ON;
							INSERT INTO [AION].[USER]
										(
										 USER_ID
										,FIRST_NM
										,LAST_NM
										,EXTERNAL_SYSTEM_REF_ID
										,SRC_SYSTEM_VAL_TXT
										,WKR_ID_CREATED_TXT
										,CREATED_DTTM
										,ACTIVE_IND
										,USER_INTERFACE_SETTING_TXT
										,IS_EXPRESS_SCHEDULED_IND
										,USER_NM
										,LAN_ID_TXT
										,PHONE_NUM
										,EMAIL_ADDR_TXT
										,CITY_IND)
									VALUES
										(
										 '{user.USER_ID}'
										,'{user.FIRST_NM}'
										,'{user.LAST_NM}'
										,'{user.EXTERNAL_SYSTEM_REF_ID}'
										,'{user.SRC_SYSTEM_VAL_TXT}'
										,'{user.WKR_ID_CREATED_TXT}'
										,'{DateTime.Now}'
										,'{user.ACTIVE_IND}'
										,'{user.USER_INTERFACE_SETTING_TXT}'
										,'{user.IS_EXPRESS_SCHEDULED_IND}'
										,'{user.USER_NM}'
										,'{user.LAN_ID_TXT}'
										,'{user.PHONE_NUM}'
										,'{user.EMAIL_ADDR_TXT}'
										,'{user.CITY_IND}')";
                            var noOfRowInserted1 = context.Database.ExecuteSqlCommand(sqlQueryInsert);
                            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [AION].[USER] OFF;");
                        }
                    }
                }
            }
        }

        private static List<ReadEPMUsersDTO> GetEpmUserInfo(string user_id)
        {
            List<ReadEPMUsersDTO> userTable = null;

            var isNumeric = int.TryParse(user_id, out _);

            if (isNumeric)
            {
                int uid = int.Parse(user_id.Replace("999", ""));
                var epmUsersQuery = $@"
				select distinct
					CAST((CONCAT(999, id_user)) AS int) as USER_ID,
					nme_first as FIRST_NM,
					nme_last AS LAST_NM,
					-1 as EXTERNAL_SYSTEM_REF_ID,
					txt_username+'EPM'+CAST(id_user AS varchar) as SRC_SYSTEM_VAL_TXT,
					1 AS WKR_ID_CREATED_TXT,					
					1 AS [WKR_ID_UPDATED_TXT],
					1 as ACTIVE_IND,
					NULL USER_INTERFACE_SETTING_TXT,
					NULL IS_EXPRESS_SCHEDULED_IND,
					txt_email AS USER_NM,
					NULL as LAN_ID_TXT,
					txt_phone PHONE_NUM,
 					txt_email EMAIL_ADDR_TXT,
 					(case 
 						when city_access_flg  = 'Y' then 1
 						when city_access_flg  = 'N' then 0
 						else null end) as CITY_IND
				from [dbo].[tb_users] where id_user='{uid}'";

                using (new Impersonator(ADUsername, ADDomain, ADPassword))
                {
                    using (var context = new EPMData.EPMDbConnect())
                    {
                        userTable = context.Database
                            .SqlQuery<ReadEPMUsersDTO>(epmUsersQuery).ToList();
                        if (userTable == null)
                        {
                            userTable = GetEpmUserInfoByUserName(user_id);
                        }
                        return userTable;
                    }
                }
            }
            else
            {
                var epmUsersQuery = $@"
				select distinct
					CAST((CONCAT(999, id_user)) AS int) as USER_ID,
					nme_first as FIRST_NM,
					nme_last AS LAST_NM,
					-1 as EXTERNAL_SYSTEM_REF_ID,
					txt_username+'EPM'+CAST(id_user AS varchar) as SRC_SYSTEM_VAL_TXT,
					1 AS WKR_ID_CREATED_TXT,					
					1 AS [WKR_ID_UPDATED_TXT],
					1 as ACTIVE_IND,
					NULL USER_INTERFACE_SETTING_TXT,
					NULL IS_EXPRESS_SCHEDULED_IND,
					txt_email AS USER_NM,
					NULL as LAN_ID_TXT,
					txt_phone PHONE_NUM,
 					txt_email EMAIL_ADDR_TXT,
 					(case 
 						when city_access_flg  = 'Y' then 1
 						when city_access_flg  = 'N' then 0
 						else null end) as CITY_IND
				from [dbo].[tb_users] where txt_username ='{user_id}'";

                using (new Impersonator(ADUsername, ADDomain, ADPassword))
                {
                    using (var context = new EPMData.EPMDbConnect())
                    {
                        userTable = context.Database
                            .SqlQuery<ReadEPMUsersDTO>(epmUsersQuery).ToList();
                        if (userTable == null)
                        {
                            userTable = GetEpmUserInfoByUserName(user_id);
                        }
                        return userTable;
                    }
                }
            }
        }

        private static List<ReadEPMUsersDTO> GetEpmUserInfoByUserName(string user_id)
        {
            List<ReadEPMUsersDTO> userTable = null;
            int uid = int.Parse(user_id.Replace("999", ""));
            var epmUsersQuery = $@"
				select distinct
					CAST((CONCAT(999, id_user)) AS int) as USER_ID,
					nme_first as FIRST_NM,
					nme_last AS LAST_NM,
					-1 as EXTERNAL_SYSTEM_REF_ID,
					txt_username+'EPM'+CAST(id_user AS varchar) as SRC_SYSTEM_VAL_TXT,
					1 AS WKR_ID_CREATED_TXT,					
					1 AS [WKR_ID_UPDATED_TXT],
					1 as ACTIVE_IND,
					NULL USER_INTERFACE_SETTING_TXT,
					NULL IS_EXPRESS_SCHEDULED_IND,
					txt_email AS USER_NM,
					NULL as LAN_ID_TXT,
					txt_phone PHONE_NUM,
 					txt_email EMAIL_ADDR_TXT,
 					(case 
 						when city_access_flg  = 'Y' then 1
 						when city_access_flg  = 'N' then 0
 						else null end) as CITY_IND
				from [dbo].[tb_users] where txt_username='{uid}'";

            using (new Impersonator(ADUsername, ADDomain, ADPassword))
            {
                using (var context = new EPMData.EPMDbConnect())
                {
                    userTable = context.Database
                        .SqlQuery<ReadEPMUsersDTO>(epmUsersQuery).ToList();

                    return userTable;
                }
            }
        }

        private static bool CheckEPMUser(int user_id)
        {

            using (new Impersonator(ADUsername, ADDomain, ADPassword))
            {
                using (var context = new EPMData.EPMDbConnect())
                {
                    decimal? data = context.Database.SqlQuery<decimal?>($@"select id_user from [dbo].[tb_users] where id_user='{user_id}'").FirstOrDefault();
                    return data.HasValue;
                }
            }
        }

        /// <summary>
        /// Check if user id exists on AION.USER table
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns>true/false</returns>
        private static bool CheckAionUser(int user_id)
        {
            using (new Impersonator(ADUsername, ADDomain, ADPassword))
            {
                using (var context = new AIONDbContext())
                {
                    int? data = context.Database.SqlQuery<int?>($@"SELECT USER_ID from [AION].[USER] where USER_ID = {user_id}").FirstOrDefault();
                    return data.HasValue;
                }
            }
        }

        /// <summary>
        /// Get the User ID passing the lan id text username
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        private static bool CheckAionUser(string user_id)
        {
            using (new Impersonator(ADUsername, ADDomain, ADPassword))
            {
                using (var context = new AIONDbContext())
                {
                    int? data1 = context.Database.SqlQuery<int?>($@"SELECT USER_ID from [AION].[USER] where LAN_ID_TXT = '{user_id}'").FirstOrDefault();
                    if (data1.HasValue)
                    {
                        return true;
                    }
                    int? data2 = context.Database.SqlQuery<int?>($@"SELECT USER_ID from [AION].[USER] where SRC_SYSTEM_VAL_TXT LIKE '%{user_id}%'").FirstOrDefault();
                    if (data2.HasValue)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        private static int GetAionUserId(string user_id)
        {
            using (new Impersonator(ADUsername, ADDomain, ADPassword))
            {
                using (var context = new AIONDbContext())
                {
                    int? data1 = context.Database.SqlQuery<int?>($@"SELECT USER_ID from [AION].[USER] where LAN_ID_TXT = '{user_id}'").FirstOrDefault();

                    if (data1.HasValue)
                    {
                        return data1.GetValueOrDefault();
                    }
                    int? data2 = context.Database.SqlQuery<int?>($@"SELECT USER_ID from [AION].[USER] where SRC_SYSTEM_VAL_TXT LIKE '%{user_id}%'").FirstOrDefault();
                    if (data2.HasValue)
                    {
                        return data2.GetValueOrDefault();
                    }
                    return 0;
                }
            }
        }
    }
}
