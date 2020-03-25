using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace StatementSystem.UI.Models
{
    public class FilterModel
    {
        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? StatusCode { get; set; }

        public OrderState Order { get; set; }

        public Func<StatementModel, bool> FilterExp
        { 
            get
            {
                return statement =>
                    (!BeginDate.HasValue || statement.CreateDate >= BeginDate.Value)
                    && (!EndDate.HasValue || statement.CreateDate <= EndDate.Value.AddDays(1).AddSeconds(-1))
                    && (!StatusCode.HasValue || statement.CurrentStatus.StatusCode == StatusCode);
            } 
        }

        public List<SelectListItem> StatusItems
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem("Все","all"),
                    new SelectListItem("Открыта","0"),
                    new SelectListItem("Решена","1"),
                    new SelectListItem("Возврат","2"),
                    new SelectListItem("Закрыта","3")
                };
            }
        }

        public List<SelectListItem> OrderItems
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem("убыванию даты",OrderState.CreateDateDesc.ToString()),
                    new SelectListItem("возрастанию даты",OrderState.CreateDateAsc.ToString()),
                    new SelectListItem("убыванию статуса",OrderState.StatusDesc.ToString()),
                    new SelectListItem("возрастанию статуса",OrderState.StatusAsc.ToString())
                };
            }
        }
    }
}
