using Domain.BaseEntity;
using Domain.Services;
using Domain.Statements;
using Domain.Statements.Status.ValueObjects;
using Domain.Statements.ValueObjects;
using System;
using System.Threading.Tasks;

namespace App.Services
{
    public class StatementService : IStatementService
    {
        private readonly IStatementFactory statementFactory;
        private readonly IStatementRepository statementRepository;
        private readonly IUnitOfWork unitOfWork;

        public StatementService(
            IStatementFactory statementFactory,
            IStatementRepository statementRepository,
            IUnitOfWork unitOfWork)
        {
            this.statementRepository = statementRepository;
            this.statementFactory = statementFactory;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IStatement> ChangeStatementStatus(
            PrimaryKey statementId, 
            StatusCode statusCode, 
            StatusComment statusComment)
        {
            var statement = await statementRepository.GetStatement(statementId);
            var currentStatus = statement.CurrentStatus.StatusCode;

            switch(statusCode)
            {
                case StatusCode.Opened: throw new Exception("The status cannot be 'Opened'");
                case StatusCode.Decided:
                    {
                        if (currentStatus != StatusCode.Opened && currentStatus != StatusCode.Returned) 
                            throw new Exception("The status cannot be 'Decided'");
                        break;
                    }
                case StatusCode.Returned:
                    {
                        if (currentStatus != StatusCode.Decided) 
                            throw new Exception("The status cannot be 'Returned'");
                        break;
                    }
                case StatusCode.Closed:
                    {
                        if (currentStatus != StatusCode.Decided) 
                            throw new Exception("The status cannot be 'Closed'");
                        break;
                    }
            }

            var statementStatus = statement.ChangeStatus(
                this.statementFactory,
                statusCode,
                statusComment);

            await this.statementRepository.Update(statement, statementStatus);
            await unitOfWork.Save();

            return statement;
        }

        public async Task<IStatement> CreateStatement(StatementTitle statementTitle)
        {
            var statement = statementFactory.NewStatement(statementTitle);
            statement.ChangeStatus(
                this.statementFactory,
                StatusCode.Opened,
                new StatusComment(" "));

            await statementRepository.AddStatement(statement);
            await statementRepository.Update(statement, statement.CurrentStatus);
            await unitOfWork.Save();

            return statement;
        }

        public async Task DeleteStatement(PrimaryKey statementId)
        {
            var statement = await statementRepository.GetStatement(statementId);
            await statementRepository.DeleteStatement(statement);
            await unitOfWork.Save();
        }

        public async Task<IStatement> ChangeStatementTitle(PrimaryKey statementId, StatementTitle newTitle)
        {
            var statement = await statementRepository.GetStatement(statementId);

            statement.ChangeTitle(newTitle);
            await unitOfWork.Save();

            return statement;
        }
    }
}
