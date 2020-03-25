using System.Collections.Generic;

namespace StatementSystem.UI.Models
{
    public class StatementIndexView
    {
        public List<StatementModel> Statements { get; set; }

        public FilterModel Filter { get; set; }
    }
}
