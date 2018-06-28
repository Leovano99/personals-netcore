using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace VDI.Demo.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));
            pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            /* MasterPlan */
            var masterPlan = pages.CreateChildPermission(AppPermissions.Pages_Tenant_MasterPlan, L("MasterPlan"), multiTenancySides: MultiTenancySides.Tenant);

            /* Territory */
            var territory = masterPlan.CreateChildPermission(AppPermissions.Pages_Tenant_Territory, L("Territory"), multiTenancySides: MultiTenancySides.Tenant);

            var subTerritory = territory.CreateChildPermission(AppPermissions.Pages_Tenant_MasterTerritory, L("MasterTerritory"), multiTenancySides: MultiTenancySides.Tenant);
            subTerritory.CreateChildPermission(AppPermissions.Pages_Tenant_MasterTerritory_Create, L("CreateMasterTerritory"), multiTenancySides: MultiTenancySides.Tenant);
            subTerritory.CreateChildPermission(AppPermissions.Pages_Tenant_MasterTerritory_Edit, L("EditMasterTerritory"), multiTenancySides: MultiTenancySides.Tenant);
            subTerritory.CreateChildPermission(AppPermissions.Pages_Tenant_MasterTerritory_Delete, L("DeleteMasterTerritory"), multiTenancySides: MultiTenancySides.Tenant);

            var subCounty = territory.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCounty, L("MasterCounty"), multiTenancySides: MultiTenancySides.Tenant);
            var subCity = territory.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCity, L("MasterCity"), multiTenancySides: MultiTenancySides.Tenant);
            var subRegion = territory.CreateChildPermission(AppPermissions.Pages_Tenant_MasterRegion, L("MasterRegion"), multiTenancySides: MultiTenancySides.Tenant);

            /* Project */
            var project = masterPlan.CreateChildPermission(AppPermissions.Pages_Tenant_Project, L("Project"), multiTenancySides: MultiTenancySides.Tenant);
            var subPosition = project.CreateChildPermission(AppPermissions.Pages_Tenant_MasterPosition, L("MasterPosition"), multiTenancySides: MultiTenancySides.Tenant);
            var subOfficer = project.CreateChildPermission(AppPermissions.Pages_Tenant_MasterOfficer, L("MasterOfficer"), multiTenancySides: MultiTenancySides.Tenant);

            var subAccount = project.CreateChildPermission(AppPermissions.Pages_Tenant_MasterAccount, L("MasterAccount"), multiTenancySides: MultiTenancySides.Tenant);
            subAccount.CreateChildPermission(AppPermissions.Pages_Tenant_MasterAccount_Create, L("AddMasterAccount"), multiTenancySides: MultiTenancySides.Tenant);
            subAccount.CreateChildPermission(AppPermissions.Pages_Tenant_MasterAccount_Edit, L("EditMasterAccount"), multiTenancySides: MultiTenancySides.Tenant);
            subAccount.CreateChildPermission(AppPermissions.Pages_Tenant_MasterAccount_Delete, L("DeleteMasterAccount"), multiTenancySides: MultiTenancySides.Tenant);
            subAccount.CreateChildPermission(AppPermissions.Pages_Tenant_MasterAccount_Detail, L("DetailMasterAccount"), multiTenancySides: MultiTenancySides.Tenant);

            var subBank = project.CreateChildPermission(AppPermissions.Pages_Tenant_MasterBank, L("MasterBank"), multiTenancySides: MultiTenancySides.Tenant);
            subBank.CreateChildPermission(AppPermissions.Pages_Tenant_MasterBank_Create, L("AddMasterBank"), multiTenancySides: MultiTenancySides.Tenant);
            subBank.CreateChildPermission(AppPermissions.Pages_Tenant_MasterBank_Edit, L("EditMasterBank"), multiTenancySides: MultiTenancySides.Tenant);
            subBank.CreateChildPermission(AppPermissions.Pages_Tenant_MasterBank_Delete, L("DeleteMasterBank"), multiTenancySides: MultiTenancySides.Tenant);

            var subBankBranch = project.CreateChildPermission(AppPermissions.Pages_Tenant_MasterBankBranch, L("MasterBankBranch"), multiTenancySides: MultiTenancySides.Tenant);
            subBankBranch.CreateChildPermission(AppPermissions.Pages_Tenant_MasterBankBranch_Create, L("AddMasterBankBranch"), multiTenancySides: MultiTenancySides.Tenant);
            subBankBranch.CreateChildPermission(AppPermissions.Pages_Tenant_MasterBankBranch_Edit, L("EditMasterBankBranch"), multiTenancySides: MultiTenancySides.Tenant);
            subBankBranch.CreateChildPermission(AppPermissions.Pages_Tenant_MasterBankBranch_Delete, L("DeleteMasterBankBranch"), multiTenancySides: MultiTenancySides.Tenant);
            subBankBranch.CreateChildPermission(AppPermissions.Pages_Tenant_MasterBankBranch_Detail, L("DetailMasterBankBranch"), multiTenancySides: MultiTenancySides.Tenant);

            var subDepartment = project.CreateChildPermission(AppPermissions.Pages_Tenant_MasterDepartment, L("MasterDepartment"), multiTenancySides: MultiTenancySides.Tenant);
            subDepartment.CreateChildPermission(AppPermissions.Pages_Tenant_MasterDepartment_Create, L("AddMasterDepartment"), multiTenancySides: MultiTenancySides.Tenant);
            subDepartment.CreateChildPermission(AppPermissions.Pages_Tenant_MasterDepartment_Edit, L("EditMasterDepartment"), multiTenancySides: MultiTenancySides.Tenant);
            subDepartment.CreateChildPermission(AppPermissions.Pages_Tenant_MasterDepartment_Delete, L("DeleteMasterDepartment"), multiTenancySides: MultiTenancySides.Tenant);

            var subEntity = project.CreateChildPermission(AppPermissions.Pages_Tenant_MasterEntity, L("MasterEntity"), multiTenancySides: MultiTenancySides.Tenant);
            subEntity.CreateChildPermission(AppPermissions.Pages_Tenant_MasterEntity_Create, L("AddMasterEntity"), multiTenancySides: MultiTenancySides.Tenant);
            subEntity.CreateChildPermission(AppPermissions.Pages_Tenant_MasterEntity_Edit, L("EditMasterEntity"), multiTenancySides: MultiTenancySides.Tenant);
            subEntity.CreateChildPermission(AppPermissions.Pages_Tenant_MasterEntity_Delete, L("DeleteMasterEntity"), multiTenancySides: MultiTenancySides.Tenant);

            /*connection string*/
            var connectionString = project.CreateChildPermission(AppPermissions.Pages_Tenant_SettingConnString, L("SettingConnString"), multiTenancySides: MultiTenancySides.Tenant);
            connectionString.CreateChildPermission(AppPermissions.Pages_Tenant_SettingConnStringDMT, L("DMT"), multiTenancySides: MultiTenancySides.Tenant);
            connectionString.CreateChildPermission(AppPermissions.Pages_Tenant_SettingConnStringCorsec, L("Corsec"), multiTenancySides: MultiTenancySides.Tenant);

            var subCompany = project.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCompany, L("MasterCompany"), multiTenancySides: MultiTenancySides.Tenant);
            subCompany.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCompany_Create, L("AddMasterCompany"), multiTenancySides: MultiTenancySides.Tenant);
            subCompany.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCompany_Edit, L("EditMasterCompany"), multiTenancySides: MultiTenancySides.Tenant);
            subCompany.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCompany_Detail, L("DetailMasterCompany"), multiTenancySides: MultiTenancySides.Tenant);
            subCompany.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCompany_Delete, L("DeleteMasterCompany"), multiTenancySides: MultiTenancySides.Tenant);

            //var subOfficer = project.CreateChildPermission(AppPermissions.Pages_Tenant_MasterOfficer, L("MasterOfficer"), multiTenancySides: MultiTenancySides.Tenant);
            subOfficer.CreateChildPermission(AppPermissions.Pages_Tenant_MasterOfficer_Create, L("AddMasterOfficer"), multiTenancySides: MultiTenancySides.Tenant);
            subOfficer.CreateChildPermission(AppPermissions.Pages_Tenant_MasterOfficer_Edit, L("EditMasterOfficer"), multiTenancySides: MultiTenancySides.Tenant);
            subOfficer.CreateChildPermission(AppPermissions.Pages_Tenant_MasterOfficer_Delete, L("DeleteMasterOfficer"), multiTenancySides: MultiTenancySides.Tenant);
            subOfficer.CreateChildPermission(AppPermissions.Pages_Tenant_MasterOfficer_Detail, L("DetailMasterOfficer"), multiTenancySides: MultiTenancySides.Tenant);

            //var subPosition = project.CreateChildPermission(AppPermissions.Pages_Tenant_MasterPosition, L("MasterPosition"), multiTenancySides: MultiTenancySides.Tenant);
            subPosition.CreateChildPermission(AppPermissions.Pages_Tenant_MasterPosition_Create, L("AddMasterPosition"), multiTenancySides: MultiTenancySides.Tenant);
            subPosition.CreateChildPermission(AppPermissions.Pages_Tenant_MasterPosition_Edit, L("EditMasterPosition"), multiTenancySides: MultiTenancySides.Tenant);
            subPosition.CreateChildPermission(AppPermissions.Pages_Tenant_MasterPosition_Delete, L("DeleteMasterPosition"), multiTenancySides: MultiTenancySides.Tenant);

            subCity.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCity_Create, L("AddMasterCity"), multiTenancySides: MultiTenancySides.Tenant);
            subCity.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCity_Edit, L("EditMasterCity"), multiTenancySides: MultiTenancySides.Tenant);
            subCity.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCity_Delete, L("DeleteMasterCity"), multiTenancySides: MultiTenancySides.Tenant);

            subCounty.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCounty_Create, L("AddMasterCounty"), multiTenancySides: MultiTenancySides.Tenant);
            subCounty.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCounty_Edit, L("EditMasterCounty"), multiTenancySides: MultiTenancySides.Tenant);
            subCounty.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCounty_Delete, L("DeleteMasterCounty"), multiTenancySides: MultiTenancySides.Tenant);

            subRegion.CreateChildPermission(AppPermissions.Pages_Tenant_MasterRegion_Create, L("AddMasterRegion"), multiTenancySides: MultiTenancySides.Tenant);
            subRegion.CreateChildPermission(AppPermissions.Pages_Tenant_MasterRegion_Edit, L("EditMasterRegion"), multiTenancySides: MultiTenancySides.Tenant);
            subRegion.CreateChildPermission(AppPermissions.Pages_Tenant_MasterRegion_Delete, L("DeleteMasterRegion"), multiTenancySides: MultiTenancySides.Tenant);

            var subCategory = project.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCategory, L("MasterCategory"), multiTenancySides: MultiTenancySides.Tenant);
            subCategory.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCategory_Create, L("AddMasterCategory"), multiTenancySides: MultiTenancySides.Tenant);
            subCategory.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCategory_Edit, L("EditMasterCategory"), multiTenancySides: MultiTenancySides.Tenant);
            subCategory.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCategory_Delete, L("DeleteMasterCategory"), multiTenancySides: MultiTenancySides.Tenant);

            var subProduct = project.CreateChildPermission(AppPermissions.Pages_Tenant_MasterProduct, L("MasterProduct"), multiTenancySides: MultiTenancySides.Tenant);
            subProduct.CreateChildPermission(AppPermissions.Pages_Tenant_MasterProduct_Create, L("CreateMasterProduct"), multiTenancySides: MultiTenancySides.Tenant);
            subProduct.CreateChildPermission(AppPermissions.Pages_Tenant_MasterProduct_Edit, L("EditMasterProduct"), multiTenancySides: MultiTenancySides.Tenant);
            subProduct.CreateChildPermission(AppPermissions.Pages_Tenant_MasterProduct_Delete, L("DeleteMasterProduct"), multiTenancySides: MultiTenancySides.Tenant);

            var subProject = project.CreateChildPermission(AppPermissions.Pages_Tenant_MasterProject, L("MasterProject"), multiTenancySides: MultiTenancySides.Tenant);
            subProject.CreateChildPermission(AppPermissions.Pages_Tenant_MasterProject_Create, L("AddMasterProject"), multiTenancySides: MultiTenancySides.Tenant);
            subProject.CreateChildPermission(AppPermissions.Pages_Tenant_MasterProject_Edit, L("EditMasterProject"), multiTenancySides: MultiTenancySides.Tenant);
            subProject.CreateChildPermission(AppPermissions.Pages_Tenant_MasterProject_Detail, L("DetailMasterProject"), multiTenancySides: MultiTenancySides.Tenant);
            subProject.CreateChildPermission(AppPermissions.Pages_Tenant_MasterProject_Delete, L("DeleteMasterProject"), multiTenancySides: MultiTenancySides.Tenant);

            /* Unit */
            var unit = masterPlan.CreateChildPermission(AppPermissions.Pages_Tenant_Unit, L("Unit"), multiTenancySides: MultiTenancySides.Tenant);

            var subItem = unit.CreateChildPermission(AppPermissions.Pages_Tenant_Item, L("MasterItem"), multiTenancySides: MultiTenancySides.Tenant);
            subItem.CreateChildPermission(AppPermissions.Pages_Tenant_Item_Create, L("CreateMasterItem"), multiTenancySides: MultiTenancySides.Tenant);
            subItem.CreateChildPermission(AppPermissions.Pages_Tenant_Item_Edit, L("EditMasterItem"), multiTenancySides: MultiTenancySides.Tenant);
            subItem.CreateChildPermission(AppPermissions.Pages_Tenant_Item_Delete, L("DeleteMasterItem"), multiTenancySides: MultiTenancySides.Tenant);

            var subArea = unit.CreateChildPermission(AppPermissions.Pages_Tenant_MasterArea, L("MasterArea"), multiTenancySides: MultiTenancySides.Tenant);
            subArea.CreateChildPermission(AppPermissions.Pages_Tenant_MasterArea_Create, L("CreateMasterArea"), multiTenancySides: MultiTenancySides.Tenant);
            subArea.CreateChildPermission(AppPermissions.Pages_Tenant_MasterArea_Edit, L("EditMasterArea"), multiTenancySides: MultiTenancySides.Tenant);
            subArea.CreateChildPermission(AppPermissions.Pages_Tenant_MasterArea_Delete, L("DeleteMasterArea"), multiTenancySides: MultiTenancySides.Tenant);
            unit.CreateChildPermission(AppPermissions.Pages_Tenant_ManageStatus, L("ManageStatus"), multiTenancySides: MultiTenancySides.Tenant);

            unit.CreateChildPermission(AppPermissions.Pages_Tenant_MasterUnitStatus, L("MasterUnitStatus"), multiTenancySides: MultiTenancySides.Tenant);

            unit.CreateChildPermission(AppPermissions.Pages_Tenant_MasterFacing, L("MasterFacing"), multiTenancySides: MultiTenancySides.Tenant);

            var subCluster = unit.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCluster, L("MasterCluster"), multiTenancySides: MultiTenancySides.Tenant);
            subCluster.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCluster_Create, L("CreateMasterCluster"), multiTenancySides: MultiTenancySides.Tenant);
            subCluster.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCluster_Edit, L("EditMasterCluster"), multiTenancySides: MultiTenancySides.Tenant);
            subCluster.CreateChildPermission(AppPermissions.Pages_Tenant_MasterCluster_Delete, L("DeleteMasterCluster"), multiTenancySides: MultiTenancySides.Tenant);

            var subUnitCode = unit.CreateChildPermission(AppPermissions.Pages_Tenant_MasterUnitCode, L("MasterUnitCode"), multiTenancySides: MultiTenancySides.Tenant);
            subUnitCode.CreateChildPermission(AppPermissions.Pages_Tenant_MasterUnitCode_Create, L("CreateMasterUnitCode"), multiTenancySides: MultiTenancySides.Tenant);
            subUnitCode.CreateChildPermission(AppPermissions.Pages_Tenant_MasterUnitCode_Edit, L("EditMasterUnitCode"), multiTenancySides: MultiTenancySides.Tenant);
            subUnitCode.CreateChildPermission(AppPermissions.Pages_Tenant_MasterUnitCode_Delete, L("DeleteMasterUnitCode"), multiTenancySides: MultiTenancySides.Tenant);

            unit.CreateChildPermission(AppPermissions.Pages_Tenant_MappingItem, L("MappingItem"), multiTenancySides: MultiTenancySides.Tenant);

            var subZoning = unit.CreateChildPermission(AppPermissions.Pages_Tenant_MasterZoning, L("MasterZoning"), multiTenancySides: MultiTenancySides.Tenant);
            subZoning.CreateChildPermission(AppPermissions.Pages_Tenant_MasterZoning_Create, L("CreateMasterZoning"), multiTenancySides: MultiTenancySides.Tenant);
            subZoning.CreateChildPermission(AppPermissions.Pages_Tenant_MasterZoning_Edit, L("EditMasterZoning"), multiTenancySides: MultiTenancySides.Tenant);
            subZoning.CreateChildPermission(AppPermissions.Pages_Tenant_MasterZoning_Delete, L("DeleteMasterZoning"), multiTenancySides: MultiTenancySides.Tenant);

            var subFacade = unit.CreateChildPermission(AppPermissions.Pages_Tenant_MasterFacade, L("MasterFacade"), multiTenancySides: MultiTenancySides.Tenant);
            subFacade.CreateChildPermission(AppPermissions.Pages_Tenant_MasterFacade_Create, L("CreateMasterFacade"), multiTenancySides: MultiTenancySides.Tenant);
            subFacade.CreateChildPermission(AppPermissions.Pages_Tenant_MasterFacade_Edit, L("EditMasterFacade"), multiTenancySides: MultiTenancySides.Tenant);
            subFacade.CreateChildPermission(AppPermissions.Pages_Tenant_MasterFacade_Delete, L("DeleteMasterFacade"), multiTenancySides: MultiTenancySides.Tenant);

            var subColor = unit.CreateChildPermission(AppPermissions.Pages_Tenant_MasterColor, L("MasterColor"), multiTenancySides: MultiTenancySides.Tenant);
            subColor.CreateChildPermission(AppPermissions.Pages_Tenant_MasterColor_Create, L("CreateMasterColor"), multiTenancySides: MultiTenancySides.Tenant);
            subColor.CreateChildPermission(AppPermissions.Pages_Tenant_MasterColor_Edit, L("EditMasterColor"), multiTenancySides: MultiTenancySides.Tenant);
            subColor.CreateChildPermission(AppPermissions.Pages_Tenant_MasterColor_Delete, L("DeleteMasterColor"), multiTenancySides: MultiTenancySides.Tenant);

            var subRenovation = unit.CreateChildPermission(AppPermissions.Pages_Tenant_MasterRenovation, L("MasterRenovation"), multiTenancySides: MultiTenancySides.Tenant);
            subRenovation.CreateChildPermission(AppPermissions.Pages_Tenant_MasterRenovation_Create, L("CreateMasterRenovation"), multiTenancySides: MultiTenancySides.Tenant);
            subRenovation.CreateChildPermission(AppPermissions.Pages_Tenant_MasterRenovation_Edit, L("EditMasterRenovation"), multiTenancySides: MultiTenancySides.Tenant);
            subRenovation.CreateChildPermission(AppPermissions.Pages_Tenant_MasterRenovation_Delete, L("DeleteMasterRenovation"), multiTenancySides: MultiTenancySides.Tenant);

            var subDetail = unit.CreateChildPermission(AppPermissions.Pages_Tenant_MasterDetail, L("MasterDetail"), multiTenancySides: MultiTenancySides.Tenant);
            subDetail.CreateChildPermission(AppPermissions.Pages_Tenant_MasterDetail_Create, L("CreateMasterDetail"), multiTenancySides: MultiTenancySides.Tenant);
            subDetail.CreateChildPermission(AppPermissions.Pages_Tenant_MasterDetail_Edit, L("EditMasterDetail"), multiTenancySides: MultiTenancySides.Tenant);
            subDetail.CreateChildPermission(AppPermissions.Pages_Tenant_MasterDetail_Delete, L("DeleteMasterDetail"), multiTenancySides: MultiTenancySides.Tenant);

            unit.CreateChildPermission(AppPermissions.Pages_Tenant_MasterTowerType, L("MasterTowerType"), multiTenancySides: MultiTenancySides.Tenant);

            var genUnit = unit.CreateChildPermission(AppPermissions.Pages_Tenant_GenerateUnit, L("GenerateUnit"), multiTenancySides: MultiTenancySides.Tenant);
            genUnit.CreateChildPermission(AppPermissions.Pages_Tenant_GenerateUnit_BySystem, L("GenerateUnitBySystem"), multiTenancySides: MultiTenancySides.Tenant);
            genUnit.CreateChildPermission(AppPermissions.Pages_Tenant_GenerateUnit_BySystem_DefineProject, L("DefineProject"), multiTenancySides: MultiTenancySides.Tenant);
            genUnit.CreateChildPermission(AppPermissions.Pages_Tenant_GenerateUnit_BySystem_SetFloor, L("SetFloor"), multiTenancySides: MultiTenancySides.Tenant);
            genUnit.CreateChildPermission(AppPermissions.Pages_Tenant_GenerateUnit_BySystem_SetUnit, L("SetUnit"), multiTenancySides: MultiTenancySides.Tenant);
            genUnit.CreateChildPermission(AppPermissions.Pages_Tenant_GenerateUnit_BySystem_SetCluster, L("SetCluster"), multiTenancySides: MultiTenancySides.Tenant);
            genUnit.CreateChildPermission(AppPermissions.Pages_Tenant_GenerateUnit_BySystem_SetBuilding, L("SetBuilding"), multiTenancySides: MultiTenancySides.Tenant);
            genUnit.CreateChildPermission(AppPermissions.Pages_Tenant_GenerateUnit_BySystem_SetUnitType, L("SetUnitType"), multiTenancySides: MultiTenancySides.Tenant);
            genUnit.CreateChildPermission(AppPermissions.Pages_Tenant_GenerateUnit_BySystem_CreateUnit, L("CreateUnit"), multiTenancySides: MultiTenancySides.Tenant);
            genUnit.CreateChildPermission(AppPermissions.Pages_Tenant_GenerateUnit_ByUploadExcel, L("GenerateUnitByUploadExcel"), multiTenancySides: MultiTenancySides.Tenant);

            /* Pricing */
            var pricing = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Pricing, L("Pricing"), multiTenancySides: MultiTenancySides.Tenant);
            var term = pricing.CreateChildPermission(AppPermissions.Pages_Tenant_MasterTerm, L("MasterTerm"), multiTenancySides: MultiTenancySides.Tenant);
            term.CreateChildPermission(AppPermissions.Pages_Tenant_MasterTerm_Create, L("CreateMasterTerm"), multiTenancySides: MultiTenancySides.Tenant);
            term.CreateChildPermission(AppPermissions.Pages_Tenant_MasterTerm_Edit, L("EditMasterTerm"), multiTenancySides: MultiTenancySides.Tenant);
            var discount = pricing.CreateChildPermission(AppPermissions.Pages_Tenant_MasterDiscount, L("MasterDiscount"), multiTenancySides: MultiTenancySides.Tenant);
            discount.CreateChildPermission(AppPermissions.Pages_Tenant_MasterDiscount_Create, L("CreateMasterDiscount"), multiTenancySides: MultiTenancySides.Tenant);
            discount.CreateChildPermission(AppPermissions.Pages_Tenant_MasterDiscount_Edit, L("EditMasterDiscount"), multiTenancySides: MultiTenancySides.Tenant);
            discount.CreateChildPermission(AppPermissions.Pages_Tenant_MasterDiscount_Delete, L("DeleteMasterDiscount"), multiTenancySides: MultiTenancySides.Tenant);
            var finType = pricing.CreateChildPermission(AppPermissions.Pages_Tenant_MasterFinType, L("MasterFinType"), multiTenancySides: MultiTenancySides.Tenant);
            finType.CreateChildPermission(AppPermissions.Pages_Tenant_MasterFinType_Create, L("CreateMasterFinType"), multiTenancySides: MultiTenancySides.Tenant);
            finType.CreateChildPermission(AppPermissions.Pages_Tenant_MasterFinType_Edit, L("EditMasterFinType"), multiTenancySides: MultiTenancySides.Tenant);
            finType.CreateChildPermission(AppPermissions.Pages_Tenant_MasterFinType_Delete, L("DeleteMasterFinType"), multiTenancySides: MultiTenancySides.Tenant);
            var basePrice = pricing.CreateChildPermission(AppPermissions.Pages_Tenant_UploadBasePrice, L("UploadBasePrice"), multiTenancySides: MultiTenancySides.Tenant);
            basePrice.CreateChildPermission(AppPermissions.Pages_Tenant_UploadBasePrice_GenBasePriceBySystem, L("GenBasePriceBySystem"), multiTenancySides: MultiTenancySides.Tenant);
            var uploadBasePrice = basePrice.CreateChildPermission(AppPermissions.Pages_Tenant_UploadBasePrice_UploadBasePriceByExcel, L("Upload"), multiTenancySides: MultiTenancySides.Tenant);
            uploadBasePrice.CreateChildPermission(AppPermissions.Pages_Tenant_UploadBasePrice_UploadBasePriceByExcel_Add, L("UploadBasePriceByExcel"), multiTenancySides: MultiTenancySides.Tenant);
            var marketing = pricing.CreateChildPermission(AppPermissions.Pages_Tenant_MasterMarketingFactor, L("MasterMarketingFactor"), multiTenancySides: MultiTenancySides.Tenant);
            marketing.CreateChildPermission(AppPermissions.Pages_Tenant_MasterMarketingFactor_Create, L("CreateMasterMarketingFactor"), multiTenancySides: MultiTenancySides.Tenant);
            marketing.CreateChildPermission(AppPermissions.Pages_Tenant_MasterMarketingFactor_Edit, L("EditMasterMarketingFactor"), multiTenancySides: MultiTenancySides.Tenant);
            marketing.CreateChildPermission(AppPermissions.Pages_Tenant_MasterMarketingFactor_Delete, L("DeleteMasterMarketingFactor"), multiTenancySides: MultiTenancySides.Tenant);
            var generatePrice = pricing.CreateChildPermission(AppPermissions.Pages_Tenant_GeneratePrice, L("GeneratePrice"), multiTenancySides: MultiTenancySides.Tenant);
            generatePrice.CreateChildPermission(AppPermissions.Pages_Tenant_GeneratePrice_GeneratePriceList, L("GeneratePriceList"), multiTenancySides: MultiTenancySides.Tenant);
            generatePrice.CreateChildPermission(AppPermissions.Pages_Tenant_GeneratePrice_UploadExcel, L("UploadGrossPrice"), multiTenancySides: MultiTenancySides.Tenant);
            generatePrice.CreateChildPermission(AppPermissions.Pages_Tenant_GeneratePrice_UploadPriceList, L("UploadPriceList"), multiTenancySides: MultiTenancySides.Tenant);
            pricing.CreateChildPermission(AppPermissions.Pages_Tenant_PriceListHistory, L("PriceListHistory"), multiTenancySides: MultiTenancySides.Tenant);
            pricing.CreateChildPermission(AppPermissions.Pages_Tenant_ManagePriceIncreases, L("ManagePriceIncreases"), multiTenancySides: MultiTenancySides.Tenant);
            pricing.CreateChildPermission(AppPermissions.Pages_Tenant_Approval, L("Approval"), multiTenancySides: MultiTenancySides.Tenant);

            /* Commission */
            var commission = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Commission, L("Commission"), multiTenancySides: MultiTenancySides.Tenant);
            var masterSchema = commission.CreateChildPermission(AppPermissions.Pages_Tenant_MasterSchema, L("MasterSchema"), multiTenancySides: MultiTenancySides.Tenant);
            masterSchema.CreateChildPermission(AppPermissions.Pages_Tenant_MasterSchema_Create, L("CreateMasterSchema"), multiTenancySides: MultiTenancySides.Tenant);
            masterSchema.CreateChildPermission(AppPermissions.Pages_Tenant_MasterSchema_Edit, L("EditMasterSchema"), multiTenancySides: MultiTenancySides.Tenant);
            masterSchema.CreateChildPermission(AppPermissions.Pages_Tenant_MasterSchema_Delete, L("DeleteMasterSchema"), multiTenancySides: MultiTenancySides.Tenant);

            var masterSchemaPerProject = commission.CreateChildPermission(AppPermissions.Pages_Tenant_MasterSchemaPerProject, L("MasterSchemaPerProject"), multiTenancySides: MultiTenancySides.Tenant);
            masterSchemaPerProject.CreateChildPermission(AppPermissions.Pages_Tenant_MasterSchemaPerProject_Create, L("CreateMasterSchemaPerProject"), multiTenancySides: MultiTenancySides.Tenant);
            masterSchemaPerProject.CreateChildPermission(AppPermissions.Pages_Tenant_MasterSchemaPerProject_Edit, L("EditMasterSchemaPerProject"), multiTenancySides: MultiTenancySides.Tenant);
            masterSchemaPerProject.CreateChildPermission(AppPermissions.Pages_Tenant_MasterSchemaPerProject_Delete, L("DeleteMasterSchemaPerProject"), multiTenancySides: MultiTenancySides.Tenant);

            var masterPPHRange = commission.CreateChildPermission(AppPermissions.Pages_Tenant_MasterPPHRange, L("MasterPPHRange"), multiTenancySides: MultiTenancySides.Tenant);
            masterPPHRange.CreateChildPermission(AppPermissions.Pages_Tenant_MasterPPHRange_Create, L("CreateMasterPPHRange"), multiTenancySides: MultiTenancySides.Tenant);
            masterPPHRange.CreateChildPermission(AppPermissions.Pages_Tenant_MasterPPHRange_Edit, L("EditMasterPPHRange"), multiTenancySides: MultiTenancySides.Tenant);
            masterPPHRange.CreateChildPermission(AppPermissions.Pages_Tenant_MasterPPHRange_Delete, L("DeleteMasterPPHRange"), multiTenancySides: MultiTenancySides.Tenant);

            var masterDeveloperSchema = commission.CreateChildPermission(AppPermissions.Pages_Tenant_MasterDeveloperSchema, L("MasterDeveloperSchema"), multiTenancySides: MultiTenancySides.Tenant);
            masterDeveloperSchema.CreateChildPermission(AppPermissions.Pages_Tenant_MasterDeveloperSchema_Create, L("CreateMasterDeveloperSchema"), multiTenancySides: MultiTenancySides.Tenant);
            masterDeveloperSchema.CreateChildPermission(AppPermissions.Pages_Tenant_MasterDeveloperSchema_Edit, L("EditMasterDeveloperSchema"), multiTenancySides: MultiTenancySides.Tenant);
            masterDeveloperSchema.CreateChildPermission(AppPermissions.Pages_Tenant_MasterDeveloperSchema_Delete, L("DeleteMasterDeveloperSchema"), multiTenancySides: MultiTenancySides.Tenant);

            var masterBobotCommission = commission.CreateChildPermission(AppPermissions.Pages_Tenant_MasterBobotCommission, L("MasterBobotCommission"), multiTenancySides: MultiTenancySides.Tenant);
            masterBobotCommission.CreateChildPermission(AppPermissions.Pages_Tenant_MasterBobotCommission_Create, L("CreateMasterBobotCommission"), multiTenancySides: MultiTenancySides.Tenant);
            masterBobotCommission.CreateChildPermission(AppPermissions.Pages_Tenant_MasterBobotCommission_Edit, L("EditMasterBobotCommission"), multiTenancySides: MultiTenancySides.Tenant);
            masterBobotCommission.CreateChildPermission(AppPermissions.Pages_Tenant_MasterBobotCommission_Delete, L("DeleteMasterBobotCommission"), multiTenancySides: MultiTenancySides.Tenant);

            var masterPointPercent = commission.CreateChildPermission(AppPermissions.Pages_Tenant_MasterPointPercent, L("MasterPointPercent"), multiTenancySides: MultiTenancySides.Tenant);
            masterPointPercent.CreateChildPermission(AppPermissions.Pages_Tenant_MasterPointPercent_Create, L("CreateMasterPointPercent"), multiTenancySides: MultiTenancySides.Tenant);
            masterPointPercent.CreateChildPermission(AppPermissions.Pages_Tenant_MasterPointPercent_Edit, L("EditMasterPointPercent"), multiTenancySides: MultiTenancySides.Tenant);
            masterPointPercent.CreateChildPermission(AppPermissions.Pages_Tenant_MasterPointPercent_Delete, L("DeleteMasterPointPercent"), multiTenancySides: MultiTenancySides.Tenant);

            var masterPPHRangeInst = commission.CreateChildPermission(AppPermissions.Pages_Tenant_MasterPPHRangeInst, L("MasterPPHRangeInst"), multiTenancySides: MultiTenancySides.Tenant);
            masterPPHRangeInst.CreateChildPermission(AppPermissions.Pages_Tenant_MasterPPHRangeInst_Create, L("CreateMasterPPHRangeInst"), multiTenancySides: MultiTenancySides.Tenant);
            masterPPHRangeInst.CreateChildPermission(AppPermissions.Pages_Tenant_MasterPPHRangeInst_Edit, L("EditMasterPPHRangeInst"), multiTenancySides: MultiTenancySides.Tenant);
            masterPPHRangeInst.CreateChildPermission(AppPermissions.Pages_Tenant_MasterPPHRangeInst_Delete, L("DeleteMasterPPHRangeInst"), multiTenancySides: MultiTenancySides.Tenant);

            //TODO: ganti jadi managementFee
            var masterManagementFee = commission.CreateChildPermission(AppPermissions.Pages_Tenant_MasterManagementFee, L("MasterManagementFee"), multiTenancySides: MultiTenancySides.Tenant);
            masterManagementFee.CreateChildPermission(AppPermissions.Pages_Tenant_MasterManagementFee_Create, L("CreateMasterManagementFee"), multiTenancySides: MultiTenancySides.Tenant);
            masterManagementFee.CreateChildPermission(AppPermissions.Pages_Tenant_MasterManagementFee_Edit, L("EditMasterManagementFee"), multiTenancySides: MultiTenancySides.Tenant);
            masterManagementFee.CreateChildPermission(AppPermissions.Pages_Tenant_MasterManagementFee_Delete, L("DeleteMasterManagementFee"), multiTenancySides: MultiTenancySides.Tenant);

            var masterReward = commission.CreateChildPermission(AppPermissions.Pages_Tenant_MasterReward, L("MasterReward"), multiTenancySides: MultiTenancySides.Tenant);
            masterReward.CreateChildPermission(AppPermissions.Pages_Tenant_MasterReward_Create, L("CreateMasterReward"), multiTenancySides: MultiTenancySides.Tenant);
            masterReward.CreateChildPermission(AppPermissions.Pages_Tenant_MasterReward_Edit, L("EditMasterReward"), multiTenancySides: MultiTenancySides.Tenant);
            masterReward.CreateChildPermission(AppPermissions.Pages_Tenant_MasterReward_Delete, L("DeleteMasterReward"), multiTenancySides: MultiTenancySides.Tenant);

            var transactionSoldUnit = commission.CreateChildPermission(AppPermissions.Pages_Tenant_TransactionSoldUnit, L("TransactionSoldUnit"), multiTenancySides: MultiTenancySides.Tenant);
            transactionSoldUnit.CreateChildPermission(AppPermissions.Pages_Tenant_TransactionSoldUnit_Create, L("CreateTransactionSoldUnit"), multiTenancySides: MultiTenancySides.Tenant);
            transactionSoldUnit.CreateChildPermission(AppPermissions.Pages_Tenant_TransactionSoldUnit_Edit, L("EditTransactionSoldUnit"), multiTenancySides: MultiTenancySides.Tenant);
            transactionSoldUnit.CreateChildPermission(AppPermissions.Pages_Tenant_TransactionSoldUnit_Delete, L("DeleteTransactionSoldUnit"), multiTenancySides: MultiTenancySides.Tenant);

            var editDealCloser = commission.CreateChildPermission(AppPermissions.Pages_Tenant_EditDealCloser, L("EditDealCloser"), multiTenancySides: MultiTenancySides.Tenant);
            editDealCloser.CreateChildPermission(AppPermissions.Pages_Tenant_EditDealCloser_Create, L("CreateEditDealCloser"), multiTenancySides: MultiTenancySides.Tenant);
            editDealCloser.CreateChildPermission(AppPermissions.Pages_Tenant_EditDealCloser_Edit, L("EditEditDealCloser"), multiTenancySides: MultiTenancySides.Tenant);
            editDealCloser.CreateChildPermission(AppPermissions.Pages_Tenant_EditDealCloser_Delete, L("DeleteEditDealCloser"), multiTenancySides: MultiTenancySides.Tenant);

            var errorCommissionHistory = commission.CreateChildPermission(AppPermissions.Pages_Tenant_ErrorCommissionHistory, L("ErrorCommissionHistory"), multiTenancySides: MultiTenancySides.Tenant);
            errorCommissionHistory.CreateChildPermission(AppPermissions.Pages_Tenant_ErrorCommissionHistory_Create, L("CreateErrorCommissionHistory"), multiTenancySides: MultiTenancySides.Tenant);
            errorCommissionHistory.CreateChildPermission(AppPermissions.Pages_Tenant_ErrorCommissionHistory_Edit, L("EditErrorCommissionHistory"), multiTenancySides: MultiTenancySides.Tenant);
            errorCommissionHistory.CreateChildPermission(AppPermissions.Pages_Tenant_ErrorCommissionHistory_Delete, L("DeleteErrorCommissionHistory"), multiTenancySides: MultiTenancySides.Tenant);

            //PERSONALS
            var personals = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Personal, L("Personal"), multiTenancySides: MultiTenancySides.Tenant);

            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkAddrType, L("LkAddrType"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkAddrType_GetLkAddrTypeDropdown, L("GetLkAddrTypeDropdown"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkBankType, L("LkBankType"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkBankType_GetAllLkBankTypeList, L("GetAllLkBankTypeList"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkBlood, L("LkBlood"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkBlood_GetAllLkBloodList, L("GetAllLkBloodList"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkCountry, L("LkCountry"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkCountry_GetAllLkCountryList, L("GetAllLkCountryList"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkFamilyStatus, L("LkFamilyStatus"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkFamilyStatus_GetLkFamilyStatusDropdown, L("GetLkFamilyStatusDropdown"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkGrade, L("LkGrade"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkGrade_GetLkGradeDropdown, L("GetLkGradeDropdown"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkIdType, L("LkIdType"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkIdType_GetAllIDTypeList, L("GetAllIDTypeList"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkKeyPeople, L("LkKeyPeople"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkKeyPeople_GetAllLkKeyPeopleDropdwon, L("GetAllLkKeyPeopleDropdwon"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkMarStatus, L("LkMarStatus"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkMarStatus_GetAllLkMarStatusList, L("GetAllLkMarStatusList"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkPhoneTypes, L("PhoneTypes"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkPhoneTypes_GetLkPhoneTypeDropdown, L("GetLkPhoneTypeDropdown"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkReligion, L("LkReligion"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkReligion_GetAllLkReligionList, L("GetAllLkReligionList"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkSpec, L("LkSpec"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_LkSpec_GetAllLkSpecList, L("GetAllLkSpecList"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterBank, L("MasterBank"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterBank_GetAllMsBankList, L("GetAllMsBankList"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterCities, L("MasterCities"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterCities_GetCityListByProvinceCode, L("GetCityListByProvinceCode"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterDocument, L("MasterDocument"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterDocument_GetAllMsDocumentList, L("GetAllMsDocumentList"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterFranchiseGroup, L("MasterFranchiseGroup"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterFranchiseGroup_GetFranchiseGroupDropdown, L("GetFranchiseGroupDropdown"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterJobTitle, L("MasterJobTitle"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterJobTitle_GetAllMsJobTitleDropdown, L("GetAllMsJobTitleDropdown"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterNational, L("MasterNational"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterNational_GetMSNationDropdown, L("GetMSNationDropdown"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterOccupation, L("MasterOccupation"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterOccupation_GetMsOccupationDropdown, L("GetMsOccupationDropdown"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterPostCode, L("MasterPostCode"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterPostCode_GetPostCodeByCity, L("GetPostCodeByCity"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterProvinces, L("MasterProvinces"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterProvinces_GetMsProvinceDropdown, L("GetMsProvinceDropdown"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterStatusMembers, L("MasterStatusMembers"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_MasterStatusMembers_GetAllMsStatusMemberDropdown, L("GetAllMsStatusMemberDropdown"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Register, L("RegisterPersonal"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Edit, L("EditPersonal"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Individual, L("Individual"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Institute, L("Institute"), multiTenancySides: MultiTenancySides.Tenant);
            personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Detail, L("Detail"), multiTenancySides: MultiTenancySides.Tenant);

            var personalMember = personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_PersonalMember, L("PersonalMember"), multiTenancySides: MultiTenancySides.Tenant);
            personalMember.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_PersonalMember_Edit, L("EditPersonalMember"), multiTenancySides: MultiTenancySides.Tenant);
            personalMember.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_PersonalMember_Delete, L("DeletePersonalMember"), multiTenancySides: MultiTenancySides.Tenant);
            personalMember.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_PersonalMember_GetAllPersonalMemberList, L("GetAllPersonalMemberList"), multiTenancySides: MultiTenancySides.Tenant);
            personalMember.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_PersonalMember_GenerateMemberCode, L("GenerateMemberCode"), multiTenancySides: MultiTenancySides.Tenant);
            personalMember.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_PersonalMember_DeleteMember, L("DeleteMember"), multiTenancySides: MultiTenancySides.Tenant);
            personalMember.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_PersonalMember_DeleteSingleMember, L("DeleteSingleMember"), multiTenancySides: MultiTenancySides.Tenant);

            var personal = personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Personal, L("Personal"), multiTenancySides: MultiTenancySides.Tenant);
            personal.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Personal_Edit, L("EditPersonal"), multiTenancySides: MultiTenancySides.Tenant);


            var TrAddress = personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrAddress, L("TrAddress"), multiTenancySides: MultiTenancySides.Tenant);
            TrAddress.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrAddress_Create, L("CreateTrAddress"), multiTenancySides: MultiTenancySides.Tenant);
            TrAddress.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrAddress_Edit, L("EditTrAddress"), multiTenancySides: MultiTenancySides.Tenant);
            TrAddress.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrAddress_Delete, L("DeleteTrAddress"), multiTenancySides: MultiTenancySides.Tenant);
            
            var TrBankAcc = personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrBankAccount, L("TrBankAccount"), multiTenancySides: MultiTenancySides.Tenant);
            TrBankAcc.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrBankAccount_Edit, L("EditTrBankAccount"), multiTenancySides: MultiTenancySides.Tenant);
            TrBankAcc.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrBankAccount_Delete, L("DeleteTrBankAccount"), multiTenancySides: MultiTenancySides.Tenant);
            TrBankAcc.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrBankAccount_GetAllMsBankListByPsCode, L("GetAllMsBankListByPsCode"), multiTenancySides: MultiTenancySides.Tenant);

            var TrCompany = personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrCompany, L("TrCompany"), multiTenancySides: MultiTenancySides.Tenant);
            TrCompany.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrCompany_Edit, L("EditTrCompany"), multiTenancySides: MultiTenancySides.Tenant);
            TrCompany.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrCompany_Delete, L("DeleteTrCompany"), multiTenancySides: MultiTenancySides.Tenant);

            var TrDocument = personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrDocument, L("TrDocument"), multiTenancySides: MultiTenancySides.Tenant);
            TrDocument.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrDocument_Edit, L("EditTrDocument"), multiTenancySides: MultiTenancySides.Tenant);
            TrDocument.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrDocument_Delete, L("DeleteTrDocument"), multiTenancySides: MultiTenancySides.Tenant);

            var TrEmail = personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrEmail, L("TrEmail"), multiTenancySides: MultiTenancySides.Tenant);
            TrEmail.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrEmail_Create, L("CreateTrEmail"), multiTenancySides: MultiTenancySides.Tenant);
            TrEmail.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrEmail_Edit, L("EditTrEmail"), multiTenancySides: MultiTenancySides.Tenant);
            TrEmail.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrEmail_Delete, L("DeleteTrEmail"), multiTenancySides: MultiTenancySides.Tenant);

            var TrFamily = personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrFamily, L("TrFamily"), multiTenancySides: MultiTenancySides.Tenant);
            TrFamily.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrFamily_Edit, L("EditTrFamily"), multiTenancySides: MultiTenancySides.Tenant);
            TrFamily.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrFamily_Delete, L("DeleteTrFamily"), multiTenancySides: MultiTenancySides.Tenant);

            var TrId = personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrId, L("TrId"), multiTenancySides: MultiTenancySides.Tenant);
            TrId.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrId_Edit, L("EditTrId"), multiTenancySides: MultiTenancySides.Tenant);
            TrId.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrId_Delete, L("DeleteTrId"), multiTenancySides: MultiTenancySides.Tenant);

            var TrKeyPeople = personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrKeyPeople, L("TrKeyPeople"), multiTenancySides: MultiTenancySides.Tenant);
            TrKeyPeople.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrKeyPeople_Edit, L("EditTrKeyPeople"), multiTenancySides: MultiTenancySides.Tenant);
            TrKeyPeople.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrKeyPeople_Delete, L("DeleteTrKeyPeople"), multiTenancySides: MultiTenancySides.Tenant);

            var TrPhone = personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrPhone, L("TrPhone"), multiTenancySides: MultiTenancySides.Tenant);
            TrPhone.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrPhone_Create, L("CreateTrPhone"), multiTenancySides: MultiTenancySides.Tenant);
            TrPhone.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrPhone_Edit, L("EditTrPhone"), multiTenancySides: MultiTenancySides.Tenant);
            TrPhone.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_TrPhone_Delete, L("DeleteTrPhone"), multiTenancySides: MultiTenancySides.Tenant);

            var Person = personals.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person, L("Person"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_GetPersonalsByAdvanceSearch, L("GetPersonalsByAdvanceSearch"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_GetPersonalsByKeyword, L("GetPersonalsByKeyword"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_CreatePersonal, L("CreatePersonal"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_CreateKeyPeople, L("CreateKeyPeople"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_CreateBankAccount, L("CreateBankAccount"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_CreateCompany, L("CreateCompany"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_CreateDocument, L("CreateDocument"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_CreateIDNumber, L("CreateIDNumber"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_CreateFamily, L("CreateFamily"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_CreateMember, L("CreateMember"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_GetUniversalPersonal, L("GetUniversalPersonal"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_GetPersonalByPsCode, L("GetPersonalByPsCode"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_GetKeyPeopleByPsCode, L("GetKeyPeopleByPsCode"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_GetContactByPsCode, L("GetContactByPsCode"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_GetAddressByPsCode, L("GetAddressByPsCode"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_GetBankAccountByPsCode, L("GetBankAccountByPsCode"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_GetCompanyByPsCode, L("GetCompanyByPsCode"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_GetDocumentByPsCode, L("GetDocumentByPsCode"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_GetIDNumberByPsCode, L("GetIDNumberByPsCode"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_GetFamilyByPsCode, L("GetFamilyByPsCode"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_GetMemberByPsCode, L("GetMemberByPsCode"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_GetPhoneByPsCode, L("GetPhoneByPsCode"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_GetEmailByPsCode, L("GetEmailByPsCode"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_UpdatePersonal, L("UpdatePersonal"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_CreateOrUpdatePhone, L("CreateOrUpdatePhone"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_CreateOrUpdateEmail, L("CreateOrUpdateEmail"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_CreateOrUpdateAddress, L("CreateOrUpdateAddress"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_generatePsCode, L("generatePsCode"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_GetAllMsSchemaDropdown, L("GetAllMsSchemaDropdown"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_GetAvailableMemberSchemaByScmCodeAndPsCode, L("GetAvailableMemberSchemaByScmCodeAndPsCode"), multiTenancySides: MultiTenancySides.Tenant);
            Person.CreateChildPermission(AppPermissions.Pages_Tenant_Personal_Person_sendEmailActivationMember, L("sendEmailActivationMember"), multiTenancySides: MultiTenancySides.Tenant);



            //PSAS
            var psas = pages.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS, L("PSAS"), multiTenancySides: MultiTenancySides.Tenant);

            var mainPsas = psas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Main, L("Main"), multiTenancySides: MultiTenancySides.Tenant);
            mainPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Main_Create, L("CreateMain"), multiTenancySides: MultiTenancySides.Tenant);
            mainPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Main_Edit, L("EditMain"), multiTenancySides: MultiTenancySides.Tenant);
            mainPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Main_Delete, L("DeleteMain"), multiTenancySides: MultiTenancySides.Tenant);

            var termPsas = psas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Term, L("Term"), multiTenancySides: MultiTenancySides.Tenant);
            termPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Term_Create, L("CreateTerm"), multiTenancySides: MultiTenancySides.Tenant);
            termPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Term_Edit, L("EditTerm"), multiTenancySides: MultiTenancySides.Tenant);
            termPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Term_Delete, L("DeleteTerm"), multiTenancySides: MultiTenancySides.Tenant);

            var pricePsas = psas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Price, L("Price"), multiTenancySides: MultiTenancySides.Tenant);
            pricePsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Price_Create, L("CreatePrice"), multiTenancySides: MultiTenancySides.Tenant);
            pricePsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Price_Edit, L("EditPrice"), multiTenancySides: MultiTenancySides.Tenant);
            pricePsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Price_Delete, L("DeletePrice"), multiTenancySides: MultiTenancySides.Tenant);

            var paymentPsas = psas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Payment, L("Payment"), multiTenancySides: MultiTenancySides.Tenant);
            paymentPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Payment_Create, L("CreatePayment"), multiTenancySides: MultiTenancySides.Tenant);
            paymentPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Payment_Edit, L("EditPayment"), multiTenancySides: MultiTenancySides.Tenant);
            paymentPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Payment_Delete, L("DeletePayment"), multiTenancySides: MultiTenancySides.Tenant);

            var otherPayPsas = psas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_OtherPayment, L("OtherPayment"), multiTenancySides: MultiTenancySides.Tenant);
            otherPayPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_OtherPayment_Create, L("CreateOtherPayment"), multiTenancySides: MultiTenancySides.Tenant);
            otherPayPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_OtherPayment_Edit, L("EditOtherPayment"), multiTenancySides: MultiTenancySides.Tenant);
            otherPayPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_OtherPayment_Delete, L("DeleteOtherPayment"), multiTenancySides: MultiTenancySides.Tenant);

            var schedulePsas = psas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Schedule, L("Schedule"), multiTenancySides: MultiTenancySides.Tenant);
            schedulePsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Schedule_Create, L("CreateSchedule"), multiTenancySides: MultiTenancySides.Tenant);
            schedulePsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Schedule_Edit, L("EditSchedule"), multiTenancySides: MultiTenancySides.Tenant);
            schedulePsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_Schedule_Delete, L("DeleteSchedule"), multiTenancySides: MultiTenancySides.Tenant);

            var legalDocumentPsas = psas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_LegalDocument, L("LegalDocument"), multiTenancySides: MultiTenancySides.Tenant);
            var kuasaDireksiPsas = legalDocumentPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MasterKuasaDireksi, L("MasterKuasaDireksi"), multiTenancySides: MultiTenancySides.Tenant);
            kuasaDireksiPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MasterKuasaDireksi_View, L("ViewMasterKuasaDireksi"), multiTenancySides: MultiTenancySides.Tenant);
            kuasaDireksiPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MasterKuasaDireksi_Create, L("CreateMasterKuasaDireksi"), multiTenancySides: MultiTenancySides.Tenant);
            kuasaDireksiPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MasterKuasaDireksi_Delete, L("DeleteMasterKuasaDireksi"), multiTenancySides: MultiTenancySides.Tenant);

            var masterTemplatePsas = legalDocumentPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MasterTemplate, L("MasterTemplate"), multiTenancySides: MultiTenancySides.Tenant);
            masterTemplatePsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MasterTemplate_View, L("ViewMasterTemplate"), multiTenancySides: MultiTenancySides.Tenant);

            var mappingTemplatePsas = legalDocumentPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MappingTemplate, L("MasterMappingTemplate"), multiTenancySides: MultiTenancySides.Tenant);
            mappingTemplatePsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MappingTemplate_View, L("ViewMappingTemplate"), multiTenancySides: MultiTenancySides.Tenant);
            mappingTemplatePsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MappingTemplate_Create, L("CreateMappingTemplate"), multiTenancySides: MultiTenancySides.Tenant);
            mappingTemplatePsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MappingTemplate_Edit, L("EditMappingTemplate"), multiTenancySides: MultiTenancySides.Tenant);
            mappingTemplatePsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MappingTemplate_Delete, L("DeleteMappingTemplate"), multiTenancySides: MultiTenancySides.Tenant);

            var bookingDocumentPsas = legalDocumentPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_LegalDocument_BookingDocument, L("BookingDocument"), multiTenancySides: MultiTenancySides.Tenant);
            bookingDocumentPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_LegalDocument_BookingDocument_View, L("ViewBookingDocument"), multiTenancySides: MultiTenancySides.Tenant);
            bookingDocumentPsas.CreateChildPermission(AppPermissions.Pages_Tenant_PSAS_LegalDocument_BookingDocument_Create, L("CreateBookingDocument"), multiTenancySides: MultiTenancySides.Tenant);

            //Payment
            var payment = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Payment, L("Payment"), multiTenancySides: MultiTenancySides.Tenant);

            var masterPayForPayment = payment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterPayFor, L("MasterPayFor"), multiTenancySides: MultiTenancySides.Tenant);
            masterPayForPayment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterPayFor_Create, L("CreateMasterPayFor"), multiTenancySides: MultiTenancySides.Tenant);
            masterPayForPayment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterPayFor_Edit, L("EditMasterPayFor"), multiTenancySides: MultiTenancySides.Tenant);
            masterPayForPayment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterPayFor_Delete, L("DeleteMasterPayFor"), multiTenancySides: MultiTenancySides.Tenant);

            var masterPayTypePayment = payment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterPayType, L("MasterPayType"), multiTenancySides: MultiTenancySides.Tenant);
            masterPayTypePayment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterPayType_Create, L("CreateMasterPayType"), multiTenancySides: MultiTenancySides.Tenant);
            masterPayTypePayment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterPayType_Edit, L("EditMasterPayType"), multiTenancySides: MultiTenancySides.Tenant);
            masterPayTypePayment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterPayType_Delete, L("DeleteMasterPayType"), multiTenancySides: MultiTenancySides.Tenant);

            var masterOthersTypePayment = payment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterOthersType, L("MasterOthersType"), multiTenancySides: MultiTenancySides.Tenant);
            masterOthersTypePayment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterOthersType_Create, L("CreateMasterOthersType"), multiTenancySides: MultiTenancySides.Tenant);
            masterOthersTypePayment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterOthersType_Edit, L("EditMasterOthersType"), multiTenancySides: MultiTenancySides.Tenant);
            masterOthersTypePayment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterOthersType_Delete, L("DeleteMasterOthersType"), multiTenancySides: MultiTenancySides.Tenant);

            var masterAllocationPayment = payment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterAllocation, L("MasterAllocation"), multiTenancySides: MultiTenancySides.Tenant);
            masterAllocationPayment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterAllocation_Create, L("CreateMasterAllocation"), multiTenancySides: MultiTenancySides.Tenant);
            masterAllocationPayment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterAllocation_Edit, L("EditMasterAllocation"), multiTenancySides: MultiTenancySides.Tenant);
            masterAllocationPayment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterAllocation_Delete, L("DeleteMasterAllocation"), multiTenancySides: MultiTenancySides.Tenant);

            var masterAccountPayment = payment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterAccount, L("MasterAccount"), multiTenancySides: MultiTenancySides.Tenant);
            masterAccountPayment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterAccount_Edit, L("EditMasterAccount"), multiTenancySides: MultiTenancySides.Tenant);
            masterAccountPayment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_MasterAccount_Delete, L("DeleteMasterAccount"), multiTenancySides: MultiTenancySides.Tenant);

            var transactionSinglePayment = payment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_Transaction_SinglePayment, L("SinglePayment"), multiTenancySides: MultiTenancySides.Tenant);

            var transactionBulkPayment = payment.CreateChildPermission(AppPermissions.Pages_Tenant_Payment_Transaction_BulkPayment, L("BulkPayment"), multiTenancySides: MultiTenancySides.Tenant);

            /* Online Booking*/
            var onlineBooking = pages.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking, L("OnlineBooking"), multiTenancySides: MultiTenancySides.Tenant);

            var bookingHistory = onlineBooking.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_BookingHistory, L("BookingHistory"));
            bookingHistory.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_BookingHistory_GetDetailBookingHistory, L("GetDetailBookingHistory"));
            bookingHistory.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_BookingHistory_GetListBookingHistory, L("GetListBookingHistory"));
            bookingHistory.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_BookingHistory_SearchingBookingHistoryMobile, L("SearchingBookingHistoryMobile"));

            var customerMember = onlineBooking.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember, L("CustomerMember"));
            customerMember.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_MemberActivation, L("MemberActivation"));
            customerMember.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_SignUpCustomer, L("SignUpCustomer"));
            customerMember.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_GetListCustomer, L("GetListCustomer"));
            customerMember.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_GetNation, L("GetNation"));
            customerMember.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_GetCountry, L("GetCountry"));
            customerMember.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_GetCity, L("GetCity"));
            customerMember.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_GetPostCode, L("GetPostCode"));
            customerMember.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_GetDetailCustomer, L("GetDetailCustomer"));
            customerMember.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_UpdateCustomer, L("UpdateCustomer"));
            customerMember.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_GetDocumentByPsCode, L("GetDocumentByPsCode"));
            customerMember.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_GetDetailCustomerMobile, L("GetDetailCustomerMobile"));
            customerMember.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_GetProvince, L("GetProvince"));

            var diagramatic = onlineBooking.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic, L("Diagramatic"));
            diagramatic.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetDetailUnitByProject, L("GetDetailUnitByProject"));
            diagramatic.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetUnitDetailWithTower, L("GetUnitDetailWithTower"));
            diagramatic.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_ListPrice, L("ListPrice"));
            diagramatic.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListUnitByUnitCode, L("GetListUnitByUnitCode"));
            diagramatic.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListTowerByProjectID, L("GetListTowerByProjectID"));
            diagramatic.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListDiagramatic, L("GetListDiagramatic"));
            diagramatic.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListRenovation, L("GetListRenovation"));
            diagramatic.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListTerm, L("GetListTerm"));
            diagramatic.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListDiagramaticWeb, L("GetListDiagramaticWeb"));
            diagramatic.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetUnitSelectionDetail, L("GetUnitSelectionDetail"));
            diagramatic.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetUnitSelectionDetailMobile, L("GetUnitSelectionDetailMobile"));
            diagramatic.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetTypeDiagramatic, L("GetTypeDiagramatic"));
            diagramatic.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListBedroom, L("GetListBedroom"));
            diagramatic.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListZoning, L("GetListZoning"));
            diagramatic.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListSumberDana, L("GetListSumberDana"));
            diagramatic.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListTujuanTransaksi, L("GetListTujuanTransaksi"));
            diagramatic.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetDetailDiagramatic, L("GetDetailDiagramatic"));
            diagramatic.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetPaymentType, L("GetPaymentType"));

            var projectOB = onlineBooking.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Project, L("Project"));
            projectOB.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetDetailListProject, L("GetDetailListProject"));
            projectOB.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetListProject, L("GetListProject"));
            projectOB.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetListProjectByName, L("GetListProjectByName"));
            projectOB.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetListProjectImageGallery, L("GetListProjectImageGallery"));
            projectOB.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetListProjectInfo, L("GetListProjectInfo"));
            projectOB.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetListProjectKeyFeatures, L("GetListProjectKeyFeatures"));
            projectOB.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetListProjectLocation, L("GetListProjectLocation"));
            projectOB.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetUnitTypeByCluster, L("GetUnitTypeByCluster"));
            projectOB.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetUnitTypeByProjectId, L("GetUnitTypeByProjectId"));
            projectOB.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetListPromotion, L("GetListPromotion"));


            var transaction = onlineBooking.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction, L("Transaction"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTransactionUniversal, L("InsertTransactionUniversal"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrUnitReserved, L("InsertTrUnitReserved"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_UpdatePsCodeTrUnitReserved, L("UpdatePsCodeTrUnitReserved"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrUnitOrderHeader, L("InsertTrUnitOrderHeader"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrUnitOrderDetail, L("InsertTrUnitOrderDetail"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingHeader, L("InsertTrBookingHeader"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingDetail, L("InsertTrBookingDetail"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrPaymentOnlineBook, L("InsertTrPaymentOnlineBook"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_DeleteTrUnitReserved, L("DeleteTrUnitReserved"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertAddDisc, L("InsertAddDisc"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingDetailDP, L("InsertTrBookingDetailDP"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrCashAddDisc, L("InsertTrCashAddDisc"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingSalesAddDisc, L("InsertTrBookingSalesAddDisc"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_GetDetailBookingUnit, L("GetDetailBookingUnit"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_UpdateBFAmount, L("UpdateBFAmount"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingHeaderTerm, L("InsertTrBookingHeaderTerm"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingItemPrice, L("InsertTrBookingItemPrice"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_UpdateUnitSold, L("UpdateUnitSold"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_UpdateRemarksTrBookingHeader, L("UpdateRemarksTrBookingHeader"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingTax, L("InsertTrBookingTax"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_UpdateOrderStatusFullyPaid, L("UpdateOrderStatusFullyPaid"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_UpdateReleaseDate, L("UpdateReleaseDate"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrSoldUnit, L("InsertTrSoldUnit"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrSoldUnitRequirement, L("InsertTrSoldUnitRequirement"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_GetTrUnitReserved, L("GetTrUnitReserved"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_UpdateNetPriceBookingHeaderDetail, L("UpdateNetPriceBookingHeaderDetail"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingDetailSchedule, L("InsertTrBookingDetailSchedule"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_DoBookingMidransReq, L("DoBookingMidransReq"));
            transaction.CreateChildPermission(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_DoBooking, L("DoBooking"));






            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("MasterMaintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);


        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, DemoConsts.LocalizationSourceName);
        }
    }
}