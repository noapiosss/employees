using System;

using Microsoft.AspNetCore.Mvc.Razor;

public abstract class Helper<TModel> : RazorPage<TModel>
{
    public string PaginationHref(string query, int page)
    {
        if (string.IsNullOrEmpty(query))
        {
            return $"?page={page}";
        }

        if (!query.Contains("page"))
        {
            return $"{query}&page={page}";
        }

        while (query[^1] != '=')
        {
            query = query.Remove(query.Length - 1);
        }

        return query + page;
    }
}