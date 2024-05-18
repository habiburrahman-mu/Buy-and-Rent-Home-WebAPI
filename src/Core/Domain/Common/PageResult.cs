using System.Collections.Generic;

namespace Domain.Common;

public class PageResult<T> where T : class
{
    public PageResult()
    {
        ResultList = new List<T>();
    }

    public int PageNo { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public List<T> ResultList { get; set; }
}
