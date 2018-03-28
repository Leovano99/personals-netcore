namespace VDI.Demo.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// <see cref="AppAuthorizationProvider"/> for permission definitions.
    /// </summary>
    public static class AppPermissions
    {
        //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

        public const string Pages = "Pages";

        public const string Pages_DemoUiComponents = "Pages.DemoUiComponents";
        public const string Pages_Administration = "Pages.Administration";

        public const string Pages_Administration_Roles = "Pages.Administration.Roles";
        public const string Pages_Administration_Roles_Create = "Pages.Administration.Roles.Create";
        public const string Pages_Administration_Roles_Edit = "Pages.Administration.Roles.Edit";
        public const string Pages_Administration_Roles_Delete = "Pages.Administration.Roles.Delete";

        public const string Pages_Administration_Users = "Pages.Administration.Users";
        public const string Pages_Administration_Users_Create = "Pages.Administration.Users.Create";
        public const string Pages_Administration_Users_Edit = "Pages.Administration.Users.Edit";
        public const string Pages_Administration_Users_Delete = "Pages.Administration.Users.Delete";
        public const string Pages_Administration_Users_ChangePermissions = "Pages.Administration.Users.ChangePermissions";
        public const string Pages_Administration_Users_Impersonation = "Pages.Administration.Users.Impersonation";

        public const string Pages_Administration_Languages = "Pages.Administration.Languages";
        public const string Pages_Administration_Languages_Create = "Pages.Administration.Languages.Create";
        public const string Pages_Administration_Languages_Edit = "Pages.Administration.Languages.Edit";
        public const string Pages_Administration_Languages_Delete = "Pages.Administration.Languages.Delete";
        public const string Pages_Administration_Languages_ChangeTexts = "Pages.Administration.Languages.ChangeTexts";

        public const string Pages_Administration_AuditLogs = "Pages.Administration.AuditLogs";

        public const string Pages_Administration_OrganizationUnits = "Pages.Administration.OrganizationUnits";
        public const string Pages_Administration_OrganizationUnits_ManageOrganizationTree = "Pages.Administration.OrganizationUnits.ManageOrganizationTree";
        public const string Pages_Administration_OrganizationUnits_ManageMembers = "Pages.Administration.OrganizationUnits.ManageMembers";

        public const string Pages_Administration_HangfireDashboard = "Pages.Administration.HangfireDashboard";

        public const string Pages_Administration_UiCustomization = "Pages.Administration.UiCustomization";

        //TENANT-SPECIFIC PERMISSIONS

        public const string Pages_Tenant_Dashboard = "Pages.Tenant.Dashboard";

        public const string Pages_Administration_Tenant_Settings = "Pages.Administration.Tenant.Settings";

        public const string Pages_Administration_Tenant_SubscriptionManagement = "Pages.Administration.Tenant.SubscriptionManagement";

        //HOST-SPECIFIC PERMISSIONS

        public const string Pages_Editions = "Pages.Editions";
        public const string Pages_Editions_Create = "Pages.Editions.Create";
        public const string Pages_Editions_Edit = "Pages.Editions.Edit";
        public const string Pages_Editions_Delete = "Pages.Editions.Delete";

        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Tenants_Create = "Pages.Tenants.Create";
        public const string Pages_Tenants_Edit = "Pages.Tenants.Edit";
        public const string Pages_Tenants_ChangeFeatures = "Pages.Tenants.ChangeFeatures";
        public const string Pages_Tenants_Delete = "Pages.Tenants.Delete";
        public const string Pages_Tenants_Impersonation = "Pages.Tenants.Impersonation";

        public const string Pages_Administration_Host_Maintenance = "Pages.Administration.Host.Maintenance";
        public const string Pages_Administration_Host_Settings = "Pages.Administration.Host.Settings";
        public const string Pages_Administration_Host_Dashboard = "Pages.Administration.Host.Dashboard";

        //MASTER PLAN PERMISSIONS
        /* MasterPlan */
        public const string Pages_Tenant_MasterPlan = "Pages.Tenant.MasterPlan";

        /* Territory */
        public const string Pages_Tenant_Territory = "Pages.Tenant.MasterPlan.Territory";

        public const string Pages_Tenant_MasterRegion = "Pages.Tenant.MasterPlan.Territory.MasterRegion";
        public const string Pages_Tenant_MasterRegion_Create = "Pages.Tenant.MasterPlan.Territory.MasterRegion.Create";
        public const string Pages_Tenant_MasterRegion_Edit = "Pages.Tenant.MasterPlan.Territory.MasterRegion.Edit";
        public const string Pages_Tenant_MasterRegion_Delete = "Pages.Tenant.MasterPlan.Territory.MasterRegion.Delete";

        /* Project */
        public const string Pages_Tenant_Project = "Pages.Tenant.MasterPlan.Project";
        public const string Pages_Tenant_MasterDepartment = "Pages.Tenant.MasterPlan.Project.MasterDepartment";
        public const string Pages_Tenant_MasterDepartment_Create = "Pages.Tenant.MasterPlan.Project.MasterDepartment.Create";
        public const string Pages_Tenant_MasterDepartment_Edit = "Pages.Tenant.MasterPlan.Project.MasterDepartment.Edit";
        public const string Pages_Tenant_MasterDepartment_Delete = "Pages.Tenant.MasterPlan.Project.MasterDepartment.Delete";
        public const string Pages_Tenant_MasterBank = "Pages.Tenant.MasterPlan.Project.MasterBank";
        public const string Pages_Tenant_MasterBank_Create = "Pages.Tenant.MasterPlan.Project.MasterBank.Create";
        public const string Pages_Tenant_MasterBank_Edit = "Pages.Tenant.MasterPlan.Project.MasterBank.Edit";
        public const string Pages_Tenant_MasterBank_Delete = "Pages.Tenant.MasterPlan.Project.MasterBank.Delete";
        public const string Pages_Tenant_MasterBankBranch = "Pages.Tenant.MasterPlan.Project.MasterBankBranch";
        public const string Pages_Tenant_MasterBankBranch_Create = "Pages.Tenant.MasterPlan.Project.MasterBankBranch.Create";
        public const string Pages_Tenant_MasterBankBranch_Edit = "Pages.Tenant.MasterPlan.Project.MasterBankBranch.Edit";
        public const string Pages_Tenant_MasterBankBranch_Delete = "Pages.Tenant.MasterPlan.Project.MasterBankBranch.Delete";
        public const string Pages_Tenant_MasterBankBranch_Detail = "Pages.Tenant.MasterPlan.Project.MasterBankBranch.Detail";
        public const string Pages_Tenant_MasterCompany = "Pages.Tenant.MasterPlan.Project.MasterCompany";
        public const string Pages_Tenant_MasterCompany_Create = "Pages.Tenant.MasterPlan.Project.MasterCompany.Create";
        public const string Pages_Tenant_MasterCompany_Edit = "Pages.Tenant.MasterPlan.Project.MasterCompany.Edit";
        public const string Pages_Tenant_MasterCompany_Detail = "Pages.Tenant.MasterPlan.Project.MasterCompany.Detail";
        public const string Pages_Tenant_MasterCompany_Delete = "Pages.Tenant.MasterPlan.Project.MasterCompany.Delete";
        public const string Pages_Tenant_MasterAccount = "Pages.Tenant.MasterPlan.Project.MasterAccount";
        public const string Pages_Tenant_MasterAccount_Create = "Pages.Tenant.MasterPlan.Project.MasterAccount.Create";
        public const string Pages_Tenant_MasterAccount_Edit = "Pages.Tenant.MasterPlan.Project.MasterAccount.Edit";
        public const string Pages_Tenant_MasterAccount_Delete = "Pages.Tenant.MasterPlan.Project.MasterAccount.Delete";
        public const string Pages_Tenant_MasterAccount_Detail = "Pages.Tenant.MasterPlan.Project.MasterAccount.Detail";
        public const string Pages_Tenant_MasterEntity = "Pages.Tenant.MasterPlan.Project.MasterEntity";
        public const string Pages_Tenant_MasterEntity_Create = "Pages.Tenant.MasterPlan.Project.MasterEntity.Create";
        public const string Pages_Tenant_MasterEntity_Edit = "Pages.Tenant.MasterPlan.Project.MasterEntity.Edit";
        public const string Pages_Tenant_MasterEntity_Delete = "Pages.Tenant.MasterPlan.Project.MasterEntity.Delete";
        public const string Pages_Tenant_MasterProject = "Pages.Tenant.MasterPlan.Project.MasterProject";
        public const string Pages_Tenant_MasterProject_Create = "Pages.Tenant.MasterPlan.Project.MasterProject.Create";
        public const string Pages_Tenant_MasterProject_Edit = "Pages.Tenant.MasterPlan.Project.MasterProject.Edit";
        public const string Pages_Tenant_MasterProject_Detail = "Pages.Tenant.MasterPlan.Project.MasterProject.Detail";
        public const string Pages_Tenant_MasterProject_Delete = "Pages.Tenant.MasterPlan.Project.MasterProject.Delete";
        public const string Pages_Tenant_MasterCategory = "Pages.Tenant.MasterPlan.Project.MasterCategory";
        public const string Pages_Tenant_MasterCategory_Create = "Pages.Tenant.MasterPlan.Project.MasterCategory.Create";
        public const string Pages_Tenant_MasterCategory_Edit = "Pages.Tenant.MasterPlan.Project.MasterCategory.Edit";
        public const string Pages_Tenant_MasterCategory_Delete = "Pages.Tenant.MasterPlan.Project.MasterCategory.Delete";
        public const string Pages_Tenant_MasterProduct = "Pages.Tenant.MasterPlan.Project.MasterProduct";
        public const string Pages_Tenant_MasterProduct_Create = "Pages.Tenant.MasterPlan.Project.MasterProduct.Create";
        public const string Pages_Tenant_MasterProduct_Edit = "Pages.Tenant.MasterPlan.Project.MasterProduct.Edit";
        public const string Pages_Tenant_MasterProduct_Delete = "Pages.Tenant.MasterPlan.Project.MasterProduct.Delete";

        public const string Pages_Tenant_MasterPosition = "Pages.Tenant.MasterPlan.Project.MasterPosition";
        public const string Pages_Tenant_MasterPosition_Create = "Pages.Tenant.MasterPlan.Project.MasterPosition.Create";
        public const string Pages_Tenant_MasterPosition_Edit = "Pages.Tenant.MasterPlan.Project.MasterPosition.Edit";
        public const string Pages_Tenant_MasterPosition_Delete = "Pages.Tenant.MasterPlan.Project.MasterPosition.Delete";
        public const string Pages_Tenant_MasterOfficer = "Pages.Tenant.MasterPlan.Project.MasterOfficer";
        public const string Pages_Tenant_MasterOfficer_Create = "Pages.Tenant.MasterPlan.Project.MasterOfficer.Create";
        public const string Pages_Tenant_MasterOfficer_Edit = "Pages.Tenant.MasterPlan.Project.MasterOfficer.Edit";
        public const string Pages_Tenant_MasterOfficer_Delete = "Pages.Tenant.MasterPlan.Project.MasterOfficer.Delete";
        public const string Pages_Tenant_MasterOfficer_Detail = "Pages.Tenant.MasterPlan.Project.MasterOfficer.Detail";

        /* SettingConnString */
        public const string Pages_Tenant_SettingConnString = "Pages.Tenant.MasterPlan.SettingConnString";
        public const string Pages_Tenant_SettingConnStringDMT = "Pages.Tenant.MasterPlan.SettingConnString.SettingConnStringDMT";
        public const string Pages_Tenant_SettingConnStringCorsec = "Pages.Tenant.MasterPlan.SettingConnString.SettingConnStringCorsec";

        /* Unit */
        public const string Pages_Tenant_Unit = "Pages.Tenant.MasterPlan.Unit";
        public const string Pages_Tenant_Item = "Pages.Tenant.MasterPlan.Unit.Item";
        public const string Pages_Tenant_Item_Create = "Pages.Tenant.MasterPlan.Unit.Item.Create";
        public const string Pages_Tenant_Item_Edit = "Pages.Tenant.MasterPlan.Unit.Item.Edit";
        public const string Pages_Tenant_Item_Delete = "Pages.Tenant.MasterPlan.Unit.Item.Delete";
        public const string Pages_Tenant_MasterUnitStatus = "Pages.Tenant.MasterPlan.Unit.MasterUnitStatus";
        public const string Pages_Tenant_MasterRenovation = "Pages.Tenant.MasterPlan.Unit.MasterRenovation";
        public const string Pages_Tenant_MasterRenovation_Create = "Pages.Tenant.MasterPlan.Unit.MasterRenovation.Create";
        public const string Pages_Tenant_MasterRenovation_Edit = "Pages.Tenant.MasterPlan.Unit.MasterRenovation.Edit";
        public const string Pages_Tenant_MasterRenovation_Delete = "Pages.Tenant.MasterPlan.Unit.MasterRenovation.Delete";
        public const string Pages_Tenant_MasterFacing = "Pages.Tenant.MasterPlan.Unit.MasterFacing";
        public const string Pages_Tenant_MasterZoning = "Pages.Tenant.MasterPlan.Unit.MasterZoning";
        public const string Pages_Tenant_MasterZoning_Create = "Pages.Tenant.MasterPlan.Unit.MasterZoning.Create";
        public const string Pages_Tenant_MasterZoning_Edit = "Pages.Tenant.MasterPlan.Unit.MasterZoning.Edit";
        public const string Pages_Tenant_MasterZoning_Delete = "Pages.Tenant.MasterPlan.Unit.MasterZoning.Delete";
        public const string Pages_Tenant_MasterFacade = "Pages.Tenant.MasterPlan.Unit.MasterFacade";
        public const string Pages_Tenant_MasterFacade_Create = "Pages.Tenant.MasterPlan.Unit.MasterFacade.Create";
        public const string Pages_Tenant_MasterFacade_Edit = "Pages.Tenant.MasterPlan.Unit.MasterFacade.Edit";
        public const string Pages_Tenant_MasterFacade_Delete = "Pages.Tenant.MasterPlan.Unit.MasterFacade.Delete";
        public const string Pages_Tenant_MasterColor = "Pages.Tenant.MasterPlan.Unit.MasterColor";
        public const string Pages_Tenant_MasterColor_Create = "Pages.Tenant.MasterPlan.Unit.MasterColor.Create";
        public const string Pages_Tenant_MasterColor_Edit = "Pages.Tenant.MasterPlan.Unit.MasterColor.Edit";
        public const string Pages_Tenant_MasterColor_Delete = "Pages.Tenant.MasterPlan.Unit.MasterColor.Delete";

        public const string Pages_Tenant_MasterTowerType = "Pages.Tenant.MasterPlan.Unit.MasterTowerType";

        public const string Pages_Tenant_GenerateUnit = "Pages.Tenant.MasterPlan.Unit.GenerateUnit";
        public const string Pages_Tenant_GenerateUnit_BySystem = "Pages.Tenant.MasterPlan.Unit.GenerateUnit.BySystem";
        public const string Pages_Tenant_GenerateUnit_BySystem_DefineProject = "Pages.Tenant.MasterPlan.Unit.GenerateUnit.DefineProject";
        public const string Pages_Tenant_GenerateUnit_BySystem_SetFloor = "Pages.Tenant.MasterPlan.Unit.GenerateUnit.SetFloor";
        public const string Pages_Tenant_GenerateUnit_BySystem_SetUnit = "Pages.Tenant.MasterPlan.Unit.GenerateUnit.SetUnit";
        public const string Pages_Tenant_GenerateUnit_BySystem_SetUnitType = "Pages.Tenant.MasterPlan.Unit.GenerateUnit.SetUnitType";
        public const string Pages_Tenant_GenerateUnit_BySystem_CreateUnit = "Pages.Tenant.MasterPlan.Unit.GenerateUnit.CreateUnit";
        public const string Pages_Tenant_GenerateUnit_ByUploadExcel = "Pages.Tenant.MasterPlan.Unit.GenerateUnit.ByUploadExcel";
        public const string Pages_Tenant_GenerateUnit_BySystem_SetCluster = "Pages.Tenant.MasterPlan.Unit.GenerateUnit.SetCluster";
        public const string Pages_Tenant_GenerateUnit_BySystem_SetBuilding = "Pages.Tenant.MasterPlan.Unit.GenerateUnit.SetBuilding";
        public const string Pages_Tenant_MasterArea = "Pages.Tenant.MasterPlan.Unit.MasterArea";
        public const string Pages_Tenant_MasterArea_Create = "Pages.Tenant.MasterPlan.Unit.MasterArea.Create";
        public const string Pages_Tenant_MasterArea_Edit = "Pages.Tenant.MasterPlan.Unit.MasterArea.Edit";
        public const string Pages_Tenant_MasterArea_Delete = "Pages.Tenant.MasterPlan.Unit.MasterArea.Delete";
        public const string Pages_Tenant_MasterCluster = "Pages.Tenant.MasterPlan.Unit.MasterCluster";
        public const string Pages_Tenant_MasterCluster_Create = "Pages.Tenant.MasterPlan.Unit.MasterCluster.Create";
        public const string Pages_Tenant_MasterCluster_Edit = "Pages.Tenant.MasterPlan.Unit.MasterCluster.Edit";
        public const string Pages_Tenant_MasterCluster_Delete = "Pages.Tenant.MasterPlan.Unit.MasterCluster.Delete";
        public const string Pages_Tenant_ManageStatus = "Pages.Tenant.MasterPlan.Unit.ManageStatus";
        public const string Pages_Tenant_MasterTerritory = "Pages.Tenant.MasterPlan.Unit.Territory.MasterTerritory";
        public const string Pages_Tenant_MasterTerritory_Create = "Pages.Tenant.MasterPlan.Unit.MasterTerritory.Create";
        public const string Pages_Tenant_MasterTerritory_Edit = "Pages.Tenant.MasterPlan.Unit.MasterTerritory.Edit";
        public const string Pages_Tenant_MasterTerritory_Delete = "Pages.Tenant.MasterPlan.Unit.MasterTerritory.Delete";
        public const string Pages_Tenant_MasterCounty = "Pages.Tenant.MasterPlan.Unit.MasterCounty";
        public const string Pages_Tenant_MasterCounty_Create = "Pages.Tenant.MasterPlan.Unit.MasterCounty.Create";
        public const string Pages_Tenant_MasterCounty_Edit = "Pages.Tenant.MasterPlan.Unit.MasterCounty.Edit";
        public const string Pages_Tenant_MasterCounty_Delete = "Pages.Tenant.MasterPlan.Unit.MasterCounty.Delete";
        public const string Pages_Tenant_MasterCity = "Pages.Tenant.MasterPlan.Unit.MasterCity";
        public const string Pages_Tenant_MasterCity_Create = "Pages.Tenant.MasterPlan.Unit.MasterCity.Create";
        public const string Pages_Tenant_MasterCity_Edit = "Pages.Tenant.MasterPlan.Unit.MasterCity.Edit";
        public const string Pages_Tenant_MasterCity_Delete = "Pages.Tenant.MasterPlan.Unit.MasterCity.Delete";
        public const string Pages_Tenant_MasterDetail = "Pages.Tenant.MasterPlan.Unit.MasterDetail";
        public const string Pages_Tenant_MasterDetail_Create = "Pages.Tenant.MasterPlan.Unit.MasterDetail.Create";
        public const string Pages_Tenant_MasterDetail_Edit = "Pages.Tenant.MasterPlan.Unit.MasterDetail.Edit";
        public const string Pages_Tenant_MasterDetail_Delete = "Pages.Tenant.MasterPlan.Unit.MasterDetail.Delete";
        public const string Pages_Tenant_MasterUnitCode = "Pages.Tenant.MasterPlan.Unit.MasterUnitCode";
        public const string Pages_Tenant_MasterUnitCode_Create = "Pages.Tenant.MasterPlan.Unit.MasterUnitCode.Create";
        public const string Pages_Tenant_MasterUnitCode_Edit = "Pages.Tenant.MasterPlan.Unit.MasterUnitCode.Edit";
        public const string Pages_Tenant_MasterUnitCode_Delete = "Pages.Tenant.MasterPlan.Unit.MasterUnitCode.Delete";
        public const string Pages_Tenant_MappingItem = "Pages.Tenant.MasterPlan.Unit.MappingItem";

        /* Pricing */
        public const string Pages_Tenant_Pricing = "Pages.Tenant.Pricing";
        public const string Pages_Tenant_MasterTerm = "Pages.Tenant.Pricing.MasterTerm";
        public const string Pages_Tenant_MasterTerm_Create = "Pages.Tenant.Pricing.MasterTerm.Create";
        public const string Pages_Tenant_MasterTerm_Edit = "Pages.Tenant.Pricing.MasterTerm.Edit";
        public const string Pages_Tenant_MasterDiscount = "Pages.Tenant.Pricing.MasterDiscount";
        public const string Pages_Tenant_MasterDiscount_Create = "Pages.Tenant.Pricing.MasterDiscount.Create";
        public const string Pages_Tenant_MasterDiscount_Edit = "Pages.Tenant.Pricing.MasterDiscount.Edit";
        public const string Pages_Tenant_MasterDiscount_Delete = "Pages.Tenant.Pricing.MasterDiscount.Delete";
        public const string Pages_Tenant_MasterFinType = "Pages.Tenant.Pricing.MasterFinType";
        public const string Pages_Tenant_MasterFinType_Create = "Pages.Tenant.Pricing.MasterFinType.Create";
        public const string Pages_Tenant_MasterFinType_Edit = "Pages.Tenant.Pricing.MasterFinType.Edit";
        public const string Pages_Tenant_MasterFinType_Delete = "Pages.Tenant.Pricing.MasterFinType.Delete";
        public const string Pages_Tenant_UploadBasePrice = "Pages.Tenant.Pricing.UploadBasePrice";
        public const string Pages_Tenant_UploadBasePrice_GenBasePriceBySystem = "Pages.Tenant.Pricing.GenBasePriceBySystem";
        public const string Pages_Tenant_UploadBasePrice_UploadBasePriceByExcel = "Pages.Tenant.Pricing.UploadBasePriceByExcel";
        public const string Pages_Tenant_UploadBasePrice_UploadBasePriceByExcel_Add = "Pages.Tenant.Pricing.UploadBasePriceByExcel.Add";
        public const string Pages_Tenant_MasterMarketingFactor = "Pages.Tenant.Pricing.MasterMarketingFactor";
        public const string Pages_Tenant_MasterMarketingFactor_Create = "Pages.Tenant.Pricing.MasterMarketingFactor.Create";
        public const string Pages_Tenant_MasterMarketingFactor_Edit = "Pages.Tenant.Pricing.MasterMarketingFactor.Edit";
        public const string Pages_Tenant_MasterMarketingFactor_Delete = "Pages.Tenant.Pricing.MasterMarketingFactor.Delete";
        public const string Pages_Tenant_GeneratePrice = "Pages.Tenant.Pricing.GeneratePrice";
        public const string Pages_Tenant_GeneratePrice_GeneratePriceList = "Pages.Tenant.Pricing.GeneratePriceList";
        public const string Pages_Tenant_GeneratePrice_UploadExcel = "Pages.Tenant.Pricing.GeneratePriceUploadExcel";
        public const string Pages_Tenant_GeneratePrice_UploadPriceList = "Pages.Tenant.Pricing.GeneratePriceUploadPriceList";
        public const string Pages_Tenant_PriceListHistory = "Pages.Tenant.Pricing.PriceListHistory";
        public const string Pages_Tenant_ManagePriceIncreases = "Pages.Tenant.Pricing.ManagePriceIncreases";
        public const string Pages_Tenant_Approval = "Pages.Tenant.Pricing.Approval";

        /* Commission */
        public const string Pages_Tenant_Commission = "Pages.Tenant.Commission";
        public const string Pages_Tenant_MasterSchema = "Pages.Tenant.Commission.MasterSchema";
        public const string Pages_Tenant_MasterSchema_Create = "Pages.Tenant.Commission.MasterSchema.Create";
        public const string Pages_Tenant_MasterSchema_Edit = "Pages.Tenant.Commission.MasterSchema.Edit";
        public const string Pages_Tenant_MasterSchema_Delete = "Pages.Tenant.Commission.MasterSchema.Delete";
        public const string Pages_Tenant_MasterSchemaPerProject = "Pages.Tenant.Commission.MasterSchemaPerProject";
        public const string Pages_Tenant_MasterSchemaPerProject_Create = "Pages.Tenant.Commission.MasterSchemaPerProject.Create";
        public const string Pages_Tenant_MasterSchemaPerProject_Edit = "Pages.Tenant.Commission.MasterSchemaPerProject.Edit";
        public const string Pages_Tenant_MasterSchemaPerProject_Delete = "Pages.Tenant.Commission.MasterSchemaPerProject.Delete";
        public const string Pages_Tenant_MasterPPHRange = "Pages.Tenant.Commission.MasterPPHRange";
        public const string Pages_Tenant_MasterPPHRange_Create = "Pages.Tenant.Commission.MasterPPHRange.Create";
        public const string Pages_Tenant_MasterPPHRange_Edit = "Pages.Tenant.Commission.MasterPPHRange.Edit";
        public const string Pages_Tenant_MasterPPHRange_Delete = "Pages.Tenant.Commission.MasterPPHRange.Delete";
        public const string Pages_Tenant_MasterDeveloperSchema = "Pages.Tenant.Commission.MasterDeveloperSchema";
        public const string Pages_Tenant_MasterDeveloperSchema_Create = "Pages.Tenant.Commission.MasterDeveloperSchema.Create";
        public const string Pages_Tenant_MasterDeveloperSchema_Edit = "Pages.Tenant.Commission.MasterDeveloperSchema.Edit";
        public const string Pages_Tenant_MasterDeveloperSchema_Delete = "Pages.Tenant.Commission.MasterDeveloperSchema.Delete";
        public const string Pages_Tenant_MasterBobotCommission = "Pages.Tenant.Commission.MasterBobotCommission";
        public const string Pages_Tenant_MasterBobotCommission_Create = "Pages.Tenant.Commission.MasterBobotCommission.Create";
        public const string Pages_Tenant_MasterBobotCommission_Edit = "Pages.Tenant.Commission.MasterBobotCommission.Edit";
        public const string Pages_Tenant_MasterBobotCommission_Delete = "Pages.Tenant.Commission.MasterBobotCommission.Delete";
        public const string Pages_Tenant_MasterPointPercent = "Pages.Tenant.Commission.MasterPointPercent";
        public const string Pages_Tenant_MasterPointPercent_Create = "Pages.Tenant.Commission.MasterPointPercent.Create";
        public const string Pages_Tenant_MasterPointPercent_Edit = "Pages.Tenant.Commission.MasterPointPercent.Edit";
        public const string Pages_Tenant_MasterPointPercent_Delete = "Pages.Tenant.Commission.MasterPointPercent.Delete";
        public const string Pages_Tenant_MasterPPHRangeInst = "Pages.Tenant.Commission.MasterPPHRangeInst";
        public const string Pages_Tenant_MasterPPHRangeInst_Create = "Pages.Tenant.Commission.MasterPPHRangeInst.Create";
        public const string Pages_Tenant_MasterPPHRangeInst_Edit = "Pages.Tenant.Commission.MasterPPHRangeInst.Edit";
        public const string Pages_Tenant_MasterPPHRangeInst_Delete = "Pages.Tenant.Commission.MasterPPHRangeInst.Delete";
        public const string Pages_Tenant_MasterManagementFee = "Pages.Tenant.Commission.MasterManagementFee";
        public const string Pages_Tenant_MasterManagementFee_Create = "Pages.Tenant.Commission.MasterManagementFee.Create";
        public const string Pages_Tenant_MasterManagementFee_Edit = "Pages.Tenant.Commission.MasterManagementFee.Edit";
        public const string Pages_Tenant_MasterManagementFee_Delete = "Pages.Tenant.Commission.MasterManagementFee.Delete";
        public const string Pages_Tenant_MasterReward = "Pages.Tenant.Commission.MasterReward";
        public const string Pages_Tenant_MasterReward_Create = "Pages.Tenant.Commission.MasterReward.Create";
        public const string Pages_Tenant_MasterReward_Edit = "Pages.Tenant.Commission.MasterReward.Edit";
        public const string Pages_Tenant_MasterReward_Delete = "Pages.Tenant.Commission.MasterReward.Delete";
        public const string Pages_Tenant_TransactionSoldUnit = "Pages.Tenant.Commission.TransactionSoldUnit";
        public const string Pages_Tenant_TransactionSoldUnit_Create = "Pages.Tenant.Commission.TransactionSoldUnit.Create";
        public const string Pages_Tenant_TransactionSoldUnit_Edit = "Pages.Tenant.Commission.TransactionSoldUnit.Edit";
        public const string Pages_Tenant_TransactionSoldUnit_Delete = "Pages.Tenant.Commission.TransactionSoldUnit.Delete";
        public const string Pages_Tenant_EditDealCloser = "Pages.Tenant.Commission.EditDealCloser";
        public const string Pages_Tenant_EditDealCloser_Create = "Pages.Tenant.Commission.EditDealCloser.Create";
        public const string Pages_Tenant_EditDealCloser_Edit = "Pages.Tenant.Commission.EditDealCloser.Edit";
        public const string Pages_Tenant_EditDealCloser_Delete = "Pages.Tenant.Commission.EditDealCloser.Delete";
        public const string Pages_Tenant_ErrorCommissionHistory = "Pages.Tenant.Commission.ErrorCommissionHistory";
        public const string Pages_Tenant_ErrorCommissionHistory_Create = "Pages.Tenant.Commission.ErrorCommissionHistory.Create";
        public const string Pages_Tenant_ErrorCommissionHistory_Edit = "Pages.Tenant.Commission.ErrorCommissionHistory.Edit";
        public const string Pages_Tenant_ErrorCommissionHistory_Delete = "Pages.Tenant.Commission.ErrorCommissionHistory.Delete";

        /* Personal */
        public const string Pages_Tenant_Personal = "Pages.Tenant.Personal";

        public const string Pages_Tenant_Personal_LkAddrType = "Pages.Tenant.Personal.LkAddrType";
        public const string Pages_Tenant_Personal_LkBankType = "Pages.Tenant.Personal.LkBankType";
        public const string Pages_Tenant_Personal_LkBlood = "Pages.Tenant.Personal.LkBlood";
        public const string Pages_Tenant_Personal_LkCountry = "Pages.Tenant.Personal.LkCountry";
        public const string Pages_Tenant_Personal_LkFamilyStatus = "Pages.Tenant.Personal.LkFamilyStatus";
        public const string Pages_Tenant_Personal_LkGrade = "Pages.Tenant.Personal.LkGrade";
        public const string Pages_Tenant_Personal_LkIdType = "Pages.Tenant.Personal.LkIdType";
        public const string Pages_Tenant_Personal_LkKeyPeople = "Pages.Tenant.Personal.LkKeyPeople";
        public const string Pages_Tenant_Personal_LkMarStatus = "Pages.Tenant.Personal.LkMarStatus";
        public const string Pages_Tenant_Personal_LkReligion = "Pages.Tenant.Personal.LkReligion";
        public const string Pages_Tenant_Personal_LkSpec = "Pages.Tenant.Personal.LkSpec";
        public const string Pages_Tenant_Personal_MasterBank = "Pages.Tenant.Personal.MasterBank";
        public const string Pages_Tenant_Personal_MasterDocument = "Pages.Tenant.Personal.MasterDocument";
        public const string Pages_Tenant_Personal_MasterFranchiseGroup = "Pages.Tenant.Personal.MasterFranchiseGroup";
        public const string Pages_Tenant_Personal_MasterJobTitle = "Pages.Tenant.Personal.MasterJobTitle";
        public const string Pages_Tenant_Personal_MasterNational = "Pages.Tenant.Personal.MasterNational";
        public const string Pages_Tenant_Personal_MasterOccupation = "Pages.Tenant.Personal.MasterOccupation";
        public const string Pages_Tenant_Personal_PersonalMember = "Pages.Tenant.Personal.PersonalMember";
        public const string Pages_Tenant_Personal_PersonalMember_Edit = "Pages.Tenant.Personal.PersonalMember.Edit";
        public const string Pages_Tenant_Personal_PersonalMember_Delete = "Pages.Tenant.Personal.PersonalMember.Delete";
        public const string Pages_Tenant_Personal_TrAddress = "Pages.Tenant.Personal.TrAddress";
        public const string Pages_Tenant_Personal_TrAddress_Create = "Pages.Tenant.Personal.TrAddress.Create";
        public const string Pages_Tenant_Personal_TrAddress_Edit = "Pages.Tenant.Personal.TrAddress.Edit";
        public const string Pages_Tenant_Personal_TrAddress_Delete = "Pages.Tenant.Personal.TrAddress.Delete";
        public const string Pages_Tenant_Personal_TrBankAccount = "Pages.Tenant.Personal.TrBankAccount";
        public const string Pages_Tenant_Personal_TrBankAccount_Edit = "Pages.Tenant.Personal.TrBankAccount.Edit";
        public const string Pages_Tenant_Personal_TrBankAccount_Delete = "Pages.Tenant.Personal.TrBankAccount.Delete";
        public const string Pages_Tenant_Personal_TrCompany = "Pages.Tenant.Personal.TrCompany";
        public const string Pages_Tenant_Personal_TrCompany_Edit = "Pages.Tenant.Personal.TrCompany.Edit";
        public const string Pages_Tenant_Personal_TrCompany_Delete = "Pages.Tenant.Personal.TrCompany.Delete";
        public const string Pages_Tenant_Personal_TrDocument = "Pages.Tenant.Personal.TrDocument";
        public const string Pages_Tenant_Personal_TrDocument_Edit = "Pages.Tenant.Personal.TrDocument.Edit";
        public const string Pages_Tenant_Personal_TrDocument_Delete = "Pages.Tenant.Personal.TrDocument.Delete";
        public const string Pages_Tenant_Personal_TrEmail = "Pages.Tenant.Personal.TrEmail";
        public const string Pages_Tenant_Personal_TrEmail_Create = "Pages.Tenant.Personal.TrEmail.Create";
        public const string Pages_Tenant_Personal_TrEmail_Edit = "Pages.Tenant.Personal.TrEmail.Edit";
        public const string Pages_Tenant_Personal_TrEmail_Delete = "Pages.Tenant.Personal.TrEmail.Delete";
        public const string Pages_Tenant_Personal_TrFamily = "Pages.Tenant.Personal.TrFamily";
        public const string Pages_Tenant_Personal_TrFamily_Edit = "Pages.Tenant.Personal.TrFamily.Edit";
        public const string Pages_Tenant_Personal_TrFamily_Delete = "Pages.Tenant.Personal.TrFamily.Delete";
        public const string Pages_Tenant_Personal_TrId = "Pages.Tenant.Personal.TrId";
        public const string Pages_Tenant_Personal_TrId_Edit = "Pages.Tenant.Personal.TrId.Edit";
        public const string Pages_Tenant_Personal_TrId_Delete = "Pages.Tenant.Personal.TrId.Delete";
        public const string Pages_Tenant_Personal_TrKeyPeople = "Pages.Tenant.Personal.TrKeyPeople";
        public const string Pages_Tenant_Personal_TrKeyPeople_Edit = "Pages.Tenant.Personal.TrKeyPeople.Edit";
        public const string Pages_Tenant_Personal_TrKeyPeople_Delete = "Pages.Tenant.Personal.TrKeyPeople.Delete";
        public const string Pages_Tenant_Personal_TrPhone = "Pages.Tenant.Personal.TrPhone";
        public const string Pages_Tenant_Personal_TrPhone_Create = "Pages.Tenant.Personal.TrPhone.Create";
        public const string Pages_Tenant_Personal_TrPhone_Edit = "Pages.Tenant.Personal.TrPhone.Edit";
        public const string Pages_Tenant_Personal_TrPhone_Delete = "Pages.Tenant.Personal.TrPhone.Delete";

        public const string Pages_Tenant_Personal_Register = "Pages.Tenant.Personal.Register";
        public const string Pages_Tenant_Personal_Edit = "Pages.Tenant.Personal.Edit";
        public const string Pages_Tenant_Personal_Individual = "Pages.Tenant.Personal.Individual";
        public const string Pages_Tenant_Personal_Institute = "Pages.Tenant.Personal.Institute";
        public const string Pages_Tenant_Personal_Detail = "Pages.Tenant.Personal.Detail";

        /* PSAS */
        public const string Pages_Tenant_PSAS = "Pages.Tenant.PSAS";

        public const string Pages_Tenant_PSAS_Main = "Pages.Tenant.PSAS.Main";
        public const string Pages_Tenant_PSAS_Main_Create = "Pages.Tenant.PSAS.Main.Create";
        public const string Pages_Tenant_PSAS_Main_Edit = "Pages.Tenant.PSAS.Main.Edit";
        public const string Pages_Tenant_PSAS_Main_Delete = "Pages.Tenant.PSAS.Main.Delete";

        public const string Pages_Tenant_PSAS_Term = "Pages.Tenant.PSAS.Term";
        public const string Pages_Tenant_PSAS_Term_Create = "Pages.Tenant.PSAS.Term.Create";
        public const string Pages_Tenant_PSAS_Term_Edit = "Pages.Tenant.PSAS.Term.Edit";
        public const string Pages_Tenant_PSAS_Term_Delete = "Pages.Tenant.PSAS.Term.Delete";

        public const string Pages_Tenant_PSAS_Price = "Pages.Tenant.PSAS.Price";
        public const string Pages_Tenant_PSAS_Price_Create = "Pages.Tenant.PSAS.Price.Create";
        public const string Pages_Tenant_PSAS_Price_Edit = "Pages.Tenant.PSAS.Price.Edit";
        public const string Pages_Tenant_PSAS_Price_Delete = "Pages.Tenant.PSAS.Price.Delete";

        public const string Pages_Tenant_PSAS_Payment = "Pages.Tenant.PSAS.Payment";
        public const string Pages_Tenant_PSAS_Payment_Create = "Pages.Tenant.PSAS.Payment.Create";
        public const string Pages_Tenant_PSAS_Payment_Edit = "Pages.Tenant.PSAS.Payment.Edit";
        public const string Pages_Tenant_PSAS_Payment_Delete = "Pages.Tenant.PSAS.Payment.Delete";

        public const string Pages_Tenant_PSAS_OtherPayment = "Pages.Tenant.PSAS.OtherPayment";
        public const string Pages_Tenant_PSAS_OtherPayment_Create = "Pages.Tenant.PSAS.OtherPayment.Create";
        public const string Pages_Tenant_PSAS_OtherPayment_Edit = "Pages.Tenant.PSAS.OtherPayment.Edit";
        public const string Pages_Tenant_PSAS_OtherPayment_Delete = "Pages.Tenant.PSAS.OtherPayment.Delete";

                public const string Pages_Tenant_PSAS_Schedule = "Pages.Tenant.PSAS.Schedule";
                    public const string Pages_Tenant_PSAS_Schedule_Create = "Pages.Tenant.PSAS.Schedule.Create";
                    public const string Pages_Tenant_PSAS_Schedule_Edit = "Pages.Tenant.PSAS.Schedule.Edit";
                    public const string Pages_Tenant_PSAS_Schedule_Delete = "Pages.Tenant.PSAS.Schedule.Delete";
        /*Legal Document*/
        public const string Pages_Tenant_PSAS_LegalDocument = "Pages.Tenant.PSAS.LegalDocument";

            public const string Pages_Tenant_PSAS_LegalDocument_MasterKuasaDireksi = "Pages.Tenant.PSAS.LegalDocument.MasterKuasaDireksi";
            public const string Pages_Tenant_PSAS_LegalDocument_MasterKuasaDireksi_View = "Pages.Tenant.PSAS.LegalDocument.MasterKuasaDireksi.View";
            public const string Pages_Tenant_PSAS_LegalDocument_MasterKuasaDireksi_Create = "Pages.Tenant.PSAS.LegalDocument.MasterKuasaDireksi.Create";
            public const string Pages_Tenant_PSAS_LegalDocument_MasterKuasaDireksi_Delete = "Pages.Tenant.PSAS.LegalDocument.MasterKuasaDireksi.Delete";

            public const string Pages_Tenant_PSAS_LegalDocument_MasterTemplate = "Pages.Tenant.PSAS.LegalDocument.MasterTemplate";
            public const string Pages_Tenant_PSAS_LegalDocument_MasterTemplate_View = "Pages.Tenant.PSAS.LegalDocument.MasterTemplate.View";

            public const string Pages_Tenant_PSAS_LegalDocument_MappingTemplate = "Pages.Tenant.PSAS.LegalDocument.MappingTemplate";
            public const string Pages_Tenant_PSAS_LegalDocument_MappingTemplate_View = "Pages.Tenant.PSAS.LegalDocument.MappingTemplate.View";
            public const string Pages_Tenant_PSAS_LegalDocument_MappingTemplate_Create = "Pages.Tenant.PSAS.LegalDocument.MappingTemplate.Create";
            public const string Pages_Tenant_PSAS_LegalDocument_MappingTemplate_Edit = "Pages.Tenant.PSAS.LegalDocument.MappingTemplate.Edit";
            public const string Pages_Tenant_PSAS_LegalDocument_MappingTemplate_Delete = "Pages.Tenant.PSAS.LegalDocument.MappingTemplate.Delete";

            public const string Pages_Tenant_PSAS_LegalDocument_BookingDocument = "Pages.Tenant.PSAS.LegalDocument.BookingDocument";
            public const string Pages_Tenant_PSAS_LegalDocument_BookingDocument_View = "Pages.Tenant.PSAS.LegalDocument.BookingDocument.View";
            public const string Pages_Tenant_PSAS_LegalDocument_BookingDocument_Create = "Pages.Tenant.PSAS.LegalDocument.BookingDocument.Create";

        /* Payment */
        public const string Pages_Tenant_Payment = "Pages.Tenant.Payment";

        public const string Pages_Tenant_Payment_MasterPayFor = "Pages.Tenant.Payment.MasterPayFor";
            public const string Pages_Tenant_Payment_MasterPayFor_Create = "Pages.Tenant.Payment.MasterPayFor.Create";
            public const string Pages_Tenant_Payment_MasterPayFor_Edit = "Pages.Tenant.Payment.MasterPayFor.Edit";
            public const string Pages_Tenant_Payment_MasterPayFor_Delete = "Pages.Tenant.Payment.MasterPayFor.Delete";

        public const string Pages_Tenant_Payment_MasterPayType = "Pages.Tenant.Payment.MasterPayType";
            public const string Pages_Tenant_Payment_MasterPayType_Create = "Pages.Tenant.Payment.MasterPayType.Create";
            public const string Pages_Tenant_Payment_MasterPayType_Edit = "Pages.Tenant.Payment.MasterPayType.Edit";
            public const string Pages_Tenant_Payment_MasterPayType_Delete = "Pages.Tenant.Payment.MasterPayType.Delete";

        public const string Pages_Tenant_Payment_MasterOthersType = "Pages.Tenant.Payment.MasterOthersType";
            public const string Pages_Tenant_Payment_MasterOthersType_Create = "Pages.Tenant.Payment.MasterOthersType.Create";
            public const string Pages_Tenant_Payment_MasterOthersType_Edit = "Pages.Tenant.Payment.MasterOthersType.Edit";
            public const string Pages_Tenant_Payment_MasterOthersType_Delete = "Pages.Tenant.Payment.MasterOthersType.Delete";

        public const string Pages_Tenant_Payment_MasterAllocation = "Pages.Tenant.Payment.MasterAllocation";
            public const string Pages_Tenant_Payment_MasterAllocation_Create = "Pages.Tenant.Payment.MasterAllocation.Create";
            public const string Pages_Tenant_Payment_MasterAllocation_Edit = "Pages.Tenant.Payment.MasterAllocation.Edit";
            public const string Pages_Tenant_Payment_MasterAllocation_Delete = "Pages.Tenant.Payment.MasterAllocation.Delete";

        public const string Pages_Tenant_Payment_MasterAccount = "Pages.Tenant.Payment.MasterAccount";
            public const string Pages_Tenant_Payment_MasterAccount_Edit = "Pages.Tenant.Payment.MasterAccount.Edit";
            public const string Pages_Tenant_Payment_MasterAccount_Delete = "Pages.Tenant.Payment.MasterAccount.Delete";

        public const string Pages_Tenant_Payment_Transaction_SinglePayment = "Pages.Tenant.Payment.Transaction.SinglePayment";
        public const string Pages_Tenant_Payment_Transaction_BulkPayment = "Pages.Tenant.Payment.Transaction.BulkPayment";

        /* Customer Member Online Booking */
        public const string Pages_Tenant_OnlineBooking = "Pages.Tenant.OnlineBooking";

        public const string Pages_Tenant_OnlineBooking_BookingHistory = "Pages.Tenant.OnlineBooking.BookingHistory";
        public const string Pages_Tenant_OnlineBooking_BookingHistory_GetDetailBookingHistory = "Pages.Tenant.OnlineBooking.BookingHistory.GetDetailBookingHistory";
        public const string Pages_Tenant_OnlineBooking_BookingHistory_GetListBookingHistory = "Pages.Tenant.OnlineBooking.BookingHistory.GetListBookingHistory";
        public const string Pages_Tenant_OnlineBooking_BookingHistory_SearchingBookingHistoryMobile = "Pages.Tenant.OnlineBooking.BookingHistory.SearchingBookingHistoryMobile";

        public const string Pages_Tenant_OnlineBooking_CustomerMember = "Pages.Tenant.OnlineBooking.CustomerMember";
        public const string Pages_Tenant_OnlineBooking_CustomerMember_MemberActivation = "Pages.Tenant.CustomerMember.MemberActivation";
        public const string Pages_Tenant_OnlineBooking_CustomerMember_SignUpCustomer = "Pages.Tenant.CustomerMember.SignUpCustomer";
        public const string Pages_Tenant_OnlineBooking_CustomerMember_GetListCustomer = "Pages.Tenant.CustomerMember.GetListCustomer";
        public const string Pages_Tenant_OnlineBooking_CustomerMember_GetNation = "Pages.Tenant.CustomerMember.GetNation";
        public const string Pages_Tenant_OnlineBooking_CustomerMember_GetCountry = "Pages.Tenant.CustomerMember.GetCountry";
        public const string Pages_Tenant_OnlineBooking_CustomerMember_GetCity = "Pages.Tenant.CustomerMember.GetCity";
        public const string Pages_Tenant_OnlineBooking_CustomerMember_GetPostCode = "Pages.Tenant.CustomerMember.GetPostCode";
        public const string Pages_Tenant_OnlineBooking_CustomerMember_GetDetailCustomer = "Pages.Tenant.CustomerMember.GetDetailCustomer";
        public const string Pages_Tenant_OnlineBooking_CustomerMember_UpdateCustomer = "Pages.Tenant.CustomerMember.UpdateCustomer";
        public const string Pages_Tenant_OnlineBooking_CustomerMember_GetDocumentByPsCode = "Pages.Tenant.CustomerMember.GetDocumentByPsCode";
        public const string Pages_Tenant_OnlineBooking_CustomerMember_GetDetailCustomerMobile = "Pages.Tenant.CustomerMember.GetDetailCustomerMobile";
        public const string Pages_Tenant_OnlineBooking_CustomerMember_GetProvince = "Pages.Tenant.CustomerMember.GetProvince";

        public const string Pages_Tenant_OnlineBooking_Diagramatic = "Pages.Tenant.OnlineBooking.Diagramatic";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_GetDetailUnitByProject = "Pages.Tenant.OnlineBooking.Diagramatic.GetDetailUnitByProject";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_GetUnitDetailWithTower = "Pages.Tenant.OnlineBooking.Diagramatic.GetUnitDetailWithTower";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_ListPrice = "Pages.Tenant.OnlineBooking.Diagramatic.ListPrice";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_GetListUnitByUnitCode = "Pages.Tenant.OnlineBooking.Diagramatic.GetListUnitByUnitCode";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_GetListTowerByProjectID = "Pages.Tenant.OnlineBooking.Diagramatic.GetListTowerByProjectID";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_GetListDiagramatic = "Pages.Tenant.OnlineBooking.Diagramatic.GetListDiagramatic";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_GetListRenovation = "Pages.Tenant.OnlineBooking.Diagramatic.GetListRenovation";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_GetListTerm = "Pages.Tenant.OnlineBooking.Diagramatic.GetListTerm";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_GetGrossPrice = "Pages.Tenant.OnlineBooking.Diagramatic.GetGrossPrice";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_GetListDiagramaticWeb = "Pages.Tenant.OnlineBooking.Diagramatic.GetListDiagramaticWeb";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_GetUnitSelectionDetail = "Pages.Tenant.OnlineBooking.Diagramatic.GetUnitSelectionDetail";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_GetUnitSelectionDetailMobile = "Pages.Tenant.OnlineBooking.Diagramatic.GetUnitSelectionDetailMobile";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_GetTypeDiagramatic = "Pages.Tenant.OnlineBooking.Diagramatic.GetTypeDiagramatic";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_GetListBedroom = "Pages.Tenant.OnlineBooking.Diagramatic.GetListBedroom";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_GetListZoning = "Pages.Tenant.OnlineBooking.Diagramatic.GetListZoning";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_GetListSumberDana = "Pages.Tenant.OnlineBooking.Diagramatic.GetListSumberDana";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_GetListTujuanTransaksi = "Pages.Tenant.OnlineBooking.Diagramatic.GetListTujuanTransaksi";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_GetDetailDiagramatic = "Pages.Tenant.OnlineBooking.Diagramatic.GetDetailDiagramatic";
        public const string Pages_Tenant_OnlineBooking_Diagramatic_GetPaymentType = "Pages.Tenant.OnlineBooking.Diagramatic.GetPaymentType";

        public const string Pages_Tenant_OnlineBooking_Project = "Pages.Tenant.OnlineBooking.Project";
        public const string Pages_Tenant_OnlineBooking_Project_GetDetailListProject = "Pages.Tenant.Project.GetDetailListProject";
        public const string Pages_Tenant_OnlineBooking_Project_GetListProject = "Pages.Tenant.Project.GetListProject";
        public const string Pages_Tenant_OnlineBooking_Project_GetListProjectByName = "Pages.Tenant.OnlineBooking.Project.GetListProjectByName";
        public const string Pages_Tenant_OnlineBooking_Project_GetListProjectImageGallery = "Pages.Tenant.OnlineBooking.Project.GetListProjectImageGallery";
        public const string Pages_Tenant_OnlineBooking_Project_GetListProjectInfo = "Pages.Tenant.OnlineBooking.Project.GetListProjectInfo";
        public const string Pages_Tenant_OnlineBooking_Project_GetListProjectKeyFeatures = "Pages.Tenant.OnlineBooking.Project.GetListProjectKeyFeatures";
        public const string Pages_Tenant_OnlineBooking_Project_GetListProjectLocation = "Pages.Tenant.OnlineBooking.Project.GetListProjectLocation";
        public const string Pages_Tenant_OnlineBooking_Project_GetUnitTypeByCluster = "Pages.Tenant.OnlineBooking.Project.GetUnitTypeByCluster";
        public const string Pages_Tenant_OnlineBooking_Project_GetUnitTypeByProjectId = "Pages.Tenant.OnlineBooking.Project.GetUnitTypeByProjectId";
        public const string Pages_Tenant_OnlineBooking_Project_GetListPromotion = "Pages.Tenant.OnlineBooking.Project.GetListPromotion";


        public const string Pages_Tenant_OnlineBooking_Transaction = "Pages.Tenant.OnlineBooking.Transaction";
        public const string Pages_Tenant_OnlineBooking_Transaction_InsertTransactionUniversal = "Pages.Tenant.OnlineBooking.Transaction.InsertTransactionUniversal";
        public const string Pages_Tenant_OnlineBooking_Transaction_InsertTrUnitReserved = "Pages.Tenant.OnlineBooking.Transaction.InsertTrUnitReserved";
        public const string Pages_Tenant_OnlineBooking_Transaction_UpdatePsCodeTrUnitReserved = "Pages.Tenant.OnlineBooking.Transaction.UpdatePsCodeTrUnitReserved";
        public const string Pages_Tenant_OnlineBooking_Transaction_InsertTrUnitOrderHeader = "Pages.Tenant.OnlineBooking.Transaction.InsertTrUnitOrderHeader";
        public const string Pages_Tenant_OnlineBooking_Transaction_InsertTrUnitOrderDetail = "Pages.Tenant.OnlineBooking.Transaction.InsertTrUnitOrderDetail";
        public const string Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingHeader = "Pages.Tenant.OnlineBooking.Transaction.InsertTrBookingHeader";
        public const string Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingDetail = "Pages.Tenant.OnlineBooking.Transaction.InsertTrBookingDetail";
        public const string Pages_Tenant_OnlineBooking_Transaction_InsertTrPaymentOnlineBook = "Pages.Tenant.OnlineBooking.Transaction.InsertTrPaymentOnlineBook";
        public const string Pages_Tenant_OnlineBooking_Transaction_DeleteTrUnitReserved = "Pages.Tenant.OnlineBooking.Transaction.DeleteTrUnitReserved";
        public const string Pages_Tenant_OnlineBooking_Transaction_InsertAddDisc = "Pages.Tenant.OnlineBooking.Transaction.InsertAddDisc";
        public const string Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingDetailDP = "Pages.Tenant.OnlineBooking.Transaction.InsertTrBookingDetailDP";
        public const string Pages_Tenant_OnlineBooking_Transaction_InsertTrCashAddDisc = "Pages.Tenant.OnlineBooking.Transaction.InsertTrCashAddDisc";
        public const string Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingSalesAddDisc = "Pages.Tenant.OnlineBooking.Transaction.InsertTrBookingSalesAddDisc";
        public const string Pages_Tenant_OnlineBooking_Transaction_GetDetailBookingUnit = "Pages.Tenant.OnlineBooking.Transaction.GetDetailBookingUnit";
        public const string Pages_Tenant_OnlineBooking_Transaction_UpdateBFAmount = "Pages.Tenant.OnlineBooking.Transaction.UpdateBFAmount";
        public const string Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingHeaderTerm = "Pages.Tenant.OnlineBooking.Transaction.InsertTrBookingHeaderTerm";
        public const string Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingItemPrice = "Pages.Tenant.OnlineBooking.Transaction.InsertTrBookingItemPrice";
        public const string Pages_Tenant_OnlineBooking_Transaction_UpdateUnitSold = "Pages.Tenant.OnlineBooking.Transaction.UpdateUnitSold";
        public const string Pages_Tenant_OnlineBooking_Transaction_UpdateRemarksTrBookingHeader = "Pages.Tenant.OnlineBooking.Transaction.UpdateRemarksTrBookingHeader";
        public const string Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingTax = "Pages.Tenant.OnlineBooking.Transaction.InsertTrBookingTax";
        public const string Pages_Tenant_OnlineBooking_Transaction_UpdateOrderStatusFullyPaid = "Pages.Tenant.OnlineBooking.Transaction.UpdateOrderStatusFullyPaid";
        public const string Pages_Tenant_OnlineBooking_Transaction_UpdateReleaseDate = "Pages.Tenant.OnlineBooking.Transaction.UpdateReleaseDate";
        public const string Pages_Tenant_OnlineBooking_Transaction_InsertTrSoldUnit = "Pages.Tenant.OnlineBooking.Transaction.InsertTrSoldUnit";
        public const string Pages_Tenant_OnlineBooking_Transaction_InsertTrSoldUnitRequirement = "Pages.Tenant.OnlineBooking.Transaction.InsertTrSoldUnitRequirement";
        public const string Pages_Tenant_OnlineBooking_Transaction_GetTrUnitReserved = "Pages.Tenant.OnlineBooking.Transaction.GetTrUnitReserved";
        public const string Pages_Tenant_OnlineBooking_Transaction_UpdateNetPriceBookingHeaderDetail = "Pages.Tenant.OnlineBooking.Transaction.UpdateNetPriceBookingHeaderDetail";
        public const string Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingDetailSchedule = "Pages.Tenant.OnlineBooking.Transaction.InsertTrBookingDetailSchedule";
        public const string Pages_Tenant_OnlineBooking_Transaction_DoBookingMidransReq = "Pages.Tenant.OnlineBooking.Transaction.DoBookingMidransReq";
        public const string Pages_Tenant_OnlineBooking_Transaction_DoBooking = "Pages.Tenant.OnlineBooking.Transaction.DoBooking";












    }
}