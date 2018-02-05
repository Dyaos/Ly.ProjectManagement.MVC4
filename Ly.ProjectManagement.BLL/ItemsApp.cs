using Ly.ProjectManagement.Data.Application;
using Ly.ProjectManagement.IBLL;
using Ly.ProjectManagement.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ly.ProjectManagement.Model;
using Ly.ProjectManagement.Code;

namespace Ly.ProjectManagement.BLL
{
    public class ItemsApp : BaseApplication, IItemsApp
    {
        public ItemsApp(IItemsRepository repository)
        {
            CurrentRepository = repository;
        }

        public void DeleteForm(string keyVlaue)
        {
            (CurrentRepository as IItemsRepository).DeleteForm(keyVlaue);
        }

        public void SubmitForm(SysItems entity, string keyValue)
        {
            var cloneEntity = entity;
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.itemGuid = Common.GuId();
                entity.createTime = DateTime.Now;
                entity.isDel = false;
                entity.createUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
            }
            else
            {
                entity = FindEntity<SysItems>(t => t.itemGuid == keyValue);
                entity.itemName = cloneEntity.itemName;
                entity.isEnabled = cloneEntity.isEnabled;
                entity.itemEnCode = cloneEntity.itemEnCode;
                entity.remarks = cloneEntity.remarks;
                entity.lastUpdateTime = DateTime.Now;
                entity.lastUpdateUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
            }
            (CurrentRepository as IItemsRepository).SubmitForm(entity, keyValue);

        }
    }
}