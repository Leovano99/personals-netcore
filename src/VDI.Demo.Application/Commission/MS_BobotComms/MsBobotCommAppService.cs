using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using System.Linq;
using VDI.Demo.Authorization;
using VDI.Demo.Commission.MS_BobotComms.Dto;
using VDI.Demo.NewCommDB;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using Visionet_Backend_NetCore.Komunikasi;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VDI.Demo.EntityFrameworkCore;

namespace VDI.Demo.Commission.MS_BobotComms
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterBobotCommission)]
    public class MsBobotCommAppService : DemoAppServiceBase, IMsBobotCommAppService
    {
        private readonly IRepository<MS_BobotComm> _msBobotCommRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;
        private readonly IRepository<MS_Cluster> _msClusterRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly IRepository<MS_UnitCode> _msUnitCodeRepo;
        private readonly NewCommDbContext _context;
        private readonly PropertySystemDbContext _contextProp;

        public MsBobotCommAppService(
            IRepository<MS_BobotComm> msBobotCommRepo,
            IRepository<MS_Project> msProjectRepo,
            IRepository<MS_Cluster> msClusterRepo,
            IRepository<MS_Unit> msUnitRepo,
            IRepository<MS_UnitCode> msUnitCodeRepo,
            NewCommDbContext context,
            PropertySystemDbContext contextProp
        )
        {
            _msBobotCommRepo = msBobotCommRepo;
            _msProjectRepo = msProjectRepo;
            _msClusterRepo = msClusterRepo;
            _msUnitRepo = msUnitRepo;
            _msUnitCodeRepo = msUnitCodeRepo;
            _context = context;
            _contextProp = contextProp;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterBobotCommission_Create)]
        public void CreateMsBobotComm(List<MsBobotCommListDto> input)
        {
            Logger.InfoFormat("CreateMsBobotComm() Started.");

            Logger.DebugFormat("CreateMsBobotComm() - Foreach input Started.");
            foreach (var item in input)
            {

                Logger.DebugFormat("CreateMsBobotComm() - Start checking existing projectID, clusterID and schemaID. Parameters sent: {0} " +
                    "   projectID = {1}{0}" +
                    "   clusterID = {2}{0}" +
                    "   schemaID = {3}"
                    , Environment.NewLine, item.projectID, item.clusterID, item.schemaID);

                var cekExistingData = (from bobotComm in _msBobotCommRepo.GetAll()
                                       where bobotComm.projectID == item.projectID && bobotComm.clusterID == item.clusterID
                                       && bobotComm.schemaID == item.schemaID && bobotComm.isComplete == true && bobotComm.isActive == true
                                       select bobotComm).Any();

                Logger.DebugFormat("CreateMsBobotComm() - End checking existing projectID, clusterID and schemaID. Result = {0}", cekExistingData);

                if (!cekExistingData)
                {
                    var createBobotComm = new MS_BobotComm
                    {
                        clusterID = item.clusterID,
                        entityID = 1,
                        projectID = item.projectID,
                        schemaID = item.schemaID,
                        pctBobot = item.pctBobot,
                        isActive = item.isActive,
                        isComplete = true
                    };

                    try
                    {
                        Logger.DebugFormat("CreateMsBobotComm() - Start insert bobotComm. Parameters sent: {0} " +
                    "   clusterID = {1}{0}" +
                    "   entityID = {2}{0}" +
                    "   projectID = {3}{0}" +
                    "   schemaID = {4}{0}" +
                    "   pctBobot = {5}{0}" +
                    "   isActive = {6}{0}" +
                    "   isComplete = {7}"
                    , Environment.NewLine, item.clusterID, 1, item.projectID, item.schemaID, item.pctBobot, item.isActive, true);

                        _msBobotCommRepo.Insert(createBobotComm);
                        CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                        Logger.DebugFormat("CreateMsBobotComm() - End insert bobotComm.");
                    }
                    catch (DataException ex)
                    {
                        Logger.DebugFormat("CreateMsBobotComm() - ERROR DataException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Db Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.DebugFormat("CreateMsBobotComm() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }
                }
                else
                {
                    Logger.DebugFormat("CreateMsBobotComm() - ERROR. Result = {0}", "The Cluster Already Exist For the current project and schema!");
                    throw new UserFriendlyException("The Cluster Already Exist For the current project and schema!");
                }
            }

            Logger.InfoFormat("CreateMsBobotComm() - Foreach input Ended.");
            Logger.InfoFormat("CreateMsBobotComm() - Finished.");
        }

        public void DeleteMsBobotComm(int Id)
        {
            Logger.InfoFormat("DeleteMsBobotComm() Started.");

            Logger.DebugFormat("DeleteMsBobotComm() - Start get data bobotComm. Parameters sent: {0} " +
                    "   bobotCommID = {1}{0}"
                    , Environment.NewLine, Id);
            var getBobotComm = (from bobotComm in _msBobotCommRepo.GetAll()
                                where Id == bobotComm.Id
                                select bobotComm).FirstOrDefault();
            var updateBobotComm = getBobotComm.MapTo<MS_BobotComm>();
            updateBobotComm.isComplete = false;
            Logger.DebugFormat("DeleteMsBobotComm() - End Start get data bobotComm. Result = {0}", updateBobotComm);

            try
            {
                Logger.DebugFormat("DeleteMsBobotComm() - Start Update bobotComm. Parameters sent: {0} " +
                "   bobotCommID = {1}{0}" +
                "   isComplete = {2}{0}"
                , Environment.NewLine, Id, false);
                _msBobotCommRepo.Update(updateBobotComm);
                CurrentUnitOfWork.SaveChanges();
                Logger.DebugFormat("DeleteMsBobotComm() - End Update bobotComm.");
            }
            catch (DataException ex)
            {
                Logger.DebugFormat("DeleteMsBobotComm() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.DebugFormat("DeleteMsBobotComm() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
            Logger.InfoFormat("DeleteMsBobotComm() - Finished.");
        }

        [UnitOfWork(isTransactional: false)]
        public ListResultDto<MsBobotCommListDto> GetMsBobotCommByProject(int projectID) //passed
        {
            List<MsBobotCommListDto> listResult = new List<MsBobotCommListDto>();
            try
            {
                #region test only without msdtc - change it if you want       

                #region non compact version
                /*
                var dataBobot = (from bobotComm in _context.MS_BobotComm
                                 join scm in _context.MS_Schema on bobotComm.schemaID equals scm.Id
                                 where bobotComm.projectID == projectID && bobotComm.isComplete == true
                                 select new MsBobotCommListDto
                                 {
                                     Id = bobotComm.Id,
                                     entityID = bobotComm.entityID,
                                     projectID = bobotComm.projectID,
                                     schemaID = bobotComm.schemaID,
                                     clusterID = bobotComm.clusterID,
                                     pctBobot = bobotComm.pctBobot,
                                     isActive = bobotComm.isActive,
                                     isComplete = bobotComm.isComplete,
                                     scmCode = (scm.scmCode ?? "")
                                 }).ToList();
                
                
                listResult = (from resultBobot in dataBobot.ToList()                             
                              join mp in _contextProp.MS_Project on resultBobot.projectID equals mp.Id
                              join mc in _contextProp.MS_Cluster on resultBobot.clusterID equals mc.Id into res
                              from resultBobotMc in res.DefaultIfEmpty()
                              select new MsBobotCommListDto
                              {
                                  Id = resultBobot.Id,
                                  scmCode = (resultBobot==null || resultBobot.scmCode == null ? null : resultBobot.scmCode),
                                  projectCode = (mp==null || mp.projectCode == null ? null : mp.projectCode),
                                  clusterCode = (resultBobotMc==null || resultBobotMc.clusterCode == null ? null : resultBobotMc.clusterCode),
                                  entityID = resultBobot.entityID,
                                  projectID = resultBobot.projectID,
                                  schemaID = resultBobot.schemaID,
                                  clusterID = resultBobot.clusterID,
                                  pctBobot = resultBobot.pctBobot,
                                  isActive = resultBobot.isActive,
                                  isComplete = resultBobot.isComplete,
                                  clusterName = (resultBobotMc==null || resultBobotMc.clusterName== null ? null : resultBobotMc.clusterName),
                                  projectName = (mp==null || mp.projectName == null ? null : mp.projectName)
                              }
                              ).OrderByDescending(a=>a.Id).ToList();
                              */
                #endregion

                listResult = (from resultBobot in _context.MS_BobotComm.ToList()
                              join scm in _context.MS_Schema.ToList() on resultBobot.schemaID equals scm.Id
                              join mp in _contextProp.MS_Project on resultBobot.projectID equals mp.Id
                              join mc in _contextProp.MS_Cluster on resultBobot.clusterID equals mc.Id into res
                              from resultBobotMc in res.DefaultIfEmpty()
                              where resultBobot.projectID == projectID && resultBobot.isComplete == true
                              select new MsBobotCommListDto
                              {
                                  Id = resultBobot.Id,
                                  scmCode = (scm == null || scm.scmCode == null ? null : scm.scmCode),
                                  projectCode = (mp == null || mp.projectCode == null ? null : mp.projectCode),
                                  clusterCode = (resultBobotMc == null || resultBobotMc.clusterCode == null ? null : resultBobotMc.clusterCode),
                                  entityID = resultBobot.entityID,
                                  projectID = resultBobot.projectID,
                                  schemaID = resultBobot.schemaID,
                                  clusterID = resultBobot.clusterID,
                                  pctBobot = resultBobot.pctBobot,
                                  isActive = resultBobot.isActive,
                                  isComplete = resultBobot.isComplete,
                                  clusterName = (resultBobotMc == null || resultBobotMc.clusterName == null ? null : resultBobotMc.clusterName),
                                  projectName = (mp == null || mp.projectName == null ? null : mp.projectName)
                              }
                              ).OrderByDescending(a => a.Id).ToList();

                #endregion

            }
            catch (Exception e)
            {
                SendConsole("" + e.Message + " " + e.StackTrace);
            }

            return new ListResultDto<MsBobotCommListDto>(listResult);
        }

        public void UpdateMsBobotComm(MsBobotCommListDto input)
        {
            Logger.InfoFormat("UpdateMsBobotComm() Started.");

            Logger.DebugFormat("UpdateMsBobotComm() - Start get data for update. Parameters sent: {0} " +
                "   bobotCommID = {1}{0}"
                , Environment.NewLine, input.Id);
            var getMsBobotComm = (from bobotComm in _msBobotCommRepo.GetAll()
                                  where input.Id == bobotComm.Id
                                  select bobotComm).FirstOrDefault();

            var updateBobotComm = getMsBobotComm.MapTo<MS_BobotComm>();

            updateBobotComm.clusterID = input.clusterID;
            updateBobotComm.pctBobot = input.pctBobot;
            updateBobotComm.isActive = input.isActive;
            Logger.DebugFormat("UpdateMsBobotComm() - End get data for update. Result = {0}", updateBobotComm);

            try
            {
                Logger.DebugFormat("UpdateMsBobotComm() - Start update bobotComm. Parameters sent: {0} " +
                    "   clusterID = {1}{0}" +
                    "   pctBobot = {2}{0}" +
                    "   isActive = {3}{0}"
                    , Environment.NewLine, input.clusterID, input.pctBobot, input.isActive);
                _msBobotCommRepo.Update(updateBobotComm);
                CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                Logger.DebugFormat("UpdateMsBobotComm() - End update bobotComm.");

            }
            catch (DataException ex)
            {
                Logger.DebugFormat("UpdateMsBobotComm() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.DebugFormat("UpdateMsBobotComm() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
            Logger.InfoFormat("UpdateMsBobotComm() - Finished.");
        }


        #region debug console
        public void SendConsole(string msg)
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

    }
}
