using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.BaseEntity;
using Domain.Services;
using Domain.Statements;
using Domain.Statements.Status.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StatementSystem.UI.Models;

namespace StatementSystem.UI.Controllers
{
    public class StatusController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ChangeStatus(
            [FromServices] IStatementRepository statementRepository,
            [FromRoute] Guid id)
        {
            var statement = await statementRepository.GetStatement(new PrimaryKey(id));
            var currentStatusCode = (int)((Statement)statement).CurrentStatus.StatusCode;
            ViewBag.StatusItems = GetStatusItems(currentStatusCode);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(
            [FromServices] IStatementService statementService,
            [FromForm] StatusModel input)
        {
            await statementService.ChangeStatementStatus(
                new PrimaryKey(input.StatementId),
                (StatusCode)input.StatusCode,
                new StatusComment(input.Comment));

            return RedirectToAction("Edit", "Statement", new { id = input.StatementId });
        }

        private List<SelectListItem> GetStatusItems(int code) => code switch
        {
            0 => new List<SelectListItem>
            {
                new SelectListItem("Решена","1")
            },
            1 => new List<SelectListItem>
            {
                new SelectListItem("Возврат","2"),
                new SelectListItem("Закрыта","3")
            },
            2 => new List<SelectListItem>
            {
                new SelectListItem("Решена","1")
            },
            _ => new List<SelectListItem>()
        };
    }
}