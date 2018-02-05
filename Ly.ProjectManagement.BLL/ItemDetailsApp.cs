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
using System.Collections.ObjectModel;
using System.Collections;

namespace Ly.ProjectManagement.BLL
{
    public class ItemDetailsApp : BaseApplication, IItemDetailsApp
    {
        public ItemDetailsApp(IItemDetailsRepository repository)
        {
            CurrentRepository = repository;
        }

        public void DeleteForm(string keyVlaue)
        {
            (CurrentRepository as IItemDetailsRepository).DeleteForm(keyVlaue);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="itemGuid"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SysItemDetails> GetItemDetailsList(string itemGuid="", string keyword="")
        {
            var expression = ExtLinq.True<SysItemDetails>();

            if (!string.IsNullOrEmpty(itemGuid))
            {
                expression = expression.And(t => t.isDel == false);
                expression = expression.And(t => t.itemGuid == itemGuid);
            }
            else
            {
                return new List<SysItemDetails>();
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.detailName.Contains(keyword));
                expression = expression.Or(t => t.detailEncode.Contains(keyword));
            }
            return CurrentRepository.IQueryable<SysItemDetails>(expression).OrderByDescending(t => t.createTime).ToList();
        }

        public void SubmitForm(SysItemDetails entity, string keyValue)
        {
            var cloneEntity = entity;
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.detailGuid = Common.GuId();
                entity.createTime = DateTime.Now;
                entity.isDel = false;
                entity.createUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
            }
            else
            {
                entity = FindEntity<SysItemDetails>(t => t.detailGuid == keyValue);
                entity.detailName = cloneEntity.detailName;
                entity.isEnabled = cloneEntity.isEnabled;
                entity.detailEncode = cloneEntity.detailEncode;
                entity.remarks = cloneEntity.remarks;
                entity.lastUpdateTime = DateTime.Now;
                entity.lastUpdateUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
            }
             (CurrentRepository as IItemDetailsRepository).SubmitForm(entity, keyValue);
        }
    }
}