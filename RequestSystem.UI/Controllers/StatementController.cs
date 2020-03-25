using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.BaseEntity;
using Domain.Services;
using Domain.Statements;
using Domain.Statements.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using StatementSystem.UI.Models;

namespace RequestSystem.UI.Controllers
{
    public class StatementController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(
            [FromServices] IStatementRepository repository,
            [FromQuery] FilterModel filter)
        {
            var statements = await repository.GetAll();

            var statementsModel = new List<StatementModel>();
            foreach(var statement in statements)
            {
                statementsModel.Add(new StatementModel(statement));
            }

            var filteredList = statementsModel
                .Where(filter.FilterExp)
                .ToList();

            var orderedList = GetOrderedList(filter.Order, filteredList);

            var model = new StatementIndexView()
            {
                Statements = orderedList,
                Filter = filter
            };

            return View(model);
        }

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(
            [FromServices] IStatementRepository repository,
            [FromRoute] Guid id)
        {
            var statement = await repository.GetStatement(new PrimaryKey(id)) as Statement;

            var model = new StatementModel(statement);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            [FromServices] IStatementService statementService,
            [FromForm] StatementModel input)
        {
            var statement = await statementService.ChangeStatementTitle(
                new PrimaryKey(input.StatementId),
                new StatementTitle(input.StatementTitle));

            var model = new StatementModel(statement);
            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromServices] IStatementService statementService,
            [FromForm] StatementModel input)
        {
            await statementService.CreateStatement(
                new StatementTitle(input.StatementTitle));
            return RedirectToAction("Index");
        }
        #endregion


        [HttpGet]
        public async Task<IActionResult> Delete(
            [FromServices] IStatementService statementService,
            [FromRoute] Guid id)
        {
            await statementService.DeleteStatement(new PrimaryKey(id));
            return RedirectToAction("Index");
        }

        private List<StatementModel> GetOrderedList(
            OrderState order, 
            List<StatementModel> statements)
        {
            switch(order)
            {
                case OrderState.CreateDateDesc: return statements.OrderByDescending(x => x.CreateDate).ToList();
                case OrderState.CreateDateAsc: return statements.OrderBy(x => x.CreateDate).ToList();
                case OrderState.StatusDesc: return statements.OrderByDescending(x => x.CurrentStatus.StatusCode).ToList();
                case OrderState.StatusAsc: return statements.OrderBy(x => x.CurrentStatus.StatusCode).ToList();
            }

            return statements;
        }
    }
}
