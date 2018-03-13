using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VDI.Demo.Authorization;
using VDI.Demo.Configuration;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.Files;
using VDI.Demo.MasterPlan.Unit.MS_Units.Dto;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.Pricing;
using Visionet_Backend_NetCore.Komunikasi;

namespace VDI.Demo.MasterPlan.Unit.MS_Units
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_GenerateUnit)]
    public class MsUnitAppService : DemoAppServiceBase, IMsUnitAppService
    {
        private readonly IRepository<MS_Category> _msCategoryRepo;
        private readonly IRepository<LK_Facing> _lkFacingRepo;
        private readonly IRepository<MS_Zoning> _msZoningRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly IRepository<MS_UnitCode> _msUnitCodeRepo;
        private readonly IRepository<MS_Cluster> _msClusterRepo;
        private readonly IRepository<MS_UnitItem> _msUnitItemRepo;
        private readonly IRepository<LK_UnitStatus> _lkUnitStatusRepo;
        private readonly IRepository<LK_Item> _lkItemRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;
        private readonly IRepository<MS_Product> _msProductRepo;
        private readonly IRepository<MS_Term> _msTermRepo;
        private readonly IRepository<MS_Area> _msAreaRepo;
        private readonly IRepository<MS_Detail> _msDetailRepo;
        private readonly IRepository<LK_RentalStatus> _lkRentalStatusRepo;
        private readonly IRepository<MS_UnitRoom> _msUnitRoomRepo;
        private readonly IRepository<MS_UnitTaskList> _msUnitTaskListRepo;
        private readonly IRepository<TR_BookingHeader> _trBookingHeaderRepo;
        private readonly FilesHelper _filesHelper;
        private readonly NewCommDbContext _context;
        private readonly PropertySystemDbContext _contextProp;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfigurationRoot _appConfiguration;

        public MsUnitAppService(
            IRepository<MS_Category> msCategoryRepo,
            IRepository<LK_Facing> lkFacingRepo,
            IRepository<MS_Zoning> msZoningRepo,
            IRepository<MS_Unit> msUnitRepo,
            IRepository<MS_UnitCode> msUnitCodeRepo,
            IRepository<MS_Cluster> msClusterRepo,
            IRepository<MS_UnitItem> msUnitItemRepo,
            IRepository<LK_UnitStatus> lkUnitStatusRepo,
            IRepository<LK_Item> lkItemRepo,
            IRepository<MS_Project> msProjectRepo,
            IRepository<MS_Product> msProductRepo,
            IRepository<MS_Area> msAreaRepo,
            IRepository<MS_Detail> msDetailRepo,
            IRepository<MS_Term> msTermRepo,
            IRepository<LK_RentalStatus> lkRentalStatusRepo,
            IRepository<MS_UnitRoom> msUnitRoomRepo,
            IRepository<MS_UnitTaskList> msUnitTaskListRepo,
            FilesHelper filesHelper,
            NewCommDbContext context,
            PropertySystemDbContext contextProp,
            IRepository<TR_BookingHeader> trBookingHeaderRepo,
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment appConfiguration
        )
        {
            _trBookingHeaderRepo = trBookingHeaderRepo;
            _msDetailRepo = msDetailRepo;
            _msAreaRepo = msAreaRepo;
            _msCategoryRepo = msCategoryRepo;
            _lkFacingRepo = lkFacingRepo;
            _msZoningRepo = msZoningRepo;
            _msUnitRepo = msUnitRepo;
            _msUnitCodeRepo = msUnitCodeRepo;
            _msClusterRepo = msClusterRepo;
            _msUnitItemRepo = msUnitItemRepo;
            _lkUnitStatusRepo = lkUnitStatusRepo;
            _lkItemRepo = lkItemRepo;
            _msProjectRepo = msProjectRepo;
            _msProductRepo = msProductRepo;
            _msTermRepo = msTermRepo;
            _lkRentalStatusRepo = lkRentalStatusRepo;
            _msUnitRoomRepo = msUnitRoomRepo;
            _msUnitTaskListRepo = msUnitTaskListRepo;
            _filesHelper = filesHelper;
            _context = context;
            _contextProp = contextProp;
            _httpContextAccessor = httpContextAccessor;
            _appConfiguration = appConfiguration.GetAppConfiguration();
        }

        public int CreateMsUnit(CreateUnitInputDto input)//passed
        {
            Logger.InfoFormat("CreateMsUnit() - Started.");
            var createMsUnit = new MS_Unit
            {
                entityID = 1,
                unitCodeID = input.unitCodeID,
                facingID = input.facingID,
                zoningID = input.zoningID,
                productID = input.productID,
                unitStatusID = input.unitStatusID,
                clusterID = input.clusterID,
                projectID = input.projectID,
                unitNo = input.unitNo,
                categoryID = input.categoryID,
                CombinedUnitNo = input.CombinedUnitNo,
                areaID = input.areaID,
                detailID = input.detailID,
                rentalStatusID = input.rentalStatusID,
                unitCertCode = "1", //input.unitCertCode,
                remarks = "remarks", //input.remarks,
                prevUnitNo = "prevUnit",
                termMainID = input.termMainID
            };

            Logger.DebugFormat("CreateMsUnit() - Start insert msUnit. Parameters sent: {0} " +
                "entityID           = {1}{0}" +
                "unitCodeID         = {2}{0}" +
                "facingID           = {3}{0}" +
                "zoningID           = {4}{0}" +
                "productID          = {5}{0}" +
                "unitStatusID       = {6}{0}" +
                "clusterID          = {7}{0}" +
                "projectID          = {8}{0}" +
                "unitNo             = {9}{0}" +
                "categoryID         = {10}{0}" +
                "CombinedUnitNo     = {11}{0}" +
                "areaID             = {12}{0}" +
                "detailID           = {13}{0}" +
                "rentalStatusID     = {14}{0}" +
                "unitCertCode       = {15}{0}" +
                "termMainID         = {16}{0}" +
                "remarks            = {17}{0}" +
                "prevUnitNo         = {18}{0}"
                , Environment.NewLine, 1, input.unitCodeID, input.facingID, input.zoningID, input.productID, input.unitStatusID,
                input.clusterID, input.projectID, input.unitNo, input.categoryID, "combine", '-', '-', '-', "1", input.termMainID, "remarks", "prevUnit");

            var unitId = _msUnitRepo.InsertAndGetId(createMsUnit);
            Logger.DebugFormat("CreateMsUnit() - End insert msUnit. Result =  {0} ", unitId);
            Logger.InfoFormat("CreateMsUnit() - Finished.");
            return unitId;
        }

        public void CreateOrUpdateMsUnitItem(CreateUnitItemInputDto input)//passed
        {
            Logger.InfoFormat("CreateOrUpdateMsUnitItem() - Started.");

            foreach (var item in input.unitItem)
            {
                var itemID = GetItemIdByItemCode(item.itemCode);

                Logger.DebugFormat("CreateOrUpdateMsUnitItem() - Start checking existing Unit Item. " +
                    "Parameters sent: {0} " +
                    "itemID     = {1}{0}" +
                    "unitID     = {2}{0}"
                    , Environment.NewLine, itemID, input.unitID);

                var checkUnitItem = (from x in _msUnitItemRepo.GetAll()
                                     where x.itemID == itemID && x.unitID == input.unitID
                                     select x);

                Logger.DebugFormat("CreateUniversalSystem() - End checking existing " +
                    "itemID, unitID. Result = {0} ", checkUnitItem.Any());

                if (!checkUnitItem.Any())
                {
                    var data = new MS_UnitItem
                    {
                        entityID = 1,
                        unitID = input.unitID,
                        itemID = itemID,
                        coCode = "N/A",
                        amount = 0,
                        pctDisc = 0,
                        pctTax = 0.1,
                        area = item.area,
                        dimension = item.dimension == null ? "-" : item.dimension
                    };
                    Logger.DebugFormat("CreateOrUpdateMsUnitItem() - Start insert MsUnitItem. Parameters sent: {0} " +
                        "unitID     = {1}{0}" +
                        "itemID     = {2}{0}" +
                        "entityID   = {3}{0}" +
                        "coCode     = {4}{0}" +
                        "amount     = {5}{0}" +
                        "pctDisc    = {6}{0}" +
                        "pctTax     = {7}{0}" +
                        "area       = {8}{0}" +
                        "dimension  = {9}{0}" +
                        Environment.NewLine, data.unitID, data.itemID, data.entityID, data.coCode, data.amount,
                        data.pctDisc, data.pctTax, data.area, data.dimension);
                    var unitItemID = _msUnitItemRepo.InsertAndGetId(data);
                    Logger.DebugFormat("CreateOrUpdateMsUnitItem() - End insert MsUnitItem");

                    if (item.itemCode != "01" && item.itemCode != "02" && item.itemCode != "06" && item.itemCode != "07")
                    {
                        var dtoUnitRoom = new CreateOrUpdateMsUnitRoomInputDto
                        {
                            bathroom = item.jumlahKamarMandi,
                            bedroom = item.jumlahKamarTidur,
                            unitItemID = unitItemID
                        };
                        CreateOrUpdateMsUnitRoom(dtoUnitRoom);
                    }
                }
                else
                {
                    var unitItemToUpdate = checkUnitItem.FirstOrDefault().MapTo<MS_UnitItem>();

                    unitItemToUpdate.dimension = item.dimension;
                    unitItemToUpdate.area = item.area;

                    Logger.DebugFormat("CreateOrUpdateMsUnitItem() - Start Update MsUnitItem. Parameters sent: {0} " +
                        "unitID     = {1}{0}" +
                        "itemID     = {2}{0}" +
                        "dimension  = {3}{0}" +
                        "area       = {4}{0}" +
                        Environment.NewLine, input.unitID, itemID, item.dimension, item.area);

                    _msUnitItemRepo.Update(unitItemToUpdate);

                    Logger.DebugFormat("CreateOrUpdateMsUnitItem() - End Update MsUnitItem");

                    if (item.itemCode != "01" && item.itemCode != "02" && item.itemCode != "06" && item.itemCode != "07")
                    {
                        var dtoUnitRoom = new CreateOrUpdateMsUnitRoomInputDto
                        {
                            bathroom = item.jumlahKamarMandi,
                            bedroom = item.jumlahKamarTidur,
                            unitItemID = checkUnitItem.FirstOrDefault().Id
                        };
                        CreateOrUpdateMsUnitRoom(dtoUnitRoom);
                    }
                }                
            }
            Logger.InfoFormat("CreateOrUpdateMsUnitItem() - Finished.");
        }

        private void CreateOrUpdateMsUnitRoom(CreateOrUpdateMsUnitRoomInputDto input)
        {
            Logger.InfoFormat("CreateOrUpdateMsUnitRoom() - Started.");

            var getUnitRoom = (from x in _msUnitRoomRepo.GetAll()
                               where x.unitItemID == input.unitItemID
                               select x);

            if (!getUnitRoom.Any())
            {
                var dtoUnitRoom = new MS_UnitRoom
                {
                    unitItemID = input.unitItemID,
                    bathroom = input.bathroom,
                    bedroom = input.bedroom
                };

                Logger.DebugFormat("CreateOrUpdateMsUnitItem() - Start insert MsUnitItem. Parameters sent: {0} " +
                        "unitItemID     = {1}{0}" +
                        "bathroom       = {2}{0}" +
                        "bedroom        = {3}{0}" +
                        Environment.NewLine, dtoUnitRoom.unitItemID, dtoUnitRoom.bathroom, dtoUnitRoom.bedroom);

                _msUnitRoomRepo.Insert(dtoUnitRoom);

                Logger.DebugFormat("CreateOrUpdateMsUnitItem() - End insert MsUnitItem");
            }
            else
            {
                var dtoUnitRoom = getUnitRoom.FirstOrDefault().MapTo<MS_UnitRoom>();

                dtoUnitRoom.bathroom = input.bathroom;
                dtoUnitRoom.bedroom = input.bedroom;

                Logger.DebugFormat("CreateOrUpdateMsUnitItem() - Start Update MsUnitItem. Parameters sent: {0} " +
                        "unitItemID     = {1}{0}" +
                        "bathroom       = {2}{0}" +
                        "bedroom        = {3}{0}" +
                        Environment.NewLine, dtoUnitRoom.unitItemID, dtoUnitRoom.bathroom, dtoUnitRoom.bedroom);

                _msUnitRoomRepo.Insert(dtoUnitRoom);

                Logger.DebugFormat("CreateOrUpdateMsUnitItem() - End Update MsUnitItem");
            }

            Logger.InfoFormat("CreateOrUpdateMsUnitRoom() - Finished.");
        }

        public JObject CreateUniversalExcel(CreateUniversalExcelInputDto input)//passed
        {
            Logger.InfoFormat("CreateUniversalExcel() - Started.");
            bool messageUpdated = false;
            JObject message = new JObject();
            string unitCodeTemp = "";
            string productCodeTemp = "";
            int unitCodeIDTemp = 0;
            int productIDTemp = 0;

            foreach (var item in input.unit)
            {
                Logger.DebugFormat("CreateUniversalExcel() - Start getFacingIdByCode. Parameters sent: {0} " +
                    "facingCode         = {1}{0}   ", Environment.NewLine, item.facingCode);
                var facingID = getFacingIdByCode(item.facingCode);
                Logger.DebugFormat("CreateUniversalExcel() - End getFacingIdByCode. Result = {0} ", facingID);

                var zoningID = item.zoningCode == null ? GetZoningIdByZoningCode("-") : GetZoningIdByZoningCode(item.zoningCode);
                var rentalStatusID = GetRentalStatusIdByRentalStatusCode();
                var detailID = item.detailCode == null ? GetDetailIdByDetailCode("-") : GetDetailIdByDetailCode(item.detailCode);
                var areaID = item.areaCode == null ? GetAreaIdByAreaCode("-") : GetAreaIdByAreaCode(item.areaCode);

                if (productCodeTemp != item.productCode)
                {
                    var productID = GetProductIdByProductCode(item.productCode);
                    productIDTemp = productID;
                    productCodeTemp = item.productCode;
                }

                if (unitCodeTemp != item.unitCode)
                {
                    var unitCodeID = GetUnitCodeIdByUnitCodeAndProject(item.unitCode, input.projectID);
                    unitCodeIDTemp = unitCodeID;
                    unitCodeTemp = item.unitCode;
                }

                Logger.DebugFormat("CreateUniversalExcel() - Start checking existing unitCodeID, unitNo, clusterID, projectID. " +
                    "Parameters sent: {0} " +
                    "unitCodeID     = {1}{0}" +
                    "unitNo         = {2}{0}" +
                    "clusterID      = {3}{0}" +
                    "projectID      = {4}{0}"
                    , Environment.NewLine, unitCodeIDTemp, item.unitNo, input.clusterID, input.projectID);

                var checkMsUnit = (from x in _msUnitRepo.GetAll()
                                   where
                                   x.unitCodeID == unitCodeIDTemp &&
                                   x.unitNo == item.unitNo &&
                                   x.clusterID == input.clusterID &&
                                   x.projectID == input.projectID
                                   select x);

                Logger.DebugFormat("CreateUniversalSystem() - End checking existing " +
                    "unitCodeID, unitNo, clusterID, projectID. Result = {0} ", checkMsUnit.Any());

                if (!checkMsUnit.Any())
                {
                    // insert ms unit
                    var dtoUnit = new CreateUnitInputDto();
                    dtoUnit.unitCodeID = unitCodeIDTemp;
                    dtoUnit.facingID = facingID;
                    dtoUnit.zoningID = zoningID;
                    dtoUnit.productID = productIDTemp;
                    dtoUnit.unitStatusID = input.unitStatusID;
                    dtoUnit.clusterID = input.clusterID;
                    dtoUnit.projectID = input.projectID;
                    dtoUnit.unitNo = item.unitNo;
                    dtoUnit.categoryID = input.categoryID;
                    dtoUnit.detailID = detailID;
                    dtoUnit.areaID = areaID;
                    dtoUnit.CombinedUnitNo = item.combinedUnitNo.IsNullOrEmpty() ? "-" : item.combinedUnitNo;
                    dtoUnit.rentalStatusID = rentalStatusID;
                    dtoUnit.termMainID = input.termMainID;

                    Logger.DebugFormat("CreateUniversalExcel() - Start Create MsUnit. Parameters sent: {0} " +
                    "unitCodeID       = {2}{0}" +
                    "facingID         = {4}{0}" +
                    "zoningID         = {5}{0}" +
                    "productID        = {6}{0}" +
                    "unitStatusID     = {7}{0}" +
                    "clusterID        = {8}{0}" +
                    "projectID        = {9}{0}" +
                    "unitNo           = {10}{0}" +
                    "categoryID       = {11}{0}" +
                    "detailID         = {12}{0}" +
                    "areaID           = {13}{0}" +
                    "CombinedUnitNo   = {14}{0}" +
                    "rentalStatusID   = {15}{0}" +
                    "termMainID       = {16}{0}", 
                    Environment.NewLine, dtoUnit.unitCodeID, dtoUnit.facingID, dtoUnit.zoningID, dtoUnit.productID, dtoUnit.unitStatusID,
                    dtoUnit.clusterID, dtoUnit.projectID, dtoUnit.unitNo, dtoUnit.categoryID, dtoUnit.detailID, dtoUnit.areaID, 
                    dtoUnit.CombinedUnitNo, dtoUnit.rentalStatusID,dtoUnit.termMainID);

                    var unitID = CreateMsUnit(dtoUnit);
                    Logger.DebugFormat("CreateUniversalSystem() - End Create MsUnit. Result = {0} ", unitID);

                    // insert ms unitItem
                    var dtoUnitItem = new CreateUnitItemInputDto();
                    dtoUnitItem.unitID = unitID;
                    dtoUnitItem.unitItem = item.unitItem;

                    CreateOrUpdateMsUnitItem(dtoUnitItem);
                }
                else
                {
                    //todo update ms unit if exist
                    var checkDataBooking = (from x in _trBookingHeaderRepo.GetAll()
                                            where x.unitID == checkMsUnit.FirstOrDefault().Id
                                            select x).Any();

                    if (!checkDataBooking)
                    {
                        // update ms unit
                        var unitToUpdate = checkMsUnit.FirstOrDefault().MapTo<MS_Unit>();

                        unitToUpdate.unitCodeID = unitCodeIDTemp;
                        unitToUpdate.facingID = facingID;
                        unitToUpdate.zoningID = zoningID;
                        unitToUpdate.productID = productIDTemp;
                        unitToUpdate.unitStatusID = input.unitStatusID;
                        unitToUpdate.clusterID = input.clusterID;
                        unitToUpdate.projectID = input.projectID;
                        unitToUpdate.unitNo = item.unitNo;
                        unitToUpdate.categoryID = input.categoryID;
                        unitToUpdate.detailID = detailID;
                        unitToUpdate.areaID = areaID;
                        unitToUpdate.CombinedUnitNo = item.combinedUnitNo.IsNullOrEmpty() ? "-" : item.combinedUnitNo;
                        unitToUpdate.rentalStatusID = rentalStatusID;
                        unitToUpdate.termMainID = input.termMainID;

                        Logger.DebugFormat("CreateUniversalExcel() - Start Create MsUnit. Parameters sent: {0} " +
                        "unitCodeID       = {2}{0}" +
                        "facingID         = {4}{0}" +
                        "zoningID         = {5}{0}" +
                        "productID        = {6}{0}" +
                        "unitStatusID     = {7}{0}" +
                        "clusterID        = {8}{0}" +
                        "projectID        = {9}{0}" +
                        "unitNo           = {10}{0}" +
                        "categoryID       = {11}{0}" +
                        "detailID         = {12}{0}" +
                        "areaID           = {13}{0}" +
                        "CombinedUnitNo   = {14}{0}" +
                        "rentalStatusID   = {15}{0}" +
                        "termMainID       = {16}{0}",
                        Environment.NewLine, unitToUpdate.unitCodeID, unitToUpdate.facingID, unitToUpdate.zoningID, unitToUpdate.productID, unitToUpdate.unitStatusID,
                        unitToUpdate.clusterID, unitToUpdate.projectID, unitToUpdate.unitNo, unitToUpdate.categoryID, unitToUpdate.detailID, unitToUpdate.areaID,
                        unitToUpdate.CombinedUnitNo, unitToUpdate.rentalStatusID, unitToUpdate.termMainID);

                        _msUnitRepo.Update(unitToUpdate);

                        Logger.DebugFormat("CreateUniversalSystem() - End Update MsUnit. Result = {0} ", checkMsUnit.FirstOrDefault().Id);

                        // insert or update ms unitItem
                        var dtoUnitItem = new CreateUnitItemInputDto();
                        dtoUnitItem.unitID = checkMsUnit.FirstOrDefault().Id;
                        dtoUnitItem.unitItem = item.unitItem;

                        CreateOrUpdateMsUnitItem(dtoUnitItem);
                    }
                    else
                    {
                        messageUpdated = true;
                    }

                }
            }

            var dtoUnitTaskList = new CreateUnitTaskListInputDto
            {
                projectID = input.projectID,
                link = input.excelFile
            };

            CreateUnitTaskList(dtoUnitTaskList);

            if (messageUpdated)
            {
                message.Add("message", "Successfully, But some Unit are used by another process");
            }
            else
            {
                message.Add("message", "Created / Updated Successfully");
            }

            Logger.InfoFormat("CreateUniversalExcel() - Finished.");
            return message;
        }

        #region Get Id By Code
        private int GetUnitCodeIdByUnitCodeAndProject(string unitCode, int projectID)
        {
            var unitCodeID = (from x in _msUnitCodeRepo.GetAll()
                              where x.unitCode == unitCode && x.projectID == projectID
                              select x.Id);

            if (unitCodeID.Any())
            {
                return unitCodeID.FirstOrDefault();
            }
            else
            {
                var projectCode = (from x in _msProjectRepo.GetAll()
                                   where x.Id == projectID
                                   select x.projectCode).FirstOrDefault();

                throw new UserFriendlyException("Invalid Unit Code " + unitCode + " with Project " + projectCode);
            }
        }

        private int GetProductIdByProductCode(string productCode)
        {
            var productID = (from x in _msProductRepo.GetAll()
                             where x.productCode == productCode
                             select x.Id);

            if (productID.Any())
            {
                return productID.FirstOrDefault();
            }
            else
            {
                throw new UserFriendlyException("Invalid Product Code " + productCode);
            }
        }

        private int getFacingIdByCode(string facingCode)//passed //TODO field facingCode di SQL harus di-index
        {
            var facingID = (from x in _lkFacingRepo.GetAll()
                            where x.facingCode == facingCode
                            select x.Id);

            if (facingID.Any())
            {
                return facingID.FirstOrDefault();
            }
            else
            {
                throw new UserFriendlyException("Invalid Facing Code " + facingCode);
            }
        }

        private int GetAreaIdByAreaCode(string areaCode)//passed //TODO field areaCode di SQL harus di-index
        {
            var areaID = (from x in _msAreaRepo.GetAll()
                          where x.areaCode == areaCode
                          select x.Id);

            if (areaID.Any())
            {
                return areaID.FirstOrDefault();
            }
            else
            {
                throw new UserFriendlyException("Invalid Area Code " + areaCode);
            }
        }

        private int GetDetailIdByDetailCode(string detailCode)//passed //TODO field detailCode di SQL harus di-index
        {
            var detailID = (from x in _msDetailRepo.GetAll()
                            where x.detailCode == detailCode
                            select x.Id);

            if (detailID.Any())
            {
                return detailID.FirstOrDefault();
            }
            else
            {
                throw new UserFriendlyException("Invalid Detail Code " + detailCode);
            }
        }

        private int GetZoningIdByZoningCode(string zoningCode)//passed //TODO field zoningCode di SQL harus di-index
        {
            var zoningID = (from x in _msZoningRepo.GetAll()
                            where x.zoningCode == zoningCode
                            select x.Id);

            if (zoningID.Any())
            {
                return zoningID.FirstOrDefault();
            }
            else
            {
                throw new UserFriendlyException("Invalid Zoning Code " + zoningCode);
            }
        }

        private int GetRentalStatusIdByRentalStatusCode()
        {
            var rentalStausID = (from x in _lkRentalStatusRepo.GetAll()
                                 where x.rentalStatusCode == "N"
                                 select x.Id);

            if (rentalStausID.Any())
            {
                return rentalStausID.FirstOrDefault();
            }
            else
            {
                throw new UserFriendlyException("Invalid Rental Status Code N");
            }
        }

        private int GetItemIdByItemCode(string itemCode)//passed //TODO field itemCode di SQL harus di-index
        {
            var itemID = (from x in _lkItemRepo.GetAll()
                          where x.itemCode == itemCode
                          select x.Id);

            if (itemID.Any())
            {
                return itemID.FirstOrDefault();
            }
            else
            {
                throw new UserFriendlyException("Invalid Item Code " + itemCode);
            }
        }
        #endregion

        public void ManageStatusMsUnit(List<ManageStatusMsUnitInput> input)
        {
            Logger.InfoFormat("ManageStatusMsUnit() - Started.");
            foreach (var unit in input)
            {
                Logger.DebugFormat("ManageStatusMsUnit() - Start get msUnit for update. Parameters sent: {0} " +
                    "unitID = {1}{0}", Environment.NewLine, unit.unitID);
                var getUnit = (from x in _msUnitRepo.GetAll()
                               where x.Id == unit.unitID
                               select x).FirstOrDefault();

                Logger.DebugFormat("ManageStatusMsUnit() - End get msUnit for update. Result = {0}", getUnit);

                var updateUnit = getUnit.MapTo<MS_Unit>();

                updateUnit.unitStatusID = unit.unitStatusID;

                try
                {
                    Logger.DebugFormat("ManageStatusMsUnit() - Start Update msUnit. Parameters sent: {0} " +
                        "unitStatusID = {1}{0}", Environment.NewLine, unit.unitStatusID);
                    _msUnitRepo.Update(updateUnit);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("ManageStatusMsUnit() - End Update msUnit");
                }
                catch (DataException ex)
                {
                    Logger.DebugFormat("ManageStatusMsUnit() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.DebugFormat("ManageStatusMsUnit() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            Logger.InfoFormat("ManageStatusMsUnit() - Finished.");
        }

        public void CreateUnitTaskList(CreateUnitTaskListInputDto input)
        {
            Logger.InfoFormat("CreateUnitTaskList() - Started.");
            string excelFile;
            if (!string.IsNullOrWhiteSpace(input.link))
            {
                excelFile = moveFile(input.link);

                try
                {
                    Regex RegexObj = new Regex("[\\w\\W]*([\\/]Assets[\\w\\W\\s]*)");
                    if (RegexObj.IsMatch(excelFile))
                    {
                        excelFile = RegexObj.Match(excelFile).Groups[1].Value;
                    }
                }
                catch (ArgumentException ex)
                {
                }
            }
            else
            {
                excelFile = "-";
            }

            var data = new MS_UnitTaskList
            {
                projectID = input.projectID,
                dataUnit = excelFile
            };

            Logger.DebugFormat("CreateUnitTaskList() - Start Insert MS_UnitTaskList. Parameters sent: {0} " +
                 "projectID        = {1}{0}" +
                 "dataUnit         = {2}{0}"
                , Environment.NewLine, input.projectID, input.link);

            _msUnitTaskListRepo.Insert(data);

            Logger.InfoFormat("CreateUnitTaskList() - Finished.");
        }

        public List<GetUnitTaskListDto> GetUnitTaskList(int projectID)//passed
        {
            var getData = (from A in _msUnitTaskListRepo.GetAll()
                           where A.projectID == projectID
                           orderby A.Id descending
                           select new GetUnitTaskListDto
                           {
                               unitTaskListID = A.Id,
                               creationTime = A.CreationTime,
                               link = (A != null && A.dataUnit != null) ? (!A.dataUnit.Equals("-"))?getAbsoluteUriWithoutTail() + GetURLWithoutHost(A.dataUnit):null : null, //TODO link + ip host
                           }).ToList();

            return getData;
        }

        #region helper
        private void GetURLWithoutHost(string path, out string finalpath)
        {
            finalpath = path;
            try
            {
                Regex RegexObj = new Regex("[\\w\\W]*([\\/]Assets[\\w\\W\\s]*)");
                if (RegexObj.IsMatch(path))
                {
                    finalpath = RegexObj.Match(path).Groups[1].Value;
                }
            }
            catch (ArgumentException ex)
            {
            }
        }

        private string GetURLWithoutHost(string path)
        {
            string finalpath = path;
            try
            {
                Regex RegexObj = new Regex("[\\w\\W]*([\\/]Assets[\\w\\W\\s]*)");
                if (RegexObj.IsMatch(path))
                {
                    finalpath = RegexObj.Match(path).Groups[1].Value;
                }
            }
            catch (ArgumentException ex)
            {
            }
            return finalpath;
        }

        private string getAbsoluteUriWithoutTail()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.ToString();
            var test = uriBuilder.ToString();
            var result = test.Replace("[", "").Replace("]", "");
            int position = result.LastIndexOf('/');
            if (position > -1)
                result = result.Substring(0, result.Length - 1);
            return result + _appConfiguration["App:VirtualDirectory"];
        }

        private string moveFile(string filename)
        {
            try
            {
                return _filesHelper.MoveFiles(filename, @"Temp\Downloads\UnitFile\", @"Assets\Upload\UnitFile\");
            }
            catch (Exception ex)
            {
                Logger.DebugFormat("test() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Upload Error, " + ex.Message, ex.Message);
            }
        }
        #endregion

        #region unused method

        #region create universal system
        //public async Task CreateUniversalSystem(CreateUniversalSystemInputDto input)
        //{
        //    Logger.InfoFormat("CreateUniversalSystem() - Started.");
        //    var unitCode = new GetUnitCodeDto();

        //    if (input.generateType == "highrise")
        //    {
        //        foreach (var item in input.unit)
        //        {
        //            // insert ms unit code
        //            var dtoUnitCode = new CreateUnitCodeInputDto();
        //            dtoUnitCode.floor = item.floorName;
        //            dtoUnitCode.generateType = input.generateType;


        //            // insert ms unitType
        //            var dtoUnitType = new CreateUnitTypeInputDto();
        //            dtoUnitType.unitTypeName = item.unitTypeName;
        //            dtoUnitType.facingID = item.facingID;
        //            dtoUnitType.zoningID = 1; //HARDCODE
        //            dtoUnitType.area = item.area;
        //            dtoUnitType.remarks = item.remarks;
        //            dtoUnitType.dueDate = item.dueDate;
        //            dtoUnitType.layout = item.layout;

        //            Logger.DebugFormat("CreateUniversalSystem() - Start checking existing unitCodeID, facingID, productID, unitStatusID, " +
        //                "projectID, unitNo. Parameters sent: {0} " +
        //                "unitCodeID         = {1}{0}" +
        //                "facingID           = {2}{0}" +
        //                "productID          = {3}{0}" +
        //                "unitStatusID       = {4}{0}" +
        //                "projectID          = {5}{0}" +
        //                "unitNo             = {6}{0}", Environment.NewLine, unitCode.unitCodeID, item.facingID, input.productID, input.unitStatusID
        //                , input.projectID, item.unitNo);
        //            var checkMsUnit = (from x in _msUnitRepo.GetAll()
        //                               where
        //                               x.unitCodeID == unitCode.unitCodeID &&
        //                               x.facingID == item.facingID &&
        //                               x.productID == input.productID &&
        //                               x.unitStatusID == input.unitStatusID &&
        //                               x.projectID == input.projectID &&
        //                               x.unitNo == item.unitNo
        //                               select x).Count();
        //            Logger.DebugFormat("CreateUniversalSystem() - End checking existing unitCodeID, facingID, productID, unitStatusID, " +
        //                "projectID, unitNo. Result = {0}", checkMsUnit);

        //            if (checkMsUnit == 0)
        //            {
        //                // insert ms unit
        //                var dtoUnit = new CreateUnitInputDto();
        //                dtoUnit.unitCodeID = unitCode.unitCodeID;
        //                dtoUnit.facingID = item.facingID;
        //                dtoUnit.zoningID = '-';
        //                dtoUnit.productID = input.productID;
        //                dtoUnit.unitStatusID = input.unitStatusID;
        //                dtoUnit.clusterID = '-';
        //                dtoUnit.projectID = input.projectID;
        //                dtoUnit.unitNo = item.unitNo;

        //                Logger.DebugFormat("CreateUniversalSystem() - Start Create MsUnit. Parameters sent: {0} " +
        //                    "unitCodeID      = {1}{0}   " +
        //                    "facingID        = {2}{0}   " +
        //                    "zoningID        = {3}{0}   " +
        //                    "productID       = {4}{0}   " +
        //                    "unitStatusID    = {5}{0}   " +
        //                    "clusterID       = {6}{0}   " +
        //                    "projectID       = {7}{0}   " +
        //                    "unitNo          = {8}{0}   ", Environment.NewLine, dtoUnit.unitCodeID, dtoUnit.facingID,
        //                    dtoUnit.zoningID, dtoUnit.productID, dtoUnit.unitStatusID, dtoUnit.clusterID, dtoUnit.projectID, dtoUnit.unitNo);
        //                var unitID = CreateMsUnit(dtoUnit);
        //                Logger.DebugFormat("CreateUniversalSystem() - End Create MsUnit. Result = {0}", unitID);


        //                // insert ms unitItem
        //                var dtoUnitItem = new CreateUnitItemInputDto();
        //                dtoUnitItem.unitID = unitID;
        //                dtoUnitItem.dimension = item.dimension;
        //                dtoUnitItem.unitItem = item.unitItem;

        //                Logger.DebugFormat("CreateUniversalSystem() - Start Create MsUnitItem. Parameters sent: {0} " +
        //                "unitID         = {1}{0}" +
        //                "dimension      = {2}{0}", Environment.NewLine, dtoUnitItem.unitID, item.dimension);
        //                await CreateMsUnitItem(dtoUnitItem).ConfigureAwait(false);
        //                Logger.DebugFormat("CreateUniversalSystem() - End Create MsUnitItem. Result = {0} ", unitID);
        //            }
        //        }
        //    }
        //    else if (input.generateType == "landed")
        //    {
        //        var cluster = new CreateClusterInputDto();
        //        var clusterInput = new CreateClusterInputDto
        //        {
        //            clusterCode = input.clusterCode,
        //            clusterName = input.clusterName,
        //            dueDateDevelopment = input.dueDateDevelopment,
        //            dueDateRemarks = input.dueDateRemarks,
        //            gracePeriod = input.gracePeriod,
        //            handOverPeriod = input.handOverPeriod,
        //            generateType = input.generateType
        //        };

        //        Logger.DebugFormat("CreateUniversalSystem() - Start Create MsCluster. Parameters sent: {0} " +
        //            "clusterCode        = {1}{0}" +
        //            "clusterName        = {2}{0}" +
        //            "dueDateDevelopment = {3}{0}" +
        //            "dueDateRemarks     = {4}{0}" +
        //            "gracePeriod        = {5}{0}" +
        //            "handOverPeriod     = {6}{0}" +
        //            "generateType       = {7}{0}", Environment.NewLine, clusterInput.clusterCode, clusterInput.clusterName, clusterInput.dueDateDevelopment,
        //            clusterInput.dueDateRemarks, clusterInput.gracePeriod, clusterInput.handOverPeriod, clusterInput.generateType);
        //        cluster = CreateMsCluster(clusterInput);
        //        Logger.DebugFormat("CreateUniversalSystem() - End Create MsCluster. Result = {0}", cluster);

        //        foreach (var item in input.unit)
        //        {
        //            // insert ms unit code
        //            var dtoUnitCode = new CreateUnitCodeInputDto();
        //            dtoUnitCode.clusterCode = cluster.clusterCode;
        //            dtoUnitCode.generateType = input.generateType;
        //            dtoUnitCode.roadName = item.roadName;

        //            Logger.DebugFormat("CreateUniversalSystem() - Start checking existing unitCodeID, facingID, productID, unitStatusID, " +
        //                "projectID, unitNo, clusterID. Parameters sent: {0} " +
        //                "unitCodeID     = {1}{0}" +
        //                "facingID       = {2}{0}" +
        //                "productID      = {3}{0}" +
        //                "unitStatusID   = {4}{0}" +
        //                "projectID      = {5}{0}" +
        //                "unitNo         = {6}{0}" +
        //                "clusterID      = {7}{0}", Environment.NewLine, unitCode.unitCodeID, item.facingID, input.productID, input.unitStatusID
        //                , input.projectID, item.unitNo, cluster.clusterID);
        //            var checkMsUnit = (from x in _msUnitRepo.GetAll()
        //                               where
        //                               x.clusterID == cluster.clusterID &&
        //                               x.unitCodeID == unitCode.unitCodeID &&
        //                               x.facingID == item.facingID &&
        //                               x.productID == input.productID &&
        //                               x.unitStatusID == input.unitStatusID &&
        //                               x.projectID == input.projectID &&
        //                               x.unitNo == item.unitNo
        //                               select x).Count();
        //            Logger.DebugFormat("CreateUniversalSystem() - End checking existing unitCodeID, facingID, productID, unitStatusID, " +
        //                "projectID, unitNo, clusterID. Result = {0}", checkMsUnit);

        //            if (checkMsUnit == 0)
        //            {
        //                // insert ms unit
        //                var dtoUnit = new CreateUnitInputDto();
        //                dtoUnit.clusterID = cluster.clusterID;
        //                dtoUnit.unitCodeID = unitCode.unitCodeID;
        //                dtoUnit.facingID = item.facingID;
        //                dtoUnit.zoningID = '-';
        //                dtoUnit.productID = input.productID;
        //                dtoUnit.unitStatusID = input.unitStatusID;
        //                dtoUnit.projectID = input.projectID;
        //                dtoUnit.unitNo = item.unitNo;

        //                Logger.DebugFormat("CreateUniversalSystem() - Start Create MsUnit. Parameters sent: {0} " +
        //                    "unitCodeID      = {1}{0}" +
        //                    "facingID        = {2}{0}" +
        //                    "zoningID        = {3}{0}" +
        //                    "productID       = {4}{0}" +
        //                    "unitStatusID    = {5}{0}" +
        //                    "clusterID       = {6}{0}" +
        //                    "projectID       = {7}{0}" +
        //                    "unitNo          = {8}{0}", Environment.NewLine, dtoUnit.unitCodeID, dtoUnit.facingID,
        //                    dtoUnit.zoningID, dtoUnit.productID, dtoUnit.unitStatusID, dtoUnit.clusterID, dtoUnit.projectID, dtoUnit.unitNo);
        //                var unitID = CreateMsUnit(dtoUnit);
        //                Logger.DebugFormat("CreateUniversalSystem() - End Create MsUnit. Result = {0}", unitID);

        //                // insert ms unitItem
        //                var dtoUnitItem = new CreateUnitItemInputDto();
        //                dtoUnitItem.unitID = unitID;
        //                Logger.DebugFormat("CreateUniversalSystem() - Start Create MsUnitItem. Parameters sent: {0} " +
        //                    "unitID         = {1}{0}   " +
        //                    "dimension      = {2}{0}   " +
        //                    "unitCode       = {3}{0}   " +
        //                    "unitNo         = {4}{0}   ", Environment.NewLine, dtoUnitItem.unitID, dtoUnitItem.dimension);
        //                await CreateMsUnitItem(dtoUnitItem).ConfigureAwait(false);
        //                Logger.DebugFormat("CreateUniversalSystem() - End Create MsUnitItem.");
        //            }
        //        }
        //    }
        //    Logger.InfoFormat("CreateUniversalSystem() - Finished.");
        //}
        #endregion

        public ListResultDto<GetProjectDropdownListDto> GetMsProjectDropdown()//passed
        {
            var getData = (from A in _msProjectRepo.GetAll()
                           select new GetProjectDropdownListDto
                           {
                               projectID = A.Id,
                               projectCode = A.projectCode,
                               projectName = A.projectName
                           }).ToList();


            return new ListResultDto<GetProjectDropdownListDto>(getData);
        }
        public ListResultDto<GetFacingDropdownListDto> GetMsFacingDropdown()//passed
        {
            var getData = (from A in _lkFacingRepo.GetAll()
                           orderby A.Id descending
                           select new GetFacingDropdownListDto
                           {
                               facingID = A.Id,
                               facingName = A.facingName
                           }).ToList();

            return new ListResultDto<GetFacingDropdownListDto>(getData);
        }

        public ListResultDto<GetZoningDropdownListDto> GetMsZoningDropdown()//passed
        {
            var getData = (from A in _msZoningRepo.GetAll()
                           orderby A.Id descending
                           select new GetZoningDropdownListDto
                           {
                               zoningID = A.Id,
                               zoningName = A.zoningName
                           }).ToList();

            return new ListResultDto<GetZoningDropdownListDto>(getData);
        }

        public int CreateMsUnitCodeForExcel(string unitCode)//passed
        {
            Logger.InfoFormat("CreateMsUnitCodeForExcel() - Started.");
            var unitCodeId = 0;
            Logger.DebugFormat("CreateMsUnitCodeForExcel() - Start checking existing unitCode. Parameters sent: {0} " +
                "unitCode = {1}{0}", Environment.NewLine, unitCode);
            var checkUnitCode = (from x in _msUnitCodeRepo.GetAll()
                                 where x.unitCode == unitCode.ToUpper()
                                 select x);
            Logger.DebugFormat("CreateMsUnitCodeForExcel() - End checking existing unitCode. Result = {0}", checkUnitCode);

            if (checkUnitCode.Count() == 0)
            {
                var data = new MS_UnitCode
                {
                    entityID = 1,
                    unitCode = unitCode.ToUpper(),
                    unitName = unitCode.ToUpper()
                };
                Logger.DebugFormat("CreateMsUnitCodeForExcel() - Start Insert to msUnit. Parameters sent: {0} " +
                    "entityID = {1}{0}" +
                    "unitCode = {2}{0}" +
                    "unitName = {3}{0}"
                    , Environment.NewLine, data.entityID, data.unitCode, data.unitName);
                unitCodeId = _msUnitCodeRepo.InsertAndGetId(data);
                Logger.DebugFormat("CreateMsUnitCodeForExcel() - End Insert to msUnit");
            }
            else
            {
                Logger.DebugFormat("CreateMsUnitCodeForExcel() - Start get first unitCode");
                unitCodeId = checkUnitCode.FirstOrDefault().Id;
                Logger.DebugFormat("CreateMsUnitCodeForExcel() - End get first unitCode. Result = {0}", unitCodeId);
            }
            Logger.InfoFormat("CreateMsUnitCodeForExcel() - Finised.");
            return unitCodeId;
        }
        public ListResultDto<GetUnavailableUnitNoListDto> GetUnavailableUnitNo(List<GetUnavailableUnitNoInputDto> inputs)//passed
        {
            //BEFORE waktu estimasi sebesar 2693 ms, 2336 ms
            //AFTER waktu estimasi sebesar 23 ms, 27 ms

            if (inputs.Any())
            {
                var notInData = (from A in inputs
                                 where !(
                                 (
                                  from mu in _msUnitRepo.GetAll()
                                  join muc in _msUnitCodeRepo.GetAll() on mu.unitCodeID equals muc.Id
                                  where muc.unitCode == A.unitCode && mu.unitNo == A.unitNo
                                  select new GetUnavailableUnitNoListDto
                                  {
                                      unitCode = muc.unitCode,
                                      unitNo = mu.unitNo
                                  }).ToList()
                                  ).Any()
                                 select new GetUnavailableUnitNoListDto
                                 {
                                     unitNo = A.unitNo,
                                     unitCode = A.unitCode
                                 }).ToList();

                return new ListResultDto<GetUnavailableUnitNoListDto>(notInData);
            }
            else
            {
                throw new UserFriendlyException("Input cannot be null !");
            }
        }

        public ListResultDto<GetUnitByFloorListDto> GetUnitByFloor(GetUnitByFloorInputDto input)//passed
        {
            var getData = (from unitcode in _msUnitCodeRepo.GetAll()
                           join unit in _msUnitRepo.GetAll() on unitcode.Id equals unit.unitCodeID
                           where unitcode.Id == input.unitCodeID
                           select new GetUnitByFloorListDto
                           {
                               unitCode = unitcode.unitCode,
                               unitNo = unit.unitNo
                           }).ToList();

            return new ListResultDto<GetUnitByFloorListDto>(getData);
        }

        [HttpPost]
        public ListResultDto<GetProjectCodeByUnitCodeUnitNoListDto> GetProductCodeByUnitCodeUnitNo(List<GetProductCodeByUnitCodeUnitNoDto> input)
        {
            List<GetProjectCodeByUnitCodeUnitNoListDto> result = new List<GetProjectCodeByUnitCodeUnitNoListDto>();
            foreach (var unit in input)
            {
                var dataProject = (from A in _msProductRepo.GetAll()
                                   join B in _msUnitRepo.GetAll() on A.Id equals B.productID
                                   join C in _msUnitCodeRepo.GetAll() on B.unitCodeID equals C.Id
                                   where C.unitCode == unit.unitCode && B.unitNo == unit.unitNo
                                   select new GetProjectCodeByUnitCodeUnitNoListDto
                                   {
                                       no = unit.no,
                                       unitCode = C.unitCode,
                                       unitNo = B.unitNo,
                                       productCode = A.productCode
                                   }).FirstOrDefault();
                result.Add(dataProject);
            }

            return new ListResultDto<GetProjectCodeByUnitCodeUnitNoListDto>(result);

        }

        public ListResultDto<GetUnitByProjectCategoryListDto> GetUnitByProjectCategory(GetUnitByProjectCategoryInput input)//passed
        {
            //BEFORE lambat jika data banyak, waktu estimasi sebesar 517164 ms
            //AFTER optimasi, waktu estimasi menjadi 4178 ms

            var listUnitBig = (from mu in _contextProp.MS_Unit
                               join lus in _contextProp.LK_UnitStatus on mu.unitStatusID equals lus.Id
                               join muc in _contextProp.MS_UnitCode on mu.unitCodeID equals muc.Id
                               join mucc in (
                                   from mu0 in _contextProp.MS_Unit
                                   join muc0 in _contextProp.MS_UnitCode on mu0.unitCodeID equals muc0.Id
                                   where mu0.projectID == input.projectID && mu0.categoryID == input.categoryID

                                   select new { muc0.unitCode, mu0.projectID, mu0.categoryID }
                                   ).Distinct().ToList()
                                   on muc.unitCode equals mucc.unitCode
                               where mu.projectID == mucc.projectID && mu.categoryID == mucc.categoryID
                               orderby mucc.unitCode ascending
                               select new { mu.Id, mu.unitNo, mu.unitStatusID, lus.unitStatusCode, mucc.unitCode }).ToList();

            if (listUnitBig.Any())
            {
                List<GetUnitByProjectCategoryListDto> listResult = new List<GetUnitByProjectCategoryListDto>();
                List<UnitStatus> unitList = null;
                try
                {
                    string tmpUnitcode = "";
                    bool isactive = false;
                    foreach (var floor in listUnitBig)
                    {
                        if (!tmpUnitcode.Equals(floor.unitCode))
                        {
                            if (isactive == true)
                            {
                                var dataUnit = new GetUnitByProjectCategoryListDto();
                                dataUnit.floor = tmpUnitcode.Substring(tmpUnitcode.Length - 2);
                                dataUnit.unit = new List<UnitStatus>();
                                dataUnit.unit.AddRange(unitList);
                                listResult.Add(dataUnit);
                            }
                            else
                            {
                                isactive = true;
                            }

                            unitList = new List<UnitStatus>();
                        }

                        var unitStatus = new UnitStatus
                        {
                            unitID = floor.Id,
                            unitNo = floor.unitNo,
                            unitStatusID = floor.unitStatusID,
                            unitStatusCode = floor.unitStatusCode
                        };
                        unitList.Add(unitStatus);

                        unitStatus = null;
                        tmpUnitcode = floor.unitCode;
                    }
                    if (unitList.Count > 0)
                    {
                        var dataUnit = new GetUnitByProjectCategoryListDto();
                        dataUnit.floor = tmpUnitcode.Substring(tmpUnitcode.Length - 2);
                        dataUnit.unit = new List<UnitStatus>();
                        dataUnit.unit.AddRange(unitList);
                        listResult.Add(dataUnit);
                    }
                }
                catch (Exception e)
                {
                    SendConsole("" + e.Message + " " + e.StackTrace);
                }

                return new ListResultDto<GetUnitByProjectCategoryListDto>(listResult);
            }
            else
            {
                throw new UserFriendlyException("No Data Unit!");
            }
        }

        public ListResultDto<GetUnitCodeByProjectListDto> GetUnitCodeByProject(int projectId)//passed
        {
            var getData = (from A in _msUnitRepo.GetAll()
                           join y in _msUnitCodeRepo.GetAll() on A.unitCodeID equals y.Id
                           where A.projectID == projectId
                           orderby A.Id descending
                           select new GetUnitCodeByProjectListDto
                           {
                               unitCode = y.unitCode
                           }).Distinct().ToList();

            return new ListResultDto<GetUnitCodeByProjectListDto>(getData);
        }

        public ListResultDto<GetUnitNoByUnitCodeListDto> GetUnitNoByUnitCode(string unitCode)//passed
        {
            var getData = (from A in _msUnitRepo.GetAll()
                           join y in _msUnitCodeRepo.GetAll() on A.unitCodeID equals y.Id
                           where y.unitCode == unitCode
                           orderby A.Id descending
                           select new GetUnitNoByUnitCodeListDto
                           {
                               id = A.Id,
                               unitNo = A.unitNo
                           }).Distinct().ToList();

            return new ListResultDto<GetUnitNoByUnitCodeListDto>(getData);
        }

        public ListResultDto<GetFloorByClusterListDto> GetFloorByCluster(int clusterID)//passed
        {
            var getData = (from A in _msUnitRepo.GetAll()
                           join y in _msUnitCodeRepo.GetAll() on A.unitCodeID equals y.Id
                           where A.clusterID == clusterID
                           orderby A.Id descending
                           select new GetFloorByClusterListDto
                           {
                               unitCodeID = A.unitCodeID,
                               floor = y.unitCode.Substring(y.unitCode.Length - 2)
                           }).Distinct().ToList();

            return new ListResultDto<GetFloorByClusterListDto>(getData);
        }

        public ListResultDto<GetUnitByClusterListDto> GetUnitByCluster(int clusterID)//passed
        {
            var getUnit = (from A in _msUnitRepo.GetAll()
                           join y in _msUnitCodeRepo.GetAll() on A.unitCodeID equals y.Id
                           join z in _lkFacingRepo.GetAll() on A.facingID equals z.Id
                           where A.clusterID == clusterID
                           select new GetUnitByClusterListDto
                           {
                               unitID = A.Id,
                               unitCodeID = A.unitCodeID,
                               zoningID = A.zoningID,
                               facingID = A.facingID,
                               unitNo = A.unitNo,
                               unitCode = y.unitCode,
                               facingName = z.facingName
                           }).ToList();

            return new ListResultDto<GetUnitByClusterListDto>(getUnit);
        }

        public ListResultDto<GetUnitByProjectClusterTermCodeTermNoDto> GetUnitByProjectClusterTermCodeTermNo(int projectID, int clusterID, string termCode, short termNo)//passed
        {
            var getData = (from x in _msUnitRepo.GetAll()
                           join y in _msTermRepo.GetAll() on x.Id equals y.Id
                           where x.projectID == projectID && x.clusterID == clusterID
                           && y.termCode == termCode && y.termNo == termNo
                           select new GetUnitByProjectClusterTermCodeTermNoDto
                           {
                               unitNo = x.unitNo,
                               unitCodeID = x.unitCodeID
                           }).ToList();
            return new ListResultDto<GetUnitByProjectClusterTermCodeTermNoDto>(getData);
        }

        public ListResultDto<GetUnitDropdownListDto> GetUnitByClusterProjectDropdown(List<int> clusterID)
        {
            var getData = (from x in _msUnitRepo.GetAll()
                           where clusterID.Contains(x.clusterID)
                           select new GetUnitDropdownListDto
                           {
                               unitNo = x.unitNo,
                               unitID = x.Id
                           }).ToList();
            return new ListResultDto<GetUnitDropdownListDto>(getData);
        }

        #region debug console
        protected void SendConsole(string msg)
        {
            if (Setting_variabel.enable_tcp_debug == true)
            {
                if (Setting_variabel.Komunikasi_TCPListener == null)
                {
                    Setting_variabel.Komunikasi_TCPListener = new Visionet_Backend_NetCore.Komunikasi.Komunikasi_TCPListener(17000);
                    Task.Run(() => StartListenerLokal());
                }

                if (Setting_variabel.Komunikasi_TCPListener != null)
                {
                    if (Setting_variabel.Komunikasi_TCPListener.IsRunning())
                    {
                        if (Setting_variabel.ConsoleBayangan != null)
                        {
                            Setting_variabel.ConsoleBayangan.Send("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "] " + msg);
                        }
                    }
                }
            }

        }
        #endregion

        #region listener tcp debug
        protected void StartListenerLokal()
        {
            if (Setting_variabel.Komunikasi_TCPListener != null)
                Setting_variabel.Komunikasi_TCPListener.StartListener();
        }

        protected void StopListenerLokal()
        {
            if (Setting_variabel.Komunikasi_TCPListener != null)
                Setting_variabel.Komunikasi_TCPListener.StopListener();
        }
        
        #endregion

        #endregion
    }
}
