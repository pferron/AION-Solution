select 
		p.project_number,
		p.app_form_xml as xml,		
		CONCAT
		(
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
		) AS DESIGNER_DESC,
		CONCAT
		(
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
		) AS SEAL_HOLDERS_DESC		
from tb_project_current_status p
where p.project_number='390604'
 
